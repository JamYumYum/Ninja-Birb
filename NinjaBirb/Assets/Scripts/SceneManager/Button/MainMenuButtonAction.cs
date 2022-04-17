using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonAction : MonoBehaviour
{
    [SerializeField]
    GameObject startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.GetComponent<ButtonController>().action = () =>
        {
            SceneManager.LoadSceneAsync(1);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
