using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Status
{
    game,
    transition,
    nextTurn
}

public class GameManager : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    public int playerNumber = 2;
    public Status gameMode = Status.game;
    public static GameManager instance;
    public movement p1;
    public movement p2;
    public ball ball;
    public int turn = 0;
    public Text scoreText;
    public int p1Score = 0;
    public int p2Score = 0;
    public Vector3 p1startPos = new Vector3(-4, 1, 0);
    public Vector3 p2startPos = new Vector3(4, 1, 0);
    public Vector3 ballstartPos = new Vector3(0, 0, 0);
    bool endturnTransition;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    float t = 0;

    void Start()
    {
        Color32 red = new Color32(253, 87, 87, 255);
        Color32 blue = new Color32(74, 109, 226, 255);
        Color32 green = new Color32(10, 157, 86, 255);
        Color32 purple = new Color32(202, 88, 253, 255);
        colors.Add(blue);
        colors.Add(green);
        colors.Add(purple);
        colors.Add(red);
        CharacterManager cm = FindObjectOfType<CharacterManager>();
        p1.colorIndex = cm.p1colorIndex;
        p1.CharacterIndex = cm.p1characterIndex;
        p2.colorIndex = cm.p2colorIndex;
        p2.CharacterIndex = cm.p2characterIndex;
        p1.sr.sprite = cm.CharacterSet[p1.CharacterIndex][p1.colorIndex];
        p2.sr.sprite = cm.CharacterSet[p2.CharacterIndex][p2.colorIndex];
        p1.teamColor = colors[p1.colorIndex];
        p2.teamColor = colors[p2.colorIndex];
        p1.initStats();
        p2.initStats();
    }
    void Update()
    {
        scoreText.text = p1Score + "   " + p2Score;
        if (endturnTransition)
        {
            t += Time.deltaTime;
            p1.transform.position = new Vector2(Mathf.Lerp(p1.transform.position.x, p1startPos.x, t),
            Mathf.Lerp(p1.transform.position.y, p1startPos.y, t));

            p2.transform.position = new Vector2(Mathf.Lerp(p2.transform.position.x, p2startPos.x, t),
            Mathf.Lerp(p2.transform.position.y, p2startPos.y, t));
            ball.transform.position = new Vector2(Mathf.Lerp(ball.transform.position.x, ballstartPos.x, t),
            Mathf.Lerp(ball.transform.position.y, ballstartPos.y, t));
            if (t >= 1)
            {
                p1.rb2D.velocity = Vector2.zero;
                p2.rb2D.velocity = Vector2.zero;
                ball.rb2D.velocity = Vector2.zero;

                gameMode = Status.game;
                endturnTransition = false;
            }
        }
    }
    public IEnumerator endturn()
    {
        t = 0;
        turn++;
        gameMode = Status.nextTurn;
        yield return new WaitForSecondsRealtime(1f);
        endturnTransition = true;

        yield return null;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
}
