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
    public float Speed { get =>  _speed; set => _speed = value;}



    //[SerializeField] float _hp;
    [SerializeField] float _sun;
    [SerializeField] float _stamina;
    [SerializeField] float _maxSUN;
    [SerializeField] float _maxStamina;
    [SerializeField] float _speed;
}
