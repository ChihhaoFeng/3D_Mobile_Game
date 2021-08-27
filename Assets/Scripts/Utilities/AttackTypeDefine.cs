using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace AttackTypeDefine 
{
    public delegate void NotifySkill();

    public enum eSkillBindType 
    {
        eEffectWorld,
        eEffectOwner,
        eDamageOwner,
    }

    public enum eTrigType 
    {
        eAuto = 0,
        eCondition,
    }


    public enum eSkillType 
    {
        eAttack = 0,
        eSkill1,
    }


    public enum eTrigSkillState 
    {
        eTrigBegin,
        eTrigEnd,
    }
    public class GameEvent : UnityEvent { };

    public class GameBtnEvent : UnityEvent <PointerEventData> { };


}