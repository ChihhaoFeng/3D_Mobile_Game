using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SphereCtrl : MonoBehaviour
{
    public float Speed;
    public float Duration;
    bool IsTrigger = false;
    float StartTime = 0f;
    string BulletPath = "Turs/Tur4/Bullet";

    public float MinWait;
    public float MaxWait;


    Transform PlayerInst;

    public void OnStart(Transform player) 
    {
        PlayerInst = player;
        StartCoroutine(FireBullet());
        
    }


    IEnumerator FireBullet() 
    {   

        while (!IsTrigger) 
        {
            var dur = Random.Range(MinWait, MaxWait);
            yield return new WaitForSeconds(dur);

            //Play a short animate
            transform.DOShakeScale(1,1,2);
            transform.GetComponent<MeshRenderer>().material.DOColor(
                new Color(
                    Random.Range(0f,1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                    )

                ,1f);

            //End of the animate
            yield return new WaitForSeconds(1.0f);

            //Onload the Bullet
            var obj = Resources.Load(BulletPath);
            var bullet = Instantiate(obj, transform.position, Quaternion.identity) as GameObject;
            bullet.transform.forward = (PlayerInst.position - transform.position).normalized;   // get the direction
            var bulletCtrl = bullet.GetComponent<Bullet>();
            bulletCtrl.OnStart();


        }

    }



    private void Update()
    {

        if (IsTrigger) 
        {
            if (Time.time - StartTime > Duration)
            {
                StopAllCoroutines();
                TestSphereMgr.Inst.Remove(this);
                return;
            }
            else
            {
                transform.position += Vector3.up * Time.deltaTime * Speed;
            }
        }
        

    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("HIT");
        TriggerSphereUpside();


    }
    void TriggerSphereUpside() 
    {
        IsTrigger = true;
        StartTime = Time.time;
    }



}
