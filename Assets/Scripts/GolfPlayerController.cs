using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayerController : MonoBehaviour
{
    private GameObject ball;

    private bool gageStart;
    private bool isAdd;

    //ゲージの数値
    private float gage;
    public float Gage { get { return gage; } }

    //打つ力(パーセント)
    private float strikePower;
    public float StrikePower { get { return strikePower; } }
    //補正値
    private float impactPower;

    [SerializeField, Header("矢印表示")]
    private GameObject arrowObj;

    [SerializeField,Header("プレイヤーマネージャー")]
    private GolfPlayerManager manager;
    [SerializeField,Header("ゲームプレイマネージャー")]
    private GamePlayManager gamePlayManager;

    [SerializeField]
    Rigidbody rb;

    private float rot;

    private void Awake()
    {
        manager = GameObject.Find("PlayerManager").GetComponent<GolfPlayerManager>();
        gamePlayManager = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        switch (manager.nowGolfTurn)
        {
            case GolfPlayerManager.golfTurn.PLAY_START:
                break;
            case GolfPlayerManager.golfTurn.RESET_SHOT_READY:
                ResetShotReady();
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
            if (gage >= 1.0f)
            {
                isAdd = false;
            }

            if (isAdd)
            {
                gage = Mathf.Clamp01(gage + (Time.deltaTime / manager.GageSpeed));
            }
            else
            {
                gage = Mathf.Clamp(gage - (Time.deltaTime / manager.GageSpeed),-0.1f,1.0f);
            }
        }
    }

    //ゲージの値などをリセットし※出来たらカップの方向を見る
    private void ResetShotReady()
    {
        gage = 0.0f;
        impactPower = 0.0f;
        strikePower = 0.0f;

        //gameObject.transform.LookAt(manager.LookTarget.transform.localPosition);
        //gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.y, 0);

        arrowObj.SetActive(true);

        //打数を追加する
        gamePlayManager.ShotCount++;
        manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT_READY;
    }

    //ここで方向の打つ角度を調整
    private void ShotReady()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, transform.rotation.y + rot, 0);

        if (Input.GetKey(KeyCode.A))
        {
            rot -= Time.deltaTime * manager.RotSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rot += Time.deltaTime * manager.RotSpeed;
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
        strikePower = gage;

        //インパクトを決めるようにする
        if (Input.GetKeyDown(KeyCode.E) || gage <= 0.0)
        {
            strikePower = gage;

            //ゲージが0だったらランダムで強制で飛んでく
            if (strikePower <= 0.0)
            {
                strikePower = Random.Range(0.01f, 1.01f);
                impactPower = Random.Range(0.00f, 1.01f);
                arrowObj.SetActive(false);
                manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT;
                return;
            }

            manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT_IMPACT;
            isAdd = false;
        }
    }

    //打つ力の補正値の確定
    private void ShotImpact()
    {
        if (Input.GetKeyDown(KeyCode.E) || gage <= -0.1f) 
        {
            impactPower = gage;

            if (impactPower < 0.0f)
            {
                //-0.1だった場合0.1～1にランダムで設定するようにする。
                if (impactPower == -0.1f)
                {
                    impactPower = Random.Range(-1.1f, -0.09f);
                    impactPower = impactPower * -1.0f;
                }
                
                impactPower = impactPower * 1.0f;
            }

            if (impactPower <= 0.05f) 
            {
                impactPower = 0.0f;
            }

            gageStart = false;
            arrowObj.SetActive(false);
            manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT;
        }
    }

    //向いてる方向へ力と補正値をあわせて打つ
    private void Shot()
    {
        var ImpactCorrection = Vector3.zero;

        if (impactPower > 0.0f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                ImpactCorrection = Vector3.left * impactPower;
            }
            else
            {
                ImpactCorrection = Vector3.right * impactPower;
            }
        }

        //Debug.Log("ImpactCorrection : " + ImpactCorrection);
        // ForceMode.Impulseは撃力
        GetComponent<SeedballBehaviour>().AddForce((transform.forward + transform.up + ImpactCorrection) * (strikePower * manager.ShotPower), ForceMode.Impulse);

        manager.nowGolfTurn = GolfPlayerManager.golfTurn.BALL_FLY;
    }
}
