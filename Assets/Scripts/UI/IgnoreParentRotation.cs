using UnityEngine;

public class IgnoreParentRotation : MonoBehaviour
{
    private Transform canvasTransform;
    private Quaternion initialRotation;

    void Start()
    {
        canvasTransform = GetComponent<RectTransform>();

        if (transform.parent.parent.eulerAngles == Vector3.zero)
        {
            initialRotation = canvasTransform.rotation;
        }
        else
        {
            initialRotation = Quaternion.Euler(canvasTransform.rotation.x + 90, canvasTransform.rotation.y, canvasTransform.rotation.z);
        }
    }

    void LateUpdate()
    {
        canvasTransform.rotation = initialRotation;
    }
}

