using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //Turning On and Off the Sword Collision box according to the percentage of character's attack animation.
    BoxCollider BC;

    Animator Anim;
    float StartPer;
    float EndPer;
    float CurrPer;
    float LastPer;
    AnimatorStateInfo StateInfo;

    AnimCtrl AnimCtrlInst;

    public void OnStart(AnimCtrl AC) 
    {
        AnimCtrlInst = AC;
    }


    private void Start()
    {
        BC = GetComponent<BoxCollider>();
        BC.enabled = false;                           //conceal the object
    }


    #region Functions

    public void OnStartWeaponCtrl(Animator _Anim, float Startpercentage, float Endpercentage)
    {
        StartPer = Startpercentage;
        EndPer = Endpercentage;
        Anim = _Anim;

        StopAllCoroutines();
        // detect the percentage of the current animation
        StartCoroutine(WaitToPlayAnim());

    }


    IEnumerator WaitToPlayAnim() 
    {
        CurrPer = StateInfo.normalizedTime % 1.0f;

        while (true) 
        {
            StateInfo = Anim.GetCurrentAnimatorStateInfo(0);
            CurrPer = StateInfo.normalizedTime % 1.0f;
            if (CurrPer >= StartPer && LastPer < StartPer)
            {
                BC.enabled = true;
            }
            else if(CurrPer > EndPer && LastPer <= EndPer)
            {
                BC.enabled = false;
                break;
            }

            LastPer = CurrPer;
            yield return null;

        } 

       
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyActor = other.gameObject.GetComponent<SkeletonActor>();

        if (enemyActor != null) 
        {
            enemyActor.GetHit();

            //player increase rage value
            AnimCtrlInst.OnModifyFSvalue();

        }
    }

    #endregion






}
