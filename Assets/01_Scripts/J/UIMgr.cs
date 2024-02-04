using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
public class UIMgr : MonoBehaviour
{
    public Player player;

    public GameObject[] npcPanel;
    public GameObject[] npcCH;
    public GameObject[] playerSkillIcon;

    public GameObject healSlider;
    void Start()
    {
        UIstartOff();
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    void UIstartOff()
    {
        foreach (GameObject obj in npcPanel)
        {
            //obj.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
