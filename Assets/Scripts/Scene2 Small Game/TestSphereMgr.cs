using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphereMgr : MonoBehaviour
{


    #region Parameters
    public Vector3 Areasize;

    public int SphereNum;
    public GameObject SpherePrefab;
    List<SphereCtrl> SphereList;

    public Transform PlayerInst;

    public static TestSphereMgr Inst =>(_inst);
    private static TestSphereMgr _inst;



    private void Awake()
    {
        SphereList = new List<SphereCtrl>();

        _inst = this;

        for (var i = 0; i < SphereNum; i++) 
        {
            Add();
        }        
    }



    #endregion
    SphereCtrl CreateSphere() 
    {
        var pos = transform.position + 0.5f * new Vector3 (
                Random.Range(Areasize.x*-1, Areasize.x),
                0,
                Random.Range(Areasize.x * -1, Areasize.z)

            );

        var sphere = Instantiate(SpherePrefab,pos,Quaternion.identity);
        return sphere.GetComponent<SphereCtrl>();
    }

    void Add() 
    {
        var sphere = CreateSphere();

        SphereList.Add(sphere);

        sphere.OnStart(PlayerInst);

    }
    public void Remove(SphereCtrl obj) 
    {
        if (null == obj) 
        {
            Debug.Log("Fatal Error: Iput pam is illegal");
            return;
        }
        if (SphereList.Contains(obj)) 
        {
            var tmp = SphereList.Remove(obj);
            Destroy(obj.gameObject);
        }




    }



    private void OnDrawGizmos()       //define a range 
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, Areasize);




    }


}
