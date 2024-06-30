using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

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

    public AudioClip deathSound;

    public List<GravityEnemy> gravityEnemyList = new();
    FlipObject currentGroundFlipObject;
    FlipObject prevFlipObject;
    GameObject currentGround;
    EnemySpawner currentGroundSpawner;

    // Input bools
    private bool flipInput = false;

    public bool isRewinding = false;
    public bool deathDone = false;

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
        HandleFall();

        if (currentGroundFlipObject != null && currentGroundFlipObject.flipSet)
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
            Debug.Log("flip input");

            if (flipInput)
            {
                flipInput = false;
                if (currentGround == null) return;

                if (currentGround.gameObject.TryGetComponent<FlipObject>(out FlipObject flipObject))
                {
                    if (prevFlipObject != flipObject)
                    {
                        prevFlipObject = flipObject;
                    }

                    if (gravityEnemyList.Count > 0)
                    {
                        if (gravityEnemyList.All(enemy => enemy.gravityMode == true))
                        {
                            Debug.Log("flip on : " + flipObject.name);

                            transform.SetParent(flipObject.transform);

                            canMove = false;
                            canRotate = false;
                            disableGravity = true;

                            // enable enemy gravity
                            foreach (GravityEnemy genemy in gravityEnemyList)
                            {
                                genemy.gravityMode = false;
                                genemy.GetComponent<NavMeshAgent>().enabled = false;
                                genemy.GetComponent<Rigidbody>().isKinematic = false;
                                genemy.GetComponent<Rigidbody>().useGravity = true;
                                genemy.enabled = false;

                                Destroy(genemy.gameObject, 3);
                            }

                            flipObject.flipSet = true;
                            currentGroundSpawner.isCleared = true;
                            gravityEnemyList.Clear();
                            Debug.Log("enemy cleared count : " + gravityEnemyList.Count);
                        }
                        else
                        {
                            Debug.Log("Not all grav enemies are disabled");
                        }
                    }
                    else if (flipObject.isFlipped)
                    {
                        flipObject.flipSet = true;
                        canMove = true;
                        canRotate = true;
                        disableGravity = false;

                        StartCoroutine(waitForReFlip());
                    }
                }
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit != null)
        {
            if (1 << hit.gameObject.layer == LayerMaskManager.instance.groundLayerMask)
            {
                currentGround = hit.gameObject;
                Debug.Log("ground : " + currentGround.name);

                if (currentGround.TryGetComponent<EnemySpawner>(out currentGroundSpawner))
                {
                    if (currentGroundSpawner != null)
                    {
                        if (currentGroundSpawner.isCleared) return;
                        if (currentGroundSpawner.spawnCount == gravityEnemyList.Count) return;

                        Debug.Log("recevied spawner : " + currentGround.gameObject.name);
                        foreach (var enemy in currentGroundSpawner.spawnedObjs)
                        {
                            gravityEnemyList.Add(enemy.GetComponent<GravityEnemy>());
                            Debug.Log("adding enemy : " + enemy.name);
                        }
                    }
                }
                Debug.Log("enemy count after: " + gravityEnemyList.Count);
            }
        }
    }

    private void HandleFall()
    {
        if (!deathDone)
        {
            if (playerMovementManager.inAirTime > 2)
            {
                deathDone = true;
                audioSource.PlayOneShot(deathSound);
                StartCoroutine(waitForSound());
            }
        }
    }

    IEnumerator waitForSound()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        MenuManager.instance.ReloadCurrentScene();
    }

    IEnumerator waitForReFlip()
    {
        while (prevFlipObject.flipSet)
        {
            yield return null;
        }
        
        prevFlipObject = null;
        transform.parent = null;
    }

    public void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(0, 0, 200, 20), this.GetType().Name + " : " + characterStateMachine.currentState.ToString());
    }
}
