using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField] AudioClip titleBackButtonSE;//タイトルバックボタン用SE

    [SerializeField] GameObject animetionController;//
    private bool buttonBool = true;//ボタン一回呼び出し

    /// <summary>
    /// タイトル画面へシーン遷移（ボタン割り当て）
    /// </summary>
    public void OnClickTitleBackButton()
    {
        if (buttonBool)
        {
            animetionController.GetComponent<GameUIAnimation>().GameClearEndAnimation();
            //audioSource.PlayOneShot(titleBackButtonSE);
            Invoke("OnClickTitleSceneInvoke", 1.5f);
            buttonBool = false;
        }
    }

    /// <summary>
    /// ゲームスタート（インヴォーク呼び出し用）
    /// </summary>
    private void OnClickTitleSceneInvoke()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
