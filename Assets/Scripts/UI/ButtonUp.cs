using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        CarController.Instance.OnButtonUpClickDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CarController.Instance.OnButtonUpClickUp();
    }
}
