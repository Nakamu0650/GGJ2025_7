using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    [SerializeField] public OrbData orbData; //オーブのパラメータ
    [SerializeField] public OrbCollider orbCol; //オーブのコライダー
    [SerializeField] public GameObject orbPrefab;

    [Header("ダメージ用パラメータ")]
    [SerializeField] public float damageCoolTime = 1.0f; //ダメージのインターバル
    private float nowDamageCooltime = 0.0f;

    [Header("回復用パラメータ")]
    [SerializeField] public float healCoolTime = 1.0f;
    private float nowHealCoolTime = 0.0f;

    [Tooltip("HP最大時の大きさ")]
    [HideInInspector] public Vector3 maxScale; // 初期スケール

    [Tooltip("HP最小時の大きさ")]
    [HideInInspector] public Vector3 minScale = new Vector3(0, 0, 0); // 最小スケール

    /// <summary>
    /// オーブのHP
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
        
        //ダメージ処理と回復処理
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
    /// オーブの耐久減少
    /// </summary>
    /// <param name="_damage"></param>
    /// <returns></returns>
    public int OnDamage(int _damage)
    {
        return HP = Mathf.Min((HP - _damage), orbData.maxHP);
    }

    /// <summary>
    /// オーブの耐久回復
    /// </summary>
    /// <param name="_heal"></param>
    /// <returns></returns>
    public int OnHeal(int _heal)
    {
        return HP = Mathf.Max((HP + _heal), 0);
    }
    
    /// <summary>
    /// オーブの大きさ更新メソッド
    /// </summary>
    public void UpdateScale()
    {
        float hpAmount = HpAmount();
        transform.localScale = Vector3.Lerp(minScale,maxScale, hpAmount);
    }

    /// <summary>
    /// HPの割合算出メソッド
    /// </summary>
    /// <returns></returns>
    public float HpAmount()
    {
        return (float)HP / orbData.maxHP;
    }
}
