using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;
using Cinemachine;

public class UnitManager : MonoBehaviour
{
    public List<Unit> selectedUnits = new List<Unit>();
    public LayerMask unitMask;
    public LayerMask groundMask;
    [SerializeField] MazeSpawner mazeSpawner;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] int numberOfUnits = 3;
    [SerializeField] CinemachineTargetGroup camera;
    NavMeshSurface surface;
    NavMeshTriangulation triangulation;
    List<GameObject> units = new List<GameObject>();
    void Awake()
    {
        mazeSpawner.OnMazeSpawned += SpawnUnits;
        mazeSpawner.OnMazeDestroyed += DestroyUnits;
    }

    private void Start()
    {
        surface = mazeSpawner.GetComponent<NavMeshSurface>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, unitMask))
            {
                Unit unit = hit.transform.GetComponent<Unit>();
                if (!selectedUnits.Contains(unit))
                {
                    selectedUnits.Add(unit);
                    unit.OnSelected();
                    //CameraMovement.followedTarget = unit.transform;
                }
            }

            else
            {
                foreach (var unit in selectedUnits)
                {
                    unit.OnDeselected();
                    //CameraMovement.followedTarget = null;
                }

                selectedUnits.Clear();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, groundMask))
            {
                foreach ( Unit unit in selectedUnits)
                {
                    unit.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(hit.point);
                }
            }
        }
    }
     
    void SpawnUnits()
    {
        triangulation = NavMesh.CalculateTriangulation();
        NavMeshHit hit;
        for (int i = 0; i < numberOfUnits; i++)
        {
            int vertexIndex = UnityEngine.Random.Range(0, triangulation.vertices.Length);
            if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, groundMask))
            {
                GameObject currentUnit = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                units.Add(currentUnit);
                if (currentUnit.GetComponent<Unit>())
                {
                    currentUnit.GetComponent<Unit>().agent.Warp(hit.position);
                    currentUnit.GetComponent<Unit>().agent.enabled = true;
                }
            }
            else
            {
                Debug.LogError($"Unable to place NavMeshAgent on NavMesh. Tried to use {triangulation.vertices[vertexIndex]}");
            }
        }
    }

    void DestroyUnits()
    {
        foreach (var unit in units)
        {
            if (unit != null)
                Destroy(unit);
        }
    }
}
