using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    Transform cameraTransform;
    GameObject Y_Yaw;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        Y_Yaw = GameObject.Find("Y_Yaw");
        player = GameObject.Find("Cube");
    }


    // Update is called once per frame
    void Update()
    {
        /*
        #if UNITY_ANDROID
        Vector2 TouchDirection = Input.GetTouch(0).deltaPosition;
        Vector3 worldVector = Camera.main.ScreenToWorldPoint(new Vector3(TouchDirection.x, TouchDirection.y, 0));

        Vector2 SpinDirection = new Vector2(TouchDirection.x - (Camera.main.pixelWidth * 0.5f), TouchDirection.y - (Camera.main.pixelHeight * 0.5f)) * 2f;

        transform.RotateAround(player.transform.position, Vector3.up, TouchDirection.x * 0.37f);
        //transform.RotateAround(player.transform.position, Vector3.right, TouchDirection.y * 0.2f);
        float maxSpin = 45f;
        
        
        Y_Yaw.transform.eulerAngles = new Vector3(Mathf.Clamp(SpinDirection.y * 0.1f, maxSpin, 0f), 10f, 0f);
        
        cameraTransform.LookAt(player.transform.position);
        #endif*/
    }
 }
