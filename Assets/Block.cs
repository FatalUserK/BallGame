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

    public float blockIdentity;

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

    public void BlockDestroy()
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

    void BlockUpdate()
    {
        //polyCollider.CreatePrimitive(6);
        switch (blockIdentity)
        {

            #region 1.Square
            case 1:
                blockName = "Square " + blockHealth;
                break;

            case 1.1f:
                blockName = "Diamond " + blockHealth;
                break;

            case 1.2f:
                blockName = "Spinning Diamond " + blockHealth;
                break;

            #endregion

            #region 2.Triangle
            case 2:
                blockName = "Triangle ";
                break;

            case 2.1f:
                blockName = "Bottom-Left Triangle ";
                break;

            case 2.2f:
                blockName = "Top-Left Triangle ";
                break;

            case 2.3f:
                blockName = "Top-Right Triangle ";
                break;

            case 2.4f:
                blockName = "Bottom-Right Triangle ";
                break;

            #endregion

            #region 3.Other Regulars

            case 6:
                blockName = "Pentagon ";
                break;

            case 3:
                blockName = "Hexagon ";
                break;

            #endregion

            #region 4.Round

            case 4:
                blockName = "Circle ";
                break;

                #endregion

            #region 5.Abnormal

            case 5:

                break;


            #endregion



            default:
                Debug.Log(gameObject + " HAS INCORRECT DATA\nData: " + blockIdentity);
                gameObject.name = "INCORRECT BLOCK IDENTITY " + blockIdentity;
                break;

        }
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }


}
