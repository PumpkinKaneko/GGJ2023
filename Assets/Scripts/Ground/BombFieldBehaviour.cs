using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFieldBehaviour : BaseField
{
    private bool _oneShot;


    public override void Skill(SeedballBehaviour seedball)
    {
        _oneShot = false;
        seedball.skillMode = SeedballBehaviour.SkillMode.Bomb;

        base.Skill(seedball);
    }


    public override void SkillUpdate(SeedballBehaviour seedball)
    {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if(!_oneShot)
            {
                _oneShot = true;

                seedball.SkillAddForce(seedball.ImpactForce * 0.95f, ForceMode.Impulse);
            }
        }

        base.SkillUpdate(seedball);
    }
}
