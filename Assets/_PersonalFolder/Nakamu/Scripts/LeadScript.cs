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
        [Header("オーブパラメータ")]
        [Tooltip("移動スピード")][SerializeField]
        private float moveSpeed = 1.0f; //先導スピード

        [Tooltip("方向回転スピード")]
        [SerializeField]
        private float changeAxisSpeed = 2.0f; //先導スピード

        [Tooltip("最大加速度")]
        [SerializeField]
        private float maxAccel = 1.5f; //最大加速度

        [Tooltip("等倍")]
        [SerializeField]
        private float  midAccel= 1.0f; //等倍

        [Tooltip("最低加速度")]
        [SerializeField]
        private float minAccel = 0.5f; //先導スピード

        [SerializeField] private Dictionary<int, float> accel;

        [Tooltip("先導経路データ")]
        [Header("経路コース配列"), SerializeField]
        public RouteSettings[] routeSettings;
        
        [SerializeField] private RoutePointer routePointer;//先導経路管理クラス

        [HideInInspector] public Vector3 baseVelocity; //オーブ本体のベクトル

        [HideInInspector] public Vector3 moveDirection = Vector3.zero;

        private bool isSelect = false;
        private bool isStopped = false;

        //[SerializeField] public float floatingeSpeed = 1.0f;
        void Awake()
        {
            if(routeSettings == null || routeSettings.Length == 0)
            {
                Debug.LogWarning("正しくrouteSettingsが設定されていません。nullです。");
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

            //経路決定
            if (!isSelect)
            {
                //ヌルチェック
                if (CheckMovePoint())
                {
                    AddAccel();
                    SelectRoute();//経路データのIDから1つの経路を選択
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
                    Debug.Log("ゴールまでたどり着きました。");
                }
            }
        }
        //ベクトルやもろもろ決めたら、velocityを変化
        void LateUpdate()
        {
            rb.velocity = baseVelocity;
        }

        /// <summary>
        /// 移動メソッド。滑らかに回転をさせる
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
        /// Dictionaryのヌルチェック
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
        /// 経路のランダム選択メソッド
        /// </summary>
        private void SelectRoute()
        {
            if (routeSettings.Length == 0) return;

            int routeNumber = routePointer.routeRandom.Next(routeSettings.Length);
            routePointer.routeID = routeNumber;
            Debug.Log($"経路{routePointer.routeID}に決定されました。");
        }

        /// <summary>
        /// 決定したrouteIDからrouteData内の先導ルートを連想配列に格納するメソッド
        /// </summary>
        public void AddMovePoint()
        {
            if (routePointer.routeID < 0 || routePointer.routeID > routeSettings.Length)
            {
                Debug.LogWarning("有効なrouteIDが選択されていません。");
                return;
            }

            //ヌルチェック
            if (routeSettings[routePointer.routeID].MovePoint == null || routeSettings[routePointer.routeID].MovePoint.Length == 0)
            {
                Debug.LogWarning("MovePointが正しく設定されていません。");
                return;
            }

            for (int i = 0; i < routeSettings[routePointer.routeID].MovePoint.Length; i++)
            {
                if (!routePointer.movePointer.ContainsKey(i))
                {
                    routePointer.movePointer.Add(i, routeSettings[routePointer.routeID].MovePoint[i]);
                    Debug.Log($"正常に{i}の地点が追加されました。: {routeSettings[routePointer.routeID].MovePoint[i].name}");
                }
            }
        }

        /// <summary>
        /// 加速度を連想配列に追加
        /// </summary>
        public void AddAccel()
        {
            accel.Add(0, minAccel);
            accel.Add(1, midAccel);
            accel.Add(2, maxAccel);
            Debug.Log($"正常に加速度が追加されました。");
        }

        /// <summary>
        /// ランダム加速度選択メソッド
        /// </summary>
        private void SelectAccel()
        {
            if (accel.Count == 0) return;
            int accelNumber = routePointer.accelRandom.Next(accel.Count);
            routePointer.accelID = accelNumber;
            Debug.Log($"加速度：{routePointer.accelID}に決定されました。");
        }

        /// <summary>
        /// 先導経路管理クラス
        /// </summary>
        public class RoutePointer
        {
            [HideInInspector] public Dictionary<int, Transform> movePointer; //選択経路管理変数<pointID, Transform>

            public System.Random routeRandom; //ルート選出用ランダム変数
            public System.Random accelRandom; //加速度選出用ランダム変数
            public int routeID = -1; //経路ID用変数(初期値_-1)
            public int accelID = -1; //加速度ID用変数(初期値_-1)
            public int leadCount;
        }

        [System.Serializable]
        public class RouteSettings
        {
            [Header("先導経路配列")]
            [SerializeField] private Transform[] movePoint; //リードポイント格納変数
            public Transform[] MovePoint { get => movePoint; private set => movePoint = value; }

        }
    }
}
