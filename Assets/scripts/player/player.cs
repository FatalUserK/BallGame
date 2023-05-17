using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Timeline;

public class player : MonoBehaviour
{

    GlobalEventsManager GEM; //creates a GEM variable

    void Start()
    {
        GEM = GlobalEventsManager.GEM; //calls on the GEM script to set itself
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GEM.cannonState == "Idle")    // check if the mouse is done and the cannon is exist, start aiming if yes
        {
            Aim();
        }
    }

    #region Aim & Fire

    #region Aim






    [Tooltip("[DEBUG] Marker used to show the points where the mouse cursor is being held and where it is aiming from")][SerializeField] GameObject marker;




    Vector2 aimPoint;
    Vector2 mousePos;
    void Aim()
    {
        aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("Player: Mouse position is locked correctly");
        StartCoroutine(AimCoroutine());
    }


    private IEnumerable AimSight(float _angle)
    {

        GameObject tempThingy = Instantiate(marker, position: mousePos * -1, Quaternion.identity);
        tempThingy.name = "temp!";


        //for (int i = 1; i < 5; ++i)
        //{
        //    //Destroy(GameObject.Find("BallGFX(Clone)"));
        //    GameObject aimBall[i] = Instantiate(ballGFX, transform.position + new Vector3(0, 0, i), Quaternion.Euler(0f, 0f, _angle));


        //}
        yield return null;
    }


    bool isAiming = false;
    private IEnumerator AimCoroutine()
    {
        bool angleIsAcceptable;
        isAiming = true;

        while (isAiming)
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 p1 = aimPoint;
            Vector2 p2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            double angle = Math.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Math.PI; // this line of code was provided by a friend of mine, it seems to run some basic math functions to check the location of the 2 points and from there, extrapolate the angle between them
            //from what i can tell, Math.Atan2 acquires a value in radeons by inputting a Y and X value, this is then multiplied by 180 and then divided by PI, likely to make it function with a circle? I will likely ask him about it later to get a better understanding of how it works

            int distance = Convert.ToInt32(Vector2.Distance(aimPoint, mousePos));


            if (angle <= -3 && angle >= -178 && distance >= 2)
            {
                angleIsAcceptable = true;
                AimSight((float)angle);
                Instantiate(marker, mousePos, Quaternion.identity); //generate at mousePos
            }
            else
            {
                angleIsAcceptable = false;
            }




            Instantiate(marker, aimPoint, transform.rotation * Quaternion.Euler(0f, 0f, (float)angle)); //generate another marker at aimPoint with rotation equal to the angle

            if (Input.GetMouseButtonUp(0)) //check if they let go of the mouse
            {

                isAiming = false;

                if (angleIsAcceptable)
                {
                    Shoot(angle); //Fire Away!
                }

            }
            yield return null; // Advance forward by 1 frame
        }

    }

    #endregion

    #region Fire

    [Tooltip("How many total shots the player has")][SerializeField] int shots;
    [Tooltip("How many shots the player has remaining")][SerializeField] int shotsRemaining;

    [Tooltip("The base time between shots")][SerializeField] float waitTime;

    [Tooltip("Projectile-type GameObject fired by the cannon")][SerializeField] GameObject projectile;

    public void Shoot(double shootAngle)
    {
        GEM.cannonState = "Firing"; //sets the cannont state so the GEM knows its firing

        shotsRemaining = shots; //sets the remaining shots to be equal to the amount of shots the player has

        Debug.Log(shotsRemaining + " " + shots); // where did shootAngle come from???

        GEM.fireAngle = shootAngle; //sets the angle the balls will be fired at


        while (shotsRemaining > 0)
        {
            Invoke("FireAway", (shots - shotsRemaining) * waitTime / GEM.timeMultiplier);
            //Invoke(Function, (amount of shots left to fire) * the default delay / the game time multiplier in the GEM)
            shotsRemaining--;
            Debug.Log(waitTime);
            GEM.mainCannon = null;
        }
    }


    public void FireAway()
    {
        GameObject shotFired = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, (float)0)); //instantiate the inputted projectile at the desired position and rotation


        foreach (GameObject ball in GEM.playerPorjectiles) //for every ball in the balls array
        {
            Physics2D.IgnoreCollision(shotFired.GetComponent<CircleCollider2D>(), ball.GetComponent<CircleCollider2D>()); //ignore collision with other balls
        }

        GEM.playerPorjectiles.Add(shotFired); //add the projectile to an array managed by the GEM
        shotFired.name = "Ball " + (GEM.playerPorjectiles.ToArray().Length); //name the projectile accordingly
        Debug.Log("Added " + shotFired.name + " to GEM.playerPorjectiles:\n" + GEM.playerPorjectiles.ToString()); //make a debug log


        GetComponent<AudioSource>().Play(0); //play the firing sound

        if (GEM.playerPorjectiles.ToArray().Length == shots) { Destroy(gameObject); } //if there are no more  projectiles to be fired, destroy cannon
    }

    #endregion

    #endregion

}
