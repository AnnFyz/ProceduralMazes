using UnityEngine;
using Unity.AI.Navigation;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    public Cell CellPrefab;
    public Vector3 CellSize = new Vector3(1,1,0);
    public HintRenderer HintRenderer;
    NavMeshSurface surface;

    public Maze maze;

    private void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        SpawnMaze();
        surface.BuildNavMesh();
    }

    void SpawnMaze()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze(width, height);

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);
                c.transform.parent = this.transform;
                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
            }
        }

        HintRenderer.DrawPath();
    }

}