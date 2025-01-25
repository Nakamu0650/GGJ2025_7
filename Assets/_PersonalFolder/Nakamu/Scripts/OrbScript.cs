using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (OrbCollider))]
public class OrbScript : MonoBehaviour
{
    [SerializeField] public OrbData orbData; //�I�[�u�̃p�����[�^
    OrbCollider orbCol;

    [Header("�_���[�W�p�p�����[�^")]
    [SerializeField] public float damageCoolTime = 1.0f; //�_���[�W�̃C���^�[�o��
    private float nowDamageCooltime;

    [Header("�񕜗p�p�����[�^")]
    [SerializeField] public float healCoolTime = 1.0f;
    private float nowHealCoolTime;

    [Header("Scale Parameters")]
    [Tooltip("HP�ő厞�̑傫��")]
    public Vector3 maxScale; // �����X�P�[��

    [Tooltip("HP�ŏ����̑傫��")]
    public Vector3 minScale = Vector3.zero; // �ŏ��X�P�[��

    /// <summary>
    /// �I�[�u��HP
    /// </summary>
    public int HP 
    { 
        get => hp;
        set {
            if (hp > orbData.maxHP)
                hp = orbData.maxHP;
            else if (hp <= orbData.maxHP)
                hp = value;
            else if (hp < 0)
                hp = 0;
        }
    }
    private int hp;

    void Start()
    {
        HP = orbData.maxHP;
        orbCol = GetComponent<OrbCollider>();
        nowDamageCooltime = 0.0f;
        maxScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        if (orbCol.isDamage)
        {
            if (nowDamageCooltime < 0.0)
            {
                OnDamage(orbData.DamageValue);
                UpdateScale();
                nowDamageCooltime = damageCoolTime;
            }
            else
            {
                nowDamageCooltime -= Time.fixedDeltaTime / orbData.DecreaseHpSpeed;
            }
        }
        else
        {
            nowDamageCooltime = 0.0f;


        }

        if (HP <= 0)
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// �I�[�u�̑ϋv����
    /// </summary>
    /// <param name="_damage"></param>
    /// <returns></returns>
    public int OnDamage(int _damage)
    {
        return HP = Mathf.Min((HP - _damage), orbData.maxHP);
    }

    /// <summary>
    /// �I�[�u�̑ϋv��
    /// </summary>
    /// <param name="_heal"></param>
    /// <returns></returns>
    public int OnHeal(int _heal)
    {
        return HP = Mathf.Max((HP + _heal), 0);
    }
    
    /// <summary>
    /// �I�[�u�̑傫���X�V���\�b�h
    /// </summary>
    public void UpdateScale()
    {
        float hpAmount = HpAmount();
        transform.localScale = Vector3.Lerp(maxScale, minScale, hpAmount);
    }

    /// <summary>
    /// HP�̊����Z�o���\�b�h
    /// </summary>
    /// <returns></returns>
    public float HpAmount()
    {
        return HP / orbData.maxHP;
    }
}
