using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Block : MonoBehaviour
{
    public TextMeshProUGUI txt;

    private string blockName;
    
    public bool mysteryHealth = false;
    public bool invulnerable = false;
    public int blockType;
    public int blockHealth = 69;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        switch (blockType)
        {
            case 0:
                blockName = "Square ";
                Debug.Log("");
                break;

            case 1:
                blockName = "Triangle ";
                Debug.Log("");
                break;

            case 2:
                blockName = "Diamond ";
                Debug.Log("");
                break;

            case 3:
                blockName = "Hexagon ";
                Debug.Log("");
                break;

            case 4:
                blockName = "Circle ";
                Debug.Log("");
                break;

            case 5:
                blockName = "SpinTriangle ";
                Debug.Log("");
                break;
        }

        gameObject.name = blockName + blockHealth;
    }


    public void OnHit(int onHitDamage)
    {
        if (!invulnerable)
        {
            if (blockHealth <= onHitDamage)
            {
                BlockDestroy();
            }
            else
            {
                blockHealth--;
                gameObject.name = blockName + blockHealth;
            }
        }

    }

    void BlockDestroy()
    {
        Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit(damage);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        txt.text = "" + blockHealth;
    }
}
