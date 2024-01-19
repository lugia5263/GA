using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Stet")]
    public int CurHP;
    public int attackDamage;
    
    public void TakeDamage(int damage)
    {
        CurHP -= damage;
        Vector3 randomUp = new Vector3(Random.Range(0f, 0.25f), Random.Range(0f, 0.25f), Random.Range(0f, 0.25f));
        DamagePopUpGenerator.current.CreatePopup(transform.position+ randomUp,damage.ToString(), Color.yellow);
    }

    public void Deal(GameObject target)
    {
        var dmg = target.GetComponent<Damage>();
        if(dmg != null)
        {
            dmg.TakeDamage(attackDamage);
        }
    }
}
