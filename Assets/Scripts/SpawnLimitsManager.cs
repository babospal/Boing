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

            spawnableNormalTCount = 2;
            spawnableLGCount = 3;
            spawnableLargeCount = 2;
            spawnablex2JumpCount = 2;
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {

            spawnableNormalTCount = 2;
            spawnableLGCount = 3;
            spawnableLargeCount = 2;
            spawnablex2JumpCount = 2;
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            spawnableNormalTCount = 2;
            spawnableLGCount = 3;
            spawnableLargeCount = 2;
            spawnablex2JumpCount = 2;
        }
        else if (SceneManager.GetActiveScene().name == "Level4")
        {

            spawnableNormalTCount = 2;
            spawnableLGCount = 3;
            spawnableLargeCount = 2;
            spawnablex2JumpCount = 2;
        }
      else if (SceneManager.GetActiveScene().name == "Level5")
        {

            spawnableNormalTCount = 2;
            spawnableLGCount = 3;
            spawnableLargeCount = 2;
            spawnablex2JumpCount = 2;
        }
       }
    }
