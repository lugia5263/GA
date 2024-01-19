using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    public GameObject SkillOneEffect;

    public Transform TargetPlayer;

    public void SkillOneAttack()
    {
        Instantiate(SkillOneEffect, TargetPlayer.position , TargetPlayer.rotation);

        Destroy(SkillOneEffect, 2f);
    }
}
