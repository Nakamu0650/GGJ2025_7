using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbAlertSound : MonoBehaviour
{
    [SerializeField] public AudioSource orbAlert;

    [SerializeField] public OrbCollider orbCol;

    private float maxDistance = 3.53f; //スペースの半径に合わせて変化
    private float distance;

    private void Update()
    {
        if (orbCol.isDamage)
        {
            distance = Vector3.Distance(transform.position, orbCol.player.transform.position);
            orbAlert.Play();
            float volume = Mathf.Lerp(1, 0, DistanceAmount()); //線形補間で距離に応じて音の大きさが変動
            orbAlert.volume = volume;
        }
        else orbAlert.Stop();
    }

    private float DistanceAmount()
    {
        return distance / maxDistance;
    }
}
