using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    [HideInInspector]
    public Color32 teamColor = Color.blue;
    [HideInInspector]
    public SpriteRenderer sr;
    [HideInInspector]
    public int colorIndex,CharacterIndex;
    [HideInInspector]
    public string x = "Horizontal", y = "Vertical", throwKey = "Throw", catchKey = "Catch";
    [HideInInspector]
    public Vector2 screenBounds;
    [HideInInspector]
    public Vector3 input = new Vector3(), forward, direction, targetPosition = new Vector3();
    [HideInInspector]
    public float speed = 0.2f, catchTime = 0.5f, holdTimer = 2, force = 20f,max, min;
    [HideInInspector]
    public GameObject ball, face;
    [HideInInspector]
    public bool catching, inCoolDown, hasBall;
    [HideInInspector]
    public Rigidbody2D rb2D;
    [HideInInspector]
    public GameManager GameManagerCopy;
    public fillTest ballTimer;

    void Start()
    {
        GameManagerCopy = GameManager.instance;
        face = transform.GetChild(0).gameObject;
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.mass = 0.02f;
        rb2D.drag = 2;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        customStart();
    }

    public virtual void customStart(){
        max = 0;
        min = -screenBounds.x;
    }

    // Update is called once per frame
    public void Update()
    {
        if (GameManagerCopy.gameMode == Status.game)
        {
            input.x = Input.GetAxis(x);
            input.y = Input.GetAxis(y);
            targetPosition = transform.position + input * speed;
            targetPosition.x = Mathf.Clamp(targetPosition.x, min, max);
            targetPosition.y = Mathf.Clamp(targetPosition.y, screenBounds.y * -1, screenBounds.y);
            transform.position = targetPosition;
            if (input.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (input.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            ballTimer.hasBall = hasBall;

            if (hasBall)
            {
                ball.transform.position = face.transform.position;
                if (Input.GetButtonDown(throwKey))
                {
                    shoot();
                }
            }
            if (Input.GetButtonDown(catchKey) && !inCoolDown)
            {
                StartCoroutine(catchingIEnumerator());
            }

        }
    }




    public IEnumerator catchingIEnumerator()
    {
        inCoolDown = true;
        catching = true;
        yield return new WaitForSecondsRealtime(catchTime);
        catching = false;
        yield return new WaitForSecondsRealtime(catchTime);
        inCoolDown = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManagerCopy.gameMode == Status.game)
        {

            if (other.gameObject.tag == "ball")
            {
                ball = other.gameObject;
                ball ballscript = ball.GetComponent<ball>();
                if (ballscript.shooting == false && ballscript.hasParent == false)
                {
                    catched(ballscript);
                }
                else if (ballscript.shooting && ballscript.parent != this.face)
                {
                    if (catching)
                    {
                        Vector2 drawBack = ballscript.rb2D.velocity;
                        rb2D.velocity = drawBack / 2;
                        catched(ballscript);
                    }
                    else
                    {

                        if (this.gameObject.tag == "p1")
                        {
                            GameManagerCopy.p2Score++;

                        }
                        else if (this.gameObject.tag == "p2")
                        {
                            GameManagerCopy.p1Score++;
                        }
                        Vector2 drawBack = ballscript.rb2D.velocity;
                        rb2D.velocity = drawBack;
                        ballscript.rb2D.velocity = drawBack / 3;
                        StartCoroutine(GameManagerCopy.endturn());
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }

                }
            }

        }
    }

    void catched(ball ballscript)
    {
        ballscript.sr.color = teamColor;
        ballscript.catchable = false;
        ballscript.rb2D.velocity = new Vector2(0, 0);
        ballscript.shooting = false;
        hasBall = true;
        ballscript.parent = face;
        ballscript.hasParent = true;
    }
     public void shoot()
    {
        hasBall = false;
        ball ballscript = ball.GetComponent<ball>();
        Vector3 forward = input;
        if (forward.y > 0)
        {
            forward.y = 1;
        }
        else if (forward.y < 0)
        {
            forward.y = -1;
        }
        if (transform.rotation.y == -1)
        {
            forward.x = 1;
        }
        else if (transform.rotation.y == 0)
        {
            forward.x = -1;
        }
        if (forward.x > 0)
        {
            forward.x = 1;
        }
        else if (forward.x < 0)
        {
            forward.x = -1;
        }
      
        ballscript.shoot(force, forward);
    }

    public void initStats()
    {
        switch (colorIndex)
        {
            default:
            case 0:
                break;
            case 1:
                speed = 0.3f;
                force = 15f;
                break;
            case 2:
                force = 25f;
                speed = 0.10f;
                break;
        }
    }
}
