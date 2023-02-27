using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] GameObject enemy; // oh the misery
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == /*enemy) { cry about it; } */ null)
        {

        }
        
    }
}
