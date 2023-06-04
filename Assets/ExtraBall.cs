using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBall : MonoBehaviour
{

    GlobalEventsManager GEM;


    private void Start()
    {
        GEM = GlobalEventsManager.GEM;
    }


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("EXTRA BALL TRIGGER 1");
        if (col.gameObject.tag == "PlayerAttack")
        {
            Debug.Log("EXTRA BALL TRIGGER 2");
            GEM.ballCount++;
            Destroy(gameObject);
            Debug.Log("EXTRA BALL TRIGGER 3");
        }
    }
}
