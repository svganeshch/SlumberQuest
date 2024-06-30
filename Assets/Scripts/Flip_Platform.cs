using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Flip_Platform : MonoBehaviour
{
    public float flipDuration = 1.0f;
    private bool isFlipping = false;

    
    public void StartFlip()
    {
        if (!isFlipping)
        {
            StartCoroutine(Flip());
        }
    }

    private IEnumerator Flip()
    {
        isFlipping = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180); 

        float elapsedTime = 0;

        while (elapsedTime < flipDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / flipDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        isFlipping = false;
    }
}
