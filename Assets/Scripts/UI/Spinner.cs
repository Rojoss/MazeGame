using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

    public Vector3 speed;

	void FixedUpdate() {
        transform.Rotate(speed);
	}

}