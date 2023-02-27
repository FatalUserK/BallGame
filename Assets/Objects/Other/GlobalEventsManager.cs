using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalEventsManager : MonoBehaviour
{
    public static GlobalEventsManager GEM;
    public static bool isReloading = false;

    [SerializeField] public List<GameObject> balls;
    public int firedBalls;

    public GameObject mainCannon;
    public string cannonState; // "Idle", "Firing", "Reloading".
    public float fireAngle;

    


    public int test;

    [SerializeField] private GameObject cannonPrefab;

    public bool boringBoolValue = true; // no idea what this does but i decided to bring it over from the old script anyway


    private void Awake()
    {
        GEM = this;
    }

    public void CreateCannon(Transform target)
    {
        mainCannon = Instantiate(cannonPrefab, target);
        isReloading = true;
        cannonState = "Reloading";
    }


}