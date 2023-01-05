using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Threading.Tasks;



// Struct that describes a tile in the level
public struct Cell
{
    public RelatedChanges relatedChanges;
    public bool collapsed;
    public bool manuallyDeleted;
    public List<TileType> options;
    public Cell(bool deleted)
    {
        manuallyDeleted = deleted;
        collapsed = false;
        relatedChanges = new RelatedChanges();
        options = new List<TileType>()
        {
            TileType.AIR_TOP,
            TileType.AIR_TOP_L_CRNR,
            TileType.AIR_TOP_R_CRNR,
            TileType.AIR_BTM,
            TileType.AIR_BTM_L_CRNR,
            TileType.AIR_BTM_R_CRNR,
            TileType.AIR_L,
            TileType.AIR_R,
            TileType.AIR_C,

            TileType.WALL_TOP_L_CRNR,
            TileType.WALL_TOP_R_CRNR,
            TileType.WALL_TOP,
            TileType.WALL_BTM,
            TileType.WALL_BTM_L_CRNR,
            TileType.WALL_BTM_R_CRNR,
            TileType.WALL_L,
            TileType.WALL_R,
            TileType.WALL_C,

            TileType.PLATFORM_C,
            TileType.PLATFORM_L,
            TileType.PLATFORM_R
        };
    }
    public Cell(bool deleted, TileType type)
    {
        // Special constructor to force a specific tile type
        collapsed = false;
        manuallyDeleted = deleted;
        options = new List<TileType>() { type };
        relatedChanges = new RelatedChanges();
    }
}
// Implemenation of the Wave Function Collapse algorithm
public class WaveFunctionCollapse : MonoBehaviour
{
    List<TileType> platforms = new List<TileType>()
    {
        TileType.PLATFORM_C,
        TileType.PLATFORM_L,
        TileType.PLATFORM_R
    };
    List<TileType> walls = new List<TileType>()
    {
        TileType.WALL_TOP_L_CRNR,
        TileType.WALL_TOP_R_CRNR,
        TileType.WALL_TOP,
        TileType.WALL_BTM,
        TileType.WALL_BTM_L_CRNR,
        TileType.WALL_BTM_R_CRNR,
        TileType.WALL_L,
        TileType.WALL_R,
        TileType.WALL_C
    };
    List<TileType> air = new List<TileType>()
    {
        TileType.AIR_TOP,
        TileType.AIR_TOP_L_CRNR,
        TileType.AIR_TOP_R_CRNR,
        TileType.AIR_BTM,
        TileType.AIR_BTM_L_CRNR,
        TileType.AIR_BTM_R_CRNR,
        TileType.AIR_L,
        TileType.AIR_R,
        TileType.AIR_C,
        TileType.AIR_GOD,
    };

    // Grid size
    private int width;
    private int height;
    public bool startCollapse = false;
    public int collapsedCount = 0;

