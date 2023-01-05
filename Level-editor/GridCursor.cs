using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class GridCursor : MonoBehaviour
{
    [SerializeField] private GameObject cursorHolder;
    private GridManager gridManager;
    private WaveFunctionCollapse wfc;
    private LevelFiller levelFiller;
    private int width;
    private int height;
    Vector2Int currentGridCoord = new Vector2Int(1, 1);
    List<TileType> currentOptions = new List<TileType>();
    int dirX, dirY;
    float heldTimer;
    float moveTileTimer;
    float holdDownThreshold = 0.35f;
    float moveTileThreshold = 0.08f;


    [Header("Feedbacks")]
    [SerializeField] private MMFeedbacks cameraShake;

    [System.Serializable]
    struct ParticleNamePair
    {
        public string name;
        public ParticleSystem particles;
    }
    [SerializeField] private ParticleNamePair[] particleNamePairs;
    private Dictionary<string, ParticleSystem> placeParticles;

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

    void Awake()
    {
        wfc = GetComponent<WaveFunctionCollapse>();
        gridManager = GetComponent<GridManager>();
        levelFiller = GetComponent<LevelFiller>();

        width = gridManager.width;
        height = gridManager.height;
        heldTimer = 0.0f;
        moveTileTimer = 0.0f;
        dirX = 0;
        dirY = 0;

    }

    void Start()
    {
        currentGridCoord = new Vector2Int(width / 2, height / 2);
        currentOptions = wfc.getOptions(currentGridCoord.x, currentGridCoord.y);
        cursorHolder.transform.SetAsLastSibling();

        placeParticles = new Dictionary<string, ParticleSystem>();
        foreach (ParticleNamePair p in particleNamePairs)
        {
            placeParticles.Add(p.name, p.particles);
        }
    }

    void OnHorizontal(InputValue value)
    {
        dirX = Mathf.RoundToInt(value.Get<float>());
        MoveSelection(dirX, 0);
    }

    void OnVertical(InputValue value)
    {
        dirY = Mathf.RoundToInt(value.Get<float>());
        MoveSelection(0, dirY);
    }

    void OnPlacePlatform()
    {
        if (wfc.isCollapsed(currentGridCoord.x, currentGridCoord.y) || gridManager.GetObject(currentGridCoord.x, currentGridCoord.y) != null) return;

        foreach (TileType tile in platforms)
        {
            if (currentOptions.Contains(tile))
            {
                wfc.SetOptions(currentGridCoord.x, currentGridCoord.y, platforms);
                levelFiller.ReplaceTile(currentGridCoord.x, currentGridCoord.y, TileType.PLATFORM_C);

                // Animation
                cameraShake.PlayFeedbacks();
                PlayPlaceParticle("platform");
                break;
            }
        }
        DisplayOptions();
    }

    void OnPlaceWall()
    {
        if (wfc.isCollapsed(currentGridCoord.x, currentGridCoord.y) || gridManager.GetObject(currentGridCoord.x, currentGridCoord.y) != null) return;

        foreach (TileType tile in walls)
        {
            if (currentOptions.Contains(tile))
            {
                wfc.SetOptions(currentGridCoord.x, currentGridCoord.y, walls);
                levelFiller.ReplaceTile(currentGridCoord.x, currentGridCoord.y, TileType.WALL_C);

                // ANIMATE THE TILE PLAYING HERE
                cameraShake.PlayFeedbacks();
                PlayPlaceParticle("wall");
                break;
            }
        }
        DisplayOptions();
    }

    void OnResetCell()
    {
        // Remove 3D graphic    
        levelFiller.RemoveTile(currentGridCoord.x, currentGridCoord.y);

        PlayPlaceParticle("remove");

        // Reset options of current tile
        wfc.DeleteCell(currentGridCoord.x, currentGridCoord.y);

        // Reevaluate all the currently deleted cells
        wfc.ReEvaluateDeletedTiles();
    }

    void OnCollapse()
    {
        wfc.CompleteCollapseGrid();
    }

    void OnEvaluate()
    {
        wfc.CompleteCollapseAir();

        // Generate Oneways for platforms
        wfc.FindPlatforms();

        // TEMP SOLUTION
        levelFiller.levelHolder.transform.position = new Vector3(-12.5f, 1.5f, -20.0f);
        SceneManager.LoadScene("LevelTest");
    }

    void OnResetStage()
    {
        wfc.CompleteReset();
    }
    void MoveSelection(int moveX, int moveY)
    {
        if ((moveX > 0.0f && currentGridCoord.x + 1 != (width - 1)) || (moveX < 0.0f && currentGridCoord.x - 1 != (0)))
        {
            gridManager.GetCellOverlay(currentGridCoord.x, currentGridCoord.y).GetComponent<CellOverlayBehavior>().ToggleSelect(false);
            currentGridCoord.x += moveX;
            gridManager.GetCellOverlay(currentGridCoord.x, currentGridCoord.y).GetComponent<CellOverlayBehavior>().ToggleSelect(true);
            currentOptions = wfc.getOptions(currentGridCoord.x, currentGridCoord.y);

        }

        if ((moveY > 0.0f && currentGridCoord.y + 1 != (height - 1)) || (moveY < 0.0f && currentGridCoord.y - 1 != (0)))
        {
            gridManager.GetCellOverlay(currentGridCoord.x, currentGridCoord.y).GetComponent<CellOverlayBehavior>().ToggleSelect(false);
            currentGridCoord.y += moveY;
            gridManager.GetCellOverlay(currentGridCoord.x, currentGridCoord.y).GetComponent<CellOverlayBehavior>().ToggleSelect(true);
            currentOptions = wfc.getOptions(currentGridCoord.x, currentGridCoord.y);

        }

        // Display correct options on cursor
        DisplayOptions();
        cursorHolder.transform.position = Camera.main.WorldToScreenPoint(gridManager.GetCoord(currentGridCoord.x, currentGridCoord.y));

        /*
        #if (UNITY_EDITOR)
                string infoString = "";
                Cell currCell = wfc.getCell(currentGridCoord.x, currentGridCoord.y);
                infoString += "Collapsed: " + currCell.collapsed + " Marked as deleted: " + currCell.manuallyDeleted + " ";
                foreach (TileType tile in currentOptions)
                {
                    infoString += tile.ToString() + ", ";
                }
                Debug.Log(infoString);
        #endif*/

    }

    void DisplayOptions()
    {
        if (currentOptions.Intersect(walls).ToList().Count > 0 && gridManager.GetObject(currentGridCoord.x, currentGridCoord.y) == null)
        {
            cursorHolder.transform.GetChild(0).GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            cursorHolder.transform.GetChild(0).GetComponent<RawImage>().color = new Color(255, 255, 255, 0.1f);
        }

        if (currentOptions.Intersect(platforms).ToList().Count > 0 && gridManager.GetObject(currentGridCoord.x, currentGridCoord.y) == null)
        {
            cursorHolder.transform.GetChild(1).GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            cursorHolder.transform.GetChild(1).GetComponent<RawImage>().color = new Color(255, 255, 255, 0.1f);
        }
    }

    void PlayPlaceParticle(string type)
    {
        placeParticles[type].Stop();
        placeParticles[type].transform.position = gridManager.GetCoord(currentGridCoord.x, currentGridCoord.y);
        placeParticles[type].Play();
    }

    void Update()
    {
        HoldToNavigate();
    }
    void HoldToNavigate()
    {
        if ((dirX != 0 || dirY != 0))
        {
            // We are in a holding down button state
            if (heldTimer > holdDownThreshold)
            {
                moveTileTimer += Time.deltaTime;
                //We move a tile every moveTileThreshold seconds
                if (moveTileTimer > moveTileThreshold)
                {
                    moveTileTimer = 0.0f;
                    MoveSelection(dirX, dirY);
                }
            }
            else
            {
                heldTimer += Time.deltaTime;
            }
        }
        else
        {
            // No input is being held, reset timers
            heldTimer = 0.0f;
            moveTileTimer = 0.0f;
        }
    }
}

