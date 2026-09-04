using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 2f;  // Movement speed
    public float rotationSpeed = 50f;  // Rotation speed

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input axes
        float moveForwardBack = Input.GetAxis("Vertical");   // W/S or Up/Down
        float moveLeftRight = Input.GetAxis("Horizontal");   // A/D or Left/Right
        float moveUpDown = 0f;

        if (Input.GetKey(KeyCode.E)) moveUpDown += 1f;  // E = Up
        if (Input.GetKey(KeyCode.Q)) moveUpDown -= 1f;  // Q = Down

        // Calculate move direction in local space
        Vector3 move = transform.forward * moveForwardBack +
                       transform.right * moveLeftRight +
                       transform.up * moveUpDown;

        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

        // Rotation
        float rotateHorizontal = 0f;
        float rotateVertical = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) rotateHorizontal = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) rotateHorizontal = 1f;

        if (Input.GetKey(KeyCode.UpArrow)) rotateVertical = -1f;
        if (Input.GetKey(KeyCode.DownArrow)) rotateVertical = 1f;

        transform.Rotate(Vector3.up, rotateHorizontal * rotationSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.right, rotateVertical * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
