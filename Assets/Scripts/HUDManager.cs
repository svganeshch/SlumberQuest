using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    public float collectDistance = 0.5f;
    public Image collectImage;
    public RawImage r1;
    public RawImage r2;
    public RawImage r3;
    public RawImage r4;

    public RectTransform collectImageRectTransform;

    private Ray collectRay;
    private RaycastHit hit;

    private RawImage rImage;

    Player player;

    private void Awake()
    {
        if (instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        collectRay.origin = player.lineCast.position;
        collectRay.direction = player.lineCast.forward;

        if (Physics.Raycast(collectRay, out hit, collectDistance, LayerMaskManager.instance.collectableMask))
        {
            var obj = hit.collider.gameObject;

            if (obj != null)
            {
                if (obj.TryGetComponent<ICollectable>(out ICollectable collectable))
                {
                    Debug.Log("collect obj : " + collectable);
                    SetCollectMark(obj.transform);

                    if (player.playerInputManager.collectAction.WasPressedThisFrame())
                    {
                        switch (collectable.getRockID())
                        {
                            case 1: rImage = r1; break;
                            case 2: rImage = r2; break;
                            case 3: rImage = r3; break;
                            case 4: rImage = r4; break;
                        }

                        collectable.OnCollect(rImage);
                    }
                }
            }
        }
    }

    public void SetCollectMark(Transform collectObj)
    {
        if (collectObj != null)
        {
            collectImage.enabled = true;
            collectImageRectTransform.position = RectTransformUtility.WorldToScreenPoint(player.playerCamera, collectObj.position);
        }
        else
        {
            collectImage.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(collectRay.origin, collectRay.direction * collectDistance, Color.red);
    }
}
