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
    public int loadMapIndex = 0;
    public BlockMapSetting mapSetting;

    private int[,] _mapZX;
    private float[,] _mapHeight;

    [ExecuteInEditMode]
    void Start()
    {
        Load(mapSetting.mapDataPath[loadMapIndex], mapSetting.heightDataPath[loadMapIndex]);
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

        _mapZX = new int[mapData.GetLength(0), mapData.GetLength(1)];
        _mapHeight = new float[heightData.GetLength(0), heightData.GetLength(1)];

        for (int x = 0; x < mapData.GetLength(1); x++)
        {
            for (int z = 0; z < mapData.GetLength(0); z++)
            {
                _mapZX[z, x] = int.Parse(mapData[z, x]);
                _mapHeight[z, x] = int.Parse(heightData[z, x]);
            }
        }
    }


    public void Create()
    {
        GameObject field = new GameObject();
        field.name = "Field";

        for(int x = 0; x < _mapZX.GetLength(1); x++)
        {
            for(int z = 0; z < _mapZX.GetLength(0); z++)
            {
                float zOffset = 0;
                if (x % 2 == 0) zOffset = 0.5f;

                GameObject block = Instantiate(
                    fieldblocks[_mapZX[z, x]],
                    new Vector3((-blockScale.x * _mapZX.GetLength(1)) / 2, 0, (-blockScale.x * _mapZX.GetLength(0)) / 2) + new Vector3(x * blockScale.x, (-3 + _mapHeight[z, x]) * blockScale.y, (zOffset + z) * blockScale.z),
                    Quaternion.Euler(0, 90, 0),
                    field.transform
                    );
            }
        }
    }
}
