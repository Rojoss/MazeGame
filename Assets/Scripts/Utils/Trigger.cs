using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Trigger : MonoBehaviour {

    public TriggerEvent onTriggerEnter;
    public TriggerEvent onTriggerExit;

    void OnTriggerEnter(Collider other) {
        onTriggerEnter.Invoke(other);
    }

    void OnTriggerExit(Collider other) {
        onTriggerExit.Invoke(other);
    }
}

[System.Serializable]
public class TriggerEvent : UnityEvent<Collider> {}