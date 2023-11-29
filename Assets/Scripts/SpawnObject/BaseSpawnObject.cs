using System.Collections;
using UnityEngine;


public abstract class BaseSpawnObject : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attack;
    [SerializeField] protected MeshRenderer[] objectFlag;
}
