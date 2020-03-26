using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Action
{
    [SerializeField] Attacks attacks;
    [SerializeField] PlayerStats playerStats;

    private AreaManager areaManager;

    /// <summary>
    /// Get the instance of the AreaManager
    /// </summary>
    private void OnEnable()
    {
        areaManager = GameObject.FindObjectOfType<AreaManager>();
    }

    /// <summary>
    /// Find the attack in the list of attacks and calculate the damage
    /// then call the attack function of the area manager.
    /// </summary>
    public override void OnAction()
    {
        if (areaManager != null)
        {
            string attackName = option.optionText;
            AttackStruct attack = attacks.GetAttack(attackName);
            int damage = CalculateDamage(attack.damage, attack.physicalOrMagic);
            areaManager.Attack(damage, attack.manacost);
        }
    }

    /// <summary>
    /// Calculte the Damage based on the multiplier.
    /// </summary>
    /// <param name="baseDamage">base damage of the attack</param>
    /// <param name="physicalOrMagic">Is the multiplier from strength or Intelligence</param>
    /// <returns></returns>
    private int CalculateDamage(int baseDamage, bool physicalOrMagic)
    {
        float damageMultiplier = 1;
        if (physicalOrMagic)
        {
            damageMultiplier = playerStats.actualStats.bonusDamageStr;
        }
        else
        {
            damageMultiplier = playerStats.actualStats.bonusDamageInt;
        }
        return Mathf.CeilToInt((float)baseDamage * damageMultiplier);
    }
}
