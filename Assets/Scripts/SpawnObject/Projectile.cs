using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private IDamageable targetScript;
    private float attack;
    private Vector3 targetPos;

    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        targetPos = GetAimLocation();
        transform.LookAt(targetPos);
    }

    private Vector3 GetAimLocation()
    {
        if (!target.TryGetComponent(out CapsuleCollider targetCollider))
        {
            return target.transform.position;
        }
        return target.transform.position + 0.2f * targetCollider.height * Vector3.up;
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