using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grounds : MonoBehaviour
{
    [SerializeField]
    private List<Ground> _grounds;

    public static Grounds Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Ground GetRandomGround()
    {
        int randomCount = Random.Range(0, _grounds.Count);
        return _grounds[randomCount];
    }
}
