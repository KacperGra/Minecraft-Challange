using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float movementSpeed = 250f;
    public float jumpForce = 10f;
    private Vector3 movementInput;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movementInput = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")) * movementSpeed;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(movementInput.x * Time.fixedDeltaTime, rigidbody.velocity.y, movementInput.z * Time.fixedDeltaTime);
    }
}
