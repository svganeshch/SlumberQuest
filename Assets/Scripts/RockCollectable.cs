using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockCollectable : MonoBehaviour, ICollectable
{
    public int rockID;

    MeshRenderer meshRenderer;
    Material material;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    public void OnCollect(RawImage rImage)
    {
        Debug.Log("Rock collected");
        rImage.texture = material.GetTexture("_DecalsTexture");
        material.SetFloat("_DECALEMISSIONONOFF", 0);
    }

    public int getRockID()
    {
        return rockID;
    }
}
