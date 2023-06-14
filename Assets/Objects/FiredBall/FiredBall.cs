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


    //Mathf.Clamp((((timeSinceLastHit + 5) / 10 ^ (timeSinceLastHit - 1000) / 500) - 43) / 100, 0, 10) //NOTE: ADD THIS AS SOMETHING THAT IS APPLIED TO GRAVITY

    int timeSinceLastHit = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb != null)
        {
            timeSinceLastHit++;
            rb.gravityScale += 0.00001f;
            //if ()
        }
        
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //find the rigidbody component in the object

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
        GEM = GlobalEventsManager.GEM;
        moveDirection = new Vector2(Mathf.Cos((float)GEM.fireAngle * Mathf.Deg2Rad) * ballSpeed, Mathf.Sin((float)GEM.fireAngle * Mathf.Deg2Rad) * ballSpeed) / -1;
        //Debug.Log("<color=light_blue>moveDirection = \"" + moveDirection + "\nmath1: \"" + (Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad) * ballSpeed) + "\nmath2: \"" + (Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad) * ballSpeed) + "\nAngle: " + transform.eulerAngles.z + "</color>");
        rb.AddForce(moveDirection * 10);
        Invoke("SetBool",.15f);

    }

    void SetBool()
    {
        boringBoolValue = true;
    }

    // Update is called once per frame
    IEnumerator ReturnToSender()
    {
        //Debug.Log(gameObject.name + "is returning to sender...");
        Destroy(rb);
        int j = 0;
        while (transform.position != GEM.mainCannon.transform.position || j == 100) // [ || i == 100 ] is a failsafe in the event something breaks and it takes longer than expected to return to sender. In this case, it will proceed regardless and remove the ball from play
        {
            transform.position = Vector3.MoveTowards(transform.position, GEM.mainCannon.transform.position, .03f);

            j++;
            yield return null;
        }
        GEM.playerPorjectiles.Remove(gameObject);
        //Debug.Log(gameObject.name + " REMOVED FROM GEM.balls");
        GEM.CheckTurn();
        //Debug.Log(gameObject.name + " IS CHECKING TURN");
        Destroy(gameObject);
    }

    bool boringBoolValue = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" && boringBoolValue)
        {
            Destroy(rb);
            //Debug.Log(gameObject.name + " COLLIDED WITH GROUND");

            if (GEM.cannonState == "Firing")
            {
                GEM.CreateCannon(new Vector2(transform.position.x, 3.2f));
                //Debug.Log(gameObject.name + " Collided with Ground for the first time! Creating Cannon at " + transform.position);
                GEM.playerPorjectiles.Remove(gameObject);
                GEM.CheckTurn();
                Destroy(gameObject);
            }

            else if (GEM.cannonState == "Reloading")
            {
                boringBoolValue = false;
                StartCoroutine(ReturnToSender());

            }
            //else { Debug.Log("<color=red>BALL TOUCHED GROUND IN UNACCEPTABLE STATE</color>\nSTATE: \"" + GEM.cannonState + "\"\n"); }

        }
        else if (col.gameObject.tag == "Block")
        {
            rb.gravityScale = 0;
            GEM.blockHits++;
        }
    }

}
