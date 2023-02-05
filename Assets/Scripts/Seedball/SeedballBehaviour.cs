using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void SkillAction(SeedballBehaviour seedball);
public delegate void SkillActionUpdate(SeedballBehaviour seedball);
public delegate void SkillActionCollision(SeedballBehaviour seedball, Collision collision);


[RequireComponent(typeof(Rigidbody))]
public class SeedballBehaviour : MonoBehaviour
{
    public SkillAction skill = null;
    public SkillActionUpdate skillUpdate;
    public SkillActionCollision skillCollision;

    public MeshRenderer seedRenderer;
    public bool showDebugGUI = false;

    private float _skillEnterDelay = 1;
    private float _stopDelayTime = 0;
    private float _stopDelay = 0.5f;
    private int _actionState = 0;
    private int _skillState = 0;
    private bool _skillPreped = true;
    private Vector3 _collisionImpulse = Vector3.zero;       // 力積（CollisionStayで使用）
    private SkillState _skillStateLog = SkillState.Stay;

    public Rigidbody GetRigidbody { get { return GetComponent<Rigidbody>(); } }
    public Collider GetCollider { get { return GetComponent<Collider>(); } }
    public Color SetMaterialColor { get { return seedRenderer.material.color; } set { seedRenderer.material.color = value; } }
    public SkillAction nextSkill { get; set; }
    public SkillActionUpdate nextSkillUpdate { get; set; }
    public SkillActionCollision nextSkillCollision { get; set; }
    public Vector3 ImpactForce { get; set; }
    public SkillMode skillMode { get; set; }
    public BaseField panelTarget { get; set; }
    public int GetActionState { get { return _actionState; } private set { _actionState = value; } }

    // Start is called before the first frame update
    void Start()
    {
        //AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        ActionStateUpdate();
        SkillStateUpdate();

        if (skillUpdate != null && _actionState == (int)ActionState.Move) skillUpdate(this);

        if (_actionState == (int)ActionState.Finished && _skillState == (int)SkillState.Finished) Setup();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(_actionState==(int)ActionState.Stay)
                AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }


    private void ActionStateUpdate()
    {
        switch(_actionState)
        {
            case (int)ActionState.Stay:
                
                break;

            case (int)ActionState.Impact:
                if (GetRigidbody.velocity.magnitude > 0.5f)
                {
                    _actionState = (int)ActionState.Move;
                }
                break;

            case (int)ActionState.Move:
                if (GetRigidbody.velocity.magnitude < 0.1f)
                {
                    _stopDelayTime += Time.deltaTime;
                    if (_stopDelay <= _stopDelayTime)
                    {
                        _actionState = (int)ActionState.Stop;
                        _skillState = (int)SkillState.Wait;

                        SetSkill(nextSkill);
                        SetSkillUpdate(nextSkillUpdate);
                        SetSkillCollision(nextSkillCollision);

                        StartCoroutine("SkillSequence");

                        _stopDelayTime = 0;
                    }
                }
                else
                {
                    _stopDelayTime = 0;
                }
                break;

            case (int)ActionState.Stop:
                break;

            case (int)ActionState.Finished:
                break;
        }
    }


    private void SkillStateUpdate()
    {
        switch(_skillState)
        {
            case (int)SkillState.Stay:
                break;

            case (int)SkillState.Wait:
                
                break;

            case (int)SkillState.Execute:
                if (GetRigidbody.velocity.magnitude < 0.1f)
                {
                    _stopDelayTime += Time.deltaTime;
                    if (_stopDelay <= _stopDelayTime)
                    {
                        if (GetRigidbody.velocity.magnitude < 0.1f)
                        {
                            _skillState = (int)SkillState.Finished;

                            _stopDelayTime = 0;
                        }
                    }
                }
                else
                {
                    _stopDelayTime = 0;
                }
                break;

            case (int)SkillState.Finished:
                _actionState = (int)ActionState.Finished;
                break;
        }
    }


    public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force)
    {
        GetRigidbody.WakeUp();

        _actionState = (int)ActionState.Impact;

        ImpactForce = force;
        GetRigidbody.AddForce(force, mode);
    }


    public void SkillAddForce(Vector3 force, ForceMode mode = ForceMode.Force)
    {
        GetRigidbody.AddForce(force, mode);
    }


    public void SetSkill(SkillAction action)
    {
        if (skill != null) skill -= skill;
        skill = action;
        //Debug.Log("スキルをセットしました... " + skill);
    }


    public void SetSkillUpdate(SkillActionUpdate action)
    {
        if (skillUpdate != null) skillUpdate -= skillUpdate;
        skillUpdate = action;
        //Debug.Log("アップデートスキルをセットしました... " + skillUpdate);
    }


    public void SetSkillCollision(SkillActionCollision action)
    {
        if (skillCollision != null) skillCollision -= skillCollision;
        skillCollision = action;
        //Debug.Log("コリンジョンスキルをセットしました... " + skillCollision);
    }


    public void Setup()
    {
        if( skill != null) skill(this);

        _actionState = (int)ActionState.Stay;
        _skillState = (int)SkillState.Stay;

        GetRigidbody.velocity = Vector3.zero;
        GetRigidbody.angularVelocity = Vector3.zero;
        GetRigidbody.drag = 0.5f;
        GetRigidbody.angularDrag = 0.05f;
        GetCollider.material.bounciness = 1f;
        GetRigidbody.Sleep();
        
        GameObject.Find("PlayerManager").GetComponent<GolfPlayerManager>().nowGolfTurn = GolfPlayerManager.golfTurn.RESET_SHOT_READY;
    }


    public void Repeat()
    {
        _actionState = (int)ActionState.Stay;
        _skillState = (int)SkillState.Stay;

        GetRigidbody.Sleep();

        GameObject.Find("PlayerManager").GetComponent<GolfPlayerManager>().nowGolfTurn = GolfPlayerManager.golfTurn.RESET_SHOT_READY;
    }


    public IEnumerator SkillSequence()
    {
        yield return new WaitForSeconds(_skillEnterDelay);

        if(skill != null) skill(this);

        _skillState = (int)SkillState.Execute;
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if(skillCollision != null) skillCollision(this, collision);
    }


    private void OnCollisionStay(Collision collision)
    {
        _collisionImpulse += collision.impulse;     // 力積チャージ！！
    }


    private void OnGUI()
    {
        if (!showDebugGUI) return;

        _skillStateLog = (SkillState)System.Enum.ToObject(typeof(SkillState), _skillState);
        ActionState actionStateLog = (ActionState)System.Enum.ToObject(typeof(ActionState), _actionState);

        string log = "=== Seedball Log ==="
            + "\nActionState > " + actionStateLog
            + "\nSkillState > " + _skillStateLog
            + "\nStopDelay > " + _stopDelayTime
            + "\nSkillMode > " + skillMode
            + "\nSkill > " + skill
            + "\nSkillUpdate > " + skillUpdate
            + "\nSkillCollision > " + skillCollision;

        GUI.backgroundColor = Color.red;
        GUI.Box(new Rect(0, 0, 200, 200), "");

        GUI.color = Color.white;
        GUI.Label(new Rect(5, 5, 195, 195), log);
    }


    public enum ActionState
    {
        Stay = 0,
        Impact,
        Move,
        Stop,
        Finished
    }


    public enum SkillState
    {
        Stay = 0,
        Wait,
        Execute,
        Finished
    }


    public enum SkillMode
    {
        None = 0,
        Cacatus,
        Flower,
        Bomb
    }
}
