using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool test = false;
    public GameObject aimPrefab;

    new Vector2 mousePos;
    new Vector2 aimPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shoot(int test2)
    {
        test = true;
        Console.WriteLine(test2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Console.WriteLine("Player: Mouse is down");
            Aim();
        }

    }

    void Aim ()
    {
        float angle;
        aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Console.WriteLine("Player: Mouse position is locked correctly");
        while (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Vector2.Angle(aimPoint, mousePos);
            Console.WriteLine(angle);
            //Instantiate(aimPrefab, mousePos, Quaternion.identity);
            //Console.WriteLine("Player: Object has spawned");
        }
    }
}
