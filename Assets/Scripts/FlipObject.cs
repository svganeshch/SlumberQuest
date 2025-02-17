using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlipObject : MonoBehaviour
{
    private Player _player;

    public bool isFlippable = true;
    public bool flipSet = false;
    public bool isFlipped = false;
    public float rotationSpeed = 5.0f;
    private Quaternion targetRotation = Quaternion.Euler(180, 0, 0);
    private float angleThreshold = 1.0f;

    public UnityEvent flipDone = new UnityEvent();

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (isFlippable && flipSet)
        {
            //Debug.Log("flip set");
            FlipGameObject();
        }
    }

    public void FlipGameObject()
    {
        if (Quaternion.Angle(transform.rotation, targetRotation) > angleThreshold)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = targetRotation;
            flipSet = false;
            isFlipped = true;
            targetRotation = transform.rotation * Quaternion.Euler(180, 0, 0);
            Debug.Log("flip completed");
        }
    }
}