using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerSelection : MonoBehaviour
{
    public Image image;
    public int index = 0;
    public int colorIndex = 0;
    public List<Image> ability = new List<Image>();
    public CharacterManager cm;


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    
    public void CustomStart()
    {
        cm = CharacterManager.instance;
        loadImage();
    }
    public void loadImage()
    {
        
        cm.colorBool[colorIndex] = true;
        image.sprite = cm.CharacterSet[index][colorIndex];
        switch (index)
        {
            case 0:
                for(int i = 0; i < 9; i++){
                    if(i == 2 || i == 5 || i == 8){
                        ability[i].enabled = false;
                    }else
                    {
                        ability[i].enabled = true;
                    }
                }
                break;
            case 1:
                for(int i = 0; i < 9; i++){
                    if(i == 5 || i == 4 || i == 8){
                        ability[i].enabled = false;
                    }else
                    {
                        ability[i].enabled = true;
                    }
               }
                break;
            case 2:
                for(int i = 0; i < 9; i++){
                    if(i == 2 || i == 1 || i == 8){
                        ability[i].enabled = false;
                    }else
                    {
                        ability[i].enabled = true;
                    }
                }
                break;
        }
    }
    public void left()
    {
        if (index == 0)
        {
            index = 2;
        }
        else
        {
            index--;

        }
        loadImage();
    }

    public void right()
    {
        if (index == 2)
        {
            index = 0;
        }
        else
        {
            index++;

        }
        loadImage();
    }

    public void colorChange(){
        colorIndex = cm.changeColor(colorIndex);
        loadImage();
    }
}
