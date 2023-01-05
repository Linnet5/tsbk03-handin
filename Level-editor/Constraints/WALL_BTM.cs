using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WALL_BTM : Constraints
{
    public WALL_BTM(){
        type = TileType.WALL_BTM;

        // Tiles allowed above this tile
        up.Add(TileType.WALL_C);
        up.Add(TileType.WALL_TOP);
        
        // Tiles allowed below this tile
        down.Add(TileType.AIR_TOP);
        down.Add(TileType.AIR_TOP_L_CRNR);
        down.Add(TileType.AIR_TOP_R_CRNR);
        
        // Tiles allowed to the left of this tile
        left.Add(TileType.WALL_BTM_L_CRNR);
        left.Add(TileType.WALL_BTM);
        left.Add(TileType.WALL_C);

        // Tiles allowed to the right of this tile
        right.Add(TileType.WALL_BTM_R_CRNR);
        right.Add(TileType.WALL_BTM);
        right.Add(TileType.WALL_C);

        //right.Add(TileType.AIR_GOD);
        //left.Add(TileType.AIR_GOD);
        //up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
        
    }
}
