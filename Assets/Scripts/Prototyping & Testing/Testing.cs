using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Testing : MonoBehaviour 
{
    private Camera mainCamera;

    [SerializeField] private float attackAmount;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (GameplaySystem.Instance.GetCurrentState() == GameState.Battle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(ray, out RaycastHit hitInfo);

                if (hitInfo.transform.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    print(damageable.GetTransform().name);
                    damageable.GetDamage(attackAmount);
                }
            }
        }
    }
}