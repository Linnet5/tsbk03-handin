using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType{

    // -- AIR TILES --
    AIR_TOP_L_CRNR,
    AIR_TOP,
    AIR_TOP_R_CRNR,
    AIR_L,
    AIR_C,
    AIR_R,
    AIR_BTM_L_CRNR,
    AIR_BTM,
    AIR_BTM_R_CRNR,

    // -- WALL TILES --
    WALL_TOP_L_CRNR,
    WALL_TOP_R_CRNR,
    WALL_BTM_L_CRNR,
    WALL_BTM_R_CRNR,
    WALL_C,
    WALL_L,
    WALL_TOP,
    WALL_R,
    WALL_BTM,

    // -- PLATFORM TILES --
    PLATFORM_L,
    PLATFORM_C,
    PLATFORM_R,
    AIR_GOD,
}

public abstract class Constraints : MonoBehaviour
{
    // Default type is air
    protected TileType type = TileType.AIR_C;

    // Get the type of this tile
    public virtual TileType GetTileType(){
        return type;
    }
    // List of constraints in every direction
    public List<TileType> up = new List<TileType>();
    public List<TileType> down = new List<TileType>();
    public List<TileType> left = new List<TileType>();
    public List<TileType> right = new List<TileType>();
}
