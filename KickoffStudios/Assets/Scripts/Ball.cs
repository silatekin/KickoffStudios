using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Ball : MonoBehaviourPunCallbacks
{
    public GameObject owner;  // the player currently owning the ball
    public float releaseForce = 10f;  // the force to release the ball from the player

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // check if the ball collides with a player and release it if the player is not the owner
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<>();
            if (player != null && player != owner.GetComponent<>())
            {
                ReleaseBall(player.gameObject);
            }
        }
    }

    void ReleaseBall(GameObject newOwner)
    {
        // release the ball from the current owner and set the new owner
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;
        rb.AddForce((newOwner.transform.position - transform.position).normalized * releaseForce, ForceMode.Impulse);
        owner = newOwner;
        photonView.RPC("SetOwner", RpcTarget.AllBuffered, owner.GetComponent<PhotonView>().ViewID);
    }

    [PunRPC]
    void SetOwner(int viewID)
    {
        // set the owner of the ball for all clients
        owner = PhotonView.Find(viewID).gameObject;
    }
}
