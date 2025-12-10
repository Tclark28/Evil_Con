using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicPlayerController : MonoBehaviour
{
    public float walkSpeed = 3.5f;
    public float sprintSpeed = 6.5f;
    public float rotationSpeed = 10f;

    public Transform cameraTransform;  // MainCamera transform
    private CharacterController controller;

    //Reference to the Animator Controller script
    private CharacterAnimatorController animatorController;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        //Get the animator script reference
        animatorController = GetComponent<CharacterAnimatorController>();

        Cursor.lockState = CursorLockMode.Locked;

        if (animatorController == null)
        {
            Debug.LogError("CharacterAnimatorController script is missing from GameObject.");
        }
    }

    void Update()
    {
        // Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical);
        float inputMagnitude = inputDir.magnitude;

        float currentSpeed = 0f; // Speed to be passed to the animator

        // Determine movement direction relative to camera
        if (inputMagnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // Rotation smoothing:
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Calculate final movement speed
            float targetMoveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            // Move the character
            controller.Move(moveDir.normalized * targetMoveSpeed * Time.deltaTime);

            // Set the speed to drive the animation. 
            // We use the full magnitude here, as the dampening will smooth the start/stop
            currentSpeed = targetMoveSpeed;
        }

        // NEW: Pass the calculated speed (even if 0) to the animator controller every frame
        if (animatorController != null)
        {
            animatorController.SetMovementSpeed(currentSpeed * inputMagnitude);
        }
    }

    private float turnVelocity;
}
