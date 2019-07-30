using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private Vector2 screenBounds;
    public GameObject parent;
    public bool shooting, catchable, hasParent;
    public Rigidbody2D rb2D;
    private float catchableSpeed = 5f;
    public SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        catchable = true;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb2D.velocity.x) <= catchableSpeed && Mathf.Abs(rb2D.velocity.y) <= catchableSpeed)
        {
            shooting = false;
        }
        else
        {
            shooting = true;
        }
        Vector3 targetPosition = transform.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, screenBounds.x * -1, screenBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, screenBounds.y * -1, screenBounds.y);
        if (targetPosition.y == screenBounds.y || targetPosition.y == -screenBounds.y)
        {
            Vector2 velocity = rb2D.velocity;
            rb2D.velocity = new Vector2(velocity.x, -velocity.y);
        }
        if (targetPosition.x == screenBounds.x || targetPosition.x == -screenBounds.x)
        {
            Vector2 velocity = rb2D.velocity;
            rb2D.velocity = new Vector2(-velocity.x, velocity.y);
        }
        transform.position = targetPosition;
    }

    public void shoot(float speed, Vector3 forward)
    {
        rb2D.AddForce(forward * speed);
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "range")
        {
            if (hasParent)
            {
                hasParent = false;
                catchable = true;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Line")
        {
            if (shooting)
            {
                sr.color = Color.white;
                parent = null;
            }
        }
    }


}
