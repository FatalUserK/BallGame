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
    // Start is called before the first frame update
    void Start()
    {
        switch (blockType)
        {
            case 0:
                blockName = "Square ";
                Console.WriteLine("");
                break;

            case 1:
                blockName = "Triangle ";
                Console.WriteLine("");
                break;

            case 2:
                blockName = "Diamond ";
                Console.WriteLine("");
                break;

            case 3:
                blockName = "Hexagon ";
                Console.WriteLine("");
                break;

            case 4:
                blockName = "Circle ";
                Console.WriteLine("");
                break;

            case 5:
                blockName = "SpinTriangle ";
                Console.WriteLine("");
                break;
        }


    }


    public void OnHit()
    {
        if (!invulnerable)
        {
            if (blockHealth <= 1)
            {
                BlockDestroy();
            }
            else
            {
                blockHealth--;
            }
        }

    }

    void BlockDestroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "" + blockHealth;
    }
}
