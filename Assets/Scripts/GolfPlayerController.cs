using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayerController : MonoBehaviour
{
    private GameObject ball;

    private bool gageStart;
    private bool isAdd;

    //�Q�[�W�̐��l
    private float gage;
    public float Gage { get { return gage; } /*set { gage = value; }*/ }

    //�ł�(�p�[�Z���g)
    private float strikePower;
    //�␳�l
    private float impactPower;

    [SerializeField,Header("�v���C���[�}�l�[�W���[")]
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

            Debug.Log("" + gage);
        }
    }

    //�Q�[�W�̒l�Ȃǂ����Z�b�g�����o������J�b�v�̕���������
    private void ResetShotReady()
    {
        gage = 0.0f;
        impactPower = 0.0f;
        strikePower = 0.0f;

        
        gameObject.transform.LookAt(manager.LookTarget.transform.localPosition);
        //gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.y, 0);
        

        manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT_READY;
    }

    //�����ŕ����̑łp�x�𒲐�
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


        //���������肵����p���[�����߂�
        if (Input.GetKeyDown(KeyCode.E))
        {
            gageStart = true;
            isAdd =true;
            manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT_POWER;
        }
    }

    //�ł͂̊m��
    private void ShotPower()
    {
        //�C���p�N�g�����߂�悤�ɂ���
        if (Input.GetKeyDown(KeyCode.E))
        {
            strikePower = gage;
            manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT_IMPACT;
            isAdd = false;
        }
    }

    //�ł͂̕␳�l�̊m��
    private void ShotImpact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            impactPower = gage;
            gageStart = false;
            manager.nowGolfTurn = GolfPlayerManager.golfTurn.SHOT;
            
        }
    }

    //�����Ă�����֗͂ƕ␳�l�����킹�đł�
    private void Shot()
    {
        // ForceMode.Impulse�͌���
        GetComponent<SeedballBehaviour>().AddForce((transform.forward + transform.up) * (strikePower * manager.ShotPower), ForceMode.Impulse);

        manager.nowGolfTurn = GolfPlayerManager.golfTurn.BALL_FLY;
    }
}
