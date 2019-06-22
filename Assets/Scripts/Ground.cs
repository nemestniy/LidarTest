using UnityEngine;
using System.Collections;
using System;

public class Ground : MonoBehaviour
{
    public int x { get; private set; }
    public int y { get; private set; }

    private bool _playerIsHere = false;

    public Vector2Int GetPosition()
    {
        return new Vector2Int(x, y);
    }

    public void SetPosition(Vector2Int position)
    {
        x = position.x;
        y = position.y;
    }


    public void GererateGround(int side)
    {
        Vector3 position = Vector3.zero;
        switch (side)
        {  
            case 0:
                position = new Vector3(0, 0, transform.localScale.z) * 10 + transform.position;
                break;
            case 1:
                position = new Vector3(transform.localScale.x, 0, 0) * 10 + transform.position;
                break;
            case 2:
                position = new Vector3(0, 0, -transform.localScale.z) * 10 + transform.position;
                break;
            case 3:
                position = new Vector3(-transform.localScale.x, 0, 0) * 10 + transform.position;
                break;
        }
        if(position != Vector3.zero)
            Instantiate(Grounds.Instance.GetRandomGround(), position, Quaternion.identity);
    }
}
