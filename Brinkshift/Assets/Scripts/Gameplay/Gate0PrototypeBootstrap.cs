using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Brinkshift.Gameplay
{
    /// <summary>
    /// Gate 0 test harness. Put this on one GameObject in Gameplay.unity.
    ///
    /// It builds the minimal "avoid something" loop at runtime so the scene stays
    /// clean: gives the player a collider, spawns one <see cref="ObstacleDrifter"/>
    /// hazard, and runs the alive -> dead -> instant-restart state.
    ///
    /// On death the game freezes (timeScale 0); the next tap / click / key press
    /// reloads the scene. No score, no graze, no Chain Meter, no UI - just the
    /// retry loop for testing relative-drag control against a hazard.
    /// </summary>
    public class Gate0PrototypeBootstrap : MonoBehaviour
    {
        public static Gate0PrototypeBootstrap Instance { get; private set; }

        [Header("Obstacle tuning")]
        [SerializeField] private Color obstacleColor = new Color(0.92f, 0.26f, 0.21f, 1f);
        [SerializeField] private Vector2 obstacleSize = new Vector2(1.3f, 1.3f);
        [SerializeField, Min(0.1f)] private float obstacleSpeed = 5f;
        [SerializeField, Min(0f)] private float obstacleLaneOffsetX = 1.4f;
        [SerializeField, Min(0f)] private float obstacleWrapMargin = 1.5f;

        [Header("References")]
        [SerializeField] private Camera targetCamera;

        private bool _dead;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Time.timeScale = 1f;

            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }

            PlayerRelativeDragController player = FindAnyObjectByType<PlayerRelativeDragController>();
            EnsurePlayerCollider(player);
            SpawnObstacle();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        /// <summary>Called by the hazard when it overlaps the player.</summary>
        public void OnPlayerHit(GameObject player)
        {
            if (_dead)
            {
                return;
            }

            _dead = true;
            Time.timeScale = 0f;

            if (player != null && player.TryGetComponent(out PlayerRelativeDragController controller))
            {
                controller.enabled = false;
            }
        }

        private void Update()
        {
            if (!_dead)
            {
                return;
            }

            if (RestartPressed())
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private static bool RestartPressed()
        {
            if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
            {
                return true;
            }

            return Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;
        }

        private void EnsurePlayerCollider(PlayerRelativeDragController player)
        {
            if (player == null)
            {
                return;
            }

            GameObject go = player.gameObject;
            go.tag = "Player";

            if (go.GetComponent<Collider2D>() == null)
            {
                CircleCollider2D circle = go.AddComponent<CircleCollider2D>();
                SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
                circle.radius = sprite != null ? sprite.bounds.extents.x : 0.5f;
            }

            // A kinematic body (moved by the controller's transform writes) is what
            // lets the trigger fire against the hazard's collider.
            if (go.GetComponent<Rigidbody2D>() == null)
            {
                Rigidbody2D body = go.AddComponent<Rigidbody2D>();
                body.bodyType = RigidbodyType2D.Kinematic;
                body.useFullKinematicContacts = true;
            }
        }

        private void SpawnObstacle()
        {
            var go = new GameObject("Obstacle");
            go.transform.localScale = new Vector3(obstacleSize.x, obstacleSize.y, 1f);

            var sprite = go.AddComponent<SpriteRenderer>();
            sprite.sprite = Sprite.Create(
                Texture2D.whiteTexture,
                new Rect(0f, 0f, Texture2D.whiteTexture.width, Texture2D.whiteTexture.height),
                new Vector2(0.5f, 0.5f),
                Texture2D.whiteTexture.width);
            sprite.color = obstacleColor;
            sprite.sortingOrder = 1;

            var playerSprite = FindAnyObjectByType<PlayerRelativeDragController>()?.GetComponent<SpriteRenderer>();
            if (playerSprite != null)
            {
                sprite.sharedMaterial = playerSprite.sharedMaterial;
            }

            var box = go.AddComponent<BoxCollider2D>();
            box.isTrigger = true;

            var drifter = go.AddComponent<ObstacleDrifter>();
            drifter.Configure(obstacleSpeed, obstacleLaneOffsetX, obstacleWrapMargin, targetCamera);
        }
    }
}
