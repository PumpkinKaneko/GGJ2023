using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFieldBehaviour01 : BaseField
{
    public override void Skill(SeedballBehaviour seedball)
    {
        seedball.SetMaterialColor = color;
        seedball.SkillAddForce((Vector3.up + Vector3.forward) * 1.5f, ForceMode.Impulse);
        seedball.GetRigidbody.angularDrag = 5f;

        base.Skill(seedball);
    }
}
