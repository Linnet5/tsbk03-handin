using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WALL_BTM_R_CRNR : Constraints
{
    public WALL_BTM_R_CRNR(){
        type = TileType.WALL_BTM_R_CRNR;

        // Tiles allowed above this tile
        up.Add(TileType.WALL_TOP_R_CRNR);
        up.Add(TileType.WALL_R);
        
        // Tiles allowed below this tile
        down.Add(TileType.AIR_TOP);
        down.Add(TileType.AIR_TOP_L_CRNR);
        down.Add(TileType.AIR_TOP_R_CRNR);
        
        // Tiles allowed to the left of this tile
        left.Add(TileType.WALL_BTM);
        left.Add(TileType.WALL_BTM_L_CRNR);

        // Tiles allowed to the right of this tile
        right.Add(TileType.PLATFORM_L);
        // right.Add(TileType.PLATFORM_R);
        right.Add(TileType.AIR_BTM_L_CRNR);
        right.Add(TileType.AIR_TOP_L_CRNR);
        right.Add(TileType.AIR_L);

        right.Add(TileType.AIR_GOD);
        //left.Add(TileType.AIR_GOD);
        //up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }
}
