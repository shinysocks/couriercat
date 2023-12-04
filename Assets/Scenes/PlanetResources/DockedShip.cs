using UnityEngine;

public class DockedShip : MonoBehaviour {
    GameObject highlight;
    bool inRange;

    void Awake() {
        highlight = transform.GetChild(0).gameObject;
    }

    void Update() {
        // check if cat is inRange and if ship touched
        if (inRange && Input.touchCount > 0 && !Cat.isWalking) {
            Touch touch = Input.GetTouch(0);
            if ((Camera.main.ScreenToWorldPoint(touch.position) - transform.position).magnitude < (24)) {
                GameManager.gameManager.Load("Space");
                inRange = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            highlight.SetActive(true);
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            highlight.SetActive(false);
            inRange = false;
        }
    }
}
