using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionHealthBar : MonoBehaviour
{
    // Variables
    public Slider healthBar;
    public Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        InitialiseUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    // Initialise UI method
    void InitialiseUI()
    {
        // If healthbar isn't null
        if (healthBar != null)
        {
            healthBar.maxValue = this.GetComponentInParent<Minion>().Health;
            healthBar.value = this.GetComponentInParent<Minion>().Health;
        }
    }

    // UI update method
    void UpdateUI()
    {
        // Update to look at camera
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back, mainCamera.transform.rotation * Vector3.up);

        // Update Health - Max is updated in case of item
        healthBar.maxValue = this.GetComponentInParent<Minion>().Health;
        healthBar.value = this.GetComponentInParent<Minion>().Health;
    }
}
