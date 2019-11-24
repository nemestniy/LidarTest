using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour
{
    private Text _speedText;

    private void Start() {
        _speedText = GetComponent<Text>();   
    }
    void Update()
    {
        _speedText.text = CarController.Instance.GetSpeed() + "rpm";
    }
}
