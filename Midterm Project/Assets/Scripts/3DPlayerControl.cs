using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float thrust = 10f; // Acceleration force
    public float drag = 0.5f; // Drag force to slow down the boat
    public float maxSpeed = 20f; // Maximum speed
    public float turnSpeed = 1f;
    public Camera playerCamera; // Reference to the camera

    private Rigidbody rb;


    // Public property to access the boat's velocity
    public Vector3 Velocity => rb.velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main; // Fallback to the main camera if none is assigned
        }
    }

    private void FixedUpdate()
    {
        // Get input for direction
        float moveInput = Input.GetAxis("Vertical"); // Forward/backward input (W/S or Up/Down arrows)
        float turnInput = Input.GetAxis("Horizontal"); // Left/right input (A/D or Left/Right arrows)

        // Calculate thrust force based on the camera's forward direction
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0; // Ignore the vertical component
        cameraForward.Normalize(); // Ensure it's a unit vector

        // Calculate the right direction based on the camera's forward direction
        Vector3 cameraRight = playerCamera.transform.right; // Right vector of the camera
        cameraRight.y = 0; // Ignore the vertical component
        cameraRight.Normalize(); // Ensure it's a unit vector

        // Calculate the desired movement direction based on camera direction
        Vector3 desiredDirection = (cameraForward * moveInput);// + (cameraRight * turnInput);
        desiredDirection.Normalize(); // Normalize to ensure consistent speed

        // Apply thrust if there is input
        if (moveInput != 0 || turnInput != 0)
        {
            rb.AddForce(desiredDirection * thrust, ForceMode.Acceleration);
        }

        // Handle turning
        if (turnInput != 0)
        {
            transform.Rotate(0, turnInput * turnSpeed, 0); // Adjust the multiplier for sensitivity
        }

        // Apply drag based on current velocity direction
        if (rb.velocity.magnitude > 0)
        {
            Vector3 dragForce = -rb.velocity.normalized * drag;
            rb.AddForce(dragForce, ForceMode.Acceleration);
        }

        // Clamp the boat's velocity to maxSpeed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
