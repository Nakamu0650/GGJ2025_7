using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerMove player;
    [SerializeField] private PlayerData playerData;

    [SerializeField] private AudioSource walk;
    [SerializeField] private AudioSource damage;
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
        /*
        if (player.IsDamage && canDamagePlay)
        {
            damage.Play();
            canDamagePlay = false;
        }
        else if (!player.IsDamage && !canDamagePlay)
        {
            canDamagePlay = true;
            damage.Stop();
        }*/
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
