using UnityEngine;
using System.Collections;

[SerializeField]
public class PointPosition : MonoBehaviour
{
    public float x { get; private set; }
    public float y { get; private set; }
    public float z { get; private set; }

    public PointPosition(Vector3 position)
    {
        x = position.x;
        y = position.y;
        z = position.z;
    }
}
