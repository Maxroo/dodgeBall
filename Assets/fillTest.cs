using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class fillTest : MonoBehaviour
{
    public GameObject parent;
    public Image image;
    public bool hasBall;
    public float t = 1;
    movement movement;
    
    public bool overTime = false;
    // Start is called before the first frame update
    void Start()
    {
        print(parent.transform.position);
        movement = parent.GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = parent.transform.position + (new Vector3(-0.5f,0.7f,0));
        
        if(hasBall){
        image.enabled = true;
        t -= Time.deltaTime;
        image.fillAmount = t / 2;
            if(t < 0){
                movement.shoot();
            } 
        }else
        {
            image.enabled = false;
            t = movement.holdTimer;
        }
    }
}
