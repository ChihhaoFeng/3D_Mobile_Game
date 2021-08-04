using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_JoyStick : MonoBehaviour
{
    private void Start()
    {
        FinalSkillBtnInst.SetFinalSkillState(ShowFinalSkillBtn);
    }





    #region JoyStick
    public CommonJoyButton CommonBtn;

    public Vector3 Dir => (CommonBtn.Dir);

    #endregion


    #region Rage Slide

    public Slider SliderInst;
    int RageIncrease = 50;
    public Image HighLight1;
    public Image HighLight2;
    public bool ShowFinalSkillBtn => (SliderInst.value > 100);


    public void OnModifyFSV() 
    {
        var RageValue = SliderInst.value;

        SliderInst.value += RageIncrease;
        if (SliderInst.value >= 200)
        {
            HighLight2.enabled = true;
        }
        else if (SliderInst.value >= 100) 
        {
            HighLight1.enabled = true;
        }


        FinalSkillBtnInst.SetFinalSkillState(ShowFinalSkillBtn);

    }




    #endregion

    #region Final Skill

    public FinalSkillBtn FinalSkillBtnInst;




    #endregion


}
