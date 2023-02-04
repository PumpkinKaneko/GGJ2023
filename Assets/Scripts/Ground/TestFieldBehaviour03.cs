using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFieldBehaviour03 : BaseField
{
    public override void Skill(SeedballBehaviour seedball)
    {
        seedball.SetMaterialColor = color;
        seedball.GetCollider.material.bounciness = 0;
        seedball.SkillAddForce(Vector3.up * 3, ForceMode.Impulse);

        base.Skill(seedball);
    }
}
