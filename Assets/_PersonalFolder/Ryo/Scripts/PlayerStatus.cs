using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] PlayerData status;
    [SerializeField] float sunThreshold = 4.0f;
    private float _moveSpeed;

    private float damageInterval;

    // Start is called before the first frame update
    void Start()
    {
        status.SUN = status.MaxSUN;
        status.Stamina = status.MaxStamina;

        damageInterval = status.DamageInterval_Normal;
    }

    // Update is called once per frame
    void Update()
    {
        // Player dead
        if (player.IsDead) return;

        if (status.SUN <= 0.0f && !player.IsDead)
        {
            PlayerDead();
        }


        // Player stamina
        if (player.IsMove)
        {
            status.Stamina += status.HealStamina_Walk;
        }
        else
        {
            status.Stamina += status.HealStamina_Wait;
        }

        if (player.IsMove && player.IsDash)
        {
            status.Stamina -= status.StaminaCost;
        }

        if (status.Stamina < 0.0f)
        {
            status.Stamina = 0.0f;
            player.CanDash = false;
            player.IsDash = false;
        }
        else if (status.Stamina > status.MaxStamina)
        {
            status.Stamina = status.MaxStamina;
        }


        // Player dash state
        if (!player.CanDash && status.Stamina >= status.MinDashCost)
        {
            player.CanDash = true;
        }


        // Player Damage
        if (player.IsDamage)
        {
            if (damageInterval >= 0.0f)
            {
                damageInterval -= Time.deltaTime;
            }
            else
            {
                status.SUN -= status.Damage;
                damageInterval = status.SUN < status.MaxSUN / sunThreshold ? status.DamageInterval_Fast : status.DamageInterval_Normal;
            }
        }
        else
        {
            damageInterval = status.SUN < status.MaxSUN / sunThreshold ? status.DamageInterval_Fast : status.DamageInterval_Normal;
        }
    }

    

    // 死亡時アニメーション
    private void PlayerDead()
    {
        if (transform.position.y >= -0.25f)
        {
            transform.Translate(new Vector3(0.0f, -0.05f, 0.0f));
        }
        else
        {
            player.IsDead = true;
        }
        
    }
}
