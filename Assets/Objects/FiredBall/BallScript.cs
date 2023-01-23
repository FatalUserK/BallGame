using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    // Rotation Speed
    public float _rotationSpeed = 1;

    // Speed
    public float _velocity = 1;


    public Rigidbody2D rb; //create the rigidbody variable

    public float ballSpeed = 1;
    public float timeMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //find the rigidbody component in the object
        rb.AddForce(new Vector2(0, ballSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.velocity = transform.forward.normalized * ballSpeed * timeMultiplier * Time.deltaTime;
        rb.AddForce(new Vector2(ballSpeed, 0));
    }
}
