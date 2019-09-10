using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime;

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void FixedUpdate()
    {
        _lifeTime -= Time.fixedDeltaTime;
        if (_lifeTime <= 0)
            Destroy(gameObject);
    }
}
