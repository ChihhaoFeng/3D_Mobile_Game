using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackTypeDefine;

public class AnimatorManger : MonoBehaviour
{
    NotifySkill SkillReadyInst;
    AnimCtrl AnimInst;
    StateMachine StateInst;

    public void OnStart(AnimCtrl animinst) 
    {
        AnimInst = animinst;
        StateInst = AnimInst.Anim.GetBehaviour<StateMachine>();
    }

    public void StartAnimation(string AnimName, NotifySkill SkillReady, NotifySkill SkillBegin, NotifySkill SkillEnd, NotifySkill SkillEnd1) 
    {
        AnimInst.Anim.SetTrigger(AnimName);

        SkillReadyInst = SkillReady;

        //clear all call back 
        StateInst.ClearAllCallBacks();

        StateInst.RegisterCallBack(eTrigSkillState.eTrigBegin, SkillBegin);

        StateInst.RegisterCallBack(eTrigSkillState.eTrigEnd, () => {              //Call Back the next one

            if (null != SkillEnd1) 
            {
                SkillEnd1();
            }
            this.InvokeNextFrame(() =>
            {
                StateInst.RegisterCallBack(eTrigSkillState.eTrigEnd, SkillEnd);
            });
            



        });
    }

    void EventSkillReady()
    {
        SkillReadyInst();
    }

}
