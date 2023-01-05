using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIR_TOP_R_CRNR : Constraints
{
    public AIR_TOP_R_CRNR(){
        type = TileType.AIR_TOP_R_CRNR;

        // Tiles allowed above this tile
        up.Add(TileType.WALL_BTM);
        up.Add(TileType.WALL_BTM_L_CRNR);
        up.Add(TileType.WALL_BTM_R_CRNR);
        up.Add(TileType.PLATFORM_C);
        up.Add(TileType.PLATFORM_R);
        up.Add(TileType.PLATFORM_L);

        // Tiles allowed below this tile
        down.Add(TileType.AIR_BTM_R_CRNR);
        down.Add(TileType.AIR_R);
        
        // Tiles allowed to the left of this tile
        left.Add(TileType.AIR_TOP_L_CRNR);
        left.Add(TileType.AIR_TOP);

        // Tiles allowed to the right of this tile
        right.Add(TileType.WALL_TOP_L_CRNR);
        right.Add(TileType.WALL_BTM_L_CRNR);
        right.Add(TileType.WALL_L);
        right.Add(TileType.PLATFORM_L);

        right.Add(TileType.AIR_GOD);
        left.Add(TileType.AIR_GOD);
        up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }
}
