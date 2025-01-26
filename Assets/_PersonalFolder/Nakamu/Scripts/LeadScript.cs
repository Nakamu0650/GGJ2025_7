using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;


namespace Nakamu
{
    [RequireComponent(typeof(Rigidbody))]
    public class LeadScript : MonoBehaviour
    {
        Rigidbody rb;
        [Header("�I�[�u�p�����[�^")]
        [Tooltip("�ړ��X�s�[�h")][SerializeField]
        private float moveSpeed = 1.0f; //�擱�X�s�[�h

        [Tooltip("������]�X�s�[�h")]
        [SerializeField]
        private float changeAxisSpeed = 2.0f; //�擱�X�s�[�h

        [Tooltip("�ő�����x")]
        [SerializeField]
        private float maxAccel = 1.5f; //�ő�����x

        [Tooltip("���{")]
        [SerializeField]
        private float  midAccel= 1.0f; //���{

        [Tooltip("�Œ�����x")]
        [SerializeField]
        private float minAccel = 0.5f; //�擱�X�s�[�h

        [SerializeField] private Dictionary<int, float> accel;

        [Tooltip("�擱�o�H�f�[�^")]
        [Header("�o�H�R�[�X�z��"), SerializeField]
        public RouteSettings[] routeSettings;
        
        [SerializeField] private RoutePointer routePointer;//�擱�o�H�Ǘ��N���X

        [HideInInspector] public Vector3 baseVelocity; //�I�[�u�{�̂̃x�N�g��

        [HideInInspector] public Vector3 moveDirection = Vector3.zero;

        private bool isSelect = false;
        private bool isStopped = false;

