using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
            Destroy(gameObject);
    }
}
