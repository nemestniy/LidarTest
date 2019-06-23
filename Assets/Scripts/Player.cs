using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Ground _currentGround = null;
    public Ground _exitGround = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            if (other.GetComponent<Ground>() != _exitGround)
            {
                _currentGround = other.GetComponent<Ground>();
                CityGenerator.Instance.GenerateGrounds(_currentGround);
                if(_exitGround != null)
                    CityGenerator.Instance.DestroyGrounds(_exitGround);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Finish")
        {
            _exitGround = _currentGround;    
        }
    }
}
