using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenFieldBehaviour : BaseField
{
    public override void Skill(SeedballBehaviour seedball)
    {
        seedball.skillMode = SeedballBehaviour.SkillMode.None;

        base.Skill(seedball);
    }
}
