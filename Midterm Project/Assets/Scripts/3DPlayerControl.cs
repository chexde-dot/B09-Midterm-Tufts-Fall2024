using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float thrust = 10f; // Acceleration force
    public float drag = 0.5f; // Drag force to slow down the boat
    public float maxSpeed = 20f; // Maximum speed
    public float turnSpeed = 1f; // Turning speed

    private Rigidbody rb;

    // Public property to access the boat's velocity
    public Vector3 Velocity => rb.velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Get input for direction
        float moveInput = Input.GetAxis("Vertical");   // Forward/backward input (W/S or Up/Down arrows)
        float turnInput = Input.GetAxis("Horizontal"); // Left/right input (A/D or Left/Right arrows)

        // Apply thrust force in the boat's forward direction
        Vector3 forwardThrust = transform.forward * moveInput;

        // Apply thrust if there is forward or backward input
        if (moveInput != 0)
        {
            rb.AddForce(forwardThrust * thrust, ForceMode.Acceleration);
        }

        // Handle turning
        if (turnInput != 0)
        {
            // Rotate the boat based on the turn input
            transform.Rotate(0, turnInput * turnSpeed, 0);
        }

        // Apply drag to gradually slow down the boat when no input is given
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
