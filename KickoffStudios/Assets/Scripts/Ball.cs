using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviourPunCallbacks
{
    public float attachForce = 10f;  // the force to attach the ball to the player

    private Rigidbody rb;
    private GameObject attachedPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attachedPlayer = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        // check if the ball collides with a player and attach it if no player is currently attached
        if (collision.gameObject.CompareTag("Player") && attachedPlayer == null)
        {
            var player = collision.gameObject;
            if (player != null)
            {
                AttachBall(player.gameObject);
            }
        }
    }

    void AttachBall(GameObject player)
    {
        // attach the ball to the player
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        transform.SetParent(player.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        attachedPlayer = player;
        photonView.RPC("SetAttachedPlayer", RpcTarget.AllBuffered, attachedPlayer.GetComponent<PhotonView>().ViewID);
    }

    void DetachBall()
    {
        // detach the ball from the player
        rb.isKinematic = false;
        transform.SetParent(null);
        attachedPlayer = null;
        photonView.RPC("SetAttachedPlayer", RpcTarget.AllBuffered, 0);
    }

    [PunRPC]
    void SetAttachedPlayer(int viewID)
    {
        // set the attached player of the ball for all clients
        if (viewID == 0)
        {
            attachedPlayer = null;
        }
        else
        {
            attachedPlayer = PhotonView.Find(viewID).gameObject;
        }
    }

    void Update()
    {
        if (attachedPlayer != null)
        {
            // apply force to the ball in the direction of the attached player's movement
            Vector3 force = attachedPlayer.GetComponent<CharacterController>().velocity.normalized * attachForce;
            rb.AddForce(force);
        }
    }
}
