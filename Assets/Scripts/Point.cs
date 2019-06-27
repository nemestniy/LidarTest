using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime;

    private void Awake()
    {
        _lifeTime *= Time.fixedDeltaTime;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void Update()
    {
        _lifeTime -= Time.fixedDeltaTime;
        if (_lifeTime <= 0)
            Destroy(gameObject);
    }
}
