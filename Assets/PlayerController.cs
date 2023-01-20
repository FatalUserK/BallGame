using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool test = false;
    public GameObject aimPrefab;

    bool isAiming = false;

    public float angle;
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
        if (Input.GetMouseButtonDown(0) && !isAiming)
        {
            Console.WriteLine("Player: Mouse is down");
            Aim();
        }

    }

    void Aim()
    {
        aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Console.WriteLine("Player: Mouse position is locked correctly");
        StartCoroutine(aimCoroutine());


    }
    private IEnumerator aimCoroutine()
    {
        isAiming = true;

        while (isAiming)
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Vector2.Angle(mousePos, aimPoint);
            Console.WriteLine(angle);
            Instantiate(aimPrefab, aimPoint, Quaternion.identity);
            Instantiate(aimPrefab, mousePos, Quaternion.identity);
            //Console.WriteLine("Player: Object has spawned");
            if (Input.GetMouseButtonUp(0))
            {
                isAiming = false;
            }
            yield return null;
        }



        //    while (isAiming)
        //{

        
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        isAiming=false;
        //        yield return null;
        //        break;
        //    }
        //}


    }
}
