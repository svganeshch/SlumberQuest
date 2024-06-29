using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [Header("Jump Controls")]
    public float jumpHeight = 0.8f;
    public float jumpForwardVelocity = 5f;
    public float freeFallControlVelocity = 2f;

    [HideInInspector] public Camera playerCamera;
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerInputManager playerInputManager;
    [HideInInspector] public PlayerMovementManager playerMovementManager;

    public List<GravityEnemy> gravityEnemyList = new();
    FlipObject flipObject;

    // Input bools
    private bool flipInput = false;

    public bool isRewinding = false;

    protected override void Awake()
    {
        base.Awake();

        playerCamera = Camera.main;

        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
    }

    protected override void Start()
    {
        base.Start();

        playerInputManager.flipAction.performed += _ => flipInput = true;
    }

    protected override void InitializeStates()
    {
        idleState = new IdleState(this, characterStateMachine);
        jumpState = new JumpState(this, characterStateMachine);
        sprintState = new SprintState(this, characterStateMachine);

        characterStateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        HandleInputs();

        if (flipObject != null && flipObject.flipSet)
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        
    }

    private void HandleInputs()
    {
        if (flipInput)
        {
            flipInput = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit != null)
        {
            if (1 << hit.gameObject.layer == LayerMaskManager.instance.groundLayerMask)
            {
                Debug.Log("ground : " + hit.gameObject.name);

                if (gravityEnemyList.Count <= 0)
                {
                    if (hit.gameObject.TryGetComponent<EnemySpawner>(out EnemySpawner spawner))
                    {
                        Debug.Log("recevied spawner : " +  spawner.gameObject.name);
                        foreach (var enemy in spawner.spawnedObjs)
                        {
                            gravityEnemyList.Add(enemy.GetComponent<GravityEnemy>());
                        }
                    }
                }

                if (flipInput)
                {
                    if (hit.gameObject.TryGetComponent<FlipObject>(out FlipObject flipObject))
                    {
                        if (gravityEnemyList.Count > 0)
                        {
                            if (gravityEnemyList.All(enemy => enemy.gravityMode == true))
                            {
                                Debug.Log("flip on : " + flipObject.name);

                                transform.SetParent(flipObject.transform);

                                canMove = false;
                                canRotate = false;
                                disableGravity = true;

                                flipObject.flipSet = true;
                            }
                            else
                            {
                                Debug.Log("Not all grav enemies are disabled");
                            }
                        }
                    }
                }
            }
        }
    }

    public void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(0, 0, 200, 20), this.GetType().Name + " : " + characterStateMachine.currentState.ToString());
    }
}
