using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] PlayerData status;
    public bool IsDash { get => isDash; set => isDash = value; }
    public bool IsWalk { get => isWalk; set => isWalk = value; }
    public bool IsMove { get => isMove; set => isMove = value; }
    public bool CanDash { get => canDash; set => canDash = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool IsDamage { get => isDamage; set => isDamage = value; }


    private bool isDash;
    private bool isWalk;
    private bool isMove;
    private bool canDash;
    private bool isDead;
    private bool isDamage;

    private float _moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        isDash = false;
        isMove = false;
        canDash = true;
        isDead = false;
        isDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead) return;
        Move();
        //Debug.Log(isDamage);
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            _moveSpeed = status.DashSpeed;
            isDash = true;
            isWalk = false;
        }
        else
        {
            _moveSpeed = status.Speed;
            isDash = false;
            isWalk = true;
        }

        isMove = false;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, _moveSpeed);
            isMove = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0.0f, 0.0f, _moveSpeed);
            isMove = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(_moveSpeed, 0.0f, 0.0f);
            isMove = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(_moveSpeed, 0.0f, 0.0f);
            isMove = true;
        }
    }
}
