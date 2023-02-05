using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGage : MonoBehaviour
{
    public GolfPlayerController controller;

    [SerializeField,Header("?????????")]
    private Slider slider;

    [SerializeField, Header("???")]
    private Image image;


    void Update()
    {
        slider.value = controller.Gage;
        image.fillAmount = controller.StrikePower;
    }
}
