using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform player;
    public bool cameraon = true;
    void Update() {
        if (cameraon = true)
        {
            transform.position = player.transform.position;
        }

    }
}
