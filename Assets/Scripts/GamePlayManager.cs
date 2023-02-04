using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    //打った回数
    private int shotCount;
    public int ShotCount { get { return shotCount; } set { shotCount = value; } }

    void Start()
    {
        shotCountReset();
    }

    /// <summary>
    /// 打ったカウントをリセット
    /// </summary>
    public void shotCountReset()
    {
        shotCount = 0;
    }
}
