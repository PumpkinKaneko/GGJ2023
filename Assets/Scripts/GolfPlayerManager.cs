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

    private void Awake()
    {
        //nowGolfTurn = golfTurn.PLAY_START;
    }
}
