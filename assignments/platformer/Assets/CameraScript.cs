using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Player;       // The player to follow
    public Vector3 offset = new Vector3(0, 3, -20); // Position offset from the player
    public float followSpeed = 5f; // Speed of camera following

    void LateUpdate()
    {
        if (Player != null)
        {
            // position of the camera based on player position and offset
            Vector3 desiredPosition = Player.position + offset;
            
            // move the camera to the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.LookAt(Player);
        }
    }
}