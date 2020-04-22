using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUIPanel : MonoBehaviour
{
    public GameObject ThisUI;

    public void CloseUI()
    {
        ThisUI.SetActive(false);
    }
}
