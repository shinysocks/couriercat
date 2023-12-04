using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static string currentPlanet = "";
    public static GameManager gameManager;
    public static bool interacting;
    [SerializeField] Animator transition;
    [SerializeField] GameObject catIcon;
    [SerializeField] float sceneSwitchDelay;
    [SerializeField] GameObject spaceUI;
    [SerializeField] GameObject joystick;
    public static Dictionary<string, Vector3> planets;
    bool sceneLoaded = false;
    bool ran = false;
    bool spin = false;
    public static List<string> completedMailboxes = new List<string>();

	void Awake() {
		if (!gameManager) {	
			gameManager = this; // In first scene, make us the singleton.
			DontDestroyOnLoad(this);
		} else if (gameManager != this) {
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (!ran) {
            planets = new Dictionary<string, Vector3>();

            // fill dict with planets
            foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet")) {
                planets.Add(planet.name, planet.transform.position);
            }
            ran = true;
        }
    }

    public void FixedUpdate() {
        if (spin) {
            catIcon.transform.Rotate(0f, 0f, -6f);
        } else {
            catIcon.transform.rotation = Quaternion.identity;
        }
    }

    public void Load(string sceneName) {
        // Animate
        StartCoroutine(AnimateAndLoad(sceneName));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) => sceneLoaded = true;

    IEnumerator AnimateAndLoad(string sceneName) {
        spin = true;
        transition.SetBool("Load", true);
        yield return new WaitForSeconds(sceneSwitchDelay);

        // check for endgame condition
        if (completedMailboxes.Count >= 5 && sceneName == "Space") {
            SceneManager.LoadScene("Credits");
            sceneName = "Credits";
        } else {
            SceneManager.LoadScene(sceneName);
        }

        if (sceneLoaded) {
            transition.SetBool("Load", false);
            if (sceneName == "Credits") {
                joystick.SetActive(false);
                spaceUI.SetActive(false);
            } else {
                // disable or enable space UI
                if (spaceUI.activeSelf) {
                    spaceUI.SetActive(false);
                } else {
                    spaceUI.SetActive(true);
                }
            }
        }
        yield return new WaitForSeconds(1.1f);
        spin = false;
    }
}
