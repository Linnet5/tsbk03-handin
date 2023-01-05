using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    public int spacing;

    [SerializeField] private GameObject cellOverlayObj;
    [SerializeField] private GameObject canvas;
    private RawImage[,] cellOverlays;
    Vector3[,] grid;
    GameObject[,] objects;

    void Awake()
    {
        objects = new GameObject[width, height];
        Generate();
    }

    // Generate the grid
    public void Generate()
    {
        cellOverlays = new RawImage[width, height];
        grid = new Vector3[width + 1, height + 1];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Vector3(x, y) * spacing;
                if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
                {
                    RawImage cellOverlay = Instantiate(cellOverlayObj, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<RawImage>();
                    cellOverlay.transform.SetParent(canvas.transform);
                    cellOverlay.rectTransform.position = Camera.main.WorldToScreenPoint(GetCoord(x, y));
                    cellOverlays[x, y] = cellOverlay;
                }
            }
        }
    }

    // Get the position of grid point
    public Vector3 GetCoord(int x, int y)
    {
        if (x > width || x < 0 || y > height || y < 0)
        {
            return new Vector3(-1, -1, -1);
        }
        return grid[x, y];
    }

    // Set object in position (x,y)
    public void SetObject(int x, int y, GameObject obj)
    {
        RemoveObject(x, y);
        objects[x, y] = obj;
    }

    public GameObject GetObject(int x, int y)
    {
        return objects[x, y];
    }

    public void RemoveObject(int x, int y)
    {
        if (objects[x, y] == null) return;

        var go = objects[x, y];
        objects[x, y] = null;
        Destroy(go);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        if (grid == null) return;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Gizmos.DrawSphere(grid[x, y], 0.1f);
            }
        }
    }

    public ref RawImage GetCellOverlay(int x, int y)
    {
        return ref cellOverlays[x, y];
    }
}
