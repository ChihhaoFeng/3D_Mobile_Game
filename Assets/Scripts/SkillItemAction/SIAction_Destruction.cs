using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIAction_Destruction : SIAction_BaseAction
{
    public override void TrigActor()
    {
        base.TrigActor();
        Destroy(gameObject);
    }
}
