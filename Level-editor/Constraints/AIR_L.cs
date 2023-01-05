using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIR_L : Constraints
{
    public AIR_L(){
        type = TileType.AIR_L;

        // Tiles allowed above this tile
        up.Add(TileType.AIR_TOP_L_CRNR);
        up.Add(TileType.AIR_L);
        up.Add(TileType.AIR_C);

        // Tiles allowed below this tile
        down.Add(TileType.AIR_BTM_L_CRNR);
        down.Add(TileType.AIR_L);
        down.Add(TileType.AIR_C);

        // Tiles allowed to the left of this tile
        left.Add(TileType.PLATFORM_R);
        left.Add(TileType.WALL_R);
        left.Add(TileType.WALL_BTM_R_CRNR);
        left.Add(TileType.WALL_TOP_R_CRNR);

        // Tiles allowed to the right of this tile
        right.Add(TileType.AIR_C);
        right.Add(TileType.AIR_R);

        right.Add(TileType.AIR_GOD);
        left.Add(TileType.AIR_GOD);
        up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }
}
