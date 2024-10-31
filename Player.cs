using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform_;
    [SerializeField] private LayerMask playerMask;
    private bool JumpKeyWasPressed_;
    private float horizontalInput_;
    private float verticalInput_;
    private Rigidbody rigidbodyComponent_;
    private int superJumpsRemaining;
    


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent_ = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //does something when space is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            JumpKeyWasPressed_ = true;
        }

        horizontalInput_ = Input.GetAxis("Horizontal");
        verticalInput_ = Input.GetAxis("Vertical");

    }
    //FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        rigidbodyComponent_.velocity = new Vector3(horizontalInput_, rigidbodyComponent_.velocity.y, 0);
        //rigidbodyComponent_.velocity = new Vector3(rigidbodyComponent_.velocity.y, 0 ,horizontalInput_ );

        if (Physics.OverlapSphere(groundCheckTransform_.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (JumpKeyWasPressed_) {
            float jumpPower = 5f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            JumpKeyWasPressed_= false;
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 0) 
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }


}
