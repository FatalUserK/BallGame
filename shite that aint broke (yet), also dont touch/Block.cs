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



    Vector2 test = new Vector2(2, 2);



    public PolygonCollider2D polyCollider;

    private string blockName;

    Sprite newSprite;

    public new Sprite[] spriteArray;

    public bool mysteryHealth = false;
    public bool invulnerable = false;
    public int _blockShape = 0;
    public int blockHealth;
    public int damageMultiplier = 1;

    AudioSource audioData;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateBlock();
    }

    public void GenerateBlock(int minRange = 1, int maxRange = 2, int blockShape = -1)
    {
        if (blockShape == -1) { blockShape = _blockShape; }

        System.Random rand = new System.Random();
        blockHealth = rand.Next(minRange, maxRange);

        GetComponent<SpriteRenderer>().sprite = spriteArray[blockShape];

        gameObject.name = blockName + blockHealth;
    }


    void SetShape(int shape)
    {
        //polyCollider.CreatePrimitive(6);
        switch (shape)
        {
            case 0:
                blockName = "Square ";
                break;

            case 1:
                blockName = "Triangle ";
                break;

            case 2:
                blockName = "Pentagon ";
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


    public void OnHit(List<int> data)
    {
        if (!invulnerable)
        {
            if (blockHealth <= data[0])
            {
                BlockDestroy();
            }
            else
            {
                Debug.Log("BLOCK HIT!");
                blockHealth = blockHealth - data[0] * data[1] * damageMultiplier;

                //audioData.Play(0);
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
