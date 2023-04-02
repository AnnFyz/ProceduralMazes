using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using TMPro;

public class BinarySaving : MonoBehaviour
{
    public GameObject InputField;
    public GameObject scoreT;
    public int highScore;

    public void SaveData()
    {
        bool tryGetScore = int.TryParse(InputField.GetComponent<TMP_InputField>().text.ToString(), out highScore);
        if (tryGetScore)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/score.txt";
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, highScore);
            stream.Close();
        }
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/score.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            highScore = (int)(formatter.Deserialize(stream));
            stream.Close();
            scoreT.GetComponent<TMP_Text>().text = highScore.ToString();
        }
    }
}
