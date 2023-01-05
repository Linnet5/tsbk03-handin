using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLATFORM_R : Constraints {
    public PLATFORM_R(){
        type = TileType.PLATFORM_R;

        // Tiles allowed above this tile
        up.Add(TileType.AIR_BTM);
        up.Add(TileType.AIR_BTM_L_CRNR);
        up.Add(TileType.AIR_BTM_R_CRNR);

        // Tiles allowed below this tile
        down.Add(TileType.AIR_TOP_R_CRNR);
        down.Add(TileType.AIR_TOP_L_CRNR);
        down.Add(TileType.AIR_TOP);

        // Tiles allowed to the left of this tile
        left.Add(TileType.PLATFORM_L);
        left.Add(TileType.PLATFORM_C);

        // left.Add(TileType.WALL_TOP_R_CRNR);
        // left.Add(TileType.WALL_BTM_R_CRNR);
        // left.Add(TileType.WALL_R);

        // Tiles allowed to the right of this tile
        right.Add(TileType.AIR_L);
        right.Add(TileType.AIR_BTM_L_CRNR);
        right.Add(TileType.AIR_TOP_L_CRNR);
        right.Add(TileType.WALL_TOP_L_CRNR);
        right.Add(TileType.WALL_BTM_L_CRNR);
        right.Add(TileType.WALL_L);

        right.Add(TileType.AIR_GOD);
        //left.Add(TileType.AIR_GOD);
        up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }
}