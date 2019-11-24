using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        CarController.Instance.OnButtonRightClickDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CarController.Instance.OnButtonRightClickUp();
    }
}
