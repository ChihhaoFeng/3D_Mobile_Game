using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


#region System Functon
    void Start()
    {
        AnimCtrlInst = GetComponent<AnimCtrl>();
        Cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove())
        {
            SetPlayerAnimMoveParameter();
        }
    }
    #endregion
    #region Player Animation Controller
    private AnimCtrl AnimCtrlInst;
    public Animator Anim;
    public CharacterController CharCtrl;
    public float MoveSpeed;
    public UI_JoyStick JoyStick;


    float horizontal;
    float vertical;
    float speed;
    float s1;
    float s2;

    Camera Cam;
    bool CanMove() 
    {
        if (AnimCtrlInst.IsPlaying) 
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    void SetPlayerAnimMoveParameter() 
    {
#if UNITY_EDITOR
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        s1 = Mathf.Sqrt(horizontal * horizontal + vertical * vertical);  
        s2 = null != JoyStick ? JoyStick.Dir.magnitude : 0;                 // if JoyStick != null s2 =  JoyStick.Dir.magnitude else s2 = 0

        speed = s1 > s2 ? s1 : s2;       //if current input is keyboard s1>s2       else current input is joystick s2 > s1

        if (s2 > s1) //joystick input
        {
            horizontal = JoyStick.Dir.x;
            vertical = JoyStick.Dir.y;
        } 

#else
        speed =  JoyStick.Dir.magnitude;
#endif

        Anim.SetFloat("IdleAndRun",speed);

        if (speed > 0.01f) 
        {
            PlayerControlMovement(horizontal, vertical);
        }

    }

    void PlayerControlMovement(float x , float z) 
    {
        var dir = x * Cam.transform.right + z * Cam.transform.forward;

        dir.y = 0f;


        transform.forward = dir;
        CharCtrl.Move(MoveSpeed * Time.deltaTime * dir);
    }

    #endregion


}
