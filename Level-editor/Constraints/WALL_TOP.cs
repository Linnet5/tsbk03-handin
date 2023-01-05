using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WALL_TOP : Constraints
{
    public WALL_TOP(){
        type = TileType.WALL_TOP;

        // Tiles allowed above this tile
        up.Add(TileType.AIR_BTM);
        up.Add(TileType.AIR_BTM_L_CRNR);
        up.Add(TileType.AIR_BTM_R_CRNR);

        // Tiles allowed below this tile
        down.Add(TileType.WALL_BTM);
        down.Add(TileType.WALL_C);

        // Tiles allowed to the left of this tile
        left.Add(TileType.WALL_TOP_L_CRNR);
        left.Add(TileType.WALL_TOP);
        left.Add(TileType.WALL_C);

        // Tiles allowed to the right of this tile
        right.Add(TileType.WALL_TOP_R_CRNR);
        right.Add(TileType.WALL_TOP);
        right.Add(TileType.WALL_C);

        //right.Add(TileType.AIR_GOD);
        //left.Add(TileType.AIR_GOD);
        up.Add(TileType.AIR_GOD);
        //down.Add(TileType.AIR_GOD);
    }
}

