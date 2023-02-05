using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleSceneManager : MonoBehaviour
{
    //[SerializeField] AudioClip gameStartButtonSE;//ゲームスタートボタン用SE

    [SerializeField] Image[] titleImages;
    [SerializeField] GameObject scoreText;
    [SerializeField] Transform gameStartButton;

    private bool buttonBool = true;//ボタン一回呼び出し

    private void Start()
    {
        TitleAnimation();
    }

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
            MainSoundManager.Instance.ButtonSECall();
            GameStartButtonAnimation();
            GameStartAnimation();
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


    /// <summary>
    /// タイトルのアニメーション
    /// </summary>
    private void TitleAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            titleImages[i].color = new Color32(255, 255, 255,0);
            titleImages[i].DOFade(1.0f, 3.0f);
        }
    }

    /// <summary>
    ///　ゲームスタートボタンアニメーション
    /// </summary>
    private void GameStartButtonAnimation()
    {
        gameStartButton.DOShakeScale(1.0f, 0.5f)
            .Play();
    }

    /// <summary>
    /// ゲームスタートアニメーション
    /// </summary>
    private void GameStartAnimation()
    {
        scoreText.SetActive(false);
            
        for (int i = 0; i < 3; i++)
        {
            titleImages[i].DOFade(0.0f, 1.5f);
        }
    }
}
