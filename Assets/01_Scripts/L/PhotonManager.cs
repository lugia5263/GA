using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // 게임의 버전
    private readonly string version = "1.0";
    // 유저의 닉네임
    private string userId = "Zack";
    
    // 유저명을 입력할 TextMeshPro Input Field
    public TMP_InputField inputUserID;

    // 캐릭터 선택창
    public GameObject selectPanel;

    // 룸 목록에 대한 데이터를 저장하기 위한 딕셔너리 자료형
    private Dictionary<string, GameObject> rooms = new Dictionary<string, GameObject>();
    // 룸 목록을 표시할 프리팹
    private GameObject roomItemPrefab;
    // RoomItem 프리팹이 추가될 ScrollContent
    //public Transform scrollContent;

    void Awake()
    {
        // 마스터 클라이언트의 씬 자동 동기화 옵션
        PhotonNetwork.AutomaticallySyncScene = true;
        //PhotonNetwork.IsMessageQueueRunning = false;

        // 게임 버전 설정
        PhotonNetwork.GameVersion = version;
        // 접속 유저의 닉네임 설정
        // PhotonNetwork.NickName = userId;

        // 포톤 서버와의 데이터의 초당 전송 횟수
        Debug.Log(PhotonNetwork.SendRate);

        // RoomItem 프리팹 로드
        //roomItemPrefab = Resources.Load<GameObject>("RoomItem");

        // 포톤 서버 접속
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    void Start()
    {

    }

    // 유저명을 설정하는 로직
    public void SetUserId()
    {
        if (string.IsNullOrEmpty(inputUserID.text))
        {
            userId = $"USER_{Random.Range(1,21):00}";
        }
        else
        {
            userId = inputUserID.text;
        }

        // 유저명 저장
        PlayerPrefs.SetString("USER_ID", userId);
        // 접속 유저의 닉네임 등록
        PhotonNetwork.NickName = userId;
    }
    #region 포톤 콜백 함수
    // 포톤 서버에 접속 후 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    // 로비에 접속 후 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");      
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Filed {returnCode}:{message}");
        // 룸을 생성하는 함수 실행
        MakeHome();
    }

    // 룸 생성이 완료된 후 호출되는 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    // 룸에 입장한 후 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName} , {player.Value.ActorNumber}");
        }
    }

    // 룸 목록을 수신하는 콜백 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // 삭제된 RoomItem 프리팹을 저장할 임시변수
        GameObject tempRoom = null;

        foreach(var roomInfo in roomList)
        {
            // 룸이 삭제된 경우
            if (roomInfo.RemovedFromList == true)
            {
                // 딕셔너리에서 룸 이름으로 검색해 저장된 RoomItem 프리팹를 추출
                rooms.TryGetValue(roomInfo.Name, out tempRoom);

                // RoomItem 프리팹 삭제
                Destroy(tempRoom);

                // 딕셔너리에서 해당 룸 이름의 데이터를 삭제
                rooms.Remove(roomInfo.Name);
            }
            else // 룸 정보가 변경된 경우
            {
                // 룸 이름이 딕셔너리에 없는 경우 새로 추가
                if (rooms.ContainsKey(roomInfo.Name) == false)
                {
                    Debug.Log("룸 이름이 딕셔너리에 없는 경우라서 추가합니다.");
                    // RoomInfo 프리팹을 scrollContent 하위에 생성
                    //GameObject roomPrefab = Instantiate(roomItemPrefab, scrollContent);
                    // 룸 정보를 표시하기 위해 RoomInfo 정보 전달
                    //roomPrefab.GetComponent<RoomData>().RoomInfo = roomInfo;
                    
                    // 딕셔너리 자료형에 데이터 추가
                    //rooms.Add(roomInfo.Name, roomPrefab);
                }
                else // 룸 이름이 딕셔너리에 없는 경우에 룸 정보를 갱신
                {
                    rooms.TryGetValue(roomInfo.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = roomInfo;
                }
            }
            
            Debug.Log($"Room={roomInfo.Name} ({roomInfo.PlayerCount}/{roomInfo.MaxPlayers})");
        }
    }
    #endregion

    #region UI_BUTTON_EVENT
    // 로그인 버튼 누르면 실행
    public void OnClickLoginBtn() 
    {
        if (inputUserID == null || string.IsNullOrEmpty(inputUserID.text))
        {
            Debug.Log("UserID를 입력하세요");
        }
        else
        {
            string userID = inputUserID.text;

            // PlayerPrefs에 userID 저장
            PlayerPrefs.SetString("UserID", userID);
            PlayerPrefs.Save();

            LoadPlayerData(userID);

            JoinHome();
        }
    }

    public void JoinHome()
    {
        // 유저명 저장
        SetUserId();

        PhotonNetwork.JoinRoom("Room_Home");
    }

    public void MakeHome()
    {
        // 유저명 저장
        SetUserId();

        // 룸의 속성 정의
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;     // 룸에 입장할 수 있는 최대 접속자 수
        ro.IsOpen = true;       // 룸의 오픈 여부
        ro.IsVisible = true;    // 로비에서 룸 목록에 노출시킬 여부

        PhotonNetwork.CreateRoom("Room_Home", ro);
    }
    #endregion

    #region PlayerPrefs 유저의 데이터 로딩
    void LoadPlayerData(string userID)
    {
        if (PlayerPrefs.HasKey(userID))
        {
            LoadExistingPlayerData(userID);
        }
        else
        {
            CreateDefaultPlayerData(userID);
        }
    }

    // 기존 플레이어 데이터 로딩
    void LoadExistingPlayerData(string userID)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", userID);
        int playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1); // 나중에 변수추가해서 성장한만큼 변수에 저장하게끔..
        int playerAtk = PlayerPrefs.GetInt(userID + "_PlayerAtk", 10);

        Debug.Log("존재하는 유저의 userID : " + userID);
        Debug.Log("존재하는 유저의 level : " + playerLevel); // 나중에 변수추가해서 성장한만큼 변수에 저장한걸 출력하게끔..
        Debug.Log("존재하는 유저의 공격력: " + playerAtk);
    }

    // Default 플레이어 데이터 생성
    void CreateDefaultPlayerData(string userID)
    {
        // 예시로 대충 1레벨일때의 능력치.. 실제로 만들때 초기화하기
        int defaultLevel = 1;
        int defaultAtk = 10;

        PlayerPrefs.SetInt(userID, defaultLevel);
        PlayerPrefs.SetInt(userID, defaultAtk);
        PlayerPrefs.Save();

        Debug.Log("처음만든 userID: " + userID + ", 시작 레벨 : " + defaultLevel + ", 시작 공격력 : " + defaultAtk);
    }
    #endregion
}
