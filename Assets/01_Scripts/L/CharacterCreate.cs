using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Auth;
using System;
using UnityEngine.UI;
using Firebase;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class CharacterCreate : MonoBehaviour
{
    private FirebaseFirestore db;
    private FirebaseAuth auth;

    public GameObject selectPanel;
    public GameObject nickNamePanel;

    public InputField nickNameIF;
    public string characterNickName;

    public string userEmail;

    public static string currentCharacterClass;

    public static int currentClassNum;
    public static int currentSlotNum;
    public Sprite[] sprites;
    public Button[] slots;

    LoadPlayerInfo loadPlayerInfoInstance;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
        selectPanel.SetActive(false);
        nickNamePanel.SetActive(false);
        userEmail = LoginSystem_test.userEmail;
        loadPlayerInfoInstance = GameObject.Find("LoadPlayerInfo").GetComponent<LoadPlayerInfo>();
        Debug.Log(userEmail);
    }

    IEnumerator CreateCharacter(string userEmail, string characterNickName, string className)
    {
        Debug.Log("코루틴 시작");
        Debug.Log(className);

        // Firestore에 사용자 데이터 추가
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        // 캐릭터 1, 2, 3컬렉션 참조 없으면 만듬.
        DocumentReference docRef = db.Collection("users").Document(userEmail).Collection($"캐릭터{currentClassNum+1}").Document("Info");
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            {"SlotNum", currentSlotNum},
            {"NickName", characterNickName},
            {"Class", className},
            {"CharacterLevel", 1},
            {"MaxHp", 100},
            {"WeaponLevel", 1},
            {"ATK", 100},
            {"CriticalPer", 50},
            {"UserGold", 0},
            {"Material", 0},
            {"ExpPotion", 0},
            {"UpdateTime", FieldValue.ServerTimestamp}
        };

        yield return docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            Debug.Log("데이터작성 시작");
            if (task.IsFaulted)
            {
                foreach (Exception exception in task.Exception.InnerExceptions)
                {
                    if (exception is FirebaseException firebaseException)
                    {
                        Debug.LogError($"FirebaseException: {firebaseException.ErrorCode} - {firebaseException.Message}");
                    }
                    else
                    {
                        Debug.LogError($"Exception: {exception}");
                    }
                }
                Debug.Log("데이터작성 실패");
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("데이터작성 취소");
            }
            else
            {
                Debug.Log($"{characterNickName} 의 캐릭터생성이 완료되었습니다...");
                Debug.Log("데이터작성 끝");
            }
        });
        Debug.Log("코루틴 종료");
    }

    // 만들 슬롯 클릭 후, 캐릭터생성버튼 클릭 => 패널띄우기
    public void OnClickPanelActiveBtn()
    {
        currentSlotNum = SelectSlot.slotNum;
        // 할당된 슬롯번호 확인
        Debug.Log($"생성하기전, Selected Slot: {currentSlotNum+1}");
        selectPanel.SetActive(true);
    }

    //캐릭터생성취소 버튼
    public void OnClickCancelCharacterBtn()
    {
        selectPanel.SetActive(false);
    }

    // 생성할 캐릭터를 고르고, 생성버튼 클릭 => 닉네임 설정 패널띄우기
    public void OnClickSelectAndCreateBtn()
    {
        currentClassNum = SelectChar.CharNum;
        Debug.Log($"클래스넘버 : {currentClassNum+1}");
        nickNameIF.text = "";
        nickNamePanel.SetActive(true);
    }

    //닉네임결정안하고 뒤로가기(캐릭터 다시고르고 싶다던지)
    public void OnClickCancelNickBtn()
    {
        nickNamePanel.SetActive(false);
    }

    // 닉네임 결정완료 버튼
    public void OnClickDecideNickBtn()
    {
        characterNickName = nickNameIF.text;
        currentCharacterClass = SelectChar.currentCharacter;
        Debug.Log(currentCharacterClass);
        StartCoroutine(CreateCharacter(userEmail, characterNickName, currentCharacterClass));
        Debug.Log("닉네임, 캐릭터생성 완료");

        Debug.Log("userEmail : " + userEmail);
        StartCoroutine(loadPlayerInfoInstance.LoadPlayerData(userEmail));

        slots[currentSlotNum].GetComponent<Image>().sprite = sprites[currentClassNum];
        selectPanel.SetActive(false);
        nickNamePanel.SetActive(false);
    }

    public void OnClickGoLoginSceneBtn()
    {
        SceneManager.LoadScene("Login");
    }
}
