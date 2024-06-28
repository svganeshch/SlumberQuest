using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerAnimatorManager : CharacterAnimatorManager
{
    Player player;

    public static readonly int jumpActionHash = Animator.StringToHash("jump_start");

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<Player>();
    }

    private void OnAnimatorMove()
    {
        if (player.applyRootMotion)
        {
            Vector3 velocity = player.animator.deltaPosition;

            player.controller.Move(velocity);
            player.transform.rotation *= player.animator.deltaRotation;
        }
    }

    public void PlayJumpAction()
    {
        PlayCharacterActionAnimation(jumpActionHash);
    }

    // Animation event calls
    public void ApplyJumpVelocity()
    {
        player.playerMovementManager.yVelocity.y = Mathf.Sqrt(player.jumpHeight * -2 * player.playerMovementManager.gravityForce);
    }
}
