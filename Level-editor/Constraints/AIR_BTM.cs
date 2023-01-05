using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIR_BTM  : Constraints
{
    // Start is called before the first frame update
    public AIR_BTM(){
        type = TileType.AIR_BTM;

        // Tiles allowed above this tile
        up.Add(TileType.AIR_C);
        up.Add(TileType.AIR_TOP);

        // Tiles allowed below this tile
        down.Add(TileType.WALL_TOP);
        down.Add(TileType.WALL_TOP_L_CRNR);
        down.Add(TileType.WALL_TOP_R_CRNR);
        down.Add(TileType.PLATFORM_L);
        down.Add(TileType.PLATFORM_C);
        down.Add(TileType.PLATFORM_R);

        // Tiles allowed to the left of this tile
        left.Add(TileType.AIR_BTM);
        left.Add(TileType.AIR_BTM_L_CRNR);
        left.Add(TileType.AIR_C);

        // Tiles allowed to the right of this tile
        right.Add(TileType.AIR_BTM);
        right.Add(TileType.AIR_BTM_R_CRNR);
        right.Add(TileType.AIR_C);

        right.Add(TileType.AIR_GOD);
        left.Add(TileType.AIR_GOD);
        up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }
}
