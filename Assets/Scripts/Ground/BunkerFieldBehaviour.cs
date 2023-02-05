using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerFieldBehaviour : BaseField
{
    public override void Skill(SeedballBehaviour seedball)
    {
        seedball.SetMaterialColor = color;

        seedball.skillMode = SeedballBehaviour.SkillMode.Cacatus;

        base.Skill(seedball);
    }


    public override void SkillUpdate(SeedballBehaviour seedball)
    {
        seedball.GetCollider.material.bounciness = 0;

        base.SkillUpdate(seedball);
    }


    public override void SkillCollision(SeedballBehaviour seedball, Collision collision)
    {
        if (collision.transform.tag.Contains("StageObject"))
        {
            seedball.GetRigidbody.velocity = Vector3.zero;
        }

        base.SkillCollision(seedball, collision);
    }
}
