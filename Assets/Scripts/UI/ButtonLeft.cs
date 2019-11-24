using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        CarController.Instance.OnButtonLeftClickDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CarController.Instance.OnButtonLeftClickUp();
    }
}
