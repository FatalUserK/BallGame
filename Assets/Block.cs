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







    public PolygonCollider2D polyCollider;

    private string blockName;

    Sprite newSprite;

    public new Sprite[] spriteArray;

    public bool mysteryHealth = false;
    public bool invulnerable = false;
    public int blockType = 1;
    public int blockHealth;
    public int damageMultiplier = 1;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    // Start is called before the first frame update
    void Start()
    {


        gameObject.name = blockName + blockHealth;
    }


    void SetShape(int Sides)
    {
        //polyCollider.CreatePrimitive(6);
        switch (blockType)
        {
            case 0:
                blockName = "Square ";
                //newSprite = 
                break;

            case 1:
                blockName = "Triangle ";
                break;

            case 2:
                blockName = "Diamond ";
                break;

            case 3:
                blockName = "Hexagon ";
                break;

            case 4:
                blockName = "Circle ";
                break;

            case 5:
                blockName = "SpinTriangle ";
                break;
        }
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
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
                blockHealth = blockHealth - damageMultiplier;
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
        if (collision == null) { } // make it check if its fired ball tag
        // OnHit(damageMultiplier * collision.damage); // if yes, deal damage that is equal to Ball Damage * damageMultiplier, also clamp to avoid going below 0
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        txt.text = "" + blockHealth;
    }
}
