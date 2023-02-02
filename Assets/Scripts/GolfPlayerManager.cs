using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayerManager : MonoBehaviour
{
    public enum golfTurn
    {
        PLAY_START,      //プレイ開始

        RESET_SHOT_READY,   //打つ前の状態に戻す状態

        SHOT_READY,      //打つ前の状態
        SHOT_POWER,      //打つ時のパワー決定の状態
        SHOT_IMPACT,     //打つ時のインパクトのズレ決定の状態
        SHOT,           //打っている状態
        BALL_FLY,        //ボールが飛んでいる状態
        BALL_LANDING,   //ボールが着地したとき

        PLAY_END,        //プレイ終了
    }

    public golfTurn nowGolfTurn;

    [SerializeField,Header("どれぐらいの強さで打つか")]
    private float shotPower;
    public float ShotPower { get { return shotPower; } set { shotPower = value; } }

    [SerializeField,Header("どれぐらいの速さで回転するか")]
    private float rotSpeed;

    private void Awake()
    {
        //nowGolfTurn = golfTurn.PLAY_START;
    }
}
