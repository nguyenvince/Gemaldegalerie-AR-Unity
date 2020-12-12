using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchMode : MonoBehaviour
{

    public GameObject comparison2D;
    public GameObject comparisonAR;
    public GameObject buttonCanvas;

    private int mode = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeMode()
    {
        if (mode % 2 == 0)
        {
            comparison2D.SetActive(false);
            comparisonAR.SetActive(true);
            buttonCanvas.SetActive(false);
            buttonCanvas.SetActive(true);
        }
        else
        {
            comparison2D.SetActive(true);
            comparisonAR.SetActive(false);
            buttonCanvas.SetActive(false);
            buttonCanvas.SetActive(true);
        }
        mode++;
    }
}
