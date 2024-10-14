using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public Transform playerObj;   //reference to the player's transform
    public float baseSpeed = 0.01f; //Base speed for camera movement
    public Vector3 baseOffset;    //Base camera offset
    public float maxOffsetMultiplier = 2.0f; //How much to scale the offset based on velocity
    private Rigidbody playerRigidbody; //Reference to the player's Rigidbody (for velocity)
    public float offsetRate = 100f;
    private Space offsetPositionSpace = Space.Self;  //Whether the offset is local or world space
    private bool lookAt = false;    //Whether the camera should look at the player

    //Mouse Camera
    public bool enableMouseControl = true; //third-person mouse control option
    public float rotationSpeed = 5f; //speed of mouse rotation
    // Rotation angles for mouse control
    private float yaw = 0f;
    private float pitch = 0f;


    //Projectiles
    public GameObject projectilePrefab; //projectile prefab
    public Transform firePos;
    public float LaunchForce = 15f;

    

    void Start()
    {
        // Find the player object by tag
        if (GameObject.FindWithTag("Player") != null)
        {
            playerObj = GameObject.FindWithTag("Player").transform;
            playerRigidbody = playerObj.GetComponent<Rigidbody>();  // Get the player's Rigidbody component
        }

        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    private void Update()
    {
        // Check if the right mouse button is held down
        if (Input.GetMouseButton(1) && enableMouseControl)
        {
            // Rotate the camera with the mouse
            RotateCameraWithMouse();

            if (Input.GetMouseButtonDown(0) && GameHandler.victimInBoat > 0) // Left-click is mouse button index 0
            {
                ShootProjectile();
                GameHandler gameHandler = FindObjectOfType<GameHandler>();
                gameHandler.DecreaseVictimInBoat(1);
            }
        }
        else
        {
            // Default camera follow behavior
            MoveAndRotateCamera();
        }
    }

    public void MoveAndRotateCamera()
    {
        // Calculate the speed of the player based on velocity magnitude
        float playerSpeed = playerRigidbody.velocity.magnitude;

        // Scale the camera offset based on the player's speed
        float offsetScale = Mathf.Lerp(1f, maxOffsetMultiplier, playerSpeed / offsetRate); // 10f can be adjusted based on how fast the player moves
        Vector3 scaledOffset = baseOffset * offsetScale;

        // Calculate the camera's new position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = playerObj.TransformPoint(scaledOffset); // Move camera in local space
        }
        else
        {
            transform.position = playerObj.position + scaledOffset; // Move camera in world space
        }

        // If the camera should look at the player
        if (lookAt)
        {
            transform.LookAt(playerObj);
        }
        else
        {
            Transform fromRot = gameObject.transform;
            Transform toRot = playerObj;
            transform.rotation = Quaternion.Lerp(fromRot.rotation, toRot.rotation, Time.time * baseSpeed);
        }

        // Update yaw and pitch based on the current position, so it starts in the same spot
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    private void RotateCameraWithMouse()
    {
        // Get mouse movement inputs
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Adjust the yaw (horizontal rotation) and pitch (vertical rotation)
        yaw += mouseX * rotationSpeed;
        pitch -= mouseY * rotationSpeed;

        // Clamp the pitch to avoid flipping the camera
        pitch = Mathf.Clamp(pitch, -40f, 80f); // Adjust to taste

        // Apply the rotation based on yaw and pitch
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Maintain the camera offset while rotating around the player
        transform.position = playerObj.position + rotation * baseOffset;

        transform.rotation = rotation;

    }

    public void ShootProjectile()
    {
        if (firePos != null && projectilePrefab != null)
        {
            Vector3 cameraForward = transform.forward;
            cameraForward.Normalize();
            GameObject projectile = Instantiate(projectilePrefab, firePos.position, firePos.rotation);
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.AddForce(cameraForward * LaunchForce, ForceMode.Impulse);
        }
    }
}
