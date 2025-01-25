using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbData", menuName = "ScriptableObjects/CreateOrbAsset")]
public class OrbData : ScriptableObject
{
    public int maxHP{ get => maxHp; private set => maxHp = value; }

    public int DamageValue { get => damageValue; private set => damageValue = value; }
    public float DecreaseHpSpeed{ get => decreaseHpSpeed; set => decreaseHpSpeed = value; }

    public int HealValue { get => healValue; private set => healValue = value; }
    public float IncreaseHpSpeed { get => IncreaseHpSpeed; set => IncreaseHpSpeed = value; }


    [SerializeField] private int maxHp = 100;
    [SerializeField] private int damageValue = 5;
    [SerializeField] private int healValue = 5;
    [SerializeField] private float decreaseHpSpeed = 2f;
    [SerializeField] private float increaseHpSpeed = 4f;
}
