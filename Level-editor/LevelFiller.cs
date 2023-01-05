using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using MoreMountains.Feedbacks;

[System.Serializable]

public struct Tile
{
    //public string name;
    public TileType type;
    public GameObject[] obj;
}
public class LevelFiller : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    public Tile[] tiles;
    public GridManager gridManager;

    public GameObject levelHolder;

    [SerializeField]
    public GameObject oneWay;

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
        TileType.AIR_C
    };

    public void Awake()
    {
        levelHolder = new GameObject();
        levelHolder.DontDestroyOnLoad();
        levelHolder.AddComponent<LevelHolder>();
    }
    public void Start()
    {

        Initialize();
        GameObject.Find("Environment").transform.SetParent(levelHolder.transform);
    }

    public void Initialize()
    {
        // Get the grid
        gridManager = GetComponent<GridManager>();
        /*
        for(int x = 0; x < gridManager.width; x++) {
            for(int y = 0; y < gridManager.height; y++) {
                if(x == 0 || x == gridManager.width - 1 || y == 0) {
                    FillTile(x,y, TileType.WALL);
                } else {
                    FillTile(x,y, TileType.AIR);
                }
            }
        } */
    }
    // Fill a given position
    public void FillTile(int x, int y, TileType type)
    {

        // if(type == TileType.AIR_BTM || type == TileType.AIR_TOP || type == TileType.AIR_L || type == TileType.AIR_R || type == TileType.AIR_TOP_L_CRNR || type == TileType.AIR_TOP_R_CRNR || type == TileType.AIR_BTM_L_CRNR || type == TileType.AIR_BTM_R_CRNR){
        //     gridManager.RemoveObject(x,y);
        //     return;
        // }

        Vector3 pos = gridManager.GetCoord(x, y);
        GameObject[] temp = Array.Find(tiles, t => t.type == type).obj;
        int index = Random.Range(0, temp.Length);

        var go = Instantiate(temp[index], pos, Quaternion.identity);
        go.transform.SetParent(levelHolder.transform);
        go.DontDestroyOnLoad();

        if (walls.Contains(type))
        {
            go.transform.position += new Vector3(0, 0, Random.Range(-0.15f, 0.15f));
        }

        // Do not rotate top tiles, as they can have decorations on top.
        if (type == TileType.WALL_C || type == TileType.WALL_L || type == TileType.WALL_R || type == TileType.WALL_BTM)
        {
            go.transform.Rotate(0, 0, 90 * Random.Range(0, 4), Space.Self);
        }

        go.name = type.ToString();

        GameObject colliderObj = GenerateCollider(x, y, type);
        if (colliderObj != null)
        {
            colliderObj.transform.parent = go.transform;
        }
        gridManager.SetObject(x, y, go);
    }

    private GameObject GenerateCollider(int x, int y, TileType type)
    {
        // Generate collider for walls
        if (walls.Contains(type))
        {
            GameObject colliderObject = new GameObject("GroundCollider");
            colliderObject.transform.position = gridManager.GetCoord(x, y);
            colliderObject.tag = "Wall";

            BoxCollider boxCollider = colliderObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
            boxCollider.size = new Vector3(1, 1, 6);

            return colliderObject;
        }

        // TODO: Generate collider for platforms

        return null;
    }

    public void GenerateOneWay(Vector2Int startPos, Vector2Int endPos)
    {
        // Convert to world coordinates
        Vector3 realStartCoord = gridManager.GetCoord(startPos.x, startPos.y);
        Vector3 realEndCoord = gridManager.GetCoord(endPos.x, endPos.y);

        // Instantiate and retrieve components
        // + 0.4f in y to get closer to the mesh. 
        Vector3 position = new Vector3(realStartCoord.x + (realEndCoord.x - realStartCoord.x) / 2, realStartCoord.y + 0.4f, realStartCoord.z);
        var go = Instantiate(oneWay, position, Quaternion.identity);
        go.name = "Generated Collider";
        go.transform.SetParent(levelHolder.transform);
        BoxCollider[] colliders = go.GetComponents<BoxCollider>();

        // Set bounding box ends.
        //col.center = realStartCoord + (realEndCoord - realStartCoord)/2;
        Vector3 colliderWidth = colliders[0].transform.InverseTransformVector(new Vector3(realEndCoord.x - realStartCoord.x, 0, 0));
        colliders[0].size = new Vector3(colliderWidth.x + 10f, colliders[0].size.y, colliders[0].size.z); // + 10.0f to reach slightly outside of platform widths
        colliders[1].size = new Vector3(colliderWidth.x + 7f, colliders[1].size.y, colliders[1].size.z);
    }

    // Replace a tile in the position (x,y), defualt sets it to air
    public void ReplaceTile(int x, int y, TileType type)
    {
        //TODO: Animate removing the tile here
        FillTile(x, y, type);
    }

    // Remove a tile in the position (x,y)
    public void RemoveTile(int x, int y)
    {
        gridManager.RemoveObject(x, y);
    }

    public void RemoveAllTiles()
    {
        for (int y = 0; y < gridManager.height; y++)
        {
            for (int x = 0; x < gridManager.width; x++)
            {
                if (x != 0 && x != gridManager.width - 1 && y != 0)
                {
                    RemoveTile(x, y);
                }
            }
        }
    }
}