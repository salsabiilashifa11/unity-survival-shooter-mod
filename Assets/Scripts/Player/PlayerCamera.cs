using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject fpsCamera;
    bool isFPS;
    
    PlayerFPSMovement playerFPSMovement;
    PlayerMovement playerMovement;
    Camera mainCameraCam;
    
    
    void Awake() {
        playerFPSMovement = GetComponent<PlayerFPSMovement>();
        playerMovement = GetComponent<PlayerMovement>();
        
        mainCameraCam = mainCamera.GetComponent<Camera>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) {
            Debug.Log("CHANGE CAMERA MODE");
            
            isFPS = GetComponent<PlayerFPSMovement>().enabled;
            
            if (isFPS) {
                //Disable FPS components
                playerFPSMovement.enabled = false;
                fpsCamera.SetActive(false);
                
                //Enable non FPS components
                playerMovement.enabled = true;
                mainCameraCam.enabled = true;
            } else {
                //Enable FPS components
                playerFPSMovement.enabled = true;
                fpsCamera.SetActive(true);
                
                //Disable non FPS components
                playerMovement.enabled = false;
                mainCameraCam.enabled = false;
            }
            
        }
    }
    
}


