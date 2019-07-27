using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    public SpriteRenderer sr;
    public playerCharacter character;
    public string x = "Horizontal";
    public string y = "Vertical";
    public string throwKey = "Throw";
    public string catchKey = "Catch";
    public Vector2 screenBounds;
    public Vector3 input = new Vector3();
    public float speed = 0.2f;
    public bool hasBall;
    public GameObject ball;
    public GameObject face;
    public Vector3 direction;
    public Vector3 forward;
    public bool catching, inCoolDown;
    public Rigidbody2D rb2D;
    public float force = 20f;
    public Vector3 targetPosition = new Vector3();
    public GameManager GameManagerCopy;
    public float catchTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        GameManagerCopy = GameManager.instance;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.mass = 0.02f;
        rb2D.drag = 2;

     

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (GameManagerCopy.gameMode == Status.game)
        {
            input.x = Input.GetAxis(x);
            input.y = Input.GetAxis(y);
            face = transform.GetChild(0).gameObject;
            targetPosition = transform.position + input * speed;
            targetPosition.x = Mathf.Clamp(targetPosition.x, screenBounds.x * -1, 0);
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


    public void shoot()
    {
        hasBall = false;
        ball ballscript = ball.GetComponent<ball>();
        ballscript.hasParent = false;
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
        if(forward.x > 0){
            forward.x = 1;
        }
        else if(forward.x < 0){
            forward.x = -1;
        }
        print(force);
        print(forward);
        ballscript.shoot(force, forward);
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
                else if(ballscript.shooting)
                {
                    if (catching)
                    {
                        Vector2 drawBack = ballscript.rb2D.velocity;
                        rb2D.velocity = drawBack / 2;
                        catched(ballscript);
                    }
                    else
                    {
                        
                        if(this.gameObject.tag == "p1"){
                            GameManagerCopy.p2Score ++;
 
                        }else if(this.gameObject.tag == "p2")
                        {
                            GameManagerCopy.p1Score ++;
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
        ballscript.rb2D.velocity = new Vector2(0, 0);
        ballscript.shooting = false;
        hasBall = true;
        ballscript.parent = face;
        ballscript.hasParent = true;
    }

    public void initStats(){
          switch(character){
            default:
            case playerCharacter.a:
            break;
            case playerCharacter.b:
            speed = 0.3f;
            force = 15f;
            break;
            case playerCharacter.c:
            force = 25f;
            speed = 0.10f;
            break;
        }
    }
}
