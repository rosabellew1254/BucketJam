using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollCredits : MonoBehaviour
{
    Vector2 positionToMoveTo;
    Vector2 velocity;
    public float moveValue;
    public float delay;
    public float duration;

    void Start()
    {
        positionToMoveTo = new Vector2(transform.position.x, transform.position.y + moveValue);
        Invoke("InvoLerp", delay);
        //InvoLerp();
    }

    void InvoLerp()
    {
        StartCoroutine(LerpPosition(positionToMoveTo, duration));
    }

    IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, time / duration, 75f);
            //transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
