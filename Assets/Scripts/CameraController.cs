using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] GameObject field;

    [SerializeField] GameObject topViewCamera;
    [SerializeField] GameObject sideViewCamera;
    [SerializeField] GameObject gameClearCamera;

    [SerializeField] GameObject playerPositionEffect;
    [SerializeField] GameObject GoalPositionEffect;

    [SerializeField] GameObject topViewCameraButton;
    [SerializeField] GameObject sideViewCameraButton;
    [SerializeField] GameObject playerViewCameraButton;
    [SerializeField] GameObject playerCameraButton;
    [SerializeField] GameObject topViewCameraMoveButtons;
    [SerializeField] GameObject sideViewCameraMoveButtons;

    [SerializeField] GameObject resaltBackImage;

    [SerializeField] CinemachineVirtualCamera playerViewCamera_Vc;
    [SerializeField] CinemachineVirtualCamera sideViewCamera_Vc;
    [SerializeField] CinemachineVirtualCamera topViewCamera_Vc;
    [SerializeField] CinemachineVirtualCamera gameClearCamera_Vc;
    [SerializeField] float mapCameraMoveSpeed;
    [SerializeField] float cameraRadiusSpeed;
    [SerializeField] bool[] zoomCameraMoveBool;

    public bool gameClearBool = false;

    private GameObject mainSoundManager;//※デバッグの為コメントアウト中※
    private Vector3 TopViewCameraStartPosition;
    private Vector3 sideViewCameraStartPosition;


    private void Start()
    {
        mainSoundManager = GameObject.FindGameObjectWithTag("MainSound");
        TopViewCameraStartPosition = topViewCamera.transform.position;
        sideViewCameraStartPosition = sideViewCamera.transform.position;
    }


    private void FixedUpdate()
    {
        TopViewCameraMove();
        sideViewCameraMove();
        GameClearCameraActive();
    }

    /// <summary>
    /// マップカメラに切り替え（ボタン割り当て）
    /// </summary>
    public void OnClickSideViewCamera()
    {
        sideViewCameraButton.SetActive(false);
        playerViewCameraButton.SetActive(true);
        topViewCameraButton.SetActive(true);
        sideViewCameraMoveButtons.SetActive(true);
        topViewCameraMoveButtons.SetActive(false);
        playerPositionEffect.SetActive(true);
        GoalPositionEffect.SetActive(true);
        playerCameraButton.SetActive(false);
        playerViewCamera_Vc.Priority = 0;
        topViewCamera_Vc.Priority = 0;
        sideViewCamera_Vc.Priority = 10;
    }

    /// <summary>
    /// マップズームカメラに切り替え（ボタン割り当て）
    /// </summary>
    public void OnClickTopViewCamera()
    {
        sideViewCameraButton.SetActive(true);
        playerViewCameraButton.SetActive(true);
        sideViewCameraMoveButtons.SetActive(false);
        topViewCameraMoveButtons.SetActive(true);
        playerPositionEffect.SetActive(true);
        GoalPositionEffect.SetActive(true);
        playerCameraButton.SetActive(false);
        playerViewCamera_Vc.Priority = 0;
        sideViewCamera_Vc.Priority = 0;
        topViewCamera_Vc.Priority = 10;
        topViewCameraButton.SetActive(false);
    }

    /// <summary>
    /// プレイヤー視点カメラに切り替え（ボタン割り当て）
    /// </summary>
    public void OnClickPlayerViewCamera()
    {
        topViewCameraButton.SetActive(true);
        sideViewCameraButton.SetActive(true);
        sideViewCameraMoveButtons.SetActive(false);
        topViewCameraMoveButtons.SetActive(false);
        playerPositionEffect.SetActive(false);
        GoalPositionEffect.SetActive(false);
        playerCameraButton.SetActive(true);
        sideViewCamera_Vc.Priority = 0;
        topViewCamera_Vc.Priority = 0;
        playerViewCamera_Vc.Priority = 10;
        playerViewCameraButton.SetActive(false);
    }

    /// <summary>
    /// ゲームクリアカメラに切り替え
    /// ※Publicに変更し別の判定で呼び出す
    /// </summary>
    public void GoalCameraActive()
    {
        topViewCameraButton.SetActive(false);
        sideViewCameraButton.SetActive(false);
        playerViewCameraButton.SetActive(false);
        topViewCameraMoveButtons.SetActive(false);
        playerPositionEffect.SetActive(false);
        GoalPositionEffect.SetActive(false);
        resaltBackImage.SetActive(true);
        //mainSoundManager.GetComponent<MainSoundManager>().GameClearBGMActive();
        sideViewCamera_Vc.Priority = 0;
        topViewCamera_Vc.Priority = 0;
        playerViewCamera_Vc.Priority = 0;
        gameClearCamera_Vc.Priority = 10;
        gameClearBool = true;
    }

    /// <summary>
    /// GoalCameraActiveでgameClearBoolをtrueにしたらカメラが周り出す。
    /// </summary>
    private void GameClearCameraActive()
    {
        if (gameClearBool)
        {
            gameClearCamera.transform.RotateAround(player.transform.position, gameClearCamera.transform.up, cameraRadiusSpeed);
        }
        else return;
    }

    /// <summary>
    /// トップカメラを上に移動
    /// </summary>
    private void TopViewCameraMoveUp()
    {
        if (!zoomCameraMoveBool[0])
            if(topViewCamera.transform.position.z < 20)
            {
                Vector3 pos = topViewCamera.transform.position;
                topViewCamera.transform.position = new Vector3(pos.x, pos.y, pos.z + mapCameraMoveSpeed);
            }
            else return;
    }

    /// <summary>
    /// トップカメラを下に移動
    /// </summary>
    private void TopViewCameraMoveDown()
    {
        if (!zoomCameraMoveBool[1])
            if (topViewCamera.transform.position.z > -20)
            {
                Vector3 pos = topViewCamera.transform.position;
                topViewCamera.transform.position = new Vector3(pos.x, pos.y, pos.z + -mapCameraMoveSpeed);
            }
            else return;
    }

    /// <summary>
    /// トップカメラを左に移動
    /// </summary>
    private void TopViewCameraMoveLeft()
    {
        if (!zoomCameraMoveBool[2])
            if (topViewCamera.transform.position.x > -20)
            {
                Vector3 pos = topViewCamera.transform.position;
                topViewCamera.transform.position = new Vector3(pos.x + -mapCameraMoveSpeed, pos.y, pos.z);
            }
            else return;
    }

    /// <summary>
    /// トップカメラを右に移動
    /// </summary>
    private void TopViewCameraMoveRight()
    {
        if (!zoomCameraMoveBool[3])
            if (topViewCamera.transform.position.x < 20)
            {
                Vector3 pos = topViewCamera.transform.position;
                topViewCamera.transform.position = new Vector3(pos.x + mapCameraMoveSpeed, pos.y, pos.z);
            }
            else return;
    }

    /// <summary>
    /// トップカメラをズームアップ
    /// </summary>
    private void TopViewCameraZoomUp()
    {
        if (!zoomCameraMoveBool[4])
            if (topViewCamera.transform.position.y > 10)
            {
            Vector3 pos = topViewCamera.transform.position;
            topViewCamera.transform.position = new Vector3(pos.x, pos.y + -mapCameraMoveSpeed, pos.z);
        }
        else return;
    }

    /// <summary>
    /// トップカメラをズームダウン
    /// </summary>
    private void TopViewCameraZoomDown()
    {
        if (!zoomCameraMoveBool[5])
            if (topViewCamera.transform.position.y < 30)
            {
                Vector3 pos = topViewCamera.transform.position;
                topViewCamera.transform.position = new Vector3(pos.x, pos.y + mapCameraMoveSpeed, pos.z);
            }
            else return;
    }

    private void TopViewCameraMove()
    {
        TopViewCameraMoveUp();
        TopViewCameraMoveDown();
        TopViewCameraMoveLeft();
        TopViewCameraMoveRight();
        TopViewCameraZoomUp();
        TopViewCameraZoomDown();
    }

    /// <summary>
    /// サイドカメラを上に移動
    /// </summary>
    private void SideViewCameraMoveUp()
    {
        if (!zoomCameraMoveBool[6])
            if (sideViewCamera.transform.position.y < 15)
            {
                Vector3 pos = sideViewCamera.transform.position;
                sideViewCamera.transform.position = new Vector3(pos.x, pos.y + mapCameraMoveSpeed, pos.z);
            }
            else return;
    }

    /// <summary>
    /// サイドカメラを下に移動
    /// </summary>
    private void SideViewCameraMoveDown()
    {
        if (!zoomCameraMoveBool[7])
            if (sideViewCamera.transform.position.y > 0)
            {
                Vector3 pos = sideViewCamera.transform.position;
                sideViewCamera.transform.position = new Vector3(pos.x, pos.y + -mapCameraMoveSpeed, pos.z);
            }
            else return;
    }

    /// <summary>
    /// サイドカメラを左に移動
    /// </summary>
    private void SideViewCameraMoveLeft()
    {
        if (!zoomCameraMoveBool[8])
        {
            Vector3 pos = field.transform.position;
            sideViewCamera.transform.RotateAround(pos, Vector3.up, + mapCameraMoveSpeed);
        }
        else return;
    }

    /// <summary>
    /// サイドカメラを右に移動
    /// </summary>
    private void SideViewCameraMoveRight()
    {
        if (!zoomCameraMoveBool[9])
        {
            Vector3 pos = field.transform.position;
            sideViewCamera.transform.RotateAround(pos, Vector3.up, + -mapCameraMoveSpeed);
        }
        else return;
    }

    private void sideViewCameraMove()
    {
        SideViewCameraMoveUp();
        SideViewCameraMoveDown();
        SideViewCameraMoveLeft();
        SideViewCameraMoveRight();
    }

    /// <summary>
    /// トップカメラを上に移動ボタン押している間（ボタン割り当て）
    /// </summary>
    public void OnClickTopCameraMoveUpActive()
    {
        zoomCameraMoveBool[0] = false;
    }

    /// <summary>
    /// トップカメラを下に移動ボタン押している間（ボタン割り当て）
    /// </summary>
    public void OnClickTopCameraMoveDownActive()
    {
        zoomCameraMoveBool[1] = false;
    }

    /// <summary>
    /// トップカメラを左に移動ボタン押している間（ボタン割り当て）
    /// </summary>
    public void OnClickTopCameraMoveLeftActive()
    {
        zoomCameraMoveBool[2] = false;
    }

    /// <summary>
    /// トップカメラを右に移動ボタン押している間（ボタン割り当て）
    /// </summary>
    public void OnClickTopCameraMoveRightActive()
    {
        zoomCameraMoveBool[3] = false;
    }

    /// <summary>
    /// トップカメラを右に移動ボタン押している間（ボタン割り当て）
    /// </summary>
    public void OnClickTopCameraZoomUpActive()
    {
        zoomCameraMoveBool[4] = false;
    }

    /// <summary>
    /// トップカメラを右に移動ボタン押している間（ボタン割り当て）
    /// </summary>
    public void OnClickTopCameraZoomDownActive()
    {
        zoomCameraMoveBool[5] = false;
    }

    /// <summary>
    /// サイドカメラを上に移動ボタン押している間（ボタン割り当て）
    /// </summary>
    public void OnClickSideCameraMoveUpActive()
    {
        zoomCameraMoveBool[6] = false;
    }

    /// <summary>
    /// サイドカメラを下に移動ボタン押している間（ボタン割り当て）
    /// </summary>
    public void OnClickSideCameraMoveDownActive()
    {
        zoomCameraMoveBool[7] = false;
    }

    /// <summary>
    /// サイドカメラを左回転に移動ボタン押している間（ボタン割り当て）
    /// </summary>
    public void OnClickSideCameraMoveLeftActive()
    {
        zoomCameraMoveBool[8] = false;
    }

    /// <summary>
    /// サイドカメラを右回転に移動ボタン押している間（ボタン割り当て）
    /// </summary>
    public void OnClickSideCameraMoveRightActive()
    {
        zoomCameraMoveBool[9] = false;
    }

    /// <summary>
    /// トップカメラ移動を解除（ボタン割り当て）
    /// </summary>
    public void OnClickCameraMoveActiveOff()
    {
        for (int i = 0; i < 10; i++)
        {
            zoomCameraMoveBool[i] = true;
        }
    }

    /// <summary>
    /// トップカメラを初期位置へリセット（ボタン割り当て）
    /// </summary>
    public void OnClickTopCameraPositionReset()
    {
        topViewCamera.transform.position = TopViewCameraStartPosition;
    }

    /// <summary>
    /// サイドカメラを初期位置へリセット（ボタン割り当て）
    /// </summary>
    public void OnClickSideCameraPositionReset()
    {
        sideViewCamera.transform.position = sideViewCameraStartPosition;
        sideViewCamera.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
