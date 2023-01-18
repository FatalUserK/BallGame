using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool test = false;
    public GameObject aimPrefab;
    new Vector2 mousePos;
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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Console.WriteLine("Player: Mouse position is locked correctly");
        Instantiate(aimPrefab, mousePos, Quaternion.identity);
        Console.WriteLine("Player: Object has spawned");
    }
}
