using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSkillBtn : CommonJoyButton
{
    public Color NormalColor;
    public Color DisableColor;
    public CanvasGroup CanvasGPInst;


    public void SetFinalSkillState(bool on) 
    {
        CanvasGPInst.blocksRaycasts = on;      // make the btn unfunction

        ImageBackGround.color = on == true ? NormalColor : DisableColor;
        ImageHandle.color = on == true ? NormalColor : DisableColor;
        // two lines above are same function as below
        /*
        if (on)
        {
            ImageBackGround.color = NormalColor;
            ImageHandle.color = NormalColor;
        }
        else
        {
            ImageBackGround.color = DisableColor;
            ImageHandle.color = DisableColor;

        }
        */
    } 

}
