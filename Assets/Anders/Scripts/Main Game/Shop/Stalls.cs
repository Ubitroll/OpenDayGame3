using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalls : MonoBehaviour
{
    public GameObject OpenShopDisplay, BaseShopUI;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OpenShopDisplay.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
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
                GameObject Player = GameObject.FindGameObjectWithTag("Player");
                Player.GetComponent<PlayerInput>().enabled = false;
                BaseShopUI.SetActive(true);
            }
        }
    }
}
