using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private Vector2 screenBounds;

    public bool hasParent;
    public GameObject parent;
    public bool shooting = false;

    public Rigidbody2D rb2D;
    private float catchableSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(rb2D.velocity.x) <= catchableSpeed && Mathf.Abs(rb2D.velocity.y) <= catchableSpeed ){
            shooting = false;
        }else
        {
            shooting = true;
        }
        Vector3 targetPosition = transform.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, screenBounds.x * -1, screenBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, screenBounds.y * -1, screenBounds.y);
        if (targetPosition.y == screenBounds.y || targetPosition.y == - screenBounds.y )
        {
            Vector2 velocity = rb2D.velocity;
            rb2D.velocity = new Vector2(velocity.x ,-velocity.y);
        }
        if (targetPosition.x == screenBounds.x || targetPosition.x == - screenBounds.x )
        {
            Vector2 velocity = rb2D.velocity;
            rb2D.velocity = new Vector2(-velocity.x ,velocity.y);
        }
        transform.position = targetPosition;
    }

    public void shoot(float speed, Vector3 forward)
    {
        rb2D.AddForce(forward * speed);
    }

}
