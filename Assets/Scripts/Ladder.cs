using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class Ladder : MonoBehaviour {

    public GameObject ladderObject;
    public Transform climbTarget;
    public GameObject UI;

    private bool climbing = false;

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
            playerPos = other.transform.position;
            other.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<FirstPersonController>().inputEnabled = false;

            StartCoroutine(Climb());
        }
    }

    private IEnumerator Climb() {
        for (float p = 0; p < 1; p += 0.02f) {
            yield return new WaitForFixedUpdate();
            player.transform.position = Vector3.Lerp(playerPos, climbTarget.position, p);
        }
        UI.SetActive(true);
    }

    private IEnumerator ClimbDown() {
        for (float p = 0; p < 1; p += 0.03f) {
            yield return new WaitForFixedUpdate();
            player.transform.position = Vector3.Lerp(climbTarget.position, playerPos, p);
        }
        climbing = false;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<FirstPersonController>().inputEnabled = true;
    }

    public void BackDown() {
        UI.SetActive(false);
        StartCoroutine(ClimbDown());
    }

    public void StepOver() {
        UI.SetActive(false);
        climbing = false;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.Translate(player.forward * 2);
        player.GetComponent<FirstPersonController>().inputEnabled = true;
    }

}