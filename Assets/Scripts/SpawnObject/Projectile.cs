using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private IDamageable targetScript;
    private float attack;
    private Vector3 targetPos;

    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 sphereCastThreshold;
    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }

        targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(targetPos);
    }

    private void Update()
    {
        if (targetScript.Health <= 0 || target == null)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable component) && component == targetScript)
        {
            targetScript.GetDamage(attack);
            Destroy(gameObject);
        }
    }

    public void SetProjectile(Transform target, IDamageable damageable, float attack)
    {
        this.targetScript = damageable;
        this.target = target;
        this.attack = attack;
    }
}