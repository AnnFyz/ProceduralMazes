using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class CameraDataManager : MonoBehaviour
{
    public CameraController camera;
    public GameObject XInputField;
    public GameObject ZInputField;
    public GameObject camSpeedInputField;
    public float x;
    public float z;
    public float panSpeed;

    public void StoreData()
    {
        x = float.Parse(XInputField.GetComponent<TMP_InputField>().text.ToString());
        z = float.Parse(ZInputField.GetComponent<TMP_InputField>().text.ToString());
        panSpeed = float.Parse(camSpeedInputField.GetComponent<TMP_InputField>().text.ToString());
        Debug.Log("X " + x);
        Debug.Log("Z " + z);

        camera.SaveCameraData(x, z, panSpeed);

    }

  
    public void LoadData()
    {
        camera.LoadCameraData();
    }
}
