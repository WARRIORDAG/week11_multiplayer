using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;       // ı have downloaded pun2 asset store!!


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    private void Awake()    // starttan önce bunu çalıştırmış olacağım
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }

        

    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }
    /*
    public override void OnConnectedToMaster()
    {
        Debug.Log("Master server bağlantı sağlandı!!");
        CreateRoom("oyun salonu 1: ");
    }
*/


    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }
    /*
    public override void OnCreatedRoom()
    {
        Debug.Log("belirtilen isimde oda oluşturuldu!!" + PhotonNetwork.CurrentRoom.Name);
    }
*/
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    [PunRPC]            //multiplayer oyun için yazdım bu scriptin tamamı böyle!!
    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }


}
