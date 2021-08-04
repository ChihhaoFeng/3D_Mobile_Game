using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tur1 : MonoBehaviour
{
    public Animator Anim;

    private void Awake()
    {
    }


    public void Attack() 
    {
        Anim.SetTrigger("Base Layer.Attack");
    }
    public void Jump()
    {
        Anim.SetTrigger("Base Layer.Jump");
    }
    public void Idle()
    {
        Anim.SetTrigger("Base Layer.Idle");
    }




}
