using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalls : MonoBehaviour
{
    public GameObject OpenShopDisplay, BaseShopUI;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            OpenShopDisplay.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            OpenShopDisplay.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("joystick button 3"))
        {
            if (OpenShopDisplay.activeSelf)
            {
                BaseShopUI.SetActive(true);
            }
        }
    }
}
