using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public GameObject questPanel;
    public GameObject achievementPanel;
    public GameObject weaponPanel;
    public GameObject lvPanel;
    public GameObject clothingPanel;



    [TextArea(1, 3)]
    public string introduce;

    public int npcNum;
    
    void Awake()
    {
        questPanel = GameObject.Find("QuestPanel");
        achievementPanel = GameObject.Find("AchievementPanel");
        weaponPanel = GameObject.Find("WeaponPanel");
        lvPanel = GameObject.Find("LvPanel");
        clothingPanel = GameObject.Find("ClothingPanel");
    }
    
    private void Start()
    {
        questPanel.SetActive(false);
        achievementPanel.SetActive(false);
        weaponPanel.SetActive(false);
        lvPanel.SetActive(false);
        clothingPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& npcNum== 1)
        {
            questPanel.SetActive(true);

        }
        if (other.CompareTag("Player") && npcNum == 2)
        {
            achievementPanel.SetActive(true);

        }
        if (other.CompareTag("Player") && npcNum == 3)
        {
            weaponPanel.SetActive(true);

        }
        if (other.CompareTag("Player") && npcNum == 4)
        {
            lvPanel.SetActive(true);

        }
        if (other.CompareTag("Player") && npcNum == 5)
        {
            clothingPanel.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questPanel.SetActive(false);
            achievementPanel.SetActive(false);
            weaponPanel.SetActive(false);
            lvPanel.SetActive(false);
            clothingPanel.SetActive(false);
        }
    }

   
}
