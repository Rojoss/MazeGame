using UnityEngine;

public class PersistentObject : MonoBehaviour {
	void Awake() {
        Object.DontDestroyOnLoad(gameObject);
	}
}
