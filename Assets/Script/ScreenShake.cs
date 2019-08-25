using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    Camera Camera; // set this via inspector
    public float shake = 0;
    float shakeAmount = 0.1f;
    float decreaseFactor = 1.0f;

    Vector3 originPos;

    void Start()
    {
        Camera = Camera.main;
        originPos = Camera.transform.position;
    }

    void Update()
    {
        if (shake > 0)
        {
            Camera.transform.localPosition = new Vector3(Random.insideUnitCircle.x,Random.insideUnitCircle.y, - 100) * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            Camera.transform.position = originPos;
            shake = 0;
        }
    }
}
