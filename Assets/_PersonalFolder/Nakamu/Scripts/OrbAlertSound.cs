using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbAlertSound : MonoBehaviour
{
    [SerializeField] public AudioSource orbAlert;

    [SerializeField] public OrbCollider orbCol;

    private float maxDistance = 3.53f; //�X�y�[�X�̔��a�ɍ��킹�ĕω�
    private float distance;

    private void Update()
    {
        if (orbCol.isDamage)
        {
            distance = Vector3.Distance(transform.position, orbCol.player.transform.position);
            orbAlert.Play();
            float volume = Mathf.Lerp(1, 0, DistanceAmount()); //���`��Ԃŋ����ɉ����ĉ��̑傫�����ϓ�
            orbAlert.volume = volume;
        }
        else orbAlert.Stop();
    }

    private float DistanceAmount()
    {
        return distance / maxDistance;
    }
}
