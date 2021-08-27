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
    }   // find the gameobject in hierarchy

    public static GameObject InstantiateMyPrefab(string path, Vector3 pos, Quaternion rot ) 
    {
        var obj = Resources.Load(path);

        var go = Object.Instantiate(obj) as GameObject;

        go.name = obj.name; //rename it, becasue when you instantiate it, it will have a prefix as clone

        go.transform.position = pos;
        go.transform.rotation = rot;
        go.transform.localScale = Vector3.one;

        return go;
    }
}
