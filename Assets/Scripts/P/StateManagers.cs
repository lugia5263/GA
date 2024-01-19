using UnityEngine;

public class StateManagers : MonoBehaviour
{
    //여기는 현재플레이어 스탯이다. 이걸 부착된 자식 객체의 클래스에서 가져온다.



    // 플레이어의 스텟!!!!
    [Header("캐릭터 상태")]
    public int level;
    public float maxhp; 
    public float hp;
    public int atk;
    public int exp;
    [Space(10)]
    [Range(0, 100)]
    public int criChance = 50; //in percentage
    public float criDamage = 1.5f;
    public int def; //안씀
    public float gageTime1; // 스킬 쿨타임
    public float gageTime2;
    public float gageTime3;

    [HideInInspector]
    public HUDManager hudManager; 

    private void Start()
    {
        hudManager = GetComponent<HUDManager>(); // 수정된 부분
    }
}