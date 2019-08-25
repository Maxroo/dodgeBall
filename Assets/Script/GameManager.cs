using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public movement p2, p3, p4;
    public ball ball;
    public int turn = 0;
    public Text scoreText;
    public int p1Score = 0;
    public int p2Score = 0;
    public Vector3 p1startPos = new Vector3(-4, 1, 0);
    public Vector3 p2startPos = new Vector3(4, 1, 0);
    public Vector3 p3startPos = new Vector3(4, -3, 0);
    public Vector3 p4startPos = new Vector3(4, -3, 0);

    public Vector3 ballstartPos = new Vector3(0, 0, 0);
    bool endturnTransition;
    ScreenShake ss;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    float t = 0;

    void Start()
    {
        ss = this.GetComponent<ScreenShake>();
        Color32 red = new Color32(253, 87, 87, 255);
        Color32 blue = new Color32(74, 109, 226, 255);
        Color32 green = new Color32(10, 157, 86, 255);
        Color32 purple = new Color32(202, 88, 253, 255);
        colors.Add(blue);
        colors.Add(green);
        colors.Add(purple);
        colors.Add(red);
        CharacterManager cm = FindObjectOfType<CharacterManager>();
        if (cm != null)
        {
            p1.colorIndex = cm.p1colorIndex;
            p2.colorIndex = cm.p2colorIndex;
            p1.CharacterIndex = cm.p1characterIndex;
            p2.CharacterIndex = cm.p2characterIndex;
            p1.sr.sprite = cm.CharacterSet[p1.CharacterIndex][p1.colorIndex];
            p2.sr.sprite = cm.CharacterSet[p2.CharacterIndex][p2.colorIndex];
            p1.teamColor = colors[p1.colorIndex];
            p2.teamColor = colors[p2.colorIndex];

            p1.initStats();
            p2.initStats();

        }
        else if (cm == null)
        {
            MPCharacterManager MPcm = FindObjectOfType<MPCharacterManager>();
            p1startPos = new Vector3(-4, 3, 0);
            p2startPos = new Vector3(4, 3, 0);
            p1.colorIndex = MPcm.p1colorIndex;
            p2.colorIndex = MPcm.p2colorIndex;
            p3.colorIndex = MPcm.p3colorIndex;
            p4.colorIndex = MPcm.p4colorIndex;
            p1.CharacterIndex = MPcm.p1characterIndex;
            p2.CharacterIndex = MPcm.p2characterIndex;
            p3.CharacterIndex = MPcm.p3characterIndex;
            p4.CharacterIndex = MPcm.p4characterIndex;
            p1.sr.sprite = MPcm.CharacterSet[p1.CharacterIndex][p1.colorIndex];
            p2.sr.sprite = MPcm.CharacterSet[p2.CharacterIndex][p2.colorIndex];
            p3.sr.sprite = MPcm.CharacterSet[p3.CharacterIndex][p3.colorIndex];
            p4.sr.sprite = MPcm.CharacterSet[p4.CharacterIndex][p4.colorIndex];
            p1.teamColor = colors[p1.colorIndex];
            p2.teamColor = colors[p2.colorIndex];
            p3.teamColor = colors[p3.colorIndex];
            p4.teamColor = colors[p4.colorIndex];

            p1.initStats();
            p2.initStats();
            p3.initStats();
            p4.initStats();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(endturnp4());
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            p1Score++;
        }
        if (endturnTransition)
        {
            t += Time.deltaTime;
            p1.transform.position = new Vector2(Mathf.Lerp(p1.transform.position.x, p1startPos.x, t),
            Mathf.Lerp(p1.transform.position.y, p1startPos.y, t));
            p2.transform.position = new Vector2(Mathf.Lerp(p2.transform.position.x, p2startPos.x, t),
            Mathf.Lerp(p2.transform.position.y, p2startPos.y, t));
            if (p3 != null)
            {
                p3.transform.position = new Vector2(Mathf.Lerp(p3.transform.position.x, p3startPos.x, t),
                Mathf.Lerp(p3.transform.position.y, p3startPos.y, t));
                p4.transform.position = new Vector2(Mathf.Lerp(p4.transform.position.x, p4startPos.x, t),
                Mathf.Lerp(p4.transform.position.y, p4startPos.y, t));

            }
            ball.transform.position = new Vector2(Mathf.Lerp(ball.transform.position.x, ballstartPos.x, t),
            Mathf.Lerp(ball.transform.position.y, ballstartPos.y, t));
            if (t >= 1)
            {
                p1.rb2D.velocity = Vector2.zero;
                p2.rb2D.velocity = Vector2.zero;
                if (p3 != null)
                {
                    p3.rb2D.velocity = Vector2.zero;
                    p4.rb2D.velocity = Vector2.zero;

                }

                ball.rb2D.velocity = Vector2.zero;

                gameMode = Status.game;
                endturnTransition = false;
            }
        }
    }
    public void checkEndturn(movement player)
    {
        ss.shake = 0.5f;

        if (p3 != null && p4 != null)
        {
            if (player == p1 || player == p3)
            {
                if (p3.isDead == true && p1.isDead == true)
                {
                    p2Score++;
                }
            }

            if (player == p2 || player == p4)
            {
                if (p2.isDead == true && p4.isDead == true)
                {
                    p1Score++;
                }
            }

            if (p2Score >= 11 || p1Score >= 11)
            {
                StartCoroutine(endGame());
            }
            else
            {
                StartCoroutine(endturnp4());
            }

        }
        else
        {
            if (player == p1)
            {
                p2Score++;
            }
            else
            {
                p1Score++;
            }
            if (p2Score >= 11 || p1Score >= 11)
            {
                StartCoroutine(endGame());
            }
            else
            {
                StartCoroutine(endturn());
            }
        }

    }
    public IEnumerator endturn()
    {
        scoreText.text = p1Score + "     " + p2Score;
        scoreTextCheck();
        t = 0;
        turn++;
        gameMode = Status.nextTurn;
        yield return new WaitForSecondsRealtime(1f);
        endturnTransition = true;
        p1.revive();
        p2.revive();
        ball.restart();
        yield return null;
    }
    public IEnumerator endturnp4()
    {
        scoreText.text = p1Score + "    " + p2Score;
        scoreTextCheck();
        t = 0;
        turn++;
        gameMode = Status.nextTurn;
        yield return new WaitForSecondsRealtime(1f);
        endturnTransition = true;
        p1.revive();
        p2.revive();
        p3.revive();
        p4.revive();
        ball.restart();
        yield return null;
    }

    public IEnumerator endGame()
    {
        gameMode = Status.nextTurn;
        yield return new WaitForSecondsRealtime(2f);
        if (p1Score >= 11)
        {
            scoreText.text = "Team 1 wins";
        }
        else
        {
            scoreText.text = "Team 2 wins";
        }
        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadScene("MainMenu");

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

    void scoreTextCheck()
    {
        if (p1Score > 9 && p2Score > 9)
        {
            scoreText.text = p1Score + "   " + p2Score;
        }
        else if (p1Score > 9)
        {
            scoreText.text = p1Score + "    " + p2Score;

        }
        else if (p2Score > 9)
        {
            scoreText.text = p1Score + "    " + p2Score;
        }
    }

}
