using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUIAnimation : MonoBehaviour
{
    [SerializeField] GameObject[] shotGageImages;

    [SerializeField] Transform gageSliderTransform;

    [SerializeField] Transform titleBackButtonTransform;

    [SerializeField] Image backImage;
   

    void Start()
    {
        GameStartAnimation();
    }


    private void GameStartAnimation()
    {
        backImage.enabled = true;
        backImage.DOFade(0.0f, 3.0f);
        Invoke("BackImageActiveOff", 2.0f);
    }

    /// <summary>
    /// Invoke呼び
    /// </summary>
    private void BackImageActiveOff()
    {
        backImage.enabled = false;
        for (int i = 0; i < 3; i++)
        {
            shotGageImages[i].SetActive(true);
        }
    }

    /// <summary>
    /// ゲームクリアのアニメーション
    /// </summary>
    public void GameClearEndAnimation()
    {
        for (int i = 0; i < 4; i++)
        {
            shotGageImages[i].SetActive(false);
        }
        MainSoundManager.Instance.ButtonSECall();
        TitleBackButtonAnimation();
        backImage.enabled = true;
        backImage.DOFade(1.0f, 3.0f);
    }

    /// <summary>
    ///　タイトルバックボタンアニメーション
    /// </summary>
    private void TitleBackButtonAnimation()
    {
        titleBackButtonTransform.DOShakeScale(1.0f, 0.5f)
            .Play();
    }

    /// <summary>
    ///　*ショット時に呼び出し*　　ショットゲージアニメーション
    /// </summary>
    public void GageShotAnimation()
    {
        gageSliderTransform.DOPunchScale(new Vector3(1, 1, 1), 0.5f)
            .Play();
    }

}
