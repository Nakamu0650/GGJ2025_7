using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] PlayerSound playerSound;
    [SerializeField] PlayerData status;
    [SerializeField] float sunThreshold = 4.0f;

    private float damageInterval;
    private float healInterval;

    private bool activeMove;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        status.SAN = status.MaxSAN;
        status.Stamina = status.MaxStamina;

        damageInterval = status.DamageInterval_Normal;
        healInterval = 3.0f;

        activeMove = true;
        animator = GetComponent<Animator>();
        animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Player dead
        if (player.IsDead) return;

        if (status.SAN <= 0.0f && !player.IsDead)
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
            activeMove = false;
        }
        else if (status.Stamina > status.MaxStamina)
        {
            status.Stamina = status.MaxStamina;
        }

        if (!activeMove && status.Stamina <= 300)
        {
            player.ActiveMove = false;
        }
        else if (!activeMove && status.Stamina > 300)
        {
            player.ActiveMove = true;
            activeMove = true;
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
                status.SAN -= status.Damage;
                playerSound.damage.Play();
                damageInterval = status.SAN < status.MaxSAN / sunThreshold ? status.DamageInterval_Fast : status.DamageInterval_Normal;
            }
            healInterval = 3.0f;
        }
        else
        {
            if (healInterval >= 0.0f)
            {
                healInterval -= Time.deltaTime;
            }
            else
            {
                status.SAN += 10.0f;
                healInterval = 3.0f;
            }
            damageInterval = status.SAN < status.MaxSAN / sunThreshold ? status.DamageInterval_Fast : status.DamageInterval_Normal;
        }
    }

    

    // 死亡時アニメーション
    private void PlayerDead()
    {
        /*
        if (transform.position.y >= -0.25f)
        {
            transform.Translate(new Vector3(0.0f, -0.05f, 0.0f));
        }
        else
        {
            player.IsDead = true;
        }
        */
        animator.SetBool("isDead", true);
        player.IsDead = true;
    }
}
