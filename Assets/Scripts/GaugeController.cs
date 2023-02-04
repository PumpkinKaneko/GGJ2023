using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    // Start is called before the first frame update
    void Start()
    {
        this.state = GAUGE_STATE.READY;
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.state) {
            case GAUGE_STATE.READY:
                this.Ready();
                break;
        }
    }

    private void Ready() {
        if (Input.GetKeyDown(KeyCode.Space) == true) {
            
        }
    }
}