    [SerializeField]
    public Cell[,] grid; // The grid of cells
    public LevelFiller levelFiller;
    List<Constraints> constraints = new List<Constraints>();
    void Awake()
    {
        // Add all tiletype constrians to the list
        constraints.Add(new AIR_BTM());
        constraints.Add(new AIR_BTM_L_CRNR());
        constraints.Add(new AIR_BTM_R_CRNR());
        constraints.Add(new AIR_L());
        constraints.Add(new AIR_R());
        constraints.Add(new AIR_TOP());
        constraints.Add(new AIR_C());
        constraints.Add(new AIR_TOP_L_CRNR());
        constraints.Add(new AIR_TOP_R_CRNR());
        constraints.Add(new PLATFORM_C());
        constraints.Add(new PLATFORM_L());
        constraints.Add(new PLATFORM_R());
        constraints.Add(new WALL_BTM());
        constraints.Add(new WALL_BTM_L_CRNR());
        constraints.Add(new WALL_BTM_R_CRNR());
        constraints.Add(new WALL_L());
        constraints.Add(new WALL_R());
        constraints.Add(new WALL_C());
        constraints.Add(new WALL_TOP());
        constraints.Add(new WALL_TOP_L_CRNR());
        constraints.Add(new WALL_TOP_R_CRNR());
        constraints.Add(new AIR_GOD());

        width = GetComponent<GridManager>().width;
        height = GetComponent<GridManager>().height;
        // Initialize the grid
        grid = new Cell[width, height];
        Initialize();
    }
    void Initialize()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                SetCorrectOptionsAt(x, y);
            }
    }
    private void Start()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0)
                {
                    CollapseTile(x, y);
                }
                else if (x == 1 || x == width - 2 || y == 1)
                {
                    levelFiller.FillTile(x, y, TileType.WALL_C);
                }
            }
        EvaluateGrid();
    }
    void SetCorrectOptionsAt(int x, int y)
    {
        if (x == 0 || x == width - 1 || y == 0)
        {
            grid[x, y] = new Cell(false, TileType.WALL_C);
        }
        else if (y == height - 1 && x < width - 3 && x > 2)
        {
            grid[x, y] = new Cell(false, TileType.AIR_C);
        }
        else
        {
            grid[x, y] = new Cell(false);
        }
    }
    public void ReEvaluateDeletedTiles()
    {
        // One pass to reset options
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y].manuallyDeleted)
                {
                    DeleteCell(x, y);

                    if (x < (width - 1) && !grid[x + 1, y].manuallyDeleted)
                    {
                        ResetOptions(x + 1, y);
                    }
                    if (x > 0 && !grid[x - 1, y].manuallyDeleted)
                    {
                        ResetOptions(x - 1, y);
                    }
                    if (y < (height - 1) && !grid[x, y + 1].manuallyDeleted)
                    {
                        ResetOptions(x, y + 1);
                    }
                    if (y > 0 && !grid[x, y - 1].manuallyDeleted)
                    {
                        ResetOptions(x, y - 1);
                    }
                }
            }
        EvaluateGrid();

    }

    // Evaluate the entropy of every tile in the grid
    void EvaluateGrid()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                // If the cell is not collapsed
                if (!grid[x, y].collapsed)
                {
                    EvaluateOptions(x, y);

                    if (grid[x, y].options.Count == 0)
                    {
                        ResetTile(x, y);
                        EvaluateGrid();
                    }
                    else if (grid[x, y].options.Count == 1)
                    {
                        CollapseTile(x, y);
                        EvaluateGrid();
                    }
                }
            }
    }

    public void EvaluateGrid(int ox, int oy)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (!grid[x, y].collapsed)
                {
                    List<TileType> change = EvaluateOptions(x, y);
                    if (change.Count > 0) grid[ox, oy].relatedChanges.AddChange(new Vector2Int(x, y), change);

                    if (grid[x, y].options.Count == 0)
                    {
                        ResetTile(x, y);
                        EvaluateGrid();
                    }
                    else if (grid[x, y].options.Count == 1)
                    {
                        CollapseTile(x, y);
                        EvaluateGrid(x, y);
                    }
                }
            }
        return;
    }
    public void CompleteCollapseGrid()
    {
        Initialize();
        CollapseGrid(width / 2, height / 2);
        MakeEditable();
        EvaluateGrid(width / 2, height / 2);
    }
    public void CompleteCollapseAir()
    {
        EvaluateGrid(width / 2, height / 2);
        CollapseGridtoAir(width / 2, height / 2);
        //MakeEditable();
    }
    // Reset all options making
    public void MakeEditable()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y].options.Intersect(platforms).ToList().Count > 0)
                {
                    grid[x, y] = new Cell(false);
                    grid[x, y].options = platforms;
                }
                else if (grid[x, y].options.Intersect(walls).ToList().Count > 0)
                {
                    grid[x, y] = new Cell(false);
                    grid[x, y].options = walls;

                }
                else
                {
                    grid[x, y] = new Cell(false);
                }
                if (x == 0 || x == width - 1 || y == 0 || (y == height - 1 && x < width - 3 && x > 2))
                {
                    SetCorrectOptionsAt(x, y);
                }
            }
    }

    // Evaluate the options of a tile and remove the ones that are not possible
    void CollapseGrid()
    {
        int lowestEntropy = constraints.Count;
        List<Tuple<int, int>> lowestEntropyCoords = new List<Tuple<int, int>>();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                // If the tile is not collapsed
                if (!grid[x, y].collapsed)
                {
                    EvaluateOptions(x, y);
                    if (grid[x, y].options.Count == 1)
                    {
                        CollapseTile(x, y);
                        return;
                    }
                    else if (grid[x, y].options.Count <= lowestEntropy)
                    {
                        if (grid[x, y].options.Count < lowestEntropy)
                        {
                            // Refreshes lowest entropy with the current entropy.
                            // Flushes the coordinate list.
                            lowestEntropy = grid[x, y].options.Count;
                            lowestEntropyCoords = new List<Tuple<int, int>>();
                        }
                        lowestEntropyCoords.Add(new Tuple<int, int>(x, y));
                    }
                }
            }
        if (lowestEntropyCoords.Count > 0)
        {
            (int k, int l) = lowestEntropyCoords[UnityEngine.Random.Range(0, lowestEntropyCoords.Count)];
            //(int k, int l) = lowestEntropyCoords[0];
            //EvaluateOptions(k,l);
            CollapseTile(k, l);
        }
    }

    // Evaluate the options of a tile and remove the ones that are not possible and save the changes in the given coordinate
    public void CollapseGrid(int ox, int oy)
    {
        int lowestEntropy = constraints.Count;
        List<Tuple<int, int>> lowestEntropyCoords = new List<Tuple<int, int>>();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (!grid[x, y].collapsed)
                {
                    List<TileType> change = EvaluateOptions(x, y);
                    if (change.Count > 0) grid[ox, oy].relatedChanges.AddChange(new Vector2Int(x, y), change);

                    if (grid[x, y].options.Count == 0)
                    {
                        ResetTile(x, y);
                        CollapseGrid(x, y);
                    }
                    else if (grid[x, y].options.Count == 1)
                    {
                        CollapseTile(x, y);
                        CollapseGrid(x, y);
                    }
                    else if (grid[x, y].options.Count <= lowestEntropy)
                    {
                        if (grid[x, y].options.Count < lowestEntropy)
                        {
                            // Refreshes lowest entropy with the current entropy.
                            // Flushes the coordinate list.
                            lowestEntropy = grid[x, y].options.Count;
                            lowestEntropyCoords = new List<Tuple<int, int>>();
                        }
                        lowestEntropyCoords.Add(new Tuple<int, int>(x, y));
                    }

                }
            }
        if (lowestEntropyCoords.Count > 0)
        {
            (int k, int l) = lowestEntropyCoords[UnityEngine.Random.Range(0, lowestEntropyCoords.Count)];
            //(int k, int l) = lowestEntropyCoords[0];
            //EvaluateOptions(k,l);
            CollapseTile(k, l);
            CollapseGrid(k, l);
        }
    }

    // Evaluate the options of a tile and remove the ones that are not possible and save the changes in the given coordinate
    public void CollapseGridtoAir(int ox, int oy)
    {
        int lowestEntropy = constraints.Count;
        List<Tuple<int, int>> lowestEntropyCoords = new List<Tuple<int, int>>();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (!grid[x, y].collapsed)
                {
                    List<TileType> change = EvaluateOptions(x, y);
                    if (change.Count > 0) grid[ox, oy].relatedChanges.AddChange(new Vector2Int(x, y), change);

                    if (grid[x, y].options.Intersect(air).ToList().Count > 0)
                    {
                        grid[x, y].options = air;
                        continue;
                    }

                    if (grid[x, y].options.Count == 0)
                    {

                        grid[x, y] = new Cell(false, TileType.AIR_GOD);
                        CollapseTile(x, y);
                        CollapseGridtoAir(x, y);
                    }
                    else if (grid[x, y].options.Count == 1)
                    {
                        CollapseTile(x, y);
                        CollapseGridtoAir(x, y);
                    }
                    else if (grid[x, y].options.Count <= lowestEntropy)
                    {
                        if (grid[x, y].options.Count < lowestEntropy)
                        {
                            // Refreshes lowest entropy with the current entropy.
                            // Flushes the coordinate list.
                            lowestEntropy = grid[x, y].options.Count;
                            lowestEntropyCoords = new List<Tuple<int, int>>();
                        }
                        lowestEntropyCoords.Add(new Tuple<int, int>(x, y));
                    }
                }
            }
        if (lowestEntropyCoords.Count > 0)
        {
            (int k, int l) = lowestEntropyCoords[UnityEngine.Random.Range(0, lowestEntropyCoords.Count)];
            //(int k, int l) = lowestEntropyCoords[0];
            //EvaluateOptions(k,l);
            CollapseTile(k, l);
            CollapseGrid(k, l);
        }
    }

    // Find connected platforms and give them the same OneWay collider
    public void FindPlatforms()
    {

        bool foundPlatformFlag = false;
        Vector2Int startPos = Vector2Int.zero;
        Vector2Int endPos = Vector2Int.zero;

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                bool isPlatform = grid[x, y].options.Intersect(platforms).ToList().Count > 0;

                if (!foundPlatformFlag && isPlatform)
                {
                    foundPlatformFlag = true;
                    startPos = new Vector2Int(x, y);
                }

                if (foundPlatformFlag && !isPlatform) // Enters branch when we have reached the end of a platform.
                {
                    foundPlatformFlag = false;
                    endPos = new Vector2Int(x - 1, y);
                    levelFiller.GenerateOneWay(startPos, endPos);
                }
            }
    }

    public void CompleteReset()
    {
        levelFiller.RemoveAllTiles();
        Awake();
        Start();
    }

    public List<TileType> getOptions(int x, int y)
    {
        return grid[x, y].options;
    }
    public Cell getCell(int x, int y)
    {
        return grid[x, y];
    }
    public void DeleteCell(int x, int y)
    {
        grid[x, y].relatedChanges.UndoChanges(ref grid);
        grid[x, y] = new Cell(false);
        grid[x, y].manuallyDeleted = true;
    }
    public void SetOptions(int x, int y, List<TileType> options)
    {
        grid[x, y].options = options;
        EvaluateGrid(x, y);
    }
    public void ResetOptions(int x, int y)
    {
        grid[x, y].relatedChanges.UndoChanges(ref grid);
        grid[x, y] = new Cell(false);
        //EvaluateOptions(x, y);
    }
    public bool isCollapsed(int x, int y)
    {
        return grid[x, y].collapsed;
    }

    // Evaluate the options of a tile and remove the ones that are not possible
    List<TileType> EvaluateOptions(int x, int y)
    {
        // Get Cell
        Cell cell = grid[x, y];
        List<TileType> toRemove = new List<TileType>();

        // Check if tiles around us contain allowed tiles for our option, if not remove that option
        foreach (TileType option in cell.options)
        {
            // Gather constraints for current option
            foreach (Constraints c in constraints)
            {
                // Check surrounding tiles if constraints match
                if (c.GetTileType() == option)
                {
                    // Does the adjacent cells contain an allowed tile option?
                    // if not: Option is invalid, add it to the remove list, and move on to the next option
                    if (y != height - 1 && c.up.Intersect(grid[x, y + 1].options).ToList().Count() == 0) { toRemove.Add(option); break; }
                    if (y != 0 && c.down.Intersect(grid[x, y - 1].options).ToList().Count() == 0) { toRemove.Add(option); break; }
                    if (x != 0 && c.left.Intersect(grid[x - 1, y].options).ToList().Count() == 0) { toRemove.Add(option); break; }
                    if (x != width - 1 && c.right.Intersect(grid[x + 1, y].options).ToList().Count() == 0) { toRemove.Add(option); break; }
                }
                // Future TODO: Apply variant model depending on model constraints
                // For example: should this be a corner?
            }
        }
        // Set the new options and return the options that were removed
        int beforeCount = cell.options.Count;
        cell.options = cell.options.Except(toRemove).ToList();
        grid[x, y] = cell;
        return toRemove;
    }

    void ResetTile(int x, int y)
    {
        startCollapse = false;
        int resetSize = 11;

        for (int i = -(resetSize - 1) / 2; i < (resetSize + 2) / 2; i++)
            for (int j = -(resetSize - 1) / 2; j < (resetSize + 2) / 2; j++)
            {
                if (x + i >= 0 && x + i < width - 1 && y + j >= 0 && y + j < height - 1)
                {
                    grid[x + i, y + j].relatedChanges.UndoChanges(ref grid);
                    levelFiller.RemoveTile(x + i, y + j);
                    SetCorrectOptionsAt(x + i, y + j);
                }
            }
        Start();
    }

    // Collapse a tile given its coords and type
    void CollapseTile(int x, int y)
    {
        // Set the tile to collapsed
        Cell c = grid[x, y];
        c.collapsed = true;
        c.manuallyDeleted = false;
        collapsedCount++;

        //TODO: Place remaining option in the correct 3d position
        if (c.options.Count == 0)
        {
            ResetTile(x, y);
            return;
        }
        else if (c.options.Count == 1)
        {
            levelFiller.FillTile(x, y, c.options[0]);
        }
        else
        {
            int index = UnityEngine.Random.Range(0, c.options.Count);
            c.options = new List<TileType>() { c.options[index] };
            levelFiller.FillTile(x, y, c.options[0]);
        }
        grid[x, y] = c;
    }
}
