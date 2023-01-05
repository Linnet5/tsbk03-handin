using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WALL_BTM_L_CRNR : Constraints
{
    public WALL_BTM_L_CRNR(){
        type = TileType.WALL_BTM_L_CRNR;

        // Tiles allowed above this tile
        up.Add(TileType.WALL_TOP_L_CRNR);
        up.Add(TileType.WALL_L);
        
        // Tiles allowed below this tile
        down.Add(TileType.AIR_TOP);
        down.Add(TileType.AIR_TOP_L_CRNR);
        down.Add(TileType.AIR_TOP_R_CRNR);
        
        // Tiles allowed to the left of this tile
        left.Add(TileType.AIR_R);
        left.Add(TileType.AIR_TOP_R_CRNR);
        left.Add(TileType.AIR_BTM_R_CRNR);
        left.Add(TileType.PLATFORM_R);
        // left.Add(TileType.PLATFORM_L);

        // Tiles allowed to the right of this tile
        right.Add(TileType.WALL_BTM);
        right.Add(TileType.WALL_BTM_R_CRNR);

        //right.Add(TileType.AIR_GOD);
        left.Add(TileType.AIR_GOD);
        //up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }
}
