using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_ : MonoBehaviour
{
    public Transform charr_;
    Vector3 start_pos;
    public float vib_distance;
    public int vib_limit;
    public static int current_vib;


    // Start is called before the first frame update
    void Start()
    { 
        start_pos=transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {


        if (current_vib<=vib_limit)
        { 
            transform.localPosition=new Vector3(transform.localPosition.x+Random.Range(-vib_distance,vib_distance),transform.localPosition.y+Random.Range(-vib_distance,vib_distance),transform.localPosition.z+Random.Range(-vib_distance,vib_distance));
            current_vib++;

            if(current_vib>vib_limit)
            {
                transform.localPosition=start_pos;
                current_vib=vib_limit+2;
            }
        }
    }

    
    void FixedUpdate()
    {
        transform.position=charr_.position; 
    }
}
