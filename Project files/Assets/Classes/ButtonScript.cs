using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    public string sceneToLoad;

    // Use this for initialization
    void Start()
    {
        if (name == "Level1" && GameStateController.Instance.level1)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }

        if (name == "Level2" && GameStateController.Instance.level2)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }

        if (name == "Level3" && GameStateController.Instance.level3)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Exit()
    {
        Application.Quit();

    }
}
