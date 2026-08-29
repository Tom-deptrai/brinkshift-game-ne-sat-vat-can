using UnityEngine;

namespace Brinkshift.Gameplay
{
    /// <summary>
    /// Gate 1 (Graze) - smallest useful slice.
    ///
    /// A larger trigger zone around the obstacle's lethal body. Brushing this
    /// zone while alive and while NOT touching the hit zone counts as one graze.
    /// The hit zone itself stays lethal and is handled by <see cref="ObstacleDrifter"/>.
    ///
    /// Anti-farming: at most one graze per obstacle pass; eligibility resets when
    /// the obstacle wraps back to the top (<see cref="ResetPass"/>).
    ///
    /// Prototype feedback only: a brief colour flash on the obstacle body, an
    /// editor gizmo, and one console line. No score total, Chain Meter,
    /// multiplier, production UI, VFX, SFX, or haptics.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class ObstacleGrazeZone : MonoBehaviour
    {
        private Collider2D _hitZone;
        private SpriteRenderer _obstacleBody;
        private Color _bodyColor = Color.white;
        private Color _flashColor = new Color(0.5f, 1f, 1f, 1f);
        private float _flashDuration = 0.12f;

        private bool _grazedThisPass;
        private float _flashUntil = -1f;

        /// <summary>Grazes registered this run. A marker only - there is no scoring yet.</summary>
        public int GrazeCount { get; private set; }

        /// <summary>True while the current pass has already been grazed.</summary>
        public bool GrazedThisPass => _grazedThisPass;

        public void Configure(Collider2D hitZone, SpriteRenderer obstacleBody, Color flashColor, float flashDuration)
        {
            _hitZone = hitZone;
            _obstacleBody = obstacleBody;
            if (_obstacleBody != null)
            {
                _bodyColor = _obstacleBody.color;
            }

            _flashColor = flashColor;
            _flashDuration = Mathf.Max(0.02f, flashDuration);
        }

        /// <summary>Called by <see cref="ObstacleDrifter"/> when the obstacle wraps to the top.</summary>
        public void ResetPass()
        {
            _grazedThisPass = false;
        }

        private void Update()
        {
            // Revert the flash on unscaled time so it still ends if the game froze.
            if (_flashUntil > 0f && Time.unscaledTime >= _flashUntil)
            {
                if (_obstacleBody != null)
                {
                    _obstacleBody.color = _bodyColor;
                }

                _flashUntil = -1f;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_grazedThisPass || !other.CompareTag("Player"))
            {
                return;
            }

            Gate0PrototypeBootstrap runner = Gate0PrototypeBootstrap.Instance;
            if (runner != null && runner.IsDead)
            {
                return;
            }

            // Only a graze if the player is NOT also inside the lethal hit zone;
            // that overlap is a death, handled by ObstacleDrifter.
            if (_hitZone != null && _hitZone.Distance(other).isOverlapped)
            {
                return;
            }

            _grazedThisPass = true;
            GrazeCount++;
            Flash();
            Debug.Log($"[Gate1] Graze registered (run total {GrazeCount}); one per obstacle pass.");
        }

        private void Flash()
        {
            if (_obstacleBody == null)
            {
                return;
            }

            _obstacleBody.color = _flashColor;
            _flashUntil = Time.unscaledTime + _flashDuration;
        }

        private void OnDrawGizmos()
        {
            Collider2D zone = GetComponent<Collider2D>();
            if (zone == null)
            {
                return;
            }

            Gizmos.color = new Color(0.5f, 1f, 1f, 0.5f);
            Gizmos.DrawWireCube(zone.bounds.center, zone.bounds.size);
        }
    }
}
