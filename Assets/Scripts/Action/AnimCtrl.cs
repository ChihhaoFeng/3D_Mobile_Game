using AttackTypeDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class AnimCtrl : MonoBehaviour
{

    #region System Functions

    public Vector2[] AnimPerArray;
    public Vector2[] AnimSkillPerArray;
    public UI_JoyStick joyStickInst;


    FinalSkillBtn FinalSkillInst;
    AnimatorManger AnimMgr;
    Animator _Anim;
    public Animator Anim => (_Anim);
    int _CurAnimAttackIndex = 1;
    int MinAnimAttackIndex = 1;
    int MaxAnimAttackIndex = 3;
    bool IsReady = true;

    Sword SwordInst;
    Camera Cam;
    eSkillType SkillTypes;

    bool _IsPlaying;
    public bool IsPlaying => (_IsPlaying);

    string CurrAnimname;
    string AttackPre = "Base Layer.Attack";
    string SkillPre = "Base Layer.Skill";
    string SkillPrePath = "Skills/";


    private void Awake()
    {
        AnimMgr = gameObject.AddComponent<AnimatorManger>();

    }


    private void Start()
    {
        _Anim = GetComponent<Animator>();
        AnimMgr.OnStart(this);

        FinalSkillInst = joyStickInst.FinalSkillBtnInst;
        Cam = Camera.main;
        
        var weaponGo = GlobalHelper.FindGOByName(gameObject, "greatesword");
        if (null != weaponGo)
        {
            SwordInst = weaponGo.GetComponent<Sword>();
            SwordInst.OnStart(this);
        }

        joyStickInst.FinalSkillBtnInst.PressDown.AddListener((a) => FinalSkillBegin(a));
        joyStickInst.FinalSkillBtnInst.OnDragEvent.AddListener((a) => OnFinalSkillDrag(a));
        joyStickInst.FinalSkillBtnInst.PressUp.AddListener((a) => OnFinalSkillEnd(a));

        LoadFinalSkillArrow();
    }



    private void Update()
    {
        UpdateSkillInput();
    }


    #endregion

    #region Cast Attack

    void UpdateSkillInput()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            CastSkill(eSkillType.eAttack);
        }

#else

