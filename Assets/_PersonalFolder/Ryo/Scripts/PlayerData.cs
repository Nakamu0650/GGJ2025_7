using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScritableObjects/Player")]
public class PlayerData : ScriptableObject
{
    public float SUN { get => _sun; set => _sun = value; }
    public float Stamina { get => _stamina; set => _stamina = value; }
    public float MaxSUN { get => _maxSUN; private set => _maxSUN = value; }
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
    [Header("���݂�SUN�l")]
    [SerializeField] float _sun;
    [Header("���݂̃X�^�~�i")]
    [SerializeField] float _stamina;
    [Header("�ő�SUN�l")]
    [SerializeField] float _maxSUN;
    [Header("�ő�X�^�~�i")]
    [SerializeField] float _maxStamina;
    [Header("�펞�ړ�")]
    [SerializeField] float _speed;
    [Header("�_�b�V��")]
    [SerializeField] float _dashSpeed;
    [Header("�X�^�~�i��(�ҋ@��)")]
    [SerializeField] float _healStamina_Wait;
    [Header("�X�^�~�i��(�ʏ�ړ�)")]
    [SerializeField] float _healStamina_Walk;
    [Header("�_���[�W�p�x(�ʏ�)")]
    [SerializeField] float _damageInterval_Normal;
    [Header("�_���[�W�p�x(����)")]
    [SerializeField] float _damageInterval_Fast;
    [Header("�X�^�~�i����R�X�g")]
    [SerializeField] float _staminaCost;
    [Header("�_�b�V���ŏ��R�X�g")]
    [SerializeField] float _minDashCost;
    [Header("��_���[�W��")]
    [SerializeField] float _damage;
    
}
