using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class trigger_flip : MonoBehaviour
{
    public Flip_Platform flipObject;
    private bool canflip = false;
    public TextMeshProUGUI interact_text;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canflip = true;
            interact_text.text = "Press E"; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canflip = false;
            interact_text.text =null;
        }
    }
    void Update()
    {
        if (canflip)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                flipObject.StartFlip();
            }
        }   
    }
}
