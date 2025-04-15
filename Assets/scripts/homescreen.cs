using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homescreen : MonoBehaviour
{
    public GameObject VolumePanel;

    public void ButtonOpen()
    {
        VolumePanel.SetActive(true);
    }
    public void ButtonExit()
    {
        VolumePanel.SetActive(false);
    }
}
