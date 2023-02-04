using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFPSMovement : MonoBehaviour
{
    public float speed = 6f;
    public float maxSpeed = 20f;
    public Text speedText;
    
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
//    float camRayLength = 100f;
    float x;
    float z;
    Vector3 move;

    private void Awake()
    {
        //mendapatkan nilai mask dari layer yang bernama Floor
        floorMask = LayerMask.GetMask("Floor");

        //Mendapatkan komponen Animator
        anim = GetComponent<Animator>();

        //Mendapatkan komponen Rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
        
        speedText.text = + speed + "/" + maxSpeed;
    }

    private void FixedUpdate()
    {
        //Mendapatkan nilai input horizontal (-1,0,1)
        float h = Input.GetAxisRaw("Horizontal");

        //Mendapatkan nilai input vertical (-1,0,1)
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Animating(h, v);
    }

    //Method player dapat berjalan
    public void Move(float h, float v)
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        
        movement = transform.right * x + transform.forward * z;
        
        playerRigidbody.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    public void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
    
    public void GainSpeed(float amount) 
    {
        speed += amount;
        if (speed > maxSpeed) {
            speed = maxSpeed;
        }
        
        speedText.text = + speed + "/" + maxSpeed;
        
//        Debug.Log(speed);
    }
}
