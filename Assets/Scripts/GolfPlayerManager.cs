using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayerManager : MonoBehaviour
{
    public enum golfTurn
    {
        PLAY_START,      //�v���C�J�n

        RESET_SHOT_READY,   //�łO�̏�Ԃɖ߂����

        SHOT_READY,      //�łO�̏��
        SHOT_POWER,      //�ł��̃p���[����̏��
        SHOT_IMPACT,     //�ł��̃C���p�N�g�̃Y������̏��
        SHOT,           //�ł��Ă�����
        BALL_FLY,        //�{�[�������ł�����
        BALL_LANDING,   //�{�[�������n�����Ƃ�

        PLAY_END,        //�v���C�I��
    }

    public golfTurn nowGolfTurn;

    [SerializeField,Header("�ǂꂮ�炢�̋����őł�")]
    private float shotPower;
    public float ShotPower { get { return shotPower; } set { shotPower = value; } }

    [SerializeField,Header("�ǂꂮ�炢�̑����ŉ�]���邩")]
    private float rotSpeed;

    private void Awake()
    {
        //nowGolfTurn = golfTurn.PLAY_START;
    }
}
