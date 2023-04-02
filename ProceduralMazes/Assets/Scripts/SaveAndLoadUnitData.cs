using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveAndLoadUnitData
{
    //[Serializable]
    //public struct PosData
    //{
        public float x;
        public float y;
        public float z;
    // }

    public float rotx;
    public float roty;
    public float rotz;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveAndLoadUnitData unitData);
    void LoadFromSaveData(SaveAndLoadUnitData unitData);
}
