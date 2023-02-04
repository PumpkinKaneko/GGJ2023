using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] GameObject player;//プレイヤー
    [SerializeField] float cameraRotateSpeed;
    [SerializeField] bool[] playerCameraMoveBool;

    private void FixedUpdate()
    {
        CameraRotatePlus();
        CameraRotateMinus();
    }

    /// <summary>
    /// Y軸のカメラを回転操作
    /// </summary>
    private void CameraRotatePlus()
    {
        if (!playerCameraMoveBool[0])
        {
            //プレイヤー位置情報
            Vector3 playerPos = player.transform.position;

            //カメラを回転させる
            transform.RotateAround(playerPos, Vector3.up, -cameraRotateSpeed);
        }
        else return;
    }

    /// <summary>
    /// Y軸のカメラを回転操作
    /// </summary>
    private void CameraRotateMinus()
    {
        if (!playerCameraMoveBool[1])
        {
            //プレイヤー位置情報
            Vector3 playerPos = player.transform.position;

            //カメラを回転させる
            transform.RotateAround(playerPos, Vector3.up, +cameraRotateSpeed);
        }
        else return;
    }

    /// <summary>
    /// （ボタン割り当て）
    /// </summary>
    public void OnClickPlayerCameraRotatePlus()
    {
        playerCameraMoveBool[0] = false;
    }

    /// <summary>
    /// （ボタン割り当て）
    /// </summary>
    public void OnClickPlayerCameraRotateMinus()
    {
        playerCameraMoveBool[1] = false;
    }

    /// <summary>
    /// ズームカメラ移動を解除（ボタン割り当て）
    /// </summary>
    public void OnClickRotateOff()
    {
        for (int i = 0; i < 2; i++)
        {
            playerCameraMoveBool[i] = true;
        }
    }
}
