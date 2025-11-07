using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicPlayerController : MonoBehaviour
{
    public float walkSpeed = 3.5f;
    public float sprintSpeed = 6.5f;
    public float rotationSpeed = 10f;

    public Transform cameraTransform;  // MainCamera transform
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        // Determine movement direction relative to camera
        if (inputDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private float turnVelocity;
}
