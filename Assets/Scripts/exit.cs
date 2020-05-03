using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel() {
        Debug.Log("Coroutine fired");
        yield return new WaitForSeconds(3);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
