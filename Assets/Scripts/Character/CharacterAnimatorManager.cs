using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterAnimatorManager : MonoBehaviour
{
    Character character;

    public static readonly int inAirTimeHash = Animator.StringToHash("inAirTime");
    public static readonly int isGroundedHash = Animator.StringToHash("isGrounded");

    public float InAirTime
    {
        set => character.animator.SetFloat(inAirTimeHash, value);
    }

    public bool IsGrounded
    {
        get => character.animator.GetBool(isGroundedHash);
        set => character.animator.SetBool(isGroundedHash, value);
    }

    protected virtual void Awake()
    {
        character = GetComponent<Character>();
    }

    protected virtual void PlayCharacterActionAnimation(
        int animationClipHash,
        bool canRotate = false,
        bool canMove = false,
        bool applyRootMotion = true)
    {
        character.animator.CrossFade(animationClipHash, character.animationFadeTime);

        character.applyRootMotion = applyRootMotion;
        character.canRotate = canRotate;
        character.canMove = canMove;
    }

    public void SetAnimatorParameters(float horizontalInput, float verticalInput)
    {
        if (character.characterStateMachine.currentState == character.sprintState)
        {
            verticalInput = 1.5f;
        }

        character.animator.SetFloat("speedX", horizontalInput, character.speedDampTime, Time.deltaTime);
        character.animator.SetFloat("speedY", verticalInput, character.speedDampTime, Time.deltaTime);
    }
}
