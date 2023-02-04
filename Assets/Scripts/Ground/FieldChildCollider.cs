using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldChildCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnCollisionEnter(Collision collision)
    {
        if(this.transform.parent.parent.GetComponent<BaseField>())
            this.transform.parent.parent.GetComponent<BaseField>().OnChildCollisionEnter(collision);
    }
}
