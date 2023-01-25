using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerScript : MonoBehaviour
{

    public List<GameObject> balls;

    [SerializeField] private int playerState = 0;
    [SerializeField] private GameObject aimPrefab;
    public GameObject firedBall;
    public GameObject ballGFX;

    bool isAiming = false;

    GlobalEventsManager GlobalEventsManager;

    public double angle;
    public int distance;
    public float speed;

    int shotsRemaining;
    public int shots;

    Vector2 mousePos;
    Vector2 aimPoint;
    // Start is called before the first frame update
    void Start()
    {
        balls = new List<GameObject>();
        if (GlobalEventsManager.isReloading) { playerState = 2; }
    }


    private void FixedUpdate()
    {
        if (playerState == 2 && GameObject.FindGameObjectsWithTag("FiredBall") != null)
        {
            //EndTurn()
        }
    }


    public void ExecuteAfterTime()
    {
        GameObject newOne = Instantiate(firedBall, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, (float)angle));
        newOne.name = "Ball " + balls.ToArray().Length;
        foreach (GameObject ball in balls) { Physics2D.IgnoreCollision(newOne.GetComponent<CircleCollider2D>(), ball.GetComponent<CircleCollider2D>()); }
        balls.Add(newOne);
        if (shotsRemaining == 0) { Destroy(gameObject); }

    }


    public void Shoot(double shootAngle, float waitTime)
    {
        shotsRemaining = shots;
        playerState = 1;
        Debug.Log(shootAngle);
        while (shotsRemaining > 0)
        {
            Invoke("ExecuteAfterTime", (shots - shotsRemaining) * waitTime);
            shotsRemaining--;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAiming && playerState == 0)
        {
            Debug.Log("Player: Mouse is down");
            Aim();
        }

    }


    void Aim()
    {
        aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Player: Mouse position is locked correctly");
        StartCoroutine(aimCoroutine());


    }

    private IEnumerable aimSight()
    {
        for (int x = 0; x< 5; ++x)
        {
            //Destroy(GameObject.Find("BallGFX(Clone)"));
            Instantiate(ballGFX, transform.position * x, Quaternion.Euler(new Vector3()));

        }
        yield return null;
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


            //Instantiate(ballGFX, transform.position






            //Instantiate(ballGFX, transform.position, UnityEngine.Quaternion.Euler(new Vector3(0, 0, -90)));

            //Instantiate(farmingPlot, farmPosition, Quaternion.Euler(Vector3(45, 0, 0)));









            aimSight();


            Instantiate(aimPrefab, aimPoint, transform.rotation * Quaternion.Euler(0f, 0f, (float)angle));
            Instantiate(aimPrefab, mousePos, Quaternion.identity);
            //Console.WriteLine("Player: Object has spawned");
            if (Input.GetMouseButtonUp(0))
            {
                isAiming = false;
                if (angleIsAcceptable)
                {
                    Shoot(angle, 0.2f);
                }
                else
                {
                    // canShoot = false;
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