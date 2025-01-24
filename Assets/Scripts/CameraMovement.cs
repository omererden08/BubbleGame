using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float rotationSpeed = 5f; // Speed of rotation
    public float smoothSpeed = 10f; // Smooth follow speed
    public float maxYOffset = 3f; // Maximum vertical offset

    public float minX;
    public float maxX;


    private float yaw; // Horizontal rotation angle
    private float pitch; // Vertical rotation angle


    private void Start()
    {
        offset = player.position - transform.position;
    }


    void LateUpdate()
    {
        // Get mouse input for rotation
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, minX, maxX); // Limit vertical rotation angle

        // Calculate new camera rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Calculate the new position based on the player's position and offset
        Vector3 targetPosition = player.position + rotation * offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);


        // Make the camera look at the player
        transform.LookAt(player.position + Vector3.up * 1.5f); // Adjust the height as needed

    }

}
