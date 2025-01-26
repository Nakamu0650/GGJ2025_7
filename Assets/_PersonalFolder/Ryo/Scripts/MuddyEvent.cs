using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MuddyEvent : MonoBehaviour
{
    private PlayerMove player;
    [SerializeField] AudioSource walk;
    [SerializeField] AudioSource dash;
    [SerializeField] AudioClip mud_walk;
    [SerializeField] AudioClip mud_dash;
    [SerializeField] AudioClip solid_walk;
    [SerializeField] AudioClip solid_dash;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("mud");
            player.IsSlow = true;
            walk.clip = mud_walk;
            dash.clip = mud_dash;
            player.IsChangeClip = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.IsSlow = false;
            walk.clip = solid_walk;
            dash.clip = solid_dash;
            player.IsChangeClip = true;
        }
    }
}
