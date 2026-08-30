using UnityEngine;

namespace Brinkshift.Gameplay
{
    /// <summary>
    /// Gate 1 (Graze) - smallest useful slice.
    ///
    /// A larger zone around the obstacle's lethal body. While alive, overlapping
    /// this zone WITHOUT overlapping the hit zone counts as one graze. The hit
    /// zone stays lethal and is handled by <see cref="ObstacleDrifter"/>.
    ///
    /// Detection is a per-frame overlap poll (Collider2D.Distance), not a trigger
    /// callback - the obstacle moves by transform, and trigger enter/exit against
    /// a moving collider is not dependable enough for "exactly one per pass".
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
        private Collider2D _grazeZone;
        private Collider2D _hitZone;
        private Collider2D _playerCollider;
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
            _grazeZone = GetComponent<Collider2D>();
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
            RevertFlashWhenDue();

            if (_grazedThisPass || _grazeZone == null)
            {
                return;
            }

            if (_playerCollider == null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                _playerCollider = player != null ? player.GetComponent<Collider2D>() : null;
                if (_playerCollider == null)
                {
                    return;
                }
            }

            Gate0PrototypeBootstrap runner = Gate0PrototypeBootstrap.Instance;
            if (runner != null && runner.IsDead)
            {
                return;
            }

            bool inGraze = _grazeZone.Distance(_playerCollider).isOverlapped;
            bool inHit = _hitZone != null && _hitZone.Distance(_playerCollider).isOverlapped;

            // A graze only counts if the player brushed the zone but NOT the body.
            if (inGraze && !inHit)
            {
                _grazedThisPass = true;
                GrazeCount++;
                Flash();
                Debug.Log($"[Gate1] Graze registered (run total {GrazeCount}); one per obstacle pass.");
            }
        }

        private void RevertFlashWhenDue()
        {
            if (_flashUntil > 0f && Time.unscaledTime >= _flashUntil)
            {
                if (_obstacleBody != null)
                {
                    _obstacleBody.color = _bodyColor;
                }

                _flashUntil = -1f;
            }
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
