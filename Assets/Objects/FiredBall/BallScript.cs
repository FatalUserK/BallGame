using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallScript : MonoBehaviour
{
     Vector2 moveDirection;


    GlobalEventsManager GEM;

    public GameObject playerPrefab;

    bool boringBoolValue = false;

    public Rigidbody2D rb; //create the rigidbody variable

    public float ballSpeed = 10;
    public float timeMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {

        //GEM = GameObject.FindGameObjectWithTag("MainCamera");












        rb = GetComponent<Rigidbody2D>(); //find the rigidbody component in the object
        moveDirection = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad) * ballSpeed, Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad) * ballSpeed)/-1;
        rb.AddForce(moveDirection);
        //PlayerScript.balls.Add(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (transform.position.y > 2.4 && !boringBoolValue)
        {
            boringBoolValue = true;
        }


        //float verticalInput = Input.GetAxis("Vertical");
        //moveDirection = orientation.forward * 1 + orientation.right * 1;
        //rb.AddForce(moveDirection.normalized * ballSpeed * 10, ForceMode2D.Force);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BallNullifier" && boringBoolValue)
        {

            if (!GlobalEventsManager.isReloading)
            {
                GlobalEventsManager.isReloading = true;
                Instantiate(playerPrefab);
                Debug.Log("OnCollisionEnter2D");
            }
        }
    }
}
