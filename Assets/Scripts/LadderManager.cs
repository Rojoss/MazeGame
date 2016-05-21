using UnityEngine;
using System.Collections;

public class LadderManager : MonoBehaviour {

    public GameObject ladderPrefab;
    public Transform[] spawnLocations;

    public float placeDistance = 1f;
    public float interactdistance = 2f;

    public float floorY = -1f;

    public LayerMask ladderLayer;
    public LayerMask obstacleLayer;

    private bool pickedUp = false;
    private bool placed = false;

    private GameObject ladder;

    public static LadderManager Instance { get; private set; }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //Spawn ladder at random location
        ladder = Instantiate<GameObject>(ladderPrefab);
        ladder.transform.position = spawnLocations[Random.Range(0, spawnLocations.Length)].position;
        Physics.IgnoreCollision(ladder.GetComponent<Collider>(), GetComponent<Collider>());
    }

    void OnDestroy() {
        Instance = null;
    }

    void Update() {
        if (!Input.GetMouseButtonDown(0)) {
            return;
        }

        //Not yet picked up. Check for clicking on ladder.
        if (!pickedUp) {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, transform.forward, out hit, interactdistance, ladderLayer)) {
                return;
            }

            ladder.SetActive(false);
            pickedUp = true;
            return;
        }

        //Picked up but not yet placed. Try to place it.
        if (!placed) {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, transform.forward, out hit, interactdistance, obstacleLayer)) {
                return;
            }
            
            Vector3 pos = hit.point; //Get the hit point position on the wall
            pos += hit.normal * placeDistance; //Offset location from wall
            pos.y = floorY; //Set y position to floor Y
            ladder.transform.position = pos;

            ladder.transform.rotation = Quaternion.FromToRotation(Vector3.right, hit.normal);
            ladder.transform.Rotate(new Vector3(0, 0, 5));

            ladder.SetActive(true);
            Physics.IgnoreCollision(ladder.GetComponent<Collider>(), GetComponent<Collider>());

            placed = true;
        }
        
    }


}