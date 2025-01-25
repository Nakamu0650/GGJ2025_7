using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MuddyEvent : MonoBehaviour
{
    private PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("mud");
            player.IsSlow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.IsSlow = false;
        }
    }
}
