using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIR_BTM_R_CRNR  : Constraints
{
    public AIR_BTM_R_CRNR(){
        type = TileType.AIR_BTM_R_CRNR;

        // Tiles allowed above this tile
        up.Add(TileType.AIR_R);
        up.Add(TileType.AIR_TOP_R_CRNR);

        // Tiles allowed below this tile
        down.Add(TileType.WALL_TOP);
        down.Add(TileType.WALL_TOP_L_CRNR);
        down.Add(TileType.WALL_TOP_R_CRNR);
        down.Add(TileType.PLATFORM_C);
        down.Add(TileType.PLATFORM_L);
        down.Add(TileType.PLATFORM_R);
        
        // Tiles allowed to the left of this tile
        left.Add(TileType.AIR_BTM);
        left.Add(TileType.AIR_BTM_L_CRNR);

        // Tiles allowed to the right of this tile
        right.Add(TileType.WALL_BTM_L_CRNR);
        right.Add(TileType.WALL_TOP_L_CRNR);
        right.Add(TileType.WALL_L);
        right.Add(TileType.PLATFORM_L);

        right.Add(TileType.AIR_GOD);
        left.Add(TileType.AIR_GOD);
        up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }
}
