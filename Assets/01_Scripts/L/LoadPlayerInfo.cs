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

    public IEnumerator LoadPlayerData(string userEmail)
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

                for(int i = 1; i <= 3; i++)
                {
                    // userEmail 문서에서 캐릭터1 컬렉션의 Info문서 참조
                    DocumentReference characterInfoDocRef = userDocRef.Collection("캐릭터" + i).Document("Info");

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

                            // characterData에서 SlotNum 값 확인
                            if (characterData.ContainsKey("SlotNum"))
                            {
                                int slotNum = (int)characterData["SlotNum"];
                                PlayerPrefs.SetInt("SlotNum" + slotNum, slotNum);
                                // 각 캐릭터의 정보를 PlayerPrefs에 저장
                                if (characterData.ContainsKey("NickName"))
                                    PlayerPrefs.SetString("NickName_" + slotNum, (string)characterData["NickName"]);

                                if (characterData.ContainsKey("Class"))
                                    PlayerPrefs.SetString("Class_" + slotNum, (string)characterData["Class"]);

                                if (characterData.ContainsKey("CharacterLevel"))
                                    PlayerPrefs.SetInt("CharacterLevel_" + slotNum, (int)characterData["CharacterLevel"]);

                                PlayerPrefs.Save();
                            }
                            // UI에 정보 표시
                            //UpdateUI(characterData);
                            UpdateUI();
                        }
                        else
                        {
                            Debug.Log("캐릭터 정보 찾지못함.");
                        }
                    });
                }
                
            }
        });
    }

    //public void UpdateUI(IDictionary<string, object> characterData)
    public void UpdateUI()
    {
        Debug.Log("UpdateUI 시작");
        if (PlayerPrefs.HasKey("SlotNum"))
        {
            int slotNum = PlayerPrefs.GetInt("SlotNum");
            string nickName = PlayerPrefs.GetString("NickName_" + slotNum);
            string className = PlayerPrefs.GetString("Class_" + slotNum);
            int level = PlayerPrefs.GetInt("CharacterLevel_" + slotNum);
            nickNameTxt.text = "닉네임 : " + nickName;
            classNameTxt.text = "직업 : " + className;
            levelTxt.text = "레벨 : " + level;
        }
    }
}