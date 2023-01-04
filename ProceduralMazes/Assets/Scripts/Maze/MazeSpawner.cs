using UnityEngine;
using Unity.AI.Navigation;
using System;
using System.Collections.Generic;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    public Cell CellPrefab;
    public Vector3 CellSize = new Vector3(1,1,0);
    public HintRenderer HintRenderer;
    NavMeshSurface surface;
    public Action OnMazeSpawned;
    public Action OnMazeDestroyed;

    public Maze maze;
    List<Cell> cells = new List<Cell>();

    private void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        SpawnMaze();
    }

    public void SpawnMaze()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze(width, height);

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);
                cells.Add(c);
                c.transform.parent = this.transform;
                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
                c.Floor.SetActive(maze.cells[x, y].Floor);
            }
        }

        HintRenderer.DrawPath();
        surface.BuildNavMesh();
        OnMazeSpawned?.Invoke();
    }

    void DestroyMaze()
    {
        foreach (var cell in cells)
        {
            if(cell != null)
            Destroy(cell.gameObject);
        }

        OnMazeDestroyed?.Invoke();
    }

    public void SpawnMazeAgain()
    {
        DestroyMaze();
        SpawnMaze();
    }

}