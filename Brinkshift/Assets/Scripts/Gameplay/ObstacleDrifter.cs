using UnityEngine;

namespace Brinkshift.Gameplay
{
    /// <summary>
    /// Gate 0 test hazard. One rectangle drifts straight down at a constant
    /// speed and wraps back to the top, alternating between a left and a right
    /// lane so the player has to actually move to avoid it. Fully deterministic
    /// - no randomness. Touching it kills the player.
    ///
    /// Velocity-based movement, so Time.deltaTime here is correct (this is the
    /// obstacle, not the relative-drag input). Freezes with Time.timeScale = 0.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class ObstacleDrifter : MonoBehaviour
    {
        private float _speed = 5f;
        private float _laneOffsetX = 1.4f;
        private float _wrapMargin = 1.5f;
        private Camera _camera;

        private float _halfHeightWorld = 0.5f;
        private int _laneSign = 1;
        private ObstacleGrazeZone _grazeZone;

        /// <summary>Called by the bootstrap right after it adds this component.</summary>
        public void Configure(float speed, float laneOffsetX, float wrapMargin, Camera view)
        {
            _speed = Mathf.Max(0.1f, speed);
            _laneOffsetX = Mathf.Max(0f, laneOffsetX);
            _wrapMargin = Mathf.Max(0f, wrapMargin);
            _camera = view != null ? view : Camera.main;

            var sprite = GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                _halfHeightWorld = sprite.bounds.extents.y;
            }

            ResetToTop();
        }

        /// <summary>Links the graze zone so its one-per-pass state resets on wrap.</summary>
        public void SetGrazeZone(ObstacleGrazeZone grazeZone)
        {
            _grazeZone = grazeZone;
        }

        private void Update()
        {
            if (_camera == null)
            {
                return;
            }

            transform.position += Vector3.down * (_speed * Time.deltaTime);

            float bottomEdge = _camera.transform.position.y - _camera.orthographicSize;
            if (transform.position.y + _halfHeightWorld < bottomEdge - _wrapMargin)
            {
                _laneSign = -_laneSign;
                ResetToTop();
            }
        }

        private void ResetToTop()
        {
            if (_camera == null)
            {
                return;
            }

            float topEdge = _camera.transform.position.y + _camera.orthographicSize;
            float x = _camera.transform.position.x + _laneSign * _laneOffsetX;
            transform.position = new Vector3(x, topEdge + _halfHeightWorld + _wrapMargin, 0f);

            // New pass down the screen -> the player may graze this obstacle again.
            if (_grazeZone != null)
            {
                _grazeZone.ResetPass();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (Gate0PrototypeBootstrap.Instance != null)
            {
                Gate0PrototypeBootstrap.Instance.OnPlayerHit(other.gameObject);
            }
        }
    }
}
