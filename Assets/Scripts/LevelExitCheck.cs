using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {
            if (1 << other.gameObject.layer == LayerMaskManager.instance.playerLayerMask)
            {
                HUDManager hud = HUDManager.instance;

                if (hud.r1 != null && hud.r2 != null && hud.r3 != null && hud.r4 != null)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 2)
                    {
                        SceneManager.LoadScene(0, LoadSceneMode.Single);
                        return;
                    }

                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
                }
            }
        }
    }
}
