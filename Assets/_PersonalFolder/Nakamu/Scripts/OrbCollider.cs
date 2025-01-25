using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollider : MonoBehaviour
{

    [HideInInspector] public bool isDamage = false;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) isDamage = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isDamage = false;
    }
}
