using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneManager_paintingOptions : MonoBehaviour
{
    public GameObject swiping;
    public GameObject peeling;
    public GameObject flipping;
    public GameObject paintingInfo;
    public GameObject backButton;

    private string nameButton;

    private Dictionary<string, GameObject> scenes = new Dictionary<string, GameObject>();
    private GameObject rectCanvas;
    void Awake()
    {
        scenes.Add("artistic history", swiping);
        scenes.Add("science", peeling);
        scenes.Add("provenance", flipping);
    }

    // Start is called before the first frame update
    private void Start()
    {
        rectCanvas = GameObject.Find("rectCanvas");
        if (rectCanvas) rectCanvas.SetActive(false);
        backButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene()
    {
        nameButton = EventSystem.current.currentSelectedGameObject.name;
        paintingInfo.SetActive(false);
        scenes[nameButton].SetActive(true);
        backButton.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void back()
    {
        scenes[nameButton].SetActive(false);
        backButton.SetActive(false);
        paintingInfo.SetActive(true);
    }
}
