using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_canvas_object : MonoBehaviour
{

    void Start()
    { 
        //close this after animation end
        StartCoroutine(close_it(1.90f));
    }


    void Update()
    { 
        StartCoroutine(close_it(1.90f));
    }

    IEnumerator close_it(float time)
    {
        yield return new WaitForSeconds(time); 
        gameObject.SetActive(false);
        Satisfy_c.killed_b_disk=0;
    }
}
