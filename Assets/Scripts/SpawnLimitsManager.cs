using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnLimitsManager : MonoBehaviour
{
    public int spawnableNormalTCount = 0;
    public int spawnableLGCount = 0;
    public int spawnableLargeCount = 0;
    public int spawnablex2JumpCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
   void Update()
    {
        
        if (SceneManager.GetActiveScene().name =="Level1")
        {

            spawnableNormalTCount = 1;
            spawnableLGCount = 1;
            spawnableLargeCount = 1;
            spawnablex2JumpCount = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {

            spawnableNormalTCount = 0;
            spawnableLGCount = 2;
            spawnableLargeCount = 0;
            spawnablex2JumpCount = 0;
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            spawnableNormalTCount = 2;
            spawnableLGCount = 1;
            spawnableLargeCount = 0;
            spawnablex2JumpCount = 0;
        }
        else if (SceneManager.GetActiveScene().name == "Level4")
        {

            spawnableNormalTCount = 1;
            spawnableLGCount = 2;
            spawnableLargeCount = 0;
            spawnablex2JumpCount = 1;
        }
      else if (SceneManager.GetActiveScene().name == "Level5")
        {

            spawnableNormalTCount = 2;
            spawnableLGCount = 4;
            spawnableLargeCount = 0;
            spawnablex2JumpCount = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Level6")
        {

            spawnableNormalTCount = 5;
            spawnableLGCount = 0;
            spawnableLargeCount = 0;
            spawnablex2JumpCount = 0;
        }
    }
}
        
    
