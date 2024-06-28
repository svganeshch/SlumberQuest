using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [HideInInspector] public CharacterController controller;

    [Header("Character controls")]
    public float walkingSpeed = 2f;
    public float rotationDampTime = 15f;

    [Header("Layer masks")]
    public LayerMask groundLayerMask;

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    protected virtual void Start() { }
}
