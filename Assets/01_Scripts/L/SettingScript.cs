using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SettingScript : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;
    public PhotonView pv;
    public GameObject settingPanel;
    public bool panelActive;

    public Slider volumeSlider;
    public Dropdown dropdown;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        settingPanel = GameObject.Find("WorldCanvas/SettingPanel");
        settingPanel.SetActive(false);
        panelActive = false;
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!panelActive)
                {
                    settingPanel.SetActive(true);
                    panelActive = true;
                }
                else
                {
                    settingPanel.SetActive(false);
                    panelActive = false;
                }
            }
        }
    }

    public void VolumeSetting()
    {
        float volume = volumeSlider.value;
        if (photonView.IsMine)
        {
            Debug.Log("Volume 실행");
            AudioListener.volume = volume;
        }
    }

    public void ScreenSizeSetting(bool check)
    {
        if (photonView.IsMine && check == true)
        {
            Debug.Log("screen 실행");
            string size = dropdown.options[dropdown.value].text;
            switch (size)
            {
                case "1920 x 1080":
                    Screen.SetResolution(1920, 1080, true);
                    break;
                case "1366 x 768":
                    Screen.SetResolution(1366, 768, true);
                    break;
                default:
                    break;
            }
        }
    }
}
