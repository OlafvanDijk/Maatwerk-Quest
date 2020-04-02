using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AreaManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private List<Area> areas;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Animator animator;

    [Header("UI")]
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider playerManaBar;
    [SerializeField] private Slider enemyHealthBar;
    [SerializeField] private Text battleInfo;
    [SerializeField] private FocusInputField inputField;

    private GameObject currentEnemy;
    private EnemyStats enemyStats;

    private bool defeatedEnemy = false;

    private void OnEnable()
    {
        defeatedEnemy = false;
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
                UpdateEnemyHealthBar(enemyHealthBar.maxValue);

                enemyStats.actualStats.name = enemyStats.baseStats.name;
                battleInfo.text = "You encountered a " + enemyStats.actualStats.name + "!";

                enemyHealthBar.onValueChanged.AddListener(UpdateEnemyHealthBar);
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
        UpdatePlayerHealthBar(playerHealthBar.maxValue);
        playerManaBar.maxValue = playerStats.actualStats.mana;
        playerManaBar.value = playerStats.actualStats.mana;
        UpdatePlayerManaBar(playerManaBar.maxValue);

        playerHealthBar.onValueChanged.AddListener(UpdatePlayerHealthBar);
        playerManaBar.onValueChanged.AddListener(UpdatePlayerManaBar);
    }

    /// <summary>
    /// Attacks player or enemy
    /// Checks if the player has enough mana
    /// Updates the health and mana when action has been performed.
    /// </summary>
    /// <param name="damage">How much damage you deal</param>
    /// <param name="playerOrEnemy">True if you are the player
    /// <param name="manaCost">The mana cost of the attack.</param>
    /// <returns></returns>
    public bool Attack(float damage, int manaCost = 0)
    {
        //Stats and Healthbar that will receive the damage
        Stats stats = enemyStats.actualStats;

        //Set stats and healthbar object for later use 
        stats = enemyStats.actualStats;


        Stats actualPlayerStats = playerStats.actualStats;

        //When a player does not have enough mana for the attack return false
        if (manaCost > actualPlayerStats.mana)
        {
            battleInfo.text = "Not enough mana. Current: " + actualPlayerStats.mana + " Needed: " + manaCost;
            return false;
        }

       
        System.Random random = new System.Random();
        int rdm = random.Next(0, 100);
        if (rdm > stats.Dex)
        {
            stats.health -= damage;

            //Update mana
            actualPlayerStats.mana -= manaCost;
            playerManaBar.value = actualPlayerStats.mana;

            if (stats.health <= 0)
            {
                stats.health = 0;
                enemyHealthBar.value = stats.health;
                KilledEnemy();
                return true;
            }
            enemyHealthBar.value = stats.health;
            battleInfo.text = "You dealt " + damage + " damage";
            EnemyAttack();
        }
        return true;
    }

    private void EnemyAttack()
    {
        int damage = 0;
        Stats stats = playerStats.actualStats;

        System.Random random = new System.Random();
        int rdm = random.Next(0, 100);
        if (rdm > stats.Dex)
        {
            damage = 7;
            stats.health -= damage;
            if (stats.health <= 0)
            {
                stats.health = 0;
                KilledPlayer();
            }

            playerHealthBar.value = stats.health;
        }

        int mana = playerStats.actualStats.mana + 5;
        int maxMana = (int)playerManaBar.maxValue;
        if (mana > maxMana)
        {
            mana = maxMana;
        }
        playerStats.actualStats.mana = mana;
        playerManaBar.value = mana;
        battleInfo.text += ", The " + enemyStats.actualStats.name + " dealt " + damage + " damage";
    }

    private void KilledEnemy()
    {
        battleInfo.text = "The " + enemyStats.actualStats.name + " has been defeated";
        inputField.interactable = false;
        animator.SetTrigger("Defeat");
        StartCoroutine(DestoryEnemyAndResetStats());
        //throw new NotImplementedException();
    }

    private void KilledPlayer()
    {
        Debug.Log("Player has been defeated");
        throw new NotImplementedException();
    }

    #region Update Sliders
    private void UpdateEnemyHealthBar(float value)
    {
        Text text = enemyHealthBar.GetComponentInChildren<Text>();
        UpdateBar(value, enemyHealthBar.maxValue, text);
    }

    private void UpdatePlayerHealthBar(float value)
    {
        Text text = playerHealthBar.GetComponentInChildren<Text>();
        UpdateBar(value, playerHealthBar.maxValue, text);
    }

    private void UpdatePlayerManaBar(float value)
    {
        Text text = playerManaBar.GetComponentInChildren<Text>();
        UpdateBar(value, playerManaBar.maxValue, text);
    }

    private void UpdateBar(float value, float maxValue, Text text)
    {
        text.text = value + "/" + maxValue; 
    }
    #endregion

    /// <summary>
    /// Destory enemy and Reset player.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestoryEnemyAndResetStats()
    {
        yield return new WaitForSecondsRealtime(1.25f);
        Destroy(currentEnemy);
        enemyStats = null;
        playerStats.actualStats.health = playerHealthBar.maxValue;
        playerStats.actualStats.mana = (int)playerManaBar.maxValue;

        enemyHealthBar.onValueChanged.RemoveListener(UpdateEnemyHealthBar);
        playerHealthBar.onValueChanged.RemoveListener(UpdatePlayerHealthBar);
        playerManaBar.onValueChanged.RemoveListener(UpdatePlayerManaBar);
        defeatedEnemy = true;
    }

    public void OnSubmit()
    {
        if (defeatedEnemy)
        {
            defeatedEnemy = false;
            inputField.interactable = true;
            //EventSystem.current.SetSelectedGameObject(inputField.gameObject);
            PanelSwitcher.Instance.SwitchPanel(Panels.Main);
        }
    }

    private void OnDisable()
    {
        Destroy(currentEnemy);
        currentEnemy = null;
        PlayerPrefs.SetString("Area", string.Empty);
    }
}