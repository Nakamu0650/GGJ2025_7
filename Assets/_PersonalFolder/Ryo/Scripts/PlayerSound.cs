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

    private bool canWalkPlay;
    private bool canDamagePlay;
    private bool canhurtBeatPlay;
    private bool candashPlay;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        

        canWalkPlay = true;
        canDamagePlay = true;
        canhurtBeatPlay = true;
        candashPlay = true;
}

    // Update is called once per frame
    void Update()
    {
        if (player.IsMove && player.IsWalk && !player.IsDash && canWalkPlay)
        {
            walk.Play();
            canWalkPlay = false;
        }
        else if (!player.IsMove && !canWalkPlay)
        {
            Debug.Log("stop walk");
            walk.Stop();
            canWalkPlay = true;
        }

        if (player.IsMove && player.IsDash && candashPlay)
        {
            dash.Play();
            walk.Stop();
            candashPlay = false;
        }
        else if (!player.IsMove && !candashPlay || playerData.Stamina <= 0.0f)
        {
            Debug.Log("stop dash");
            dash.Stop();
            candashPlay = true;
            
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

        if (player.IsDamage && canhurtBeatPlay)
        {
            hurtBeat.Play();
            canhurtBeatPlay = false;
        }
        else if (!player.IsDamage && !canhurtBeatPlay)
        {
            hurtBeat.Stop();
            canhurtBeatPlay = true;
        }

        /*
        if (player.IsMove && canWalkPlay)
        {
            walk.Play();
            canWalkPlay = false;
        }
        else if (!player.IsMove && !canWalkPlay)
        {
            canWalkPlay = true;
            walk.Stop();
        }
        */
    }
}
