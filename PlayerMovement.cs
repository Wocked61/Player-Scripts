using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 5f;
    public float speed = 5f;
    public bool isOnGround = true;
    private float horizontalIn;
    private float verticalIn;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb= GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {  
        //gets the player input
        horizontalIn = Input.GetAxis("Horizontal");
        verticalIn = Input.GetAxis("Vertical");

        //move the player 
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalIn);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalIn);

        //makes the player jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

}
