using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionAlert : MonoBehaviour
{
    public Text alert;
    public GameObject canvas;
    // Start is called before the first frame update
    private void Start()
    {
        canvas.SetActive(false);
        alert = GetComponent<Text>();
        alert.enabled = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "human colon 1 meshed")
        {
            canvas.SetActive(true);
            alert.enabled = true;
            Debug.Log("Collision detected!!!!!!!!");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        canvas.SetActive(false);
        alert.enabled = false;
    }
}
