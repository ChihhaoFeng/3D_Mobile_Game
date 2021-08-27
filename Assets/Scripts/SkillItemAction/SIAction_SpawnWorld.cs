using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIAction_SpawnWorld : SIAction_BaseAction
{
    SIAction_DataStore se;
    
    public GameObject EffectSpawnInst;

    public string SocketName;

    public float EffectDestroyDelay;

    public Vector3 Offset;
    public Vector3 OffRot;

    GameObject Owner;


    public override void TrigActor()
    {
        se = GetComponent<SIAction_DataStore>();

        Owner = se.Owner;
        //find the object on the sword
        var socket = GlobalHelper.FindGOByName(Owner, SocketName);

        if (socket == null)
        {
            socket = Owner;
        }

        //spawn effect

        var effect = Instantiate(EffectSpawnInst);

        var des = effect.GetComponent<SIAction_Destruction>();
        if (null != des) 
        {
            des.Duration = EffectDestroyDelay;
            des.OnStart();
        }
        
        // let the effect follow the position of sword
        effect.transform.position = socket.transform.position;
        effect.transform.Translate(Offset, Space.Self);

        effect.transform.rotation = socket.transform.rotation;
        effect.transform.Rotate(OffRot, Space.Self);

    }
}
