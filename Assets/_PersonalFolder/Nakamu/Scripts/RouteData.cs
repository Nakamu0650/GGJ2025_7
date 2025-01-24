using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RouteData", menuName = "ScriptableObjects/CreateRouteDataAsset")]
public class RouteData : ScriptableObject
{
    public Transform[] MovePoint{ get => movePoint; private set => movePoint = value; }

    [SerializeField] private Transform[] movePoint; //リードポイント格納変数
}
