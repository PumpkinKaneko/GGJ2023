using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] fieldblocks;
    public Vector3 blockScale = new Vector3(0, 0, 0);

    private int[,] _mapZX =
    {
        { 0, 0, 0 },
        { 0, 0, 0 },
        { 0, 0, 0 },
        { 0, 0, 0 }
    };


    private float[,] _mapHeight =
    {
        { 0.5f, 0.5f, 0.5f },
        { 0.5f, 0f, 0.5f },
        { 0.5f, 0f, 0.5f },
        { 0.5f, 0.5f, 0.5f }
    };


    void Start()
    {
        Create();
    }


    public void Create()
    {
        for(int x = 0; x < _mapZX.GetLength(1); x++)
        {
            for(int z = 0; z < _mapZX.GetLength(0); z++)
            {
                float zOffset = 0;
                if (x % 2 == 0) zOffset = 0.5f;

                GameObject block = Instantiate(
                    fieldblocks[_mapZX[z, x]],
                    Vector3.zero + new Vector3(x * blockScale.x, _mapHeight[z, x] * blockScale.y, (zOffset + z) * blockScale.z),
                    Quaternion.identity
                    );

                //block.transform.localScale = blockScale;
            }
        }
    }
}
