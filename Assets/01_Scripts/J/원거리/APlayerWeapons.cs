using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APlayerWeapons : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;
    public GameObject weapons;
    public BoxCollider meleeArea;
    
    public APlayer aplayer;

    private void Start()
    {
        aplayer = GetComponentInParent<APlayer>();
    }
    public void WeaponUse()
    {

    }

    public void Attack1()
    {
        aplayer.isAttack1 = true;
    }
    public void Attack2()
    {
        aplayer.isAttack2 = true;
    }

    public void Attack3()
    {
        aplayer.isAttack3 = true;
    }
    

    public void EnableCollider(int isEnable)
    {
        if (weapons != null)
        {
            var col = weapons.GetComponent<BoxCollider>();
            if (col != null)
            {
                if (isEnable == 1)
                {
                    col.enabled = true;
                }
                else
                {
                    col.enabled = false;
                }
            }
        }
    }
}
