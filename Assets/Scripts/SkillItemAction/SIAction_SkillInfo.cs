using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackTypeDefine;

public class SIAction_SkillInfo : SIAction_BaseAction
{
    [HideInInspector]
    public eSkillBindType SkillBindType;
    [HideInInspector]
    public string ObjName;

    public override void TrigActor()
    {
        Destroy(gameObject);
    }
    public void SetOwner(GameObject Owner) 
    {
        SIAction_DataStore[] ses = gameObject.GetComponentsInChildren<SIAction_DataStore>();

        for (var i = 0; i < ses.Length; i++) 
        {
            ses[i].Owner = Owner;
        }
    }



}
