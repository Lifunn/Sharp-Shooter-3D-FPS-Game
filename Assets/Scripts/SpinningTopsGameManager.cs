using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class SpinningTopsGameManager :MonoBehaviourPunCallbacks
{

    [Header("UI")]
    public GameObject uI_InformPanelGameobject;
    public TextMeshProUGUI uI_InformText;
    public GameObject searchForGamesButtonGameobject;
    public GameObject adjust_Button;
    public GameObject raycastCenter_Image;


    // Start is called before the first frame update
    void Start()
    {
        uI_InformPanelGameobject.SetActive(true);
        uI_InformText.text = "Search For Games tp Battle!";
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        uI_InformText.text = "Searching for available rooms...";

        PhotonNetwork.JoinRandomRoom();

        searchForGamesButtonGameobject.SetActive(false);


    }


    public void OnQuitMatchButtonClicked()
    {

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();

        }
        else
        {
            SceneLoader.Instance.LoadScene("Scene_Lobby");
        }
        


    }
    #endregion


    #region PHOTON Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
      
        Debug.Log(message);
        uI_InformText.text = message;

        CreateAndJoinRoom();
    }


    public override void OnJoinedRoom()
    {
        adjust_Button.SetActive(false);
        raycastCenter_Image.SetActive(false);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            uI_InformText.text = "Joined to " + PhotonNetwork.CurrentRoom.Name + ". Waiting for other players...";


        }
        else
        {
            uI_InformText.text = "Joined to " + PhotonNetwork.CurrentRoom.Name;
            StartCoroutine(DeactivateAfterSeconds(uI_InformPanelGameobject, 2.0f));
        }

        Debug.Log( " joined to "+ PhotonNetwork.CurrentRoom.Name);
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to "+ PhotonNetwork.CurrentRoom.Name+ " Player count "+ PhotonNetwork.CurrentRoom.PlayerCount);
        uI_InformText.text = newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " Player count " + PhotonNetwork.CurrentRoom.PlayerCount;

        StartCoroutine(DeactivateAfterSeconds(uI_InformPanelGameobject, 2.0f));


    }


    public override void OnLeftRoom()
    {
        // PENGAMAN: Cek apakah SceneLoader masih ada?
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene("Scene_Lobby");
        }
        else
        {
            // Fallback: Jika SceneLoader sudah hancur (misal saat quit game),
            // kita pakai cara standar Unity atau biarkan saja (tergantung situasi).
            // Untuk aman, kita pakai SceneManager bawaan Unity sebagai cadangan.
            Debug.LogWarning("SceneLoader Instance is null (mungkin sedang Quit Game). Menggunakan SceneManager standar.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Lobby");
        }
    }


    #endregion


    #region PRIVATE Methods
    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room" + Random.Range(0,1000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        //Creatin the room
        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);

    }

    IEnumerator DeactivateAfterSeconds(GameObject _gameObject, float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        _gameObject.SetActive(false);

    }


    #endregion



}