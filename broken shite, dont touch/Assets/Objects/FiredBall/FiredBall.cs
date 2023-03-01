using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FiredBall : MonoBehaviour
{
    GlobalEventsManager GEM;

    [SerializeField] Rigidbody2D rb;
    AudioSource audioData;

    Vector2 moveDirection;


    public float ballSpeed = 8;


    /*[TooltipAttribute("Variables that are delivered through the ballData List")]*/[Header("Ball Data")]
    [Tooltip("Base Damage of the ball.")]public int ballDamage = 1;
    [Tooltip("Multiplier for the ballDamage.")]public int ballDamageMultiplier = 1;
    [Tooltip("Unused Variable.")]public int ballVariable2 = 0;
    [Tooltip("Unused Variable.")] public int ballVariable3 = 16;
    [Tooltip("Unused Variable.")] public int ballVariable4 = 29;
    [Tooltip("Unused Variable.")] public int ballVariable5 = 31276239;

    [Tooltip("Contains relevant data about the object to more easily transfer it to other objects.")]public List<int> ballData;



    private void Awake()
    {
        GEM = GlobalEventsManager.GEM;
        ballData.Add(ballDamage);
        ballData.Add(ballDamageMultiplier);
        ballData.Add(ballVariable2);
        ballData.Add(ballVariable3);
        ballData.Add(ballVariable4);
        ballData.Add(ballVariable5);



    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //find the rigidbody component in the object
        moveDirection = new Vector2(Mathf.Cos(GEM.fireAngle * Mathf.Deg2Rad) * ballSpeed, Mathf.Sin(GEM.fireAngle * Mathf.Deg2Rad) * ballSpeed) / -1;
        //Debug.Log("<color=light_blue>moveDirection = \"" + moveDirection + "\nmath1: \"" + (Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad) * ballSpeed) + "\nmath2: \"" + (Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad) * ballSpeed) + "\nAngle: " + transform.eulerAngles.z + "</color>");
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
