using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfPlayerController : MonoBehaviour
{
    #region//デバック用
    public Text test;
    #endregion

    private GameObject ball;

    private bool gageStart;
    private bool isAdd;

    //ゲージの数値
    private float gage;
    public float Gage { get { return gage; } /*set { gage = value; }*/ }

    //打つ力(パーセント)
    private float strikePower;
    //補正値
    private float impactPower;

    [SerializeField,Header("プレイヤーマネージャー")]
    private GolfPlayerManager manager;

    [SerializeField]
    Rigidbody rb;

    private float rot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        test.text = gage + ":" + strikePower + ":" + impactPower;

        switch (manager.nowGolfTurn)
        {
            case GolfPlayerManager.golfTurn.PLAY_START:
                break;
            case GolfPlayerManager.golfTurn.RESET_SHOT_READY:
                break;
            case GolfPlayerManager.golfTurn.SHOT_READY:
                ShotReady();
                break;
            case GolfPlayerManager.golfTurn.SHOT_POWER:
                ShotPower();
                break;
            case GolfPlayerManager.golfTurn.SHOT_IMPACT:
                ShotImpact();
                break;
            case GolfPlayerManager.golfTurn.SHOT:
                Shot();
                break;
            case GolfPlayerManager.golfTurn.BALL_FLY:
                break;
            case GolfPlayerManager.golfTurn.BALL_LANDING:
                break;
            case GolfPlayerManager.golfTurn.PLAY_END:
                break;
            default:
                break;
        }

        if (gageStart)
        {
            //if (gage <= 0.0f)
            //{
            //    isAdd = true;
            //}
            if (gage >= 100.0f)
            {
                isAdd = false;
            }

            if (isAdd)
            {
                gage += 0.01f;
            }
            else
            {
                gage -= 0.01f;
            }
        }
    }

    //ここで方向の打つ角度を調整
    private void ShotReady()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, transform.rotation.y + rot, 0);
        if (Input.GetKey(KeyCode.A))
        {
            rot -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rot += 1;
        }


        //向きが決定したらパワーを決める
        if (Input.GetKeyDown(KeyCode.E))
        {
            gageStart = true;
            isAdd =true;
            manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT_POWER;
        }
    }

    //打つ力の確定
    private void ShotPower()
    {
        //インパクトを決めるようにする
        if (Input.GetKeyDown(KeyCode.E))
        {
            strikePower = gage;
            manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT_IMPACT;
            isAdd = false;
        }
    }

    //打つ力の補正値の確定
    private void ShotImpact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            impactPower = gage;
            gageStart = false;
            manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT;
            
        }
    }

    //向いてる方向へ力と補正値をあわせて打つ
    private void Shot()
    {
        // 力を加える向きをVector3型で定義
        // 今回はY軸から45度の向きに射出するため、YとZを1:1にする
        Vector3 forceDirection = new Vector3(impactPower, 1.0f, 1.0f);

        // 上の向きに加わる力の大きさを定義
        float forceMagnitude = 10f;

        // 向きと大きさからSphereに加わる力を計算する
        Vector3 force = forceMagnitude * forceDirection;

        // SphereオブジェクトのRigidbodyコンポーネントへの参照を取得
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        // 力を加えるメソッド
        // ForceMode.Impulseは撃力
        rb.AddForce(force /** (strikePower / 100)*/, ForceMode.Impulse);

        manager.nowGolfTurn = GolfPlayerManager.golfTurn.BALL_FLY;
    }
}
