using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : movement
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        x = "p2Horizontal";
        y = "p2Vertical";
        throwKey = "p2Throw";
        catchKey = "p2Catch";
    }
    public override void Update()
    {
        if (GameManagerCopy.gameMode == Status.game)
        {
            input.x = Input.GetAxis(x);
            input.y = Input.GetAxis(y);
            face = transform.GetChild(0).gameObject;
            targetPosition = transform.position + input * speed;
            targetPosition.x = Mathf.Clamp(targetPosition.x, 0, screenBounds.x);
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
                    print("shoot");
                    shoot();
                }
            }
            if (Input.GetButtonDown(catchKey) && !inCoolDown)
            {
                StartCoroutine(catchingIEnumerator());
            }
        }
    }

    // void shoot()
    // {
    //     hasBall = false;
    //     ball ballscript = ball.GetComponent<ball>();
    //     Vector3 forward = input;
    //     if (forward.y > 0)
    //     {
    //         forward.y = 1;
    //     }
    //     else if (forward.y < 0)
    //     {
    //         forward.y = -1;
    //     } 
    //     if (transform.rotation.y == -1)
    //     {
    //         forward.x = 1;
    //     }else if (transform.rotation.y == 0){
    //         forward.x = -1;
    //     }
    //     ballscript.shoot(force, forward);
    // }

    // void OnTriggerEnter2D(Collider2D other)
    // {

    //     ball = other.gameObject;
    //     ball ballscript = ball.GetComponent<ball>();
    //     if (ballscript.shooting == false)
    //     {
    //         hasBall = true;
    //         ballscript.parent = face;
    //         ballscript.rb2D.velocity = new Vector2(0, 0);
    //     }
    //     else
    //     {
    //         if (Input.GetButtonDown("p2Catch"))
    //         {
    //             hasBall = true;
    //             ballscript.parent = face;
    //             ballscript.rb2D.velocity = new Vector2(0, 0);
    //         }
    //         else
    //         {
    //             print("get hitted");
    //         }

    //     }
    // }


}
