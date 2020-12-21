using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_ : MonoBehaviour
{
    public AudioSource _as;


    
    void Update()
    {
        transform.Rotate(0,3,0);
    }

    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _as.Play();
            charr_.total_knock_out_ui+=1;
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled=false;
            Destroy(gameObject,1f);
        }
    }
}