#endif


    }


    void CastSkill(eSkillType type)
    {
        if (!IsReady)
        {
            return;
        }

        SkillTypes = type;

        if (type == eSkillType.eSkill1)
        {
            CurrAnimname = SkillPre + ((int)SkillTypes).ToString();
        }
        else if (type == eSkillType.eAttack) 
        {

            //Debug.Log("Animate Control : CastSkill");
            if (_CurAnimAttackIndex > MaxAnimAttackIndex)
            {
                _CurAnimAttackIndex = MinAnimAttackIndex;
            }
            CurrAnimname = AttackPre + _CurAnimAttackIndex.ToString();

        }

        //Debug.Log(CurrAnimname);
        AnimMgr.StartAnimation(CurrAnimname, CastSkillReady, CastSkillBegin, CastSkillEnd, CastSkillEnd1);

    }

    void CastSkillReady()
    {
        IsReady = true;
        //Debug.Log("CastSkillReady");
    }


    void CastSkillBegin()
    {
        _IsPlaying = true;
        var item = Vector2.zero;

        if (SkillTypes == eSkillType.eAttack)
        {
            IsReady = false;
            
            //load animateor

            //calculate current index of attack

            item = AnimPerArray[_CurAnimAttackIndex - 1];

            SwordInst.OnStartWeaponCtrl(Anim, item.x, item.y);


            //Debug.Log(_CurAnimAttackIndex);

            if (_CurAnimAttackIndex == MinAnimAttackIndex)
            {
                //Debug.Log("START Reset");
                StartCoroutine("AttackPeriod");
            }
            else if (_CurAnimAttackIndex == MaxAnimAttackIndex)
            {
                StopCoroutine("AttackPeriod");
            }


            _CurAnimAttackIndex++;
            // Load Effects

            // make rules about how to combine your effect with the animation.

            var path = SkillPrePath + (1000 + _CurAnimAttackIndex - 1).ToString();
            var SkillPrefab = GlobalHelper.InstantiateMyPrefab(path, transform.position - Vector3.up* 0.5f, Quaternion.identity );

            var SkillInfo = SkillPrefab.GetComponent<SIAction_SkillInfo>();
            SkillInfo.SetOwner(gameObject);

             //attack more times
            

        }
        else if (SkillTypes == eSkillType.eSkill1) 
        {
            item = AnimSkillPerArray[(int)(SkillTypes - 1)];

           SwordInst.OnStartWeaponCtrl(Anim, item.x, item.y);
        }







    }



    void CastSkillEnd()
    {

        _IsPlaying = false;

        if (SkillTypes == eSkillType.eAttack) 
        {

            if (_CurAnimAttackIndex > MaxAnimAttackIndex)
            {
                _CurAnimAttackIndex = MinAnimAttackIndex;
            }

            //_CurAnimAttackIndex = MinAnimAttackIndex;
        }





    }

    void CastSkillEnd1()
    {
        /*
        if (_CurAnimAttackIndex <= 1) 
        {
            return;
        }

        var item = AnimPerArray[_CurAnimAttackIndex - 2];

        SwordInst.OnStartWeaponCtrl(Anim, item.x, item.y);
        */
    }


    IEnumerator AttackPeriod()
    {
        yield return new WaitForSeconds(2.0f);
        _CurAnimAttackIndex = MinAnimAttackIndex;
        Debug.Log("Reset");
    }




    #endregion

    #region Final Skill

    Vector3 FinalSkillDir;
    bool IsUsingAbility = false;
    bool IsFinishFinalSkill = false;
    public float FinalSkillDis = 1.0f;
    



    public void OnModifyFSvalue(int value)
    {
        //increase vage value
        joyStickInst.OnModifyFSV(value);
    }

    public void FinalSkillBegin(PointerEventData data)
    {
        if (IsUsingAbility) 
        {
            return;
        }
        IsFinishFinalSkill = true ;

        

        IsUsingAbility = true;

        Time.timeScale = 0.1f;
        
        _GroundArrow.SetActive(true);

        var dir = FinalSkillInst.Dir.x* Cam.transform.right + FinalSkillInst.Dir.y * Cam.transform.forward;
        dir.y = 0f;
        if (dir == Vector3.zero) 
        {
            dir = transform.forward;
        }
        _GroundArrow.transform.forward = dir;
    }

    public void OnFinalSkillDrag(PointerEventData data)
    {
        if (!IsFinishFinalSkill) 
        {
            return;
        }

        FinalSkillDir = FinalSkillInst.Dir.x * Cam.transform.right + FinalSkillInst.Dir.y * Cam.transform.forward;

        FinalSkillDir.y = 0f;
        if (FinalSkillDir == Vector3.zero)
        {
            FinalSkillDir = transform.forward;
        }
        _GroundArrow.transform.forward = FinalSkillDir;
    }

    public void OnFinalSkillEnd(PointerEventData data)
    {

        if (!IsFinishFinalSkill)
        {
            return;
        }

        Time.timeScale = 1f;

        OnModifyFSvalue(-100);
        
        _GroundArrow.SetActive(false);
        FinalSkillDir = Vector3.zero;

        //play the skill animation
        CastSkill(eSkillType.eSkill1);

        var FinalPos = transform.position + _GroundArrow.transform.forward * FinalSkillDis;
        transform.DOMove(FinalPos, 0.7f).OnComplete(() =>
        {
            IsUsingAbility = false;
            IsFinishFinalSkill = false;
        });
        transform.DOLookAt(FinalPos,0.35f);
    }




    #endregion

    #region Load Arrow
    private GameObject _GroundArrow;
    public GameObject GroundArrow => (_GroundArrow);


    void LoadFinalSkillArrow() 
    {
        var obj = Resources.Load("Weapons/GrounArrow");

        _GroundArrow = Instantiate(obj, transform.position,transform.rotation) as GameObject;

        _GroundArrow.transform.parent = transform;
        _GroundArrow.transform.localPosition = Vector3.zero;
        _GroundArrow.transform.localRotation = Quaternion.identity;
        _GroundArrow.transform.localScale = Vector3.one;

        _GroundArrow.SetActive(false);


    }

    #endregion

}
