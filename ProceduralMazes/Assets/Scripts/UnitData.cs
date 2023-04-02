using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class UnitData : MonoBehaviour, ISaveable
{
    NavMeshAgent agent;
    void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }
    public void SaveJsonData()
    {
        SaveAndLoadUnitData data = new SaveAndLoadUnitData();
        PopulateSaveData(data);
        if (FileManager.WriteToFile("SaveData.dat", data.ToJson()))
        {
            Debug.Log("Save successful");
            Debug.Log("X " + data.x);
        }
        else
        {
            Debug.Log("Fail");
        }

    }
    public void PopulateSaveData(SaveAndLoadUnitData data)
    {

        data.x = agent.transform.position.x;
        data.y = agent.transform.position.y;
        data.z = agent.transform.position.z;
    }

    public void LoadFromJsonData()
    {
        if (FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveAndLoadUnitData data = new SaveAndLoadUnitData();
            data.LoadFromJson(json);
            LoadFromSaveData(data);
            Debug.Log("Load complete");
            Debug.Log("X " + data.x);
        }
        else
        {
            Debug.Log("Fail");
        }

    }

    public void LoadFromSaveData(SaveAndLoadUnitData data)
    {
        //agent.updatePosition = false;
        //agent.Stop();
        //agent.isStopped = true;
        //agent.transform.position = new Vector3(data.x, data.y, data.z);
        //agent.ResetPath();
        //agent.updatePosition = true;


        NavMeshHit hit;
        if (NavMesh.SamplePosition(new Vector3(data.x, data.y, data.z), out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
    }


}
