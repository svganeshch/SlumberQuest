using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskManager : MonoBehaviour
{
    public static LayerMaskManager instance;

    [Header("Layer masks")]
    public LayerMask groundLayerMask;
    public LayerMask playerLayerMask;
    public LayerMask enemyLayerMask;
    public LayerMask obstacleLayerMask;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
