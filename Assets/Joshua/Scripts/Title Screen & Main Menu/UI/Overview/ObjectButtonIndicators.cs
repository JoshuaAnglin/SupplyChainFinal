using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectButtonIndicators : MonoBehaviour
{
    [SerializeField] Transform lockedOnObject;

    RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        var screenLocation = Camera.main.WorldToScreenPoint(lockedOnObject.position);
        rt.position = screenLocation;

        Vector3 center = lockedOnObject.GetComponent<Renderer>().bounds.center;
        var viewportLocation = Camera.main.WorldToViewportPoint(center);
    }
}