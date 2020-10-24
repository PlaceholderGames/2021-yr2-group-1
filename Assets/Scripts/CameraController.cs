using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateY;
    public float mouseSensitivity;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        //Removes cursor from the screen and locks its position
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Vertical camera movement
        float vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        //Horizontal camera movement
        float horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;

        rotateY -= vertical;
        //Constrains the camera along the Y axis
        rotateY = Mathf.Clamp(rotateY, -60f, 50f);
        //Applies the rotation to camera
        transform.localEulerAngles = new Vector3(rotateY, 0f, 0f);

        //Rotates the player along X axis
        player.Rotate(Vector3.up * horizontal);
    }
}
