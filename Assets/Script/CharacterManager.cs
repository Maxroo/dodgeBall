using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public List<bool> colorBool = new List<bool>();
    public int p1colorIndex;
    public int p2colorIndex;
    public int p1characterIndex;
    public int p2characterIndex;
    public List<Sprite> characterA = new List<Sprite>();

    public List<Sprite> characterB = new List<Sprite>();

    public List<Sprite> characterC = new List<Sprite>();

    public List<List<Sprite>> CharacterSet = new List<List<Sprite>>();
    public int p1Index;
    public int p2Index;
    public playerSelection p1;
    public playerSelection p2;
    public static CharacterManager instance;

    // Start is called before the first frame upda
    void Awake()
    {

        if (instance)
        {
            Destroy(this);
        }
        instance = this;
    }
    void Start()
    {
        CharacterSet.Add(characterA);
        CharacterSet.Add(characterB);
        CharacterSet.Add(characterC);
        p1.myStart();
        p2.myStart();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void gameScene()
    {
        p1characterIndex = p1.index;
        p2characterIndex = p2.index;
        p1colorIndex = p1.colorIndex;
        p2colorIndex = p2.colorIndex;
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.LoadScene("game");
    }

    public int changeColor(int index)
    {
        int newIndex = index;
        newIndex++;
        while (newIndex != index)
        {
            if (newIndex > colorBool.Count - 1)
            {
                newIndex = 0;
            }
            if (colorBool[newIndex] == false)
            {
                break;
            }
            newIndex++;
        }
        colorBool[index] = false;
        return newIndex;
    }
}
