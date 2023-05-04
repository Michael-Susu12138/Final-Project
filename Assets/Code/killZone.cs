using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class killZone : MonoBehaviour
{
    public string levelToLoad;
    public string[] tagsToDestroy;

    void OnTriggerEnter2D(Collider2D other) {
        if(tagsToDestroy.Contains(other.tag)) {
            //Destroy(other.gameObject);
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
