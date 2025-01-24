using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] PlayerData status;
    private float _moveSpeed;

    private float damageInterval;

    // Start is called before the first frame update
    void Start()
    {
        status.SUN = status.MaxSUN;
        status.Stamina = status.MaxStamina;

        damageInterval = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Player dead
        if (player.IsDead) return;

        if (status.SUN <= 0 && !player.IsDead)
        {
            PlayerDead();
        }


        // Player stamina
        if (player.IsMove)
        {
            status.Stamina += 1.0f;
        }
        else
        {
            status.Stamina += 10.0f;
        }

        if (player.IsMove && player.IsDash)
        {
            status.Stamina -= 10.0f;
        }

        if (status.Stamina < 0)
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
        if (!player.CanDash && status.Stamina >= 60)
        {
            player.CanDash = true;
        }


        // Player Damage
        if (player.IsDamage)
        {
            if (damageInterval >= 0)
            {
                damageInterval -= Time.deltaTime;
            }
            else
            {
                status.SUN -= 10;
                damageInterval = status.SUN < status.MaxSUN / 4.0f ? 2.0f : 3.0f;
            }
        }
        else
        {
            damageInterval = status.SUN < status.MaxSUN / 4.0f ? 2.0f : 3.0f;
        }
    }

    


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
