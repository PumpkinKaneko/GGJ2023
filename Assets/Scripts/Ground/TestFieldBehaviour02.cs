using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFieldBehaviour02 : BaseField
{
    public override void Skill(SeedballBehaviour seedball)
    {
        seedball.SetMaterialColor = color;
        seedball.transform.position += Vector3.forward + (Vector3.up * 2);

        base.Skill(seedball);
    }
}
