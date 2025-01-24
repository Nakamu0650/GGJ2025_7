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

        [Tooltip("先導経路データ")]
        [SerializeField]
        public RouteData[] routeData; //移動経路データ

        private RoutePointer routePointer;//先導経路管理クラス

        [HideInInspector] public Vector3 moveDirection = Vector3.zero;

        private bool isSelect = false;

        // Start is called before the first frame update
        void Start()
        {
            routePointer.routeRandom = new System.Random();
            rb = GetComponent<Rigidbody>();

        }

        // Update is called once per frame
        void Update()
        {
            //経路決定
            if (!isSelect)
            {
                //ヌルチェック
                if (CheckMovePoint())
                {
                    SelectRoute();//経路データのIDから1つの経路を選択
                    isSelect = true;
                }
            }

            if (routePointer.routeID >= 0)
            {

            }
        }

        private void Move()
        {

        }
        /// <summary>
        /// 先導経路管理クラス
        /// </summary>
        [System.Serializable]
        public class RoutePointer
        {
            [HideInInspector] public Dictionary<int, Transform> movePointer; //選択経路管理変数<pointID, Transform>

            public System.Random routeRandom; //ルート選出用ランダム変数
            public int routeID = -1; //経路ID用変数(初期値_-1)
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
                return false;
            }
            return true;
        }

        /// <summary>
        /// 経路のランダム選択メソッド
        /// </summary>
        private void SelectRoute()
        {
            if (routeData.Length == 0) return;

            int routeNumber = routePointer.routeRandom.Next(routeData.Length);
            routePointer.routeID = routeNumber;
        }

        /// <summary>
        /// 決定したrouteIDからrouteData内の先導ルートを連想配列に格納するメソッド
        /// </summary>
        public void AddMovePoint()
        {
            //ヌルチェック
            if (routeData[routePointer.routeID].MovePoint == null || routeData[routePointer.routeID].MovePoint.Length == 0)
            {
                Debug.LogWarning("MovePointが正しく設定されていません。");
                return;
            }

            for (int i = 0; i < routeData[routePointer.routeID].MovePoint.Length; i++)
            {
                if (!routePointer.movePointer.ContainsKey(i))
                {
                    routePointer.movePointer.Add(i, routeData[routePointer.routeID].MovePoint[i]);
                    Debug.Log($"正常に{i}の地点が追加されました。: {routeData[routePointer.routeID].MovePoint[i].name}");
                }
            }
        }
    }
}
