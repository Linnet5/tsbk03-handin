using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIR_TOP : Constraints
{
    public AIR_TOP(){
        type = TileType.AIR_TOP;

        // Tiles allowed above this tile
        up.Add(TileType.WALL_BTM);
        up.Add(TileType.WALL_BTM_L_CRNR);
        up.Add(TileType.WALL_BTM_R_CRNR);
        up.Add(TileType.PLATFORM_L);
        up.Add(TileType.PLATFORM_C);
        up.Add(TileType.PLATFORM_R);

        // Tiles allowed below this tile
        down.Add(TileType.AIR_C);
        down.Add(TileType.AIR_BTM);

        // Tiles allowed to the left of this tile
        left.Add(TileType.AIR_TOP);
        left.Add(TileType.AIR_TOP_L_CRNR);
        left.Add(TileType.AIR_C);

        // Tiles allowed to the right of this tile
        right.Add(TileType.AIR_TOP);
        right.Add(TileType.AIR_TOP_R_CRNR);
        right.Add(TileType.AIR_C);

        right.Add(TileType.AIR_GOD);
        left.Add(TileType.AIR_GOD);
        up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }
}
