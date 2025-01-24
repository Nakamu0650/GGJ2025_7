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

        [Tooltip("�擱�o�H�f�[�^")]
        [SerializeField]
        public RouteData[] routeData; //�ړ��o�H�f�[�^

        private RoutePointer routePointer;//�擱�o�H�Ǘ��N���X

        [HideInInspector] public Vector3 moveDirection = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            routePointer.routeRandom = new System.Random();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Move()
        {

        }
        /// <summary>
        /// �擱�o�H�Ǘ��N���X
        /// </summary>
        public class RoutePointer
        {
            [HideInInspector] public Dictionary<int, RouteData> routeDataDictionary; //�o�H�f�[�^�Ǘ��ϐ�<dataID, RouteData>
            [HideInInspector] public Dictionary<int, Transform> movePointer; //�I���o�H�Ǘ��ϐ�<pointID, Transform>

            public System.Random routeRandom; //���[�g�I�o�p�����_���ϐ�
            public int routeID = -1; //�o�HID�p�ϐ�(�����l_-1)
        }

        /// <summary>
        /// Dictionary�̃k���`�F�b�N
        /// </summary>
        /// <returns></returns>
        private bool CheckDictionaries()
        {
            if (routePointer.movePointer == null && routePointer.routeDataDictionary == null)
            {
                routePointer.routeDataDictionary = new Dictionary<int, RouteData>();
                routePointer.movePointer = new Dictionary<int, Transform>();
                return false;
            }
            return true;
        }

        /// <summary>
        /// �C���X�y�N�^�[��ɂ���RouteData��A�z�z��Ɋi�[���郁�\�b�h
        /// </summary>
        private void AddRouteData()
        {
            if (routeData == null || routeData.Length == 0)
            {
                Debug.LogWarning("routeData���������ݒ肳��Ă��܂���B");
                return;
            }

            for (int i = 0; i < routeData.Length; i++)
            {
                if (!routePointer.routeDataDictionary.ContainsKey(i))
                {
                    routePointer.routeDataDictionary.Add(i, routeData[i]);
                    Debug.Log($"�����{i}�̌o�H���ǉ�����܂����B: {routeData[i].name}");
                }
            }
        }

        /// <summary>
        /// �o�H�̃����_���I�����\�b�h
        /// </summary>
        private void SelectRoute()
        {
            if (routePointer.routeDataDictionary.Count == 0) return;

            int routeNumber = routePointer.routeRandom.Next(routePointer.routeDataDictionary.Count);
            routePointer.routeID = routeNumber;
        }

        /// <summary>
        /// ���肵��routeID����routeData���̐擱���[�g��A�z�z��Ɋi�[���郁�\�b�h
        /// </summary>
        public void AddMovePoint()
        {
            //�k���`�F�b�N
            if (routeData[routePointer.routeID].MovePoint == null || routeData[routePointer.routeID].MovePoint.Length == 0)
            {
                Debug.LogWarning("MovePoint���������ݒ肳��Ă��܂���B");
                return;
            }

            for (int i = 0; i < routeData[routePointer.routeID].MovePoint.Length; i++)
            {
                if (!routePointer.movePointer.ContainsKey(i))
                {
                    routePointer.movePointer.Add(i, routeData[routePointer.routeID].MovePoint[i]);
                    Debug.Log($"�����{i}�̒n�_���ǉ�����܂����B: {routeData[routePointer.routeID].MovePoint[i].name}");
                }
            }
        }
    }
}
