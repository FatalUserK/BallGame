using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalEventsManager : MonoBehaviour
{
    public static bool isReloading = false;
    public static GlobalEventsManager Instance { get; private set; }

}