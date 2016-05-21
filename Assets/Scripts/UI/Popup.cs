using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public abstract class Popup : MonoBehaviour {

    private static Popup current = null;
    public RigidbodyFirstPersonController playerController;

    public void Open() {
        if (isOpen()) {
            return;
        }
        playerController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (current != null) {
            current.Close();
        }
        current = this;
        current.gameObject.SetActive(true);
        onOpen();
    }

    public void Close() {
        playerController.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (current == null) {
            return;
        }
        current.gameObject.SetActive(false);
        current = null;
        onClose();
    }

    public bool isOpen() {
        if (current == null) {
            return false;
        }
        return current.Equals(this);
    }

    public abstract void onOpen();

    public abstract void onClose();

    void Update() {
        if (current == null) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Close();
        }
    }
}