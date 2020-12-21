using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disk : MonoBehaviour
{
    public Transform target;
    public bool go=false;
    public Rigidbody rb;
    public float time;
    public float radius;
    public float power;





    void FixedUpdate()
    {
        target= GameObject.FindGameObjectWithTag("Player").transform;

        if(!go)
        {
            //look at the charr_ so you can go opposite of it
            if (Vector3.Distance(transform.position,target.transform.position)>=12)
            {
                transform.LookAt(new Vector3(target.transform.position.x,transform.position.y,target.position.z));
            }
        }

        if(go)
            rb.AddRelativeForce(0,0,-30f,ForceMode.Impulse);
    }



    int killed=0; 
    bool timer_protector=false;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) 
        { 
            StartCoroutine(kicked_(time));
        }        

        if(other.CompareTag("person"))
        {
            killed+=2;

            if(!timer_protector)
            {
                StartCoroutine(killed_timer(0.05f));
                timer_protector=true;
            }
        }
    }

    IEnumerator kicked_(float time)
    {
        yield return new WaitForSeconds(time); 
        go=true;
    }

    IEnumerator killed_timer(float time)
    {
        yield return new WaitForSeconds(time); 
        Debug.Log("isOK");
        Satisfy_c.killed_b_disk=killed; 
    }
}
