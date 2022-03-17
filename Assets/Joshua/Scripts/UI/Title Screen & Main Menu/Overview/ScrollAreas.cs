using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollAreas : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int direction;

    bool mouseOverArea;

    void Update()
    {
        if (mouseOverArea) GameEventSystem.GES.CameraTurn(direction);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOverArea = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOverArea = false;
    }
}