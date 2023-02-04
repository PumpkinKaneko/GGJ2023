using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    //[SerializeField] AudioClip gameStartButtonSE;//ゲームスタートボタン用SE

    private bool buttonBool = true;//ボタン一回呼び出し

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            OnClickGameStartButton();
        }
    }

    /// <summary>
    /// ゲーム画面へシーン遷移（ボタン割り当て）
    /// </summary>
    public void OnClickGameStartButton()
    {
        if (buttonBool)
        {
            //audioSource.PlayOneShot(gameStartButtonSE);
            Invoke("OnClickGameStartInvoke", 1.5f);
            buttonBool = false;
        }
    }

    /// <summary>
    /// ゲームスタート（インヴォーク呼び出し用）
    /// </summary>
    private void OnClickGameStartInvoke()
    {
        SceneManager.LoadScene("GameScene");
    }
}
