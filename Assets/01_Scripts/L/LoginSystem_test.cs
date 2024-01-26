using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;
using Firebase;
using UnityEngine.SceneManagement;

public class LoginSystem_test : MonoBehaviour
{
    public static string userEmail;
    public string password;
    public InputField emailInput;
    public InputField pwInput;

    public Text outputText;

    public bool isExist = false;
    private FirebaseAuth auth; // 로그인 or 회원가입 등에 사용
    private FirebaseUser user; // 인증이 완료된 유저 정보

    void Start()
    {
        LoginState += OnChangedState;
        Init();
    }

    public string UserId => user.UserId;

    public Action<bool> LoginState;

    public void Init()
    {
        auth = FirebaseAuth.DefaultInstance;

        // 임시 처리
        if (auth.CurrentUser != null)
        {
            Logout();
        }
        auth.StateChanged += OnChanged;
    }

    private void OnChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signed = auth.CurrentUser != user && auth.CurrentUser != null;
            if (!signed && user != null)
            {
                Debug.Log("로그아웃");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if (signed)
            {
                Debug.Log("로그인");
                LoginState?.Invoke(true);
            }
        }
    }
    
    // 신규유저 데이터 FireStore에 작성
    IEnumerator CreateUserInFirestore(string userEmail, string userPassword)
    {
        Debug.Log("코루틴 시작");

        // Firestore에 사용자 데이터 추가
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(userEmail);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            {"UserPw", userPassword },
            {"UpdateTime", FieldValue.ServerTimestamp },
            {"UID", FirebaseAuth.DefaultInstance.CurrentUser.UserId} // 현재 사용자의 UID 추가
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
                Debug.Log($"{userEmail} 의 회원가입이 완료되었습니다...");
                Debug.Log("데이터작성 끝");
            }
        });
        Debug.Log("코루틴 종료");
    }

    public void OnClickCreateBtn()
    {
        CheckingEmailAndPw();

        auth.CreateUserWithEmailAndPasswordAsync(userEmail, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("회원가입 취소");
                return;
            }
            if (task.IsFaulted)
            {
                AggregateException exception = task.Exception;
                if (exception != null)
                {
                    foreach (Exception innerException in exception.InnerExceptions)
                    {
                        if (innerException is FirebaseException firebaseException)
                        {
                            // FirebaseException의 ErrorCode 및 Message를 디버그 로그에 출력
                            Debug.LogError($"FirebaseException: {firebaseException.ErrorCode} - {firebaseException.Message}");
                        }
                        else
                        {
                            // 기타 예외의 경우 메시지만 출력
                            Debug.LogError($"Exception: {innerException.Message}");
                        }
                    }
                }

                // 회원가입 실패 이유 => 이메일이 비정상 / 비밀번호가 너무 간단 / 이미 가입된 이메일 등등..
                Debug.Log("회원가입 실패");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            Debug.Log("회원가입 완료");

            // 회원가입이 완료된 후에 다른 동작을 수행할 수 있음
            StartCoroutine(CreateUserInFirestore(userEmail, password));
        });
    }

    // 기존유저 데이터 불러오기
    IEnumerator ReadUserData(string userEmail)
    {
        Debug.Log("코루틴 시작");
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(userEmail);
        yield return docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists) // 이메일이 있다면...
            {
                Debug.Log($"{snapshot.Id}");
                isExist = true;
                Dictionary<string, object> doc = snapshot.ToDictionary();

                foreach (KeyValuePair<string, object> pair in doc)
                {
                    if (pair.Key == "UserPw")
                    {
                        Debug.Log("Password :: " + pair.Value.ToString());
                    }
                    if (pair.Key == "UID")
                    {
                        Debug.Log("UID :: " + pair.Value.ToString());
                    }
                }
            }
            else
            {
                Debug.Log($"{userEmail} 은 존재하지 않습니다...");
                outputText.text = $"{userEmail} 은 존재하지 않습니다...";
                isExist = false;
            }
        });
        Debug.Log("코루틴 종료");
    }

    public void OnClickLoginBtn()
    {
        Debug.Log("로그인버튼 누름");

        CheckingEmailAndPw();
        auth.SignInWithEmailAndPasswordAsync(userEmail, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("로그인 취소");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("로그인 실패");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            Debug.Log("로그인 완료");

            StartCoroutine(ReadUserDataAndLoadScene(userEmail));
        });
    }

    IEnumerator ReadUserDataAndLoadScene(string userEmail)
    {
        yield return StartCoroutine(ReadUserData(userEmail));
        SceneManager.LoadScene("Lobby_test");
    }

    public void Logout()
    {
        auth.SignOut();
        Debug.Log("로그아웃");
    }

    public void CheckingEmailAndPw()
    {
        userEmail = emailInput.text;
        password = pwInput.text;
    }

    private void OnChangedState(bool sign)
    {
        outputText.text = sign ? "로그인 : " : "로그아웃 : ";
        outputText.text += UserId;
    }
}
