using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [System.Serializable]
    public class BlockMapSetting
    {
        public string[] mapDataPath;
        public string[] heightDataPath;
    }


    public GameObject[] fieldblocks;
    public Vector3 blockScale = new Vector3(0, 0, 0);
    public BlockMapSetting mapSetting;

    private int[,] _mapZX;
    private float[,] _mapHeight;


    void Start()
    {
        Load(mapSetting.mapDataPath[0], mapSetting.heightDataPath[0]);
        Create();

        blockScale.z = (1 * ((Mathf.Sqrt(3) / 2))) * 2;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath">ファイルパス（ローカル）</param>
    public void Load(string mapPath, string heightPath)
    {
        string[,] mapData = CSVIO.Read(mapPath);
        string[,] heightData = CSVIO.Read(heightPath);

        for (int x = 0; x < _mapZX.GetLength(1); x++)
        {
            for (int z = 0; z < _mapZX.GetLength(0); z++)
            {
                _mapZX[z, x] = int.Parse(mapData[z, x]);
                _mapHeight[z, x] = int.Parse(heightData[z, x]);
            }
        }
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
                    Quaternion.Euler(0, 90, 0)
                    );
            }
        }
    }
}
