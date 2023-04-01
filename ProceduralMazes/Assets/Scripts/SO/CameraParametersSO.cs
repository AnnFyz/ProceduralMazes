using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "CameraPar/Simple")]
public class CameraParametersSO : ScriptableObject
{
   public Vector2 cameraPos = new Vector2(5,5);
   public float cameraSpeed = 40f;
   public bool IsReadingValuesFromSO;
   
}
