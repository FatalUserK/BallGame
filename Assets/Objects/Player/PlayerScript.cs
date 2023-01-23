using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private bool shootIsSuccessful = false;
    [SerializeField] private GameObject aimPrefab;
    public GameObject firedBall;

    bool isAiming = false;

    public double angle;
    public int distance;
    public float speed;

    new Vector2 mousePos;
    new Vector2 aimPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Shoot(double shootAngle)
    {
        shootIsSuccessful = true;
        Console.WriteLine(shootAngle);
        Instantiate(firedBall, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, (float)angle));

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
            bool angleIsAcceptable;

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 p1 = aimPoint;
            Vector2 p2 = mousePos;
            angle = Math.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Math.PI; // this line of code was provided by a friend of mine, it seems to run some basic math functions to check the location of the 2 points and from there, extrapolate the angle between them

            distance = Convert.ToInt32(Vector2.Distance(aimPoint, mousePos));

            if (angle <= -3 && angle >= -178 && distance >= 2)
            {
                angleIsAcceptable = true;
            }
            else
            {
                angleIsAcceptable = false;
            }
            //angle = Vector2.Angle(mousePos, aimPoint);
            //Console.WriteLine(angle);

            Instantiate(aimPrefab, aimPoint, transform.rotation * Quaternion.Euler(0f, 0f, (float)angle));
            Instantiate(aimPrefab, mousePos, Quaternion.identity);
            //Console.WriteLine("Player: Object has spawned");
            if (Input.GetMouseButtonUp(0))
            {
                isAiming = false;
                if (angleIsAcceptable)
                {
                    Shoot(angle);
                }
                else
                {
                    shootIsSuccessful = false;
                }
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