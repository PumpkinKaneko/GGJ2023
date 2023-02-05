using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseField : MonoBehaviour
{
    public Renderer rendererSelf;
    public Material texture;
    public Color color = Color.white;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rendererSelf.material.color = color;
        rendererSelf.material = texture;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

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

            //Debug.Log("スキルゲット" + this.name);

            if (collision.contacts[0].normal.y * -1 > 0.5f && seed.GetRigidbody.velocity.magnitude < 0.75f && seed.GetActionState == (int)SeedballBehaviour.ActionState.Move)
            {
                seed.nextSkill = new SkillAction(Skill);
                seed.nextSkillUpdate = new SkillActionUpdate(SkillUpdate);
                seed.nextSkillCollision = new SkillActionCollision(SkillCollision);
            }

            if (seed.GetRigidbody.velocity.y > 0.75f)
            {
                try
                {
                    MainSoundManager.Instance.GroundOnSECall();
                }
                catch(System.Exception e)
                {
                    Debug.LogWarning("サウンドマネージャーが見つかりません。\n " + e);
                }
            }
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
