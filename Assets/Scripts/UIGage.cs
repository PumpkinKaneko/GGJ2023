using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGage : MonoBehaviour
{
    public GolfPlayerController controller;

    [SerializeField,Header("�}���h���S�������")]
    private Slider slider;

    [SerializeField, Header("�Q�[�W")]
    private Image image;


    void Update()
    {
        slider.value = controller.Gage;
        image.fillAmount = controller.StrikePower;
    }
}
