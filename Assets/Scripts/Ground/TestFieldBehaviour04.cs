using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFieldBehaviour04 : BaseField
{
    protected override void Start()
    {
        base.Start();
    }


    public override void Skill(SeedballBehaviour seedball)
    {
        seedball.SetMaterialColor = color;
        seedball.SkillAddForce((Vector3.up * 10) + (Vector3.forward * 20) , ForceMode.Impulse);;

        base.Skill(seedball);
    }


    public override void SkillUpdate(SeedballBehaviour seedball)
    {
        

        base.SkillUpdate(seedball);
    }


    public override void SkillCollision(SeedballBehaviour seedball, Collision collision)
    {
        if(collision.transform.name.Contains("Cube"))
        {
            seedball.GetRigidbody.useGravity = false;
            seedball.GetRigidbody.velocity = Vector3.zero;
            seedball.GetRigidbody.angularVelocity = Vector3.zero;
        }

        base.SkillCollision(seedball, collision);
    }
}
