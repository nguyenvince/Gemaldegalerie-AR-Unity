using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine.SceneManagement;

public class SceneManager_Instruction : MonoBehaviour
{
    public GameObject scrollObject;
    public GameObject startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showStartButton()
    {
        //if the last panel, show start button
        if (scrollObject.GetComponent<SimpleScrollSnap>().CurrentPanel == scrollObject.GetComponent<SimpleScrollSnap>().NumberOfPanels - 1)
        {
            Debug.Log("start");
            startButton.SetActive(true);
        }
    }

    public void nextScene()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }
}
