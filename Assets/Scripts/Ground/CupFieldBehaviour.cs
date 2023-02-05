using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupFieldBehaviour : BaseField
{
    public Animator tree;


    protected override void Start()
    {
        base.Start();
    }


    public override void OnChildCollisionEnter(Collision collision)
    {
        if(collision.transform.name.Contains("SeedballObj"))
        {
            tree.SetTrigger("grow");

            GolfPlayerManager controller = GameObject.Find("PlayerManager").GetComponent<GolfPlayerManager>();
            if (controller != null) controller.nowGolfTurn = GolfPlayerManager.golfTurn.PLAY_END;
        }

        base.OnChildCollisionEnter(collision);
    }
}
