using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScritableObjects/Player")]
public class PlayerData : ScriptableObject
{
    public float SAN { get => _san; set => _san = value; }
    public float Stamina { get => _stamina; set => _stamina = value; }
    public float MaxSAN { get => _maxSAN; private set => _maxSAN = value; }
    public float MaxStamina { get => _maxStamina; private set => _maxStamina = value; }
    public float Speed { get =>  _speed; private set => _speed = value;}
    public float DashSpeed { get => _dashSpeed; private set => _dashSpeed = value; }
    public float HealStamina_Wait { get => _healStamina_Wait; private set => _healStamina_Wait = value; }
    public float HealStamina_Walk { get => _healStamina_Walk; private set => _healStamina_Walk = value; }
    public float DamageInterval_Normal { get => _damageInterval_Normal; private set => _damageInterval_Normal = value; }
    public float DamageInterval_Fast { get => _damageInterval_Fast; private set => _damageInterval_Fast = value; }
    public float StaminaCost { get => _staminaCost; private set => _staminaCost = value;}
    public float MinDashCost { get => _minDashCost; private set => _minDashCost = value; }  
    public float Damage { get => _damage; private set => _damage = value; }



    //[SerializeField] float _hp;
    [Header("現在のSUN値")]
    [SerializeField] float _san;
    [Header("現在のスタミナ")]
    [SerializeField] float _stamina;
    [Header("最大SUN値")]
    [SerializeField] float _maxSAN;
    [Header("最大スタミナ")]
    [SerializeField] float _maxStamina;
    [Header("常時移動")]
    [SerializeField] float _speed;
    [Header("ダッシュ")]
    [SerializeField] float _dashSpeed;
    [Header("スタミナ回復(待機時)")]
    [SerializeField] float _healStamina_Wait;
    [Header("スタミナ回復(通常移動)")]
    [SerializeField] float _healStamina_Walk;
    [Header("ダメージ頻度(通常)")]
    [SerializeField] float _damageInterval_Normal;
    [Header("ダメージ頻度(高速)")]
    [SerializeField] float _damageInterval_Fast;
    [Header("スタミナ消費コスト")]
    [SerializeField] float _staminaCost;
    [Header("ダッシュ最小コスト")]
    [SerializeField] float _minDashCost;
    [Header("被ダメージ量")]
    [SerializeField] float _damage;
    
}
