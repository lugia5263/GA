using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LoadPlayerInfo : MonoBehaviour
{
    private FirebaseFirestore db;
    public string curUserEmail;

    public Text nickNameTxt;
    public Text classNameTxt;
    public Text levelTxt;

    public CharacterCreate characterCreate;

    private void Start()
    {
        // 다른 스크립트(LoginSystem 등)에서 userEmail 가져오기
        curUserEmail = LoginSystem_test.userEmail;

        // Firebase Firestore 초기화
        db = FirebaseFirestore.DefaultInstance;

        characterCreate = GameObject.Find("CharacterCreate").GetComponent<CharacterCreate>();

        // 플레이어 정보 로드 코루틴 시작
        StartCoroutine(LoadPlayerData(curUserEmail));
    }

    IEnumerator LoadPlayerData(string userEmail)
    {
        // users 컬렉션에서 userEmail(ID역할)로 된 이름의 문서 읽기
        DocumentReference userDocRef = db.Collection("users").Document(userEmail);
        AggregateQuery cnt = userDocRef.Collection(userEmail).Count;
        Debug.Log(cnt);
        // 사용자 문서의 컬렉션들 가져오기
        yield return userDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            Debug.Log(snapshot.ToDictionary().Count); //나중에 카운트만큼 업데이트돌리면될듯.
            if (snapshot.Exists)
            {
                Debug.Log("Document ID: " + snapshot.Id);

                // userEmail 문서에서 캐릭터1 컬렉션의 Info문서 참조
                DocumentReference characterInfoDocRef = userDocRef.Collection("캐릭터1").Document("Info");

                // 데이터 읽기
                characterInfoDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                if (task.IsFaulted)
                {
                    Debug.LogError("캐릭터 정보 읽기 실패: " + task.Exception);
                    return;
                }

                DocumentSnapshot characterSnapshot = task.Result;

                if (characterSnapshot.Exists)
                {
                        // 캐릭터 정보 가져오기
                        IDictionary<string, object> characterData = characterSnapshot.ToDictionary();

                        // UI에 정보 표시
                        UpdateUI(characterData);
                }
                else
                {
                    Debug.Log("캐릭터 정보 찾지못함.");
                }
                });
            }
        });
    }

    public void UpdateUI(IDictionary<string, object> characterData)
    {
        Debug.Log("UpdateUI 시작");
        nickNameTxt.text = "닉네임: " + characterData["NickName"];
        classNameTxt.text = "직업: " + characterData["Class"];
        levelTxt.text = "레벨: " + characterData["CharacterLevel"];

        //Text[] infoText = characterCreate.slots[CharacterCreate.currentSlotNum].GetComponentsInChildren<Text>();

        //infoText[0].text = "닉네임: " + characterData["NickName"].ToString();
        //infoText[1].text = "직업: " + characterData["Class"].ToString();
        //infoText[2].text = "레벨: " + characterData["CharacterLevel"].ToString();
    }
}