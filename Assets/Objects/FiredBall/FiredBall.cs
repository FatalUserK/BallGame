using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FiredBall : MonoBehaviour
{
    GlobalEventsManager GEM;

    [SerializeField] Rigidbody2D rb;

    Vector2 moveDirection;


    public float ballSpeed = 10;





    private void Awake()
    {
        GEM = GlobalEventsManager.GEM;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //find the rigidbody component in the object
        moveDirection = new Vector2(Mathf.Cos(GEM.fireAngle * Mathf.Deg2Rad) * ballSpeed, Mathf.Sin(GEM.fireAngle * Mathf.Deg2Rad) * ballSpeed) / -1;
        Debug.Log("<color=light_blue>moveDirection = \"" + moveDirection + "\nmath1: \"" + (Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad) * ballSpeed) + "\nmath2: \"" + (Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad) * ballSpeed) + "\nAngle: " + transform.eulerAngles.z + "</color>");
        rb.AddForce(moveDirection * 10);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" && GEM.boringBoolValue)
        {

            if (GEM.cannonState == "Firing")
            {
                GEM.CreateCannon(transform);
                Debug.Log("Collided with Ground");
            }

            else if (GEM.cannonState == "Reloading")
            {
                Component.Destroy(rb);

                while (transform.position != GEM.mainCannon.transform.position)
                {
                    Vector2.MoveTowards(transform.position, GEM.mainCannon.transform.position, 1);
                    Debug.Log("moving ball to New Cannon...");
                }

            }
            else { Debug.Log("<color=red>BALL TOUCHED GROUND IN UNACCEPTABLE STATE</color>\nSTATE: \"" + GEM.cannonState + "\"\n"); }
            Destroy(gameObject);

        }
        else
        {
            Debug.Log("Ball has Bounced!");
        }
    }

}
