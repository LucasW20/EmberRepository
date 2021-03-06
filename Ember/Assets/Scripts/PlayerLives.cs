using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    int maxLives = 2;
    int currentLives;

    [SerializeField] LivesBar livesBar;
    PassingScene passingScene;

    // Start is called before the first frame update
    void Start()
    {
        passingScene = GameObject.Find("SaveObject").GetComponent<PassingScene>();
        maxLives = passingScene.getMaxLives();
        currentLives = maxLives;
        livesBar.setLives(currentLives);
    }
    public int getLives() { return currentLives; }

    public void setLives(int n) { currentLives = n; }

    public void adjustMaxLives(int n)
    {
        maxLives += n;
        currentLives += n;
        livesBar.setLives(currentLives);
    }

    public int getMaxLives()
    {
        return maxLives;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void loseLives(int livesLost)
    {
        currentLives -= livesLost;
        livesBar.setLives(currentLives);

        if (currentLives <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
