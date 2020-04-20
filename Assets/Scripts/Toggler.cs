using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggler : MonoBehaviour
{
    public GameObject settingGO;

    public void SetSettingsActive(Toggle toggle)
    {
        settingGO.SetActive(toggle.isOn);
    }
}
