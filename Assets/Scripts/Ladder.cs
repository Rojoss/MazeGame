using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

    public GameObject ladderObject;
    public Transform climbTarget;

    private bool climbing = false;
    private bool onTop = false;

    private Transform player;
    private Vector3 playerPos;

    void OnTriggerEnter(Collider other) {
        if (climbing) {
            return;
        }

        if (!other.CompareTag("Player")) {
            return;
        }

        Vector3 relativePoint = ladderObject.transform.InverseTransformPoint(other.transform.position);
        if (relativePoint.x >= 0.3f && relativePoint.x <= 0.7f) {
            climbing = true;

            player = other.transform;
            other.GetComponent<Rigidbody>().useGravity = false;
            playerPos = other.transform.position;

            StartCoroutine(Climb());
        }
    }

    private IEnumerator Climb() {
        for (float p = 0; p < 1; p += 0.01f) {
            yield return new WaitForFixedUpdate();
            player.transform.position = Vector3.Lerp(playerPos, climbTarget.position, p);
        }
        climbing = false;
        player.GetComponent<Rigidbody>().useGravity = true;
    }

}