using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFieldBehaviour06 : BaseField
{
    public override void Skill(SeedballBehaviour seedball)
    {
        seedball.SetMaterialColor = color;
        seedball.SkillAddForce((Vector3.up * 5) + (Vector3.forward * 10), ForceMode.Impulse);

        base.Skill(seedball);
    }


    public override void SkillUpdate(SeedballBehaviour seedball)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            seedball.SkillAddForce((Vector3.up * 20) + (Vector3.forward * 50), ForceMode.Impulse);
        }

        base.SkillUpdate(seedball);
    }
}
