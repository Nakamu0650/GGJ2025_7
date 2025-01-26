using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    [SerializeField] public OrbData orbData; //�I�[�u�̃p�����[�^
    [SerializeField] public OrbCollider orbCol; //�I�[�u�̃R���C�_�[
    [SerializeField] public GameObject orbPrefab;

    [Header("�_���[�W�p�p�����[�^")]
    [SerializeField] public float damageCoolTime = 1.0f; //�_���[�W�̃C���^�[�o��
    private float nowDamageCooltime = 0.0f;

    [Header("�񕜗p�p�����[�^")]
    [SerializeField] public float healCoolTime = 1.0f;
    private float nowHealCoolTime = 0.0f;

    [Tooltip("HP�ő厞�̑傫��")]
    [HideInInspector] public Vector3 maxScale; // �����X�P�[��

    [Tooltip("HP�ŏ����̑傫��")]
    [HideInInspector] public Vector3 minScale = new Vector3(0, 0, 0); // �ŏ��X�P�[��

    /// <summary>
    /// �I�[�u��HP
    /// </summary>
    public int HP 
    { 
        get => hp;
        set { hp = Mathf.Clamp(value, 0, orbData.maxHP); }
    }
    private int hp;

    void Start()
    {
        HP = orbData.maxHP;
        nowDamageCooltime = 0.0f;
        nowHealCoolTime = 0.0f;
        maxScale = transform.localScale;
    }

    void Update()
    {
        UpdateScale();
        
        //�_���[�W�����Ɖ񕜏���
        if (orbCol.isDamage)
        {
            nowHealCoolTime = 0.0f;
            if (nowDamageCooltime < 0.0)
            {
                OnDamage(orbData.DamageValue);
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

            if (HP < orbData.maxHP)
            {
                if (nowHealCoolTime < 0.0)
                {
                    OnHeal(orbData.HealValue);
                    nowHealCoolTime = healCoolTime;
                }
                else
                {
                    nowHealCoolTime -= Time.fixedDeltaTime / orbData.IncreaseHpSpeed;
                }
            }
        }

        if (HP == 0)
        {
            Destroy(orbPrefab);
            orbPrefab.GetComponent<OrbScript>().enabled = false;
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
        transform.localScale = Vector3.Lerp(minScale,maxScale, hpAmount);
    }

    /// <summary>
    /// HP�̊����Z�o���\�b�h
    /// </summary>
    /// <returns></returns>
    public float HpAmount()
    {
        return (float)HP / orbData.maxHP;
    }
}
