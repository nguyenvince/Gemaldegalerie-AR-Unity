using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class flippingManager : MonoBehaviour
{
    public GameObject painting;
    private Rigidbody rb;
    public float torque = 1;
    public float snapAngle = 30f;
    private bool down = false;

    public float maxVelocity = 1.0f;
    

    //target angle for the flipping painting
    private Quaternion targetAngle;

    //obect hit for raycast
    private GameObject objectHit;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = painting.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Moved:
                down = true;
                Vector3 startPoint = touch.position;
                Ray theRay = Camera.main.ScreenPointToRay(startPoint);
                RaycastHit hitInfo;

                if (Physics.Raycast(theRay, out hitInfo, 100))
                {
                    objectHit = hitInfo.transform.gameObject;
                    //Debug.Log("The name of the object we've just hit is " + objectHit.name);
                    if (objectHit.name == painting.name)
                    {
                        
                        rb.angularDrag = 0.5f;
                        
                        float turnX = touch.deltaPosition.x;
                        //float turnY = -touch.deltaPosition.y;
                        //Only rotate around the dominant axis, and clamp the velocity below a certain value
                        if (Mathf.Abs(rb.angularVelocity.y) < maxVelocity && Mathf.Abs(rb.angularVelocity.x) < maxVelocity)
                        {
                            //if (Mathf.Abs(turnX) >= Mathf.Abs(turnY))
                            //{
                                rb.AddTorque(-Vector3.up * torque * turnX);//Vector3 for local rotation because painitng is child of prefab
                            //}
                            //else
                            //{
                            //    rb.AddTorque(-Vector3.right * torque * turnY);
                            //}
                        }
                        

                    }
                }
                break;

            case TouchPhase.Ended:
                down = false;
                rb.angularDrag = 5;
                snapToPosition();
                break;

        }
        //if rotation stops, check if painting can be snapped to angle
        if (!checkMoving()) snapToPosition();
    }

    private void snapToPosition()
    {
        //if currentX or Y angle is less than a certain thresold, then snap to upright position
        float currentY = painting.transform.rotation.eulerAngles.y;
        float currentX = Mathf.Abs(painting.transform.rotation.eulerAngles.x) % 180f;
        Debug.Log(currentY);
        if (180f - currentX < snapAngle || currentX < snapAngle) {
            //Debug.Log(currentY);
            //snap to front upright
            if (inRange(-snapAngle,snapAngle,currentY) || inRange(360-snapAngle,360,currentY))
            {
                targetAngle = Quaternion.Euler(0, 0, 0);
                rb.MoveRotation(targetAngle);
                rb.angularVelocity = Vector3.zero;
                //Debug.Log("if 2");
            }
            //snap to back upright
            else if (inRange(180-snapAngle,180+snapAngle,currentY))
            {
                targetAngle = Quaternion.Euler(0, 180, 0);
                rb.MoveRotation(targetAngle);
                rb.angularVelocity = Vector3.zero;
                //Debug.Log("else if 2");
            }
            //Debug.Log("if 1");
        }
        //Debug.Log("function called");
    }

    //check if c in range [a,b]
    private bool inRange(float a, float b, float c)
    {
        if (a <= c && c <= b)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //check if stop moving
    public bool checkMoving()
    {
        if (rb)
        {
            if (!down && Mathf.Abs(rb.angularVelocity.y) < 0.1f) return false;
            else return true;
        }
        else return false;
    }

}
