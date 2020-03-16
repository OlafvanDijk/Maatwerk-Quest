using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats.CalculateStats();
    }
}
