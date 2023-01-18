using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Aim : MonoBehaviour
{

    bool acceptablePath;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Console.WriteLine("Aim: Object is created");
        //// This returns the GameObject named Hand.
        //player = GameObject.Find("Player");

        //// This returns the GameObject named Hand.
        //// Hand must not have a parent in the Hierarchy view.
        //player = GameObject.Find("/Player");
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (acceptablePath)
            {


                player.GetComponent<PlayerController>().Shoot(1);

                //PlayerController controller = player.GetComponent<PlayerController>();
                //controller.Shoot(1);
            }
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
