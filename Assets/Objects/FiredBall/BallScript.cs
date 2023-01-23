using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Transform orientation;

    // Rotation Speed
    public float _rotationSpeed = 1;

    // Speed
    public float _velocity = 1;


    Vector2 moveDirection;

    public Rigidbody2D rb; //create the rigidbody variable

    public float ballSpeed = 10;
    public float timeMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //find the rigidbody component in the object
        moveDirection = orientation.forward * 1 + orientation.right * 1;
        rb.AddForce(moveDirection.normalized * ballSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        moveDirection = orientation.forward * 1 + orientation.right * 1;
        rb.AddForce(moveDirection.normalized * ballSpeed * 10, ForceMode2D.Force);
    }
}
