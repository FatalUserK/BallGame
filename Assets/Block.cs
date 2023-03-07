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


    public bool mysteryHealth = false;
    public bool invulnerable = false;
    public int blockShape = 2;
    public int blockHealth;
    public int blockDamageMultiplier = 1;

    AudioSource audioData;
    //Renderer renderer;



    void Awake()
    {
        audioData = GetComponent<AudioSource>();
    }

    public void GenerateBlock(int minRange = 1, int maxRange = 3, int _blockShape = 1)
    {
        if (_blockShape > -1) { blockShape = _blockShape; }

        
        blockHealth = UnityEngine.Random.Range(minRange, maxRange);

        GetComponentInChildren<Renderer>().Fill(blockShape);

        //GetComponent<SpriteRenderer>().sprite = spriteArray[blockShape];

        gameObject.name = blockName + blockHealth;
    }




    public void OnHit(List<int> data)
    {
        if (!invulnerable)
        {

            blockHealth = blockHealth - data[0] * data[1] * blockDamageMultiplier;


            audioData.Play(0);
            gameObject.name = blockName + blockHealth + gameObject.transform.position;
            if (blockHealth < 1) { BlockDestroy(); }
        }

    }

    void BlockDestroy()
    {
        audioData.Play(1);
        Debug.Log("DESTROYING BLOCK " + gameObject.name);
        Destroy(gameObject);
    }





    // Update is called once per frame
    void FixedUpdate()
    {
        txt.text = "" + blockHealth;
        //try { transform.position = Vector3.MoveTowards(transform.position, GlobalEventsManager.GEM.mainCannon.transform.position, .01f); Debug.Log("TRIED TO MOVE OBJECT, " + transform.position + " " + GlobalEventsManager.GEM.mainCannon.transform.position); }
        //catch { Debug.Log("UNABLE TO MOVE OBJECT, " + transform.position + " " + GlobalEventsManager.GEM.mainCannon.transform.position); };
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


}
