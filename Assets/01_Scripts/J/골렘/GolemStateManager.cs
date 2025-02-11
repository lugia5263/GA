using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemStateManager : MonoBehaviour
{
    //여기는 현재플레이어 스탯이다. 이걸 부착된 자식 객체의 클래스에서 가져온다.


    [Header("Stet")]
    // 플레이어의 스텟!!!!
    public float maxhp;
    public float hp;
    public int atk;
    [Space(10)]
    [Range(0, 100)]
    public int criChance = 50; //in percentage
    public float criDamage = 1.5f;
    public int def;
    public float gageTime;

    [HideInInspector]
    public int asdf;
    //public HUDManager hudManager;
    //public AttackController atkctrl;


    private void Start()
    {
        //atkctrl = GetComponentInChildren<AttackController>();
        //hudManager = gameObject.GetComponent<HUDManager>();
    }


    #region 대미지 계산식######
    //공격력 30 100%
    // 스킬공 60 200 %= 200 / 100
    //내가 float값에 200 넣어서 60 딜이 나오려면
    //30 x 200 x n = 60
    //200n = 2
    //100n = 1
    //n = 0.01
    // 따라서 공격력 x 스킬대미지 x 0.01f 해야 퍼센트대미지가 나온다!
    // skillDMG는 기본적으로 100 줘야함
    /// atk * ( skillDMG * 0.01f); 를 하면, 150%로 줬을 때, 1.5배의 대미지가 들어간다!

    #endregion

    public void DealDamage(GameObject target, float skillDMG)//딜 계산, 105% 느낌으로 할 것!
    {
        Color popupColorsend = Color.white;
        var monster = target.GetComponent<StateManager>();
        if (monster != null)
        {
            float totalDamage = atk * (skillDMG * Random.Range(0.005f, 0.01f));
            if (Random.Range(0f, 100f) <= criChance)
            {
                totalDamage *= criDamage * 0.02f;
                popupColorsend = Color.yellow;
            }

            monster.TakeDamage((int)totalDamage, popupColorsend);
        }
    }
    public void TakeDamage(int hit, Color popupColor) // 딜 팝업
    {
        hp -= hit;
        Vector3 randomness = new Vector3(Random.Range(-0.45f, 0.45f), Random.Range(-0.45f, 0.45f), Random.Range(0f, 0.25f));
        // hit - (hit*def/100)

        DamagePopUpGenerator.current.CreatePopup(transform.position + randomness, hit.ToString(), popupColor);
        //hudManager.ChangeUserHUD();
    }
}

