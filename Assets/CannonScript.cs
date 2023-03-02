using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CannonScript : MonoBehaviour
{
    [SerializeField] GameObject marker;
    [SerializeField] GameObject ballGFX;
    [SerializeField] GameObject firedBall;
    [SerializeField] GameObject aimPrefab;

    GlobalEventsManager GEM;


    [SerializeField] double angle;
    [SerializeField] int distance;

    float speed;

    public int shotsRemaining;
    public int shots;
    [Tooltip("The base time between shots")][SerializeField]float waitTime;



    bool isAiming = false;


    Vector2 mousePos;
    Vector2 aimPoint;

    // Start is called before the first frame update
    void Start()
    {
        GEM = GlobalEventsManager.GEM;
        GEM.mainCannon = gameObject;
        GEM.cannonState = "Idle";
    }

    // Update is called once per frame


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GEM.cannonState == "Idle")    // check if the mouse is done and the cannon is exist, start aiming if yes
        {
            //Debug.Log("Player: Mouse is down");
            Aim();
        }
    }


    public void FireAway(double shootAngle)
    {
        GEM.cannonState = "Firing";
        shotsRemaining = shots;
        Debug.Log(shotsRemaining + " " + shots); // where did shootAngle come from???

        GEM.fireAngle = (float)angle;







        while (shotsRemaining > 0)
        {
            Invoke("ExecuteAfterTime", (shots - shotsRemaining) * waitTime / GEM.timeMultiplier);

            shotsRemaining--;
            Debug.Log(waitTime);
        }
    }

    public void ExecuteAfterTime()
    {

        GameObject shotFired = Instantiate(firedBall, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, (float)0)); //shoot after time

        shotFired.name = "Ball " + GEM.balls.ToArray().Length + 1; // names the ball accordingly

        foreach (GameObject ball in GEM.balls) // for every ball in the balls array
        {
            Physics2D.IgnoreCollision(shotFired.GetComponent<CircleCollider2D>(), ball.GetComponent<CircleCollider2D>()); //ignore collision with other balls
        }

        GEM.balls.Add(shotFired);
        Debug.Log("Added " + shotFired.name + " to GEM.balls:\n" + GEM.balls.ToString());
        //if (shotsRemaining == 0) { Destroy(gameObject); }

    }


    void Aim()
    {
        aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("Player: Mouse position is locked correctly");
        StartCoroutine(AimCoroutine());
    }

    private IEnumerable AimSight(float _angle)
    {

        GameObject tempThingy = Instantiate(aimPrefab, position: mousePos * -1, Quaternion.identity);



        //for (int i = 1; i < 5; ++i)
        //{
        //    //Destroy(GameObject.Find("BallGFX(Clone)"));
        //    GameObject aimBall[i] = Instantiate(ballGFX, transform.position + new Vector3(0, 0, i), Quaternion.Euler(0f, 0f, _angle));


        //}
        yield return null;
    }


    private IEnumerator AimCoroutine()
    {
        bool angleIsAcceptable;
        isAiming = true;

        while (isAiming)
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 p1 = aimPoint;
            Vector2 p2 = mousePos;

            angle = Math.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Math.PI; // this line of code was provided by a friend of mine, it seems to run some basic math functions to check the location of the 2 points and from there, extrapolate the angle between them

            distance = Convert.ToInt32(Vector2.Distance(aimPoint, mousePos));


            if (angle <= -3 && angle >= -178 && distance >= 2)
            {
                angleIsAcceptable = true;
                AimSight((float)angle);
            }

            else
            {
                angleIsAcceptable = false;
            }

            //transform.rotation = Quaternion.Euler(0f, 0f, );



            Instantiate(marker, aimPoint, transform.rotation * Quaternion.Euler(0f, 0f, (float)angle));
            Instantiate(marker, mousePos, Quaternion.identity);

            //Console.WriteLine("Player: Object has spawned");
            if (Input.GetMouseButtonUp(0))
            {
                isAiming = false;
                if (angleIsAcceptable)
                {
                    FireAway(angle);
                }
                else
                {
                    // canShoot = false;
                }
            }
            yield return null;
        }

    }





}
