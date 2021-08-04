using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    public float Speed;
    public float Duration;
    private bool IsTriggered = false;
    float StartTime = 0f;


    // Start is called before the first frame update
    public void OnStart()
    {
        IsTriggered = true;
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTriggered)
        {
            return;
        }
        if (Time.time - StartTime > Duration)
        {
            Destroy(gameObject);
        }
        else 
        {
            transform.position += transform.forward * Time.deltaTime * Speed;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Movement>() != null) 
        {
            //Player Hurt Animation

            //destroy bullet
            Destroy(gameObject);
        }
    }


}
