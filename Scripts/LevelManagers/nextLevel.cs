using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {

        // Esto hay que quitarlo
        /*if(SceneManager.GetActiveScene().buildIndex == 3){
            SceneManager.LoadScene(0);
            return;
        }*/


        loadNextLevel();
    }

    public void loadNextLevel(){
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        if (level == 7) level = 0;
        PlayerPrefs.SetInt("SavedLevel", level);
        SceneManager.LoadScene(level);
    }
}
