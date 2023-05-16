using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

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

    void Aim()
    {

    }


    [Tooltip("How many total shots the player has")][SerializeField] int shots;
    [Tooltip("How many shots the player has remaining")][SerializeField] int shotsRemaining;

    [Tooltip("The base time between shots")][SerializeField] float waitTime;

    public void FireAway(double shootAngle)
    {
        GEM.cannonState = "Firing"; //sets the cannont state so the GEM knows its firing

        shotsRemaining = shots; //sets the remaining shots to be equal to the amount of shots the player has

        Debug.Log(shotsRemaining + " " + shots); // where did shootAngle come from???

        GEM.fireAngle = shootAngle; //sets the angle the balls will be fired at


        while (shotsRemaining > 0)
        {
            Invoke("ExecuteAfterTime", (shots - shotsRemaining) * waitTime / GEM.timeMultiplier);
            //Invoke(Function, (amount of shots left to fire) * the default delay / the game time multiplier in the GEM)
            shotsRemaining--;
            Debug.Log(waitTime);
            GEM.mainCannon = null;
        }
    }

    #endregion

}
