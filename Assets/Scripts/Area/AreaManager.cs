using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private List<Area> areas;
    private GameObject currentEnemy;

    private void OnEnable()
    {
        string currentArea = PlayerPrefs.GetString("Area");
        Debug.Log(currentArea);
        foreach (Area area in areas)
        {
            if (area.areaName.Equals(currentArea))
            {
                System.Random random = new System.Random();
                GameObject enemy = area.enemies[random.Next(area.enemies.Count)];
                currentEnemy = Instantiate(enemy);
            }
        }
    }

    private void OnDisable()
    {
        Destroy(currentEnemy);
        currentEnemy = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
