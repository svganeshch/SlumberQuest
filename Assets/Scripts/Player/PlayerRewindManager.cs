using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRewindManager : MonoBehaviour
{
    public float maxRewindDuration = 5f;
    public float rewindSpeed = 2f;

    private List<PositionSnaps> positionSnaps = new List<PositionSnaps>();
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private struct PositionSnaps
    {
        public Vector3 position;
        public Quaternion rotation;

        public PositionSnaps(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.playerInputManager.rewindAction.WasPressedThisFrame() && !player.isRewinding)
        {
            StartRewind();
        }

        if (player.playerInputManager.rewindAction.WasReleasedThisFrame())
        {
            StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (!player.isRewinding)
        {
            RecordSnaps();
        }
        else
        {
            PerformRewind();
        }
    }

    private void RecordSnaps()
    {
        if (positionSnaps.Count == 0 || positionSnaps[0].position != transform.position)
        {
            positionSnaps.Insert(0, new PositionSnaps(transform.position, transform.rotation));
        }

        if (positionSnaps.Count > Mathf.Round(maxRewindDuration / Time.fixedDeltaTime))
        {
            positionSnaps.RemoveAt(positionSnaps.Count - 1);
        }
    }

    private void PerformRewind()
    {
        if (positionSnaps.Count > 0)
        {
            PositionSnaps snap = positionSnaps[0];
            positionSnaps.RemoveAt(0);

            targetPosition = snap.position;
            targetRotation = snap.rotation;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * rewindSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rewindSpeed);

        if (positionSnaps.Count == 0)
        {
            StopRewind();
        }
    }

    private void StartRewind()
    {
        player.isRewinding = true;
        //Time.timeScale = 0.5f;
    }

    private void StopRewind()
    {
        player.isRewinding = false;
        //Time.timeScale = 1f;
    }
}
