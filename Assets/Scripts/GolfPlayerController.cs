using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfPlayerController : MonoBehaviour
{
    #region//�f�o�b�N�p
    public Text test;
    #endregion

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

    //�����ŕ����̑łp�x�𒲐�
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
        // �͂������������Vector3�^�Œ�`
        // �����Y������45�x�̌����Ɏˏo���邽�߁AY��Z��1:1�ɂ���
        Vector3 forceDirection = new Vector3(impactPower, 1.0f, 1.0f);

        // ��̌����ɉ����͂̑傫�����`
        float forceMagnitude = 10f;

        // �����Ƒ傫������Sphere�ɉ����͂��v�Z����
        Vector3 force = forceMagnitude * forceDirection;

        // Sphere�I�u�W�F�N�g��Rigidbody�R���|�[�l���g�ւ̎Q�Ƃ��擾
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        // �͂������郁�\�b�h
        // ForceMode.Impulse�͌���
        rb.AddForce(force /** (strikePower / 100)*/, ForceMode.Impulse);

        manager.nowGolfTurn = GolfPlayerManager.golfTurn.BALL_FLY;
    }
}
