using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerFieldBehaviour : BaseField
{
    public override void SkillUpdate(SeedballBehaviour seedball)
    {
        if (seedball.GetRigidbody.velocity.y > -0.3f) return;

        seedball.GetRigidbody.AddForce(Vector3.up * 2.5f);

        base.SkillUpdate(seedball);
    }
}
