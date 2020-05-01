using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other) {
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel() {
        yield return new WaitForSeconds(3);
        Debug.Log("Coroutine fired");
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
