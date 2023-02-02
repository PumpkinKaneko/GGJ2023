using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFieldBehaviour05 : BaseField
{
    public override void Skill(SeedballBehaviour seedball)
    {
        seedball.SetMaterialColor = color;
        seedball.SkillAddForce(Vector3.up * 10, ForceMode.Impulse);

        base.Skill(seedball);
    }


    public override void SkillUpdate(SeedballBehaviour seedball)
    {
        if (seedball.GetRigidbody.velocity.y > -0.1f) return;

        if(Input.GetKey(KeyCode.W))
        {
            seedball.transform.Translate(0, 0, 3 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            seedball.transform.Translate(-3 * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            seedball.transform.Translate(0, 0, -3 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            seedball.transform.Translate(3 * Time.deltaTime, 0, 0);
        }

        base.SkillUpdate(seedball);
    }
}
