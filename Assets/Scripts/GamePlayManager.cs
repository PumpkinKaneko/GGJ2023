using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    /// <summary>
    /// ステージ
    /// </summary>
    public enum StageSelect
    {
        SRAGE_01,
        SRAGE_02,
        SRAGE_03,
    }

    private StageSelect stage;
    public StageSelect Stage { get { return stage; }set { stage = value; } }

    //打った回数
    private int shotCount;
    public int ShotCount { get { return shotCount; } set { shotCount = value; } }

    void Start()
    {
        shotCountReset();
    }


    public void StartReset()
    {

    }



    /// <summary>
    /// 打ったカウントをリセット
    /// </summary>
    public void shotCountReset()
    {
        shotCount = 0;
    }
}
