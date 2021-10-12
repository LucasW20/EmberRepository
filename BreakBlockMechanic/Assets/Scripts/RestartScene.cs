using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            restartLevel();
        }
    }

    public void restartLevel()
    {
        SceneManager.LoadScene("SampleScene"); //Load scene called Game
    }
}
