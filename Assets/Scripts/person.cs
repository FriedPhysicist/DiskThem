using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class person : MonoBehaviour
{
    public ParticleSystem ps;
    public Rigidbody rb;
    SkinnedMeshRenderer sm;
    GameObject target;
    Quaternion look_rot;
    Vector3 target_loc;
    Vector3 disk;

    public Animator anim;

    public AudioSource _as;
    [SerializeField]
    public AudioClip[] _ac;

    public bool death=false;







    void Start()
    {
        sm=transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>();

    }

    void FixedUpdate()
    { 
        target=GameObject.FindGameObjectWithTag("Player"); 
        anim.SetBool("death",death);

        if(!death)
        {
            while (Vector3.Distance(target.transform.position,transform.position)<=40 && sm.enabled)
            { 
                transform.LookAt(target.transform.position);
                anim.SetBool("jump",true);
    
                if(transform.position.y<3.0f) 
                {
                    transform.position+=transform.up*0.5f;
                }
    
                transform.position=Vector3.Lerp(transform.position,target.transform.position,0.05f);
                break;
            }
        }
    }



    void OnTriggerEnter(Collider other)
    { 
        if(sm.enabled)
        {
            if (other.CompareTag("disk") && !death)
            { 
                Destroy(gameObject,4f);

                transform.LookAt(new Vector3(other.gameObject.transform.position.x,other.gameObject.transform.position.y,other.gameObject.transform.position.z)); 

                if(transform.localPosition.x>0) rb.AddRelativeForce(5,0,-9,ForceMode.Impulse);
                if(transform.localPosition.x<0) rb.AddRelativeForce(-5,0,-9,ForceMode.Impulse);

                charr_.total_knock_out+=2; 

                gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color=Color.gray;

                StartCoroutine(kinematic_on(1f));

                if(Random.Range(0,101)>=50) 
                {           
                    _as.clip=_ac[0];
                    _as.Play();
                }

                death=true;
            }

            if(other.CompareTag("Player"))
            {
                sm.enabled=false;

                if(Random.Range(0,101)>=50) 
                {           
                    _as.clip=_ac[1];
                    _as.Play();
                }

                Destroy(gameObject,2f); 
            }
        } 
    }

    IEnumerator kinematic_on(float seconds)
    {
        yield return new WaitForSeconds(seconds);            
        rb.isKinematic=true;
    }
}
