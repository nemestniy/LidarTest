using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityGenerator : MonoBehaviour
{
    public static CityGenerator Instance { get; private set; }

    [SerializeField]
    private Ground _startGround;

    private Dictionary<Vector2Int, Ground> _map;
    private Ground _currentGround;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _startGround.SetPosition(Vector2Int.zero);
        _map = new Dictionary<Vector2Int, Ground>();
        _map.Add(Vector2Int.zero, _startGround);

        GenerateGrounds(_startGround);
    }

    public void GenerateGrounds(Ground ground)
    {
        _currentGround = ground;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                var currentPosition = ground.GetPosition() + new Vector2Int(j, i);
                if (!_map.ContainsKey(currentPosition))
                {
                    var generatePosition = new Vector3(transform.localScale.x * currentPosition.x,
                        0,
                        transform.localScale.z * currentPosition.y) * 100;
                    var nextGround = Instantiate(Grounds.Instance.GetRandomGround(), generatePosition, Quaternion.identity);
                    nextGround.SetPosition(currentPosition);
                    _map.Add(currentPosition, nextGround);
                }
            }
        }
    }

    public void DestroyGrounds(Ground lastGround)
    {
        if (_currentGround != null)
        {
            for (int i = -3; i <= 3; i++)
            {
                for (int j = -3; j <= 3; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    var currentPosition = lastGround.GetPosition() + new Vector2Int(j, i);
                    if (_map.ContainsKey(currentPosition))
                    {
                        Ground ground = null;
                        _map.TryGetValue(currentPosition, out ground);
                        if (ground != null)
                        {
                            if (Mathf.Abs(ground.x - _currentGround.x) > 1 || Mathf.Abs(ground.y - _currentGround.y) > 1)
                            {
                                _map.Remove(currentPosition);
                                Destroy(ground.gameObject);
                            }
                        }
                    }
                }
            }
        }
    }
}
