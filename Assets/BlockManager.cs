using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI.Table;

public class BlockManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool blockSpawnMode;

    [SerializeField] System.Random rand = new System.Random();

    [SerializeField] GameObject block;
    [SerializeField] GameObject extraBall;


    GlobalEventsManager GEM;


    public IEnumerator Descend(int amount = 1, int recall = 0)
    {
        float waitTime = (.05f / amount) + .1f;
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            child.transform.DOMove(new Vector3(child.position.x, child.position.y - amount, child.position.z), waitTime);
        }
        yield return new WaitForSeconds(waitTime);


        Debug.Log("rows at Descend recall start: " + recall);
        recall--;
        if (recall > 0) { GenerateNewRow(rows: recall); }

        Debug.Log("rows at Descend recall end: " + recall);
        #region cringe naenae commented stuff
        //List<Transform> targetList = new List<Transform>();
        //for (int i = 0; i < transform.childCount - 1; ++i)
        //{
        //    targetList.Add(transform.GetChild(i));
        //}
        //while (targetList.Count > 0)
        //{
        //    foreach (Transform t in targetList)
        //    {
        //        Vector3 destination = new Vector3(t.position.x, t.position.y - amount, t.position.z);
        //        t.position = Vector3.MoveTowards(t.position, destination, .1f);
        //    }
        //    yield return null;
        //}


        //    for (int i = 0; i < transform.childCount - 1; ++i)
        //{
        //    Debug.Log("Moving object " + i + "/" + transform.childCount);
        //    Transform target = transform.GetChild(i);
        //    Vector3 destination = new Vector3(target.position.x, target.position.y - amount, target.position.z);
        //    if (target.position != destination)
        //    {
        //        target.position = Vector3.MoveTowards(target.position, destination, .1f);
        //    }
        //}
        #endregion


        yield return null;

    }

    void Start()
    {
        GEM = GlobalEventsManager.GEM;

        if (GEM.tutorialMode)
        {

            //for (int i = 7; i > 0; i--) // Y
            //{
            //    rand.Next();
            //    for (int j = 0; j < 8; j++) // X
            //    {
            //        int rnd = UnityEngine.Random.Range(-15, 5);
            //        rnd = rand.Next(-15, 5);

            //        //Debug.Log("GENERATE TUTORIAL BLOCK RND NUMBER IS " + rnd);

            //        if (rnd > 0 && rnd <= 3) { GenerateNewBlock(new Vector2(transform.localPosition.x + j + .5f, transform.localPosition.y + i - .5f), 1, 5, "square"); }
            //        else if (rnd == 4) { GenerateNewBlock(new Vector2(transform.localPosition.x + j + .5f, transform.localPosition.y + i - .5f), 1, 5, "corner"); }
            //    }
            //}



            GenerateNewRow(7, 3, 9);

        }
    }

    void GenerateNewBlock(Vector2 _position, int minHealth, int maxHealth,string _blockType)
    {
        GameObject newBlock = Instantiate(block, _position, Quaternion.identity, transform);
        newBlock.GetComponent<BlockGenerator>().GenerateBlock(rand.Next(minHealth, maxHealth), _blockType);
    }


    //block types: square 1, corner 2, everything 0
    public void GenerateNewRow(int baseHP = 10, int blockTypes = 1, int rows = 1, bool doDescend = true, int[] blockData = null)
    {
        if (blockData != null)
        {
            baseHP = blockData[0];
            blockTypes = blockData[1];
        }

        List<string> blockTypeList = new List<string>();

        
        if (blockTypes - 2 >= 0)
        {
            blockTypeList.Add("corner");
            blockTypes -= 2;
        }
        if (blockTypes - 1 >= 0)
        {
            blockTypeList.Add("square");
        }



        int ballUp = UnityEngine.Random.Range(0, 8);

        for (int i = 0; i <= 8; i++)
        {
            int r = rand.Next(1, 3);
            if (i != ballUp && r == 1)
            {
                int typePoolDraw = rand.Next(0, blockTypeList.Count);
                Debug.Log("Luck of the draw: " + typePoolDraw);
                GenerateNewBlock(new Vector2(transform.localPosition.x + i + .5f, transform.localPosition.y - .5f), Mathf.Clamp(Mathf.RoundToInt(baseHP * (float)0.9), 1, baseHP * 2), Mathf.RoundToInt(baseHP * (float)1.3), blockTypeList.ElementAt(typePoolDraw)); //this is probably somewhat inefficient, but eh
            }                                                           //minRange = Clamp( Rounded( score/1.2 ), 1, -1 ), maxRange = Rounded( score*1.2 )

            else if (i == ballUp)
            {
                GameObject newBlock = Instantiate(extraBall, new Vector2(transform.localPosition.x + i + .5f, transform.localPosition.y - .5f), Quaternion.identity, transform);
            }
        }

        Debug.Log("rows at StartCoroutine: " + rows);
        if (doDescend) { StartCoroutine(Descend(1, rows)); }
        else if ( rows > 0 )
        {
            GenerateNewRow(rows);
        }
    }



}
