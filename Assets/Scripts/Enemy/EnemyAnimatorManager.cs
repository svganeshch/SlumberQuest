using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : CharacterAnimatorManager
{
    protected override void Start()
    {
        base.Awake();

        Debug.Log("ok");
    }
}