        //[SerializeField] public float floatingeSpeed = 1.0f;
        void Awake()
        {
            if(routeSettings == null || routeSettings.Length == 0)
            {
                Debug.LogWarning("������routeSettings���ݒ肳��Ă��܂���Bnull�ł��B");
            }

            if (routePointer == null)
            {
                routePointer = new RoutePointer();
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            routePointer.routeRandom = new System.Random();
            routePointer.accelRandom = new System.Random();
            rb = GetComponent<Rigidbody>();
            baseVelocity = Vector3.zero;
            routePointer.leadCount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (transform.position.y <= 2.0f)
            {
                baseVelocity.y += Time.fixedDeltaTime * floatingeSpeed;
            }else if (transform.position.y > 2.0f)
            {
                baseVelocity.y -= Time.fixedDeltaTime * floatingeSpeed;
            }*/

            //�o�H����
            if (!isSelect)
            {
                //�k���`�F�b�N
                if (CheckMovePoint())
                {
                    AddAccel();
                    SelectRoute();//�o�H�f�[�^��ID����1�̌o�H��I��
                    AddMovePoint();
                    isSelect = true;
                }
            }

            if (routePointer.routeID >= 0 && !isStopped)
            {
                if (routePointer.leadCount < routePointer.movePointer.Count)
                {
                    Transform currentTarget = routePointer.movePointer[routePointer.leadCount];
                    moveDirection = (currentTarget.transform.position - transform.position).normalized;

                    SelectAccel();
                    Move(moveSpeed, new Vector2(moveDirection.x,moveDirection.z));

                    float distance = Vector3.Distance(transform.position, currentTarget.position);
                    if (distance < 1.0f)
                    {
                        routePointer.leadCount++;
                        Debug.Log($"{routePointer.leadCount}");
                    }

                }
                else
                {
                    rb.velocity = Vector3.zero;
                    baseVelocity = Vector3.zero;

                    isStopped = true;
                    Debug.Log("�S�[���܂ł��ǂ蒅���܂����B");
                }
            }
        }
        //�x�N�g���������댈�߂���Avelocity��ω�
        void LateUpdate()
        {
            rb.velocity = baseVelocity;
        }

        /// <summary>
        /// �ړ����\�b�h�B���炩�ɉ�]��������
        /// </summary>
        /// <param name="_speed"></param>
        /// <param name="_direction"></param>
        private void Move(float _speed, Vector2 _direction)
        {
            
            var legacyAxis = new Vector2(baseVelocity.x, baseVelocity.z);
            Vector2 newAxis = _direction * _speed * accel[routePointer.accelID];

            float value = Mathf.Clamp01(velocitySimilar(legacyAxis, newAxis) + changeAxisSpeed * Time.fixedDeltaTime);
            Vector2 velocity = legacyAxis * (1f - value) + newAxis * value;
            baseVelocity = new Vector3(velocity.x, baseVelocity.y, velocity.y);
        }

        private float velocitySimilar(Vector2 v1, Vector2 v2)
        {
            float dot;
            dot = Vector2.Dot(v1, v2);
            return Mathf.Clamp01((1f + dot) / 2f);

        }

        /// <summary>
        /// Dictionary�̃k���`�F�b�N
        /// </summary>
        /// <returns></returns>
        private bool CheckMovePoint()
        {
            if (routePointer.movePointer == null)
            {
                routePointer.movePointer = new Dictionary<int, Transform>();
                accel = new Dictionary<int, float>();
                return false;
            }
            return true;
        }

        /// <summary>
        /// �o�H�̃����_���I�����\�b�h
        /// </summary>
        private void SelectRoute()
        {
            if (routeSettings.Length == 0) return;

            int routeNumber = routePointer.routeRandom.Next(routeSettings.Length);
            routePointer.routeID = routeNumber;
            Debug.Log($"�o�H{routePointer.routeID}�Ɍ��肳��܂����B");
        }

        /// <summary>
        /// ���肵��routeID����routeData���̐擱���[�g��A�z�z��Ɋi�[���郁�\�b�h
        /// </summary>
        public void AddMovePoint()
        {
            if (routePointer.routeID < 0 || routePointer.routeID > routeSettings.Length)
            {
                Debug.LogWarning("�L����routeID���I������Ă��܂���B");
                return;
            }

            //�k���`�F�b�N
            if (routeSettings[routePointer.routeID].MovePoint == null || routeSettings[routePointer.routeID].MovePoint.Length == 0)
            {
                Debug.LogWarning("MovePoint���������ݒ肳��Ă��܂���B");
                return;
            }

            for (int i = 0; i < routeSettings[routePointer.routeID].MovePoint.Length; i++)
            {
                if (!routePointer.movePointer.ContainsKey(i))
                {
                    routePointer.movePointer.Add(i, routeSettings[routePointer.routeID].MovePoint[i]);
                    Debug.Log($"�����{i}�̒n�_���ǉ�����܂����B: {routeSettings[routePointer.routeID].MovePoint[i].name}");
                }
            }
        }

        /// <summary>
        /// �����x��A�z�z��ɒǉ�
        /// </summary>
        public void AddAccel()
        {
            accel.Add(0, minAccel);
            accel.Add(1, midAccel);
            accel.Add(2, maxAccel);
            Debug.Log($"����ɉ����x���ǉ�����܂����B");
        }

        /// <summary>
        /// �����_�������x�I�����\�b�h
        /// </summary>
        private void SelectAccel()
        {
            if (accel.Count == 0) return;
            int accelNumber = routePointer.accelRandom.Next(accel.Count);
            routePointer.accelID = accelNumber;
            Debug.Log($"�����x�F{routePointer.accelID}�Ɍ��肳��܂����B");
        }

        /// <summary>
        /// �擱�o�H�Ǘ��N���X
        /// </summary>
        public class RoutePointer
        {
            [HideInInspector] public Dictionary<int, Transform> movePointer; //�I���o�H�Ǘ��ϐ�<pointID, Transform>

            public System.Random routeRandom; //���[�g�I�o�p�����_���ϐ�
            public System.Random accelRandom; //�����x�I�o�p�����_���ϐ�
            public int routeID = -1; //�o�HID�p�ϐ�(�����l_-1)
            public int accelID = -1; //�����xID�p�ϐ�(�����l_-1)
            public int leadCount;
        }

        [System.Serializable]
        public class RouteSettings
        {
            [Header("�擱�o�H�z��")]
            [SerializeField] private Transform[] movePoint; //���[�h�|�C���g�i�[�ϐ�
            public Transform[] MovePoint { get => movePoint; private set => movePoint = value; }

        }
    }
}
