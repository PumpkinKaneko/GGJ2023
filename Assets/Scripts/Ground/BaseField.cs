using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseField : MonoBehaviour
{
    public Renderer rendererSelf;
    public Color color = Color.white;

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        rendererSelf.material.color = color;
    }


    public virtual void Skill(SeedballBehaviour seedball)
    {
        //Debug.Log("ベーススキル > " + seedball);
    }


    public virtual void SkillUpdate(SeedballBehaviour seedball)
    {
        //Debug.Log("アップデートスキル > " + seedball);
    }


    public virtual void SkillCollision(SeedballBehaviour seedball, Collision collision)
    {
        //Debug.Log("ヒットスキル > " + seedball);
    }



    public virtual void OnChildCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("SeedballObj"))
        {
            SeedballBehaviour seed = collision.transform.GetComponent<SeedballBehaviour>();
            seed.SetSkill(new SkillAction(Skill));
            seed.SetSkillUpdate(new SkillActionUpdate(SkillUpdate));
            seed.SetSkillCollision(new SkillActionCollision(SkillCollision));
        }
    }


    protected virtual void OnCollisionEnter(Collision collision)
    {
        /*
        Debug.Log("hit > " + collision.transform.name);

        if(collision.transform.name.Contains("SeedballObj"))
        {
            SeedballBehaviour seed = collision.transform.GetComponent<SeedballBehaviour>();
            Debug.Log("Hit");
            seed.SetSkill(new SkillAction(Skill));
            seed.SetSkillUpdate(new SkillActionUpdate(SkillUpdate));
            seed.SetSkillCollision(new SkillActionCollision(SkillCollision));
        }
        */
    }
}
