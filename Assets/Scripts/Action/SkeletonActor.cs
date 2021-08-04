using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonActor : MonoBehaviour
{
    Animator Anim;


    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    public void GetHit()
    {
        Anim.SetTrigger("Base Layer.GetHit");
    }



}
