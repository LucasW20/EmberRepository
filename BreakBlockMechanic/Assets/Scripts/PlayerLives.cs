using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] int maxLives = 5;
    int currentLives;

    [SerializeField] LivesBar livesBar;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLives <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public void loseLives(int livesLost)
    {
        currentLives -= livesLost;
        livesBar.setLives(currentLives);
    }
}
