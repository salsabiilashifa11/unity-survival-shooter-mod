using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotationOnX = -20;
    public float mouseSensitivity = 100;
    public Transform player;
    
    void Start() {
    
    }
    
    void Update() {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        
        rotationOnX -= mouseY;
        rotationOnX = Mathf.Clamp(rotationOnX, -60f, 20f);
        transform.localEulerAngles = new Vector3(rotationOnX, 0f, 0f);
        
        player.Rotate(Vector3.up * mouseX);
    }
}
