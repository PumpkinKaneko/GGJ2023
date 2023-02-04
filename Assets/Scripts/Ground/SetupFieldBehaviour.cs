using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupFeldBehaviour : BaseField
{
    public GameObject seedball;


    // Start is called before the first frame update
    protected override void Start()
    {
        Instantiate(seedball, this.transform.position + (transform.up * 4), Quaternion.identity);

        base.Start();
    }
}
