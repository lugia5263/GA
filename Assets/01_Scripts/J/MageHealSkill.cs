using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageHealSkill : MonoBehaviour
{
    Player player;
    UIMgr uimgr;
    public GameObject healS;
    public Slider charging;
    void Start()
    {
        player = GetComponent<Player>();
        uimgr = GameObject.FindGameObjectWithTag("UImgr").GetComponent<UIMgr>();
        healS = uimgr.healSlider;
        charging = uimgr.healSlider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.rischarging)
        {
            if (Input.GetKey(KeyCode.R))
            {
                player.animator.SetTrigger("SkillR");
                player.Skill[2].SetActive(true);
                healS.SetActive(true);
                charging.value += Time.deltaTime * 0.35f;

                if (charging.value == 1)
                {
                    charging.value = 0;
                    player.Skill[2].SetActive(false);
                    healS.SetActive(false);
                    player.rischarging = false;
                    player.rskillcool = 0;
                }
            }
        }
    }
}
