using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : CharacterMovementManager
{
    Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<Player>();
    }

    protected override void GetMovementInput()
    {
        base.GetMovementInput();

        verticalInput = player.playerInputManager.verticalInput;
        horizontalInput = player.playerInputManager.horizontalInput;
    }
}
