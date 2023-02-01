using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFieldBehaviour04 : BaseField
{
    protected override void Start()
    {
        Debug.Log("");

        base.Start();
    }


    public override void Skill(SeedballBehaviour seedball)
    {
        seedball.SetMaterialColor = color;
        seedball.GetCollider.material.bounciness = 0;
        seedball.SkillAddForce(Vector3.up * 3, ForceMode.Impulse);

        base.Skill(seedball);
    }


    public override void SkillUpdate(SeedballBehaviour seedball)
    {
        Vector3 position = seedball.transform.position;

        if (seedball.GetRigidbody.velocity.y > 0.1f)
        {
            seedball.GetRigidbody.drag = 0;
            return;
        }

        if (seedball.GetRigidbody.velocity.magnitude < 0.1f) return;

        seedball.GetRigidbody.drag = 5;

        if (Input.GetKey(KeyCode.W))
        {
            seedball.transform.Translate(0, 0, 1 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            seedball.transform.Translate(-1 * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            seedball.transform.Translate(0, 0, -1 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            seedball.transform.Translate(1 * Time.deltaTime, 0, 0);
        }

        base.SkillUpdate(seedball);
    }
}
