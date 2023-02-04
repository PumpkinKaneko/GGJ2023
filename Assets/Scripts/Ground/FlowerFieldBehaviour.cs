using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerFieldBehaviour : BaseField
{
    public int floatLifeTime;
    private float _floatTimer;


    public override void Skill(SeedballBehaviour seedball)
    {
        _floatTimer = 0;

        base.Skill(seedball);
    }



    public override void SkillUpdate(SeedballBehaviour seedball)
    {
        if (seedball.GetRigidbody.velocity.y > -0.1f) return;

        _floatTimer += Time.deltaTime;
        if (floatLifeTime <= _floatTimer) return;

        seedball.GetRigidbody.AddForce(Vector3.up * 2.5f);

        base.SkillUpdate(seedball);
    }
}
