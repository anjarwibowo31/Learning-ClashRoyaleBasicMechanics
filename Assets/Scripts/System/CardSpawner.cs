using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask spawnArea;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, spawnArea);

            if (hitInfo.transform.CompareTag("DropArea"))
            {
                print(hitInfo.point);
            }
        }
    }
}