using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GaugeSettings {
    public const float minValue = -0.2f;
    public const float maxValue = 1.0f;
    public const float initValue = 0.0f; // left side is 0.0f, right side is 1.0f;
    public const float speed = 0.005f;

    public const float initPower = 0;

    public const float initImpact = 0;
}

public class GaugeController : MonoBehaviour
{
    private enum GAUGE_STATE {
        INIT = 0,
        READY,
        PLAYING_POWER,
        PLAYING_IMPACT,
        RESULT,
        END
    }

    [SerializeField]
    private GAUGE_STATE state = GAUGE_STATE.INIT;

    [SerializeField]
    private Slider gaugeSlider = null;

    [SerializeField]
    private float power = GaugeSettings.initValue;

    [SerializeField]
    private float impact = GaugeSettings.initImpact;


    // Start is called before the first frame update
    void Start()
    {
        this.state = GAUGE_STATE.INIT;
        this.gaugeSlider.minValue = GaugeSettings.minValue;
        this.gaugeSlider.maxValue = GaugeSettings.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.state) {
            case GAUGE_STATE.INIT:
                this.Init();
                break;

            case GAUGE_STATE.READY:
                this.Ready();
                break;

            case GAUGE_STATE.PLAYING_POWER:
                this.PlayingPower();
                break;

            case GAUGE_STATE.PLAYING_IMPACT:
                this.PlayingImpact();
                break;

            case GAUGE_STATE.RESULT:
                this.Result();
                break;

            case GAUGE_STATE.END:
                this.End();
                break;
        }
    }

    private void Init() {
        this.gaugeSlider.value = GaugeSettings.initValue;
        this.power = GaugeSettings.initPower;
        this.impact = GaugeSettings.initImpact;
        this.state = GAUGE_STATE.READY;
    }
    private void Ready() {
        if (Input.GetKeyDown(KeyCode.Space) == true) {
            this.state = GAUGE_STATE.PLAYING_POWER;
        }
    }
    private void PlayingPower() {
        this.gaugeSlider.value += GaugeSettings.speed;
        if (this.gaugeSlider.value >= GaugeSettings.maxValue) {
            this.state = GAUGE_STATE.PLAYING_IMPACT;
        }
        if (Input.GetKeyDown(KeyCode.Space) == true) {
            this.power = this.gaugeSlider.value;
        }
    }

    private void PlayingImpact() {
        this.gaugeSlider.value -= GaugeSettings.speed;
        if (this.gaugeSlider.value <= GaugeSettings.minValue) {
            this.state = GAUGE_STATE.RESULT;
        }
        if (Input.GetKeyDown(KeyCode.Space) == true) {
            this.impact = this.gaugeSlider.value;
        }
    }

    private void Result() {
        if (this.power == GaugeSettings.initPower) {
            this.power = Random.Range(GaugeSettings.initValue, GaugeSettings.maxValue);
        }
        if (this.impact == GaugeSettings.initImpact) {
            this.impact = Random.Range(GaugeSettings.initValue, GaugeSettings.maxValue);
        }

        this.state = GAUGE_STATE.END;
    }

    private void End() {
        if (Input.GetKeyDown(KeyCode.Space) == true) {
            this.state = GAUGE_STATE.INIT;
        }
    }


    // Accessor functions
    public float GetPower () {
        return this.power;
    }
    public float GetImpact () {
        return this.impact;
    }
}
