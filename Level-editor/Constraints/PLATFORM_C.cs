using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLATFORM_C : Constraints {
    public PLATFORM_C(){
        type = TileType.PLATFORM_C;

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

        // Tiles allowed to the right of this tile
        right.Add(TileType.PLATFORM_R);
        right.Add(TileType.PLATFORM_C);
        
        //right.Add(TileType.AIR_GOD);
        //left.Add(TileType.AIR_GOD);
        up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }

}
