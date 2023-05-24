using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalEventsManager : MonoBehaviour
{

    [SerializeField] public List<GameObject> playerPorjectiles;
    public int firedBalls;


    public static GlobalEventsManager GEM;
    public GameObject mainCannon; // main cannon/player when in Idle or Reloading state
    public string cannonState; // "Idle", "Firing", "Reloading".
    public double fireAngle;

    public static bool isReloading = false;




    public int test;



    public bool tutorialMode = true;

    int level = 0;

    public float timeMultiplier = 1;


    [SerializeField] private GameObject blockManager;
    [SerializeField] private GameObject cannonPrefab;

    [Tooltip("Disable Ball Collection")] public bool boringBoolValue = true; // no idea what this does but i decided to bring it over from the old script anyway lmao

    public void CheckTurn()
    {
        Debug.Log("balls.Count = " + playerPorjectiles.Count);
        if (playerPorjectiles.Count == 0)
        {
            EndTurn();
        }
    }


    public void EndTurn()
    {
        Debug.Log("damn, turn end :pensive:");
        //timeMultiplier = 1 + (level / 100);
        
        if (blockManager.GetComponentsInChildren<BlockGenerator>().Length == 0) { tutorialMode = false; Debug.Log("i am one in a krillion"); } //check if all blocks in tutorial mode have been destroyed, disable tutorialMode if there are no more remaining blocks

        if (!tutorialMode)
        {
            level++;
            blockManager.GetComponent<BlockManager>().GenerateNewBlocks(level);
            for (int i = 0; i <= blockManager.GetComponentsInChildren<BlockGenerator>().Length - 1; i++)
            {
                blockManager.GetComponentsInChildren<BlockGenerator>()[i].CallBlockDescendbcUnitySucks();
            }
        }
        
        cannonState = "Idle";
    }


    private void Awake()
    {
        GEM = this;
    }

    public void CreateCannon(Vector3 target)
    {
        mainCannon = Instantiate(cannonPrefab, target, Quaternion.identity);
        isReloading = true;
        cannonState = "Reloading";
    }


}