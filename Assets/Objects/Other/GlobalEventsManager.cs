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


    public float timeMultiplier = 1;


    public int test;

    [SerializeField] private GameObject cannonPrefab;

    [Tooltip("Disable Ball Collection")] public bool boringBoolValue = true; // no idea what this does but i decided to bring it over from the old script anyway lmao

    public void CheckTurn()
    {
        Debug.Log("balls.Count = " + playerPorjectiles.Count);
        if (playerPorjectiles.Count == 0)
        {
            StartTurn();
        }
    }


    public void StartTurn()
    {
        //timeMultiplier = 1 + (level / 100);
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