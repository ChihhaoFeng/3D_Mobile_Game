using UnityEngine;

public class GlobalHelper : MonoBehaviour
{
    public static GameObject FindGOByName(GameObject target, string targetname) 
    {
        if (null == target) 
        {
            return null;
        }
        GameObject resultGo = null;

        if (target.name.Equals(targetname) == true) 
        {
            return target;
        }
        for (var i = 0; i < target.transform.childCount; i++) 
        {
            var child = target.transform.GetChild(i).gameObject;
            if (child.name.Equals(targetname) == true)
            {
                return child;
            }
            else 
            {
                if (child.transform.childCount > 0) 
                {
                    resultGo = FindGOByName(child, targetname);
                    if (null != resultGo) 
                    {
                        return resultGo;
                    }
                }
            }
        }




        return null;
    } 
}
