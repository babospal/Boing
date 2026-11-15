using UnityEngine;
using UnityEngine.SceneManagement;

public class Bed : MonoBehaviour
{
    public GameObject ending;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D bed)
    {
      if(bed.CompareTag("Player"))
      {
        LoadNextLevel();
      }
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Instantiate(ending);
            Invoke("BackToMenu",5f);

        }
    }
    void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
