using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;        // normal walk speed
    public float sprintSpeed = 10f;     // sprint speed
    public float rotationSpeed = 720f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Hide cursor
    }

    void Update()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S

        // Determine speed
        float currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) // Hold shift to sprint
        {
            currentSpeed = sprintSpeed;
        }

        // Move relative to player forward
        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Rotate player with mouse
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);
    }
}
