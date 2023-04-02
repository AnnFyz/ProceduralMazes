using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
   public void SaveData()
    {
        if (UnitManager.selectedUnit != null)
        UnitManager.selectedUnit.GetComponent<UnitData>().SaveJsonData();
    }

    public void LoadData()
    {
        if (UnitManager.selectedUnit != null)
            UnitManager.selectedUnit.GetComponent<UnitData>().LoadFromJsonData();
    }
}
