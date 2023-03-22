using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {

        // Esto hay que quitarlo
        if(SceneManager.GetActiveScene().buildIndex == 3){
            SceneManager.LoadScene(0);
            return;
        }


        loadNextLevel();
    }

    public void loadNextLevel(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
