using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : MonoBehaviour
{

    #region System Functions

    public Vector2[] AnimPerArray;
    public UI_JoyStick joyStickInst;

    AnimatorManger AnimMgr;
    Animator _Anim;
    public Animator Anim => (_Anim);
    int _CurAnimAttackIndex = 1;
    int MinAnimAttackIndex = 1;
    int MaxAnimAttackIndex = 3;
    bool IsReady = true;

    Sword SwordInst;

    bool _IsPlaying;
    public bool IsPlaying => (_IsPlaying);

    string CurrAnimname;
    string AttackPre = "Base Layer.Attack";




    private void Awake()
    {
        AnimMgr = gameObject.AddComponent<AnimatorManger>();

    }


    private void Start()
    {
        _Anim = GetComponent<Animator>();
        AnimMgr.OnStart(this);

        var weaponGo = GlobalHelper.FindGOByName(gameObject, "greatesword");
        if (null != weaponGo) 
        {
            SwordInst = weaponGo.GetComponent<Sword>();
            SwordInst.OnStart(this);
        }
        


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
            CastSkill();
        }
#else

#endif


    }


    void CastSkill() 
    {
        if (!IsReady)
        {
            return;
        }
        


        //Debug.Log("Animate Control : CastSkill");
        if (_CurAnimAttackIndex > MaxAnimAttackIndex) 
        {
            _CurAnimAttackIndex = MinAnimAttackIndex;
        }
        CurrAnimname = AttackPre + _CurAnimAttackIndex.ToString();

        //Debug.Log(CurrAnimname);
        AnimMgr.StartAnimation(CurrAnimname, CastSkillReady , CastSkillBegin, CastSkillEnd, CastSkillEnd1);
    }

    void CastSkillReady() 
    {
        IsReady = true;
        //Debug.Log("CastSkillReady");
    }


    void CastSkillBegin() 
    {
        _IsPlaying = true;
        IsReady = false;


        var item = AnimPerArray[_CurAnimAttackIndex - 1];

        SwordInst.OnStartWeaponCtrl(Anim, item.x, item.y);


        Debug.Log(_CurAnimAttackIndex);

        if (_CurAnimAttackIndex == MinAnimAttackIndex)
        {
            //Debug.Log("START Reset");
            StartCoroutine("AttackPeriod");
        }
        else if (_CurAnimAttackIndex == MaxAnimAttackIndex)
        {
            StopCoroutine("AttackPeriod");
        }
        

        _CurAnimAttackIndex++; //attack more times


        //load animateor

        //calculate current index of attack
    }



    void CastSkillEnd() 
    {

        _IsPlaying = false;


        
        if (_CurAnimAttackIndex > MaxAnimAttackIndex) 
        {
            _CurAnimAttackIndex = MinAnimAttackIndex;
        }
        
        //_CurAnimAttackIndex = MinAnimAttackIndex;


        

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
    public void OnModifyFSvalue() 
    {
        //increase vage value
        joyStickInst.OnModifyFSV();
    } 







    #endregion




}
