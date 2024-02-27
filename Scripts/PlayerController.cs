using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;



public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public int id;

    [Header("Info")]
    public float moveSpeed;
    public float jumpForce;
    public GameObject hatObject;

    [HideInInspector]
    public float curHatTime;

    [Header("Components")]
    public Rigidbody rig;
    public Player photonPlayer;

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))        // space tuşu için koşul oluşturdum
        {
            TryJump();
        }

    }

    void Move()     // horizontal ve vertical hareketler için bu fonksiyonu yazdım
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        rig.velocity = new Vector3 (x, rig.velocity.y, z);  // rigid body olarak tanımladığım değişkenin yön değişimiiçin yazdım
    }

    void TryJump()
    {
        Ray ray = new Ray (transform.position, Vector3.down);
        if (Physics.Raycast(ray, 0.7f))
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }
}
