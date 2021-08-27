using UnityEngine;
using AttackTypeDefine;
public class SIAction_BaseAction : MonoBehaviour
{

    public eTrigType TrigType;
    public float Duration;
    float StartTime = 0f;

    bool IsTriggered = false;

    void Start()
    {

        if (TrigType == eTrigType.eAuto) 
        {
            StartTime = Time.time;
            IsTriggered = true;
        }

    }
    public void OnStart() 
    {

        if (TrigType == eTrigType.eCondition)
        {
            StartTime = Time.time;
            IsTriggered = true;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (!IsTriggered) 
        {
            return;
        }

        if (Time.time - StartTime >= Duration)
        {
            IsTriggered = false;
            TrigActor();
        }
    }


    public virtual void TrigActor() 
    {
        
    }
}
