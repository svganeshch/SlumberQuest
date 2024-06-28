using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [HideInInspector] public PlayerInputManager playerInputManager;

    protected override void Awake()
    {
        base.Awake();

        playerInputManager = GetComponent<PlayerInputManager>();
    }
}
