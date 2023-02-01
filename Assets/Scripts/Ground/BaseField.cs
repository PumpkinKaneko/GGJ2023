using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BaseField : MonoBehaviour
{
    public Color color = Color.white;

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        GetComponent<MeshRenderer>().sharedMaterial.color = color;
    }


    public virtual void Skill(SeedballBehaviour seedball)
    {
        //Debug.Log("ベーススキル > " + seedball);
    }


    public virtual void SkillUpdate(SeedballBehaviour seedball)
    {
        //Debug.Log("アップデートスキル > " + seedball);
    }


    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name.Contains("SeedballObj"))
        {
            SeedballBehaviour seed = collision.transform.GetComponent<SeedballBehaviour>();
            seed.SetSkill(new SkillAction(Skill));
            seed.SetSkillUpdate(new SkillActionUpdate(SkillUpdate));
        }
    }
}
