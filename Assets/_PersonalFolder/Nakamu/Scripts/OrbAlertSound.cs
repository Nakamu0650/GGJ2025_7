using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbAlertSound : MonoBehaviour
{
    [SerializeField] public AudioSource orbAlert; // AudioSourceにあるOrbAlertのファイルを格納

    [SerializeField] public OrbCollider orbCol;　 // Playerとの当たり判定

    private float maxDistance = 3.53f; //ダメージエリアとの境界距離_当たり判定スペースの半径に合わせて変更
    private float distance; //オーブとPlayerとの距離の格納用変数

    private void Update()
    {
        //ダメージ圏内でOrbAlertの音が発生、かつオーブとPlayerとの距離に応じて音の大きさが変化
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
