using UnityEngine.Events;

namespace AttackTypeDefine 
{
    public delegate void NotifySkill();

    public enum eTrigSkillState 
    {
        eTrigBegin,
        eTrigEnd,
    }
    public class GameEvent : UnityEvent { };

    public class GameBtnEvent : UnityEvent <PointEventData> { };




}