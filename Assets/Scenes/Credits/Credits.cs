using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{   
    void Start() {
        StartCoroutine(Wait());
    }
    void FixedUpdate() {
        transform.position += new Vector3(0f, 3f, 0f);
    }

    public void OpenURL(string url) {
        Application.OpenURL(url);
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(32.5f);
        Application.Quit();
    }     
}
