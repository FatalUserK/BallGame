using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using TMPro;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    //[SerializeField] GameObject enemy; // oh the misery

    GameObject parent;

    List<int> collisionData;

    //Component attackerScript;


    private void Awake()
    {
        parent = gameObject.transform.parent.gameObject;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack" && gameObject.tag != "Boss") 
        {
            switch (parent.tag) {
                case "Block":
                    parent.GetComponent<Block>().OnHit(collision.gameObject.GetComponent<FiredBall>().ballData);
                    break;
                case "Boss":
                    parent.GetComponent<Boss>().OnHit(collision.gameObject.GetComponent<FiredBall>().ballData);
                    break;
            }
        }
    }
}
