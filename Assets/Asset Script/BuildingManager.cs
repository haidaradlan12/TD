using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject selectedTower;

    public void SelectTowerType(GameObject prefab)
    {
        selectedTower = prefab;
    }
}
