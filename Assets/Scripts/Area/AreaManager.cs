using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private List<Area> areas;
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider playerManaBar;
    [SerializeField] private Slider enemyHealthBar;

    private GameObject currentEnemy;
    private EnemyStats enemyStats;

    private void OnEnable()
    {
        SetEnemy();
        SetPlayer();
    }

    /// <summary>
    /// Set a random enemy for the current area.
    /// </summary>
    private void SetEnemy()
    {
        string currentArea = PlayerPrefs.GetString("Area");
        foreach (Area area in areas)
        {
            if (area.areaName.Equals(currentArea))
            {
                System.Random random = new System.Random();
                GameObject enemy = area.enemies[random.Next(area.enemies.Count)];
                currentEnemy = Instantiate(enemy);
                enemyStats = currentEnemy.GetComponent<EnemyStats>();
                enemyStats.SetLevel(random.Next(area.minLevelEnemy, area.maxLevelEnemy + 1));
                enemyHealthBar.maxValue = enemyStats.actualStats.health;
                enemyHealthBar.value = enemyStats.actualStats.health;
                break;
            }
        }
    }

    /// <summary>
    /// Set Health and Mana for player
    /// </summary>
    private void SetPlayer()
    {
        playerHealthBar.maxValue = playerStats.actualStats.health;
        playerHealthBar.value = playerStats.actualStats.health;
        playerManaBar.maxValue = playerStats.actualStats.mana;
        playerManaBar.value = playerStats.actualStats.mana;
    }

    public bool Attack(float damage, bool playerOrEnemy, int manaCost = 0)
    {
        Stats stats = null;
        Slider healthBar = null;
        if (playerOrEnemy)
        {
            //When a player does not have enough mana for the attack return false
            if (manaCost > stats.mana)
            {
                return false;
            }

            //Set stats and healthbar object for later use 
            stats = playerStats.actualStats;
            healthBar = playerHealthBar;

            //Update mana
            stats.mana -= manaCost;
            playerManaBar.value = stats.mana;
        }
        else
        {
            stats = enemyStats.actualStats;
            healthBar = enemyHealthBar;
        }

        stats.health -= damage;
        if (stats.health <= 0)
        {
            stats.health = 0;
            Defeat(playerOrEnemy);
        }

        healthBar.value = stats.health;
        return true;
    }

    private void Defeat(bool playerOrEnemy)
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        Destroy(currentEnemy);
        currentEnemy = null;
        PlayerPrefs.SetString("Area", string.Empty);
    }
}