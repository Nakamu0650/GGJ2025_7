using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] PlayerData status;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public bool IsDash { get => isDash; set => isDash = value; }
    public bool IsWalk { get => isWalk; set => isWalk = value; }
    public bool IsMove { get => isMove; set => isMove = value; }
    public bool CanDash { get => canDash; set => canDash = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool IsDamage { get => isDamage; set => isDamage = value; }
    public bool ActiveMove { get => activeMove; set => activeMove = value; }
    public bool IsSlow { get => isSlow; set => isSlow = value; }


    private bool isDash;
    private bool isWalk;
    private bool isMove;
    private bool canDash;
    private bool isDead;
    private bool isDamage;
    private bool activeMove;
    private bool isSlow;

    private float _moveSpeed;

    private Rigidbody rb;
    private Animator animator;

    
    Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetBool("isWalk", false);
        animator.SetBool("isDash", false);

        currentPos = transform.position;

        isDash = false;
        isMove = false;
        canDash = true;
        isDead = false;
        isDamage = false;
        activeMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        //Move();
        MouseCtrl();
        //Debug.Log(isDamage);
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            _moveSpeed = status.DashSpeed;
            isDash = true;
            isWalk = false;
            animator.SetBool("isWalk", false);
            animator.SetBool("isDash", true);
        }
        else
        {
            _moveSpeed = status.Speed;
            isDash = false;
            isWalk = true;
            animator.SetBool("isWalk", true);
            animator.SetBool("isDash", false);
        }

        isMove = false;
        if (!activeMove) return;
        if (isSlow)
        {
            _moveSpeed /= 4.0f;
        }
        RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0.0f, 0.7f * transform.localScale.y, 0.0f), 0.1f, transform.forward, 0.5f);//Rayを飛ばし、ヒットしたオブジェクト群を配列に格納             
        hits = hits.OrderBy(hit => hit.distance).ToArray();//配列を距離ごとの昇順に並び替え
        Debug.DrawRay(transform.position + new Vector3(0.0f, 0.7f * transform.localScale.y, 0.0f), transform.forward * 0.5f, Color.green, 0.5f);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Stage"))
            {
                Debug.Log("stop");
                return;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += transform.forward * _moveSpeed;
            rb.MovePosition(transform.position + transform.forward * _moveSpeed);
            isMove = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.position -= transform.forward * _moveSpeed;
            rb.MovePosition(transform.position - transform.forward * _moveSpeed);
            isMove = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += transform.right * _moveSpeed;
            rb.MovePosition(transform.position + transform.right * _moveSpeed);
            isMove = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.position -= transform.right * _moveSpeed;
            rb.MovePosition(transform.position - transform.right * _moveSpeed);
            isMove = true;
        }
        if (!isMove)
        {
            rb.MovePosition(currentPos);
        }
        else
        {
            currentPos = transform.position;
        }
    }

    private void MouseCtrl()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        
        if (Mathf.Abs(mx) > 0.01f)
        {
            transform.RotateAround(transform.position, Vector3.up, mx * 2.0f);
        }
    }

    
}
