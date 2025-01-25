using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbAlertSound : MonoBehaviour
{
    [SerializeField] public AudioSource orbAlert; // AudioSource�ɂ���OrbAlert�̃t�@�C�����i�[

    [SerializeField] public OrbCollider orbCol;�@ // Player�Ƃ̓����蔻��

    private float maxDistance = 3.53f; //�_���[�W�G���A�Ƃ̋��E����_�����蔻��X�y�[�X�̔��a�ɍ��킹�ĕύX
    private float distance; //�I�[�u��Player�Ƃ̋����̊i�[�p�ϐ�

    private void Update()
    {
        //�_���[�W������OrbAlert�̉��������A���I�[�u��Player�Ƃ̋����ɉ����ĉ��̑傫�����ω�
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
