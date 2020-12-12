using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;

public class labelManager : MonoBehaviour
{
    public GameObject labelImage;
    public GameObject labelText;

    public Sprite label1_flipped;
    // Start is called before the first frame update
    void Start()
    {
        labelImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);

        //only check touch input if the painting has stopped moving
        if (!gameObject.GetComponent<flippingManager>().checkMoving())
        {
            if (touch.phase == TouchPhase.Ended)
            {
                if (Input.touchCount == 1)
                {
                    Vector3 startPoint = touch.position;
                    Ray theRay = Camera.main.ScreenPointToRay(startPoint);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(theRay, out hitInfo, 100))
                    {
                        GameObject objectHit = hitInfo.collider.transform.gameObject;
                        //Debug.Log("The name of the object we've just hit is " + objectHit.name);
                        //check if hit labels
                        if (objectHit.name.Contains("label")) displayLabelContent(objectHit);
                    }
                }
            }
        }

        
    }

    private void displayLabelContent(GameObject objectHit)
    {
        //set active the image
        if (!labelImage.activeSelf)
        {
            labelImage.SetActive(true);
        }

        //change color of sprite image
        objectHit.GetComponent<SpriteRenderer>().color = Color.white;


        //change sprite 
        //special case for label1 which has to be flipped
        Sprite tmpSprite;
        if (objectHit.name == "label1")
        {
            tmpSprite = label1_flipped;
        }
        //else, assign the image's sprite to that of the objectHit
        else
        {
            tmpSprite = objectHit.GetComponent<SpriteRenderer>().sprite;
        }
        labelImage.GetComponent<Image>().sprite = tmpSprite;

        //change sprite size accordingly
        //float multiplier = GameObject.Find("Canvas").GetComponent<Canvas>().referencePixelsPerUnit * 1.5f;
        float w = tmpSprite.bounds.size.x;
        float h = tmpSprite.bounds.size.y;
        float ratio = h / w;
        float new_w = 2048 - 200 - 700 - 200;//screen width - margins - textBox - marginMiddle
        float new_h = ratio * new_w;
        //clamp image height to 900
        float maxHeight = 900;
        if (new_h > maxHeight)
        {
            new_h = maxHeight;
            new_w = new_h / ratio;
        }
        labelImage.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(new_w, new_h);

        //change text content
        string text = objectHit.GetComponent<Text>().text;
        labelText.GetComponent<TextMeshProUGUI>().SetText(text);
    }
}
