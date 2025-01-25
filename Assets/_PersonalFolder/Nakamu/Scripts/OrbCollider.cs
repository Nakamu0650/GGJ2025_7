using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollider : MonoBehaviour
{

    [HideInInspector] public bool isDamage = false;
    [HideInInspector] public Transform player;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isDamage = true;
            player = other.transform;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDamage = false;
        }
    }
}
