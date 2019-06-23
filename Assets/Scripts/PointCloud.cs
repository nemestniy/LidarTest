using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class PointCloud : MonoBehaviour
{
    [SerializeField]
    private string _path;

    [SerializeField]
    private Point _readPoint;

    private void Awake()
    {
        _path = Directory.GetCurrentDirectory() + _path + "\\PointCloud.txt";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OutputPoints();
            Debug.Log(_path);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            InputPoints();
        }
    }

    private void OutputPoints()
    {
        try
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_path, FileMode.Create)))
            {
                var points = FindObjectsOfType<Point>();
                foreach(Point point in points)
                {
                    var position = point.GetPosition(); 
                    writer.Write((double)position.x);
                    writer.Write((double)position.y);
                    writer.Write((double)position.z);
                }
                writer.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void InputPoints()
    {
        try
        {
            using (BinaryReader reader = new BinaryReader(File.Open(_path, FileMode.Open)))
            {
                while(reader.PeekChar() > -1)
                {
                    var x = (float)reader.ReadDouble();
                    var y = (float)reader.ReadDouble();
                    var z = (float)reader.ReadDouble();
                    Instantiate(_readPoint, new Vector3(x, y, z), Quaternion.identity);
                }
                reader.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
