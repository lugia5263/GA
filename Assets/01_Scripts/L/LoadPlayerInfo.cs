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

    public Text[] slot1Text;
    public Text[] slot2Text;
    public Text[] slot3Text;

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
        // 사용자 문서의 컬렉션들 가져오기
        yield return userDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            Debug.Log(snapshot.ToDictionary().Count); //나중에 카운트만큼 업데이트돌리면될듯.
            if (snapshot.Exists)
            {
                //Debug.Log("Document ID: " + snapshot.Id);

                #region 캐릭터1
                //Debug.Log($"for문 1번째");
                // userEmail 문서에서 캐릭터1 컬렉션의 Info문서 참조
                DocumentReference character1InfoDocRef = userDocRef.Collection("캐릭터1").Document("Info");

                // 데이터 읽기
                character1InfoDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError($"캐릭터1의 정보 읽기실패." + task.Exception);
                        return;
                    }

                    DocumentSnapshot characterSnapshot = task.Result;
                    Debug.Log($"캐릭터1의 정보 읽기성공.");
                    if (characterSnapshot.Exists)
                    {
                        //Debug.Log("characterSnapshot.Exists == true");
                        // 캐릭터 정보 가져오기
                        IDictionary<string, object> characterData = characterSnapshot.ToDictionary();
                        slot1Text[0].text = characterData["NickName"].ToString();
                        slot1Text[1].text = characterData["Class"].ToString();
                        slot1Text[2].text = "레벨 " + characterData["CharacterLevel"].ToString();
                    }
                    else
                    {
                        Debug.Log($"캐릭터1의 정보 찾지못함.");
                    }
                });
                #endregion

                #region 캐릭터2
                //Debug.Log($"for문 2번째");
                // userEmail 문서에서 캐릭터1 컬렉션의 Info문서 참조
                DocumentReference character2InfoDocRef = userDocRef.Collection("캐릭터2").Document("Info");

                // 데이터 읽기
                character2InfoDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError($"캐릭터2의 정보 읽기실패." + task.Exception);
                        return;
                    }

                    DocumentSnapshot characterSnapshot = task.Result;
                    Debug.Log($"캐릭터2의 정보 읽기성공.");
                    if (characterSnapshot.Exists)
                    {
                        Debug.Log("characterSnapshot.Exists == true");
                        // 캐릭터 정보 가져오기
                        IDictionary<string, object> characterData = characterSnapshot.ToDictionary();
                        slot2Text[0].text = characterData["NickName"].ToString();
                        slot2Text[1].text = characterData["Class"].ToString();
                        slot2Text[2].text = "레벨 " + characterData["CharacterLevel"].ToString();
                    }
                    else
                    {
                        //Debug.Log($"캐릭터2의 정보 찾지못함.");
                    }
                });
                #endregion

                #region 캐릭터3
                //Debug.Log($"for문 3번째");
                // userEmail 문서에서 캐릭터1 컬렉션의 Info문서 참조
                DocumentReference character3InfoDocRef = userDocRef.Collection("캐릭터3").Document("Info");

                // 데이터 읽기
                character3InfoDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError($"캐릭터3의 정보 읽기실패." + task.Exception);
                        return;
                    }

                    DocumentSnapshot characterSnapshot = task.Result;
                    //Debug.Log($"캐릭터3의 정보 읽기성공.");
                    if (characterSnapshot.Exists)
                    {
                        Debug.Log("characterSnapshot.Exists == true");
                        // 캐릭터 정보 가져오기
                        IDictionary<string, object> characterData = characterSnapshot.ToDictionary();
                        slot3Text[0].text = characterData["NickName"].ToString();
                        slot3Text[1].text = characterData["Class"].ToString();
                        slot3Text[2].text = "레벨 " + characterData["CharacterLevel"].ToString();
                    }
                    else
                    {
                        Debug.Log($"캐릭터3의 정보 찾지못함.");
                    }
                });
                #endregion
            }
        });
    }
}