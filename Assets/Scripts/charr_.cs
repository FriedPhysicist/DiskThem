using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class charr_ : MonoBehaviour
{
    public Rigidbody rb;
    Touch touch;
    Input input;
    float x_axis;
    public int speed;
    public static Vector3 charr_scale=new Vector3(0.7f,0.7f,0.7f);
    public float percent;
    public ParticleSystem ps;
    public Animator anim;
    public AudioSource _as;
    [SerializeField]
    public AudioClip[] _ac;
    public ParticleSystem celeb;

    public static int total_knock_out=0;
    static public int total_knock_out_ui=0;
    public TMP_Text score;
    
    public Transform Start_;
    public Transform Final_; 
    public float final_court_distance;
    bool final_court_bool=false;
    float step;

    public ParticleSystem left_engine;
    public ParticleSystem right_engine;

    public static int last_score;
    public ParticleSystem cloud;
    public Slider fuel;







    void Start()
    {
        final_court_distance=Final_.position.z-Start_.position.z;
        step=final_court_distance/100;
    }



    int half_road;

    void Update()
    { 
        x_axis=Input.GetAxis("Horizontal");
        rb.isKinematic=anim.GetCurrentAnimatorStateInfo(0).IsName("kick") || anim.GetCurrentAnimatorStateInfo(0).IsName("dance")? true:false;
        anim.SetBool("fly",final_court_bool && total_knock_out>0); 

        canvas_();

        if(final_court_bool)
        { 
            left_engine.gameObject.SetActive(true);
            right_engine.gameObject.SetActive(true); 
        }

        if (false)
        {
            x_axis = Input.touches[0].deltaPosition.x/100; 
        } 
    }   


    public float turn_multiplier;
    bool finish=false;
    Vector3 start_point;
    float end_point;

    void FixedUpdate()
    { 
        //normal game start here
        if(!final_court_bool)
        { 
            normal_road();

            //get these value end of normal road to use final road
            half_road=total_knock_out/2;
            start_point=transform.position;
            end_point=start_point.z+step*total_knock_out; 
        }
        
        //last road start here
        if(final_court_bool && total_knock_out>0)
        { 
            final_road();
        }

        //end of the game
        if(final_court_bool && total_knock_out<=0)
        { 
            transform.rotation=Quaternion.Euler(0,180,0); 

            if(total_knock_out_ui<=last_score)
            {
                total_knock_out_ui++; 
            }
        } 
    }



    void normal_road()
    {
        rb.velocity=new Vector3(x_axis*speed/4, rb.velocity.y,speed);

        while(x_axis!=0)
        {
            transform.rotation=Quaternion.Euler(transform.rotation.x+Mathf.Abs(x_axis)*turn_multiplier,transform.rotation.y+x_axis*turn_multiplier,0); 
            break;
        }
        
        while(x_axis!=0 && !for_once)
        {
            transform.rotation=Quaternion.Euler(0,0,0); 
            break;
        }
    } 



    void final_road()
    {
        transform.rotation=Quaternion.Euler(40,transform.rotation.y,transform.rotation.z);

        if(!_as.isPlaying) sfx_play(_ac[0]);

        rb.velocity=new Vector3(x_axis*speed/4, rb.velocity.y,rb.velocity.z);
        
        transform.position=Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,transform.position.y,end_point),1.2f);

                

        if(Vector3.Distance(transform.position,new Vector3(transform.position.x,transform.position.y,end_point))<=0.1f)
        { 
            total_knock_out=0;
            _as.volume=0.5f;
            sfx_play(_ac[2]);
            rb.isKinematic=true; 
            //Play confetti system
            celeb.Play();
            anim.SetBool("dance",true); 
        }

        while(x_axis!=0)
        {
            transform.rotation=Quaternion.Euler(40,transform.rotation.y+x_axis*turn_multiplier,transform.rotation.z); 
            break;
        }
    }


    float last_fuel;
    void canvas_()
    {
        score.text=total_knock_out_ui.ToString();

        Debug.Log(last_score);

        if(!final_court_bool)
        {
            if(total_knock_out_ui <total_knock_out) 
            {
                total_knock_out_ui++;
            }

            if(fuel.value<total_knock_out)
            {
                fuel.value++;
                last_fuel=fuel.value;
            }
        }

        if(final_court_bool && total_knock_out>0)
        {
            fuel.value=(end_point-transform.position.z)/step;
        }

        //end of the game
        if(final_court_bool && total_knock_out<=0)
        { 
            if(total_knock_out_ui<=last_score)
            {
                total_knock_out_ui++;
            }
        } 

    }



    string last_stage;
    bool for_once=true;

    void OnTriggerEnter(Collider other)
    {
        //when person touch to the character, character size reduce
        if(other.CompareTag("person"))
        {
            gameObject.transform.localScale=gameObject.transform.localScale*percent;
            ps.startSize=0.7f*percent;
            ps.Play();
        }
        
        if(other.CompareTag("disk") && for_once)
        {
            anim.SetTrigger("kick"); 
            rb.isKinematic=true;
            //look at the disk
            transform.LookAt(new Vector3(other.gameObject.transform.position.x,0,other.gameObject.transform.position.z)); 
            for_once=false;
            //turn into the running mode after 0.3f seconds
            StartCoroutine(turn_()); 
        }

        if(other.CompareTag("Start"))
        {
            final_court_bool=true;
        }


    }


    bool calc_protect=false;
    void OnTriggerStay(Collider other)
    {        
        if(final_court_bool && total_knock_out==0 && !calc_protect)
        {
            last_stage=other.gameObject.tag;
            last_score=las_poit_calc(last_stage,total_knock_out_ui);
        }
    }

    IEnumerator turn_()
    {
        yield return new WaitForSeconds(0.7f);
        for_once=true;
        transform.rotation=Quaternion.Euler(0,0,0);
    }

    public void sfx_play(AudioClip clip)
    { 
        _as.clip=clip;
        if(clip==_ac[1]) cloud.Play(); 
        _as.Play(); 
    }

    int las_poit_calc(string tag, int total_ui)
    {
        int final=0; 

        if(tag=="5X")
        {
            final=total_ui*5;
            calc_protect=true;
        }

        if(tag=="10X")
        {
            final=total_ui*10;
            calc_protect=true;
        }

        if(tag=="15X")
        {
            final=total_ui*15;
            calc_protect=true;
        }

        if(tag=="20X")
        {
            final=total_ui*20;
            calc_protect=true;
        }

        if(tag=="100X")
        {
            final=total_ui*100;
            calc_protect=true;
        }


        return final; 
    }
    

}
