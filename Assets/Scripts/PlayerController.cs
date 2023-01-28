using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private float speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if(photonView.IsMine != true)
        {
            return;
        }

        if(Input.GetKey(KeyCode.UpArrow) == true) {
            this.gameObject.transform.position += this.gameObject.transform.forward * speed;
        }
        if(Input.GetKey(KeyCode.DownArrow) == true) {
            this.gameObject.transform.position -= this.gameObject.transform.forward * speed;
        }
        if(Input.GetKey(KeyCode.LeftArrow) == true) {
            this.gameObject.transform.Rotate(0, -1.0f, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow) == true) {
            this.gameObject.transform.Rotate(0, 1.0f, 0);
        }
    }
}
