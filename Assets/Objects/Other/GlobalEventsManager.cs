using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalEventsManager : MonoBehaviour
{

    [SerializeField] public List<GameObject> balls;
    public int firedBalls;


    public static GlobalEventsManager GEM;
    public GameObject mainCannon;
    public string cannonState; // "Idle", "Firing", "Reloading".
    public float fireAngle;

    public static bool isReloading = false;

    public int stage;
    public int level;

    public float timeMultiplier = 1;


    public int test;

    [SerializeField] private GameObject cannonPrefab;

    [Tooltip("Disable Ball Collection")] public bool boringBoolValue = true; // no idea what this does but i decided to bring it over from the old script anyway lmao

    public void CheckTurn()
    {
        Debug.Log("balls.Count = " + balls.Count);
        if (balls.Count == 0)
        {
            StartTurn();
        }
    }


    public void StartTurn()
    {
        timeMultiplier = 1 + (level / 100);
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