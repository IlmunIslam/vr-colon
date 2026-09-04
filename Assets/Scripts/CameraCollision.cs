using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
    public float collisionOffset = 0.1f; // distance to keep from wall
    public LayerMask collisionMask; // set to Colon layer

    private Vector3 lastSafePosition;

    // --- UI Overlay ---
    private bool showCollisionMessage = false;
    private float messageTimer = 0f;
    private GUIStyle guiStyle = new GUIStyle();

    void Start()
    {
        lastSafePosition = transform.position;

        // setup style
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.red;
    }

    void Update()
    {
        // --- Movement input (keyboard only) ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float upDown = 0f;
        if (Input.GetKey(KeyCode.E)) upDown = 1f;
        if (Input.GetKey(KeyCode.Q)) upDown = -1f;

        Vector3 move = (transform.forward * v + transform.right * h + transform.up * upDown).normalized;

        Vector3 targetPosition = transform.position + move * moveSpeed * Time.deltaTime;

        // --- Collision check ---
        if (!Physics.CheckSphere(targetPosition, collisionOffset, collisionMask))
        {
            transform.position = targetPosition;
            lastSafePosition = transform.position;
        }
        else
        {
            transform.position = lastSafePosition;

            // Show collision message
            Debug.Log("⚠️ Collision detected with colon wall!");
            showCollisionMessage = true;
            messageTimer = 1.5f; // show for 1.5 sec
        }

        // --- Timer for UI message ---
        if (messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0) showCollisionMessage = false;
        }

        // --- Rotation (keyboard only) ---
        if (Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.up, -rotationSpeed);
        if (Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.up, rotationSpeed);
        if (Input.GetKey(KeyCode.UpArrow)) transform.Rotate(Vector3.right, -rotationSpeed);
        if (Input.GetKey(KeyCode.DownArrow)) transform.Rotate(Vector3.right, rotationSpeed);
    }

    void OnGUI()
    {
        if (showCollisionMessage)
        {
            GUI.Label(
                new Rect(Screen.width - 350, Screen.height - 40, 300, 30),
                "Collision detected",
                guiStyle
            );
        }
    }
}
