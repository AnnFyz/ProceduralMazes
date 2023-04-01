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
    float x;
    float z;
    float panSpeed;
    public GameObject errorMessage;

    public void StoreData()
    {
        bool tryX = float.TryParse(XInputField.GetComponent<TMP_InputField>().text.ToString(), out x);
        bool tryZ = float.TryParse(ZInputField.GetComponent<TMP_InputField>().text.ToString(), out z);
        bool tryPanSpeed = float.TryParse(camSpeedInputField.GetComponent<TMP_InputField>().text.ToString(), out panSpeed);
        Debug.Log("X " + x);
        Debug.Log("Z " + z);

        if (tryX || tryZ || tryPanSpeed)
        {
            camera.SaveCameraData(x, z, panSpeed);
            errorMessage.SetActive(false);
        }
        else
        {
            errorMessage.SetActive(true);
        }

    }

  
    public void LoadData()
    {
        camera.LoadCameraData();
    }
}
