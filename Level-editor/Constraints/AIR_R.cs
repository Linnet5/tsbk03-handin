using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIR_R : Constraints
{
    public AIR_R(){
        type = TileType.AIR_R;

        // Tiles allowed above this tile
        up.Add(TileType.AIR_TOP_R_CRNR);
        up.Add(TileType.AIR_R);
        up.Add(TileType.AIR_C);

        // Tiles allowed below this tile
        down.Add(TileType.AIR_BTM_R_CRNR);
        down.Add(TileType.AIR_R);
        down.Add(TileType.AIR_C);

        // Tiles allowed to the left of this tile
        left.Add(TileType.AIR_C);
        left.Add(TileType.AIR_L);

        // Tiles allowed to the right of this tile
        right.Add(TileType.PLATFORM_L);
        right.Add(TileType.WALL_L);
        right.Add(TileType.WALL_BTM_L_CRNR);
        right.Add(TileType.WALL_TOP_L_CRNR);

        right.Add(TileType.AIR_GOD);
        left.Add(TileType.AIR_GOD);
        up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }
}