using UnityEngine;

public class WatchPlayer : MonoBehaviour
{
    public float moveSpeedCamera = 1.0f;

    private Transform player;
    private Vector3 playerVector;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        playerVector = player.position;
        playerVector.z = -10;

        transform.position = Vector3.Lerp(transform.position, playerVector, Time.deltaTime * moveSpeedCamera);
    }
}