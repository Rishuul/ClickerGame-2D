// using UnityEngine;
// using UnityEngine.InputSystem;

// public class CameraPan : MonoBehaviour
// {
//     private Vector2 touchStartWorldPosition;
//     private bool isPanning = false;
//     private GameInputs inputActions;

//     public float smoothSpeed = 10f; // Smooth movement adjustment
//     private Vector3 targetPosition;

//     void Awake()
//     {
//         inputActions = new GameInputs();
//         inputActions.CameraPan.Enable();

//         inputActions.CameraPan.ScreenTouched.started += OnTouchStarted;
//         inputActions.CameraPan.ScreenTouched.performed += OnTouchMoved;
//         inputActions.CameraPan.ScreenTouched.canceled += OnTouchEnded;
//     }

//     void OnDestroy()
//     {
//         inputActions.CameraPan.Disable();
//     }

//     private void Start()
//     {
//         targetPosition = transform.position; // Initialize target position
//     }

//     private void Update()
//     {
//         // Smoothly move the camera towards the target position
//         transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
//     }

//     private void OnTouchStarted(InputAction.CallbackContext context)
//     {
//         Vector2 screenPosition = context.ReadValue<Vector2>();
//         touchStartWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));
//         isPanning = true;
//         Debug.Log("Touch Started at: " + touchStartWorldPosition);
//     }

//     private void OnTouchMoved(InputAction.CallbackContext context)
//     {
//         if (!isPanning) return;

//         Vector2 screenPosition = context.ReadValue<Vector2>();
//         Vector3 currentTouchWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));

//         // Calculate the difference between the current touch and the initial touch
//         Vector3 delta = currentTouchWorldPosition - (Vector3)touchStartWorldPosition;
//         delta.z = 0; // Ensure movement stays in the XY plane

//         targetPosition -= delta; // Move the camera based on the drag delta

//         touchStartWorldPosition = currentTouchWorldPosition; // Update the touch start position for continuous dragging
//         Debug.Log("Touch Moved to: " + currentTouchWorldPosition + ", Delta: " + delta);
//     }

//     private void OnTouchEnded(InputAction.CallbackContext context)
//     {
//         isPanning = false;
//         Debug.Log("Touch Ended");
//     }
// }
