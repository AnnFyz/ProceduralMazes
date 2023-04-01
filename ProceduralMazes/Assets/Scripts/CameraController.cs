using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    [SerializeField] float panBorderThickness = 10f;
    [SerializeField] Vector2 panLimit = new Vector2(40f, 50f);
    [SerializeField] float scrollSpeed = 20f;
    [SerializeField] float minY = 20f;
    [SerializeField] float maxY = 120f;
    public Vector3 pos;
    [SerializeField] CameraParametersSO cameraParSO;
    [SerializeField] string XKey = "XKey";
    [SerializeField] string ZKey = "ZKey";
    [SerializeField] string PanSpeedKey = "PanSpeedKey";

    private void Awake()
    {
        pos = transform.position;
        panLimit += new Vector2(transform.position.x, transform.position.y);
    }
    private void Update()
    {
        if (!cameraParSO.IsReadingValuesFromSO)
        {
            
            if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                pos.z += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
            {
                pos.z -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                pos.x += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
            {
                pos.x -= panSpeed * Time.deltaTime;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

            pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
            pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);

            transform.position = pos;
        }

        else if (cameraParSO.IsReadingValuesFromSO)
        {
            pos.x = cameraParSO.cameraPos.x;
            pos.z = cameraParSO.cameraPos.y;
            panSpeed = cameraParSO.cameraSpeed;
            transform.position = pos;
        }
    }

    [Button]
    public void SaveCameraData()
    {
        PlayerPrefs.SetFloat(XKey, pos.x);
        PlayerPrefs.SetFloat(ZKey, pos.z);
        PlayerPrefs.SetFloat(PanSpeedKey, panSpeed);
    }


    [Button]
    public void LoadCameraData()
    {
        pos.x = PlayerPrefs.GetFloat(XKey, 0);
        pos.z = PlayerPrefs.GetFloat(ZKey, 0);
        panSpeed = PlayerPrefs.GetFloat(PanSpeedKey, 0);
    }
}
