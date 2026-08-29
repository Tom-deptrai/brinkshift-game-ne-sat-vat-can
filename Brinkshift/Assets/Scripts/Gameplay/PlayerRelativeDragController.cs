using UnityEngine;
using UnityEngine.InputSystem;

namespace Brinkshift.Gameplay
{
    /// <summary>
    /// Gate 0 prototype: one-finger relative-drag control.
    ///
    /// While a pointer (touch on device, mouse in the Editor) is held down, the
    /// player moves by the <em>delta</em> of the pointer's travel - never toward
    /// the pointer's absolute position. Pressing down only records a reference
    /// point, so the player never teleports, and the player can lift a finger and
    /// press again anywhere on screen to keep dragging from where it left off.
    ///
    /// No virtual joystick, no physics, no gameplay - just the control feel.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerRelativeDragController : MonoBehaviour
    {
        [Header("Tuning")]
        [Tooltip("How far the player travels per unit of finger travel, in world units. " +
                 "1 = exact 1:1 mapping; higher needs less finger movement. Tune on device.")]
        [SerializeField, Min(0.05f)] private float sensitivity = 1.5f;

        [Tooltip("Extra gap kept between the player sprite and the screen edge, in world units.")]
        [SerializeField, Min(0f)] private float edgeMargin = 0.1f;

        [Header("References")]
        [Tooltip("Camera used to convert screen movement into world movement. " +
                 "Falls back to Camera.main when left empty.")]
        [SerializeField] private Camera targetCamera;

        private SpriteRenderer _spriteRenderer;
        private bool _isDragging;
        private Vector2 _previousPointerPosition;

        /// <summary>Current sensitivity. Exposed so tuning UI/tests can adjust it at runtime.</summary>
        public float Sensitivity
        {
            get => sensitivity;
            set => sensitivity = Mathf.Max(0.05f, value);
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }
        }

        private void Update()
        {
            if (targetCamera == null)
            {
                return;
            }

            Pointer pointer = Pointer.current;
            if (pointer == null)
            {
                return;
            }

            Vector2 pointerPosition = pointer.position.ReadValue();

            if (pointer.press.wasPressedThisFrame)
            {
                // Down: remember where the finger landed. The player does not move.
                _previousPointerPosition = pointerPosition;
                _isDragging = true;
            }
            else if (_isDragging && pointer.press.isPressed)
            {
                // Held: translate the finger's travel this frame into world travel.
                // This is positional (delta-based), so it is already frame-rate
                // independent - it must NOT be multiplied by Time.deltaTime.
                Vector2 worldDelta = ScreenToWorldDelta(_previousPointerPosition, pointerPosition);
                _previousPointerPosition = pointerPosition;

                Vector3 target = transform.position + (Vector3)(worldDelta * sensitivity);
                transform.position = ClampToCameraView(target);
            }
            else if (pointer.press.wasReleasedThisFrame || !pointer.press.isPressed)
            {
                // Up (or focus/press lost): end the drag. The next press starts fresh.
                _isDragging = false;
            }
        }

        /// <summary>
        /// World-space movement between two screen points. Correct for an
        /// orthographic camera regardless of the z distance passed in.
        /// </summary>
        private Vector2 ScreenToWorldDelta(Vector2 fromScreen, Vector2 toScreen)
        {
            Vector3 fromWorld = targetCamera.ScreenToWorldPoint(new Vector3(fromScreen.x, fromScreen.y, 1f));
            Vector3 toWorld = targetCamera.ScreenToWorldPoint(new Vector3(toScreen.x, toScreen.y, 1f));
            return new Vector2(toWorld.x - fromWorld.x, toWorld.y - fromWorld.y);
        }

        /// <summary>
        /// Keeps the whole player sprite inside the orthographic camera view,
        /// with <see cref="edgeMargin"/> to spare, for any portrait aspect ratio.
        /// </summary>
        private Vector3 ClampToCameraView(Vector3 worldPosition)
        {
            float halfHeight = targetCamera.orthographicSize;
            float halfWidth = halfHeight * targetCamera.aspect;

            Vector3 spriteExtents = _spriteRenderer.bounds.extents;
            float limitX = Mathf.Max(0f, halfWidth - spriteExtents.x - edgeMargin);
            float limitY = Mathf.Max(0f, halfHeight - spriteExtents.y - edgeMargin);

            Vector3 cameraPosition = targetCamera.transform.position;
            worldPosition.x = Mathf.Clamp(worldPosition.x, cameraPosition.x - limitX, cameraPosition.x + limitX);
            worldPosition.y = Mathf.Clamp(worldPosition.y, cameraPosition.y - limitY, cameraPosition.y + limitY);
            return worldPosition;
        }
    }
}
