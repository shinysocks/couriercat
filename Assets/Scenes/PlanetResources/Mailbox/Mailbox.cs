using UnityEngine;

public class Mailbox : MonoBehaviour {
    GameObject highlight;
    bool inRange;
    Animator animator;
    bool mailed = false;

    void Awake() {
        highlight = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();

        foreach (string name in GameManager.completedMailboxes) {
            if (name == gameObject.name) {
                mailed = true;
                animator.SetBool("Interacted", true);
            }
        }
    }

    void Update() {
        if (inRange && Input.touchCount > 0 && !Cat.isWalking && !mailed) {
            Touch touch = Input.GetTouch(0);
            if ((Camera.main.ScreenToWorldPoint(touch.position) - transform.position).magnitude < (25)) {
                animator.SetBool("Interacted", true);
                highlight.SetActive(false);
                GameManager.completedMailboxes.Add(gameObject.name);
                mailed = true;

                if (GameManager.completedMailboxes.Count > 4) {
                    // wait for animation to finish
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Delivered")) {
                        Debug.Log("You Win!!");
                        // GameManager.EndGame();
                    }
                }
            }
        }        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player" && !mailed) {
            highlight.SetActive(true);
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player" && !mailed) {
            highlight.SetActive(false);
            inRange = false;
        }
    }
}
