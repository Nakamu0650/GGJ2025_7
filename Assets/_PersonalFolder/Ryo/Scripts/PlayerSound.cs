using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerMove player;
    [SerializeField] private PlayerData playerData;

    public AudioSource damage;

    [SerializeField] private AudioSource walk;
    [SerializeField] private AudioSource hurtBeat;
    [SerializeField] private AudioSource dash;
    [SerializeField] private AudioSource lose;

    private bool canWalkPlay;
    private bool canDamagePlay;
    private bool canHurtBeatPlay;
    private bool canDashPlay;
    private bool canLosePlay;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        

        canWalkPlay = true;
        canDamagePlay = true;
        canHurtBeatPlay = true;
        canDashPlay = true;
        canLosePlay = true;

        animator = GetComponent<Animator>();
        animator.SetBool("isMove", false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"move:{player.IsMove}, cwalk:{canWalkPlay}, cdash:{canDashPlay}");

        
        if (player.IsMove && player.IsWalk && canWalkPlay)
        {
            Debug.Log("Play walk");
            animator.SetBool("isMove", true);
            walk.Play();
            dash.Stop();
            canDashPlay = true;
            canWalkPlay = false;
        }
        else if (!player.IsMove && !canWalkPlay)
        {
            Debug.Log("stop walk");
            animator.SetBool("isMove", false);
            walk.Stop();
            canWalkPlay = true;
        }

        if (player.IsMove && player.IsDash && canDashPlay)
        {
            animator.SetBool("isMove", true);
            dash.Play();
            walk.Stop();
            canWalkPlay = true;
            canDashPlay = false;
        }
        else if (!player.IsMove && !canDashPlay || playerData.Stamina <= 0.0f)
        {
            Debug.Log("stop dash");
            animator.SetBool("isMove", false);
            dash.Stop();
            canDashPlay = true;
            
        }
        

        if (playerData.SAN > playerData.MaxSAN / 2.0f)
        {
            damage.pitch = 1.0f;
            hurtBeat.pitch = 1.0f;
        }
        else if (playerData.SAN <= playerData.MaxSAN / 2.0f && playerData.SAN > playerData.MaxSAN / 4.0f)
        {
            damage.pitch = 1.2f;
            hurtBeat.pitch = 1.2f;
        }
        else if (playerData.SAN <= playerData.MaxSAN / 4.0f)
        {
            damage.pitch = 1.5f;
            hurtBeat.pitch = 1.5f;
        }

        if (player.IsDamage && canHurtBeatPlay)
        {
            hurtBeat.Play();
            canHurtBeatPlay = false;
        }
        else if (!player.IsDamage && !canHurtBeatPlay)
        {
            hurtBeat.Stop();
            canHurtBeatPlay = true;
        }

        
        if (player.IsDead && canLosePlay)
        {
            lose.Play();
            canLosePlay = false;
        }
        else if (!player.IsDead && !canLosePlay)
        {
            canLosePlay = true;
            lose.Stop();
        }
        
    }
}
