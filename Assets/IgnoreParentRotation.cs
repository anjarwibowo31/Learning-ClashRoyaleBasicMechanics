using UnityEngine;

public class IgnoreParentRotation : MonoBehaviour
{
    private Transform canvasTransform;
    private Transform parentTransform;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        canvasTransform = GetComponent<RectTransform>();
        parentTransform = transform.parent;

        //initialPosition = canvasTransform.position;
        initialRotation = canvasTransform.rotation;
    }

    void LateUpdate()
    {
        //canvasTransform.position = initialPosition;
        canvasTransform.rotation = initialRotation;
    }
}
