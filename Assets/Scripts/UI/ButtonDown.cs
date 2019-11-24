using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        CarController.Instance.OnButtonDownClickDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CarController.Instance.OnButtonDownClickUp();
    }
}
