using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Satisfy_c : MonoBehaviour
{
    static public int killed_b_disk;

    [SerializeField]
    public GameObject[] texts;
    int numb;

    //nice
    //good
    //great
    //perfect
    //excellent

    
    void Start()
    {
        
    }

    void Update()
    { 
        //get active if disk hit something and make killed_b_disk greater than zero
        if(killed_b_disk>0)
        {
            if(killed_b_disk>0 && killed_b_disk<=4)
            {
                texts[0].SetActive(true); 
                Debug.Log(killed_b_disk);
            }

            if(killed_b_disk>4 && killed_b_disk<=8)
            { 
                texts[1].SetActive(true); 
                Debug.Log(killed_b_disk);
            } 

            if(killed_b_disk>8 && killed_b_disk<=12)
            {
                texts[2].SetActive(true); 
                Debug.Log(killed_b_disk);
            }

            if(killed_b_disk>12 && killed_b_disk<=16)
            {
                texts[3].SetActive(true); 
                Debug.Log(killed_b_disk);
            }

            if(killed_b_disk>16)
            {
                texts[4].SetActive(true); 
                Debug.Log(killed_b_disk);
            } 
        } 
    } 
}
