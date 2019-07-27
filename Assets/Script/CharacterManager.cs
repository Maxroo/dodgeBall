using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public int p1Index;
    public int p2Index;
    public List<Sprite> character = new List<Sprite>();
    public playerSelection p1;
    public playerSelection p2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gameScene(){
        p1Index = p1.index;
        p2Index = p2.index;
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.LoadScene("game");
    }


}
