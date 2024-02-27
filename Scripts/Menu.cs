using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;    // text mesh pro ekledim
using Photon.Pun;   // PHOTON EKLEDİM RPM için!!
using Photon.Realtime;


public class Menu : MonoBehaviourPunCallbacks   // RPC(REMOTE PROCEDURE CALLS) İÇİN BÖYLE YAPTIM
{
    [Header("Screens")]
    public GameObject mainScreen;
    public GameObject lobbyScreen;

    [Header("Main Screen")]
    public Button createRoomButton;
    public Button joinRoomButton;

    [Header("Lobby Screen")]
    public TextMeshProUGUI playerListText;
    public Button startGameButton;


    // Start is called before the first frame update
    void Start()
    {
        createRoomButton.interactable = false;      // İLK BAŞTA FALSE OLARAK DEĞER ALSIN!! 
        joinRoomButton.interactable = false;
        
    }

    public override void OnConnectedToMaster()
    {
        createRoomButton.interactable = true;   // bağlantı olunca "TRUE" yaptım    // İLK BAŞTA FALSE OLARAK DEĞER ALSIN!! 
        joinRoomButton.interactable = true;
    }

    void SetScreen(GameObject screen)
    {
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(false);
        screen.SetActive(true);

    }

    public void OnCreateRoomButton(TMP_InputField roomNameInput)
    {
        NetworkManager.instance.CreateRoom(roomNameInput.text);
    }

    public void OnJoinRoomButton(TMP_InputField roomNameInput)
    {
        NetworkManager.instance.JoinRoom(roomNameInput.text);
    }

    public void OnPlayerNameUpdate(TMP_InputField playerNameInput)
    {
        PhotonNetwork.NickName = playerNameInput.text;
    }

    public override void OnJoinedRoom()
    {
        SetScreen(lobbyScreen);
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);     // giren çıkan herkesi burada duyurusunu yapıyorum!!
 
    }

    [PunRPC]        //bunu yazarak duyuru yapıyorum!!
    public void UpdateLobbyUI()
    {
        playerListText.text = "";   // listeyi her seferinde temizle!!

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n";
        }
        if (PhotonNetwork.IsMasterClient)
        {
            startGameButton.interactable = true;
        }
        else
        {
            startGameButton.interactable = false;
            
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateLobbyUI();       // UI kısmının güncellemesini yapıyoruz!!!
    }
    public void OnLeaveLobbyButton()
    {
        PhotonNetwork.LeaveRoom();
        SetScreen(mainScreen);      // lobiden ayrılınca ana ekrana gitmesini istiyorum!!
    }

    public void OnStartGameButton()
    {
        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Game"); // herkese duyurduk ve game sahnesine yönlendirdim!!!

    }





}
