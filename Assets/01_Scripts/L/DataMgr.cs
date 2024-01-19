using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// 저장하는 방법
// 1. 저장할 데이터가 존재
// 2. 데이터를 json으로 변환
// 3. json을 외부에 저장

// 불러오는 방법
// 1. 외부에 저장된 json을 가져옴
// 2. json을 데이터 형태로 변환
// 3. 불러온 데이터를 사용

public enum Character
{
    Blue, Red, Yellow
}

//public class PlayerData
//{
//    // 직업이름, 레벨, 공격력, 체력, 골드, 아이템, 경험치포션
//    public string className;
//public int level;
//public int atkDmg;
//public int hp;
//public int haveGold;
//public int item;
//public int expPotion;
//}

public class DataMgr : MonoBehaviour
{
    // 싱글톤
    public static DataMgr instance;

    //PlayerData currentPlayer = new PlayerData();

    //string path;
    //string filename = "save";

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        //path = Application.persistentDataPath + "/";
    }

    public Character currentCharacter;

    void Start()
    {
        //string jsondata = JsonUtility.ToJson(currentPlayer);
        // print(path);
        //File.WriteAllText(path + filename, data); //json을 저장할때 쓰는 함수

        //PlayerData loadPlayerData = JsonUtility.FromJson<PlayerData>(jsondata); // json에서 불러오는 함수 <클래스명>(변수이름)

        //print(loadPlayerData.className);
        //print(loadPlayerData.level);
    }
}
