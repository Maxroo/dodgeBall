﻿
using UnityEngine;
using UnityEngine.SceneManagement;


public class scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void characterselection(){
        SceneManager.LoadScene("characterSelection");
    }
     public void characterselectionp4(){
        SceneManager.LoadScene("characterSelection2");
    }
    public void quit(){
        Application.Quit();
    }
}
