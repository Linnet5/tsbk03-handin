using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WALL_C : Constraints
{
    public WALL_C(){
        type = TileType.WALL_C;

        // Tiles allowed above this tile
        up.Add(TileType.WALL_L);
        up.Add(TileType.WALL_R);
        up.Add(TileType.WALL_TOP);
        up.Add(TileType.WALL_C);

        // Tiles allowed below this tile
        down.Add(TileType.WALL_BTM);
        down.Add(TileType.WALL_C);
        down.Add(TileType.WALL_L);
        down.Add(TileType.WALL_R);

        // Tiles allowed to the left of this tile
        left.Add(TileType.WALL_TOP);
        left.Add(TileType.WALL_BTM);
        left.Add(TileType.WALL_L);
        left.Add(TileType.WALL_C);
        
        // Tiles allowed to the right of this tile
        right.Add(TileType.WALL_R);
        right.Add(TileType.WALL_C);
        right.Add(TileType.WALL_TOP);
        right.Add(TileType.WALL_BTM);
        
        // right.Add(TileType.AIR_GOD);
        // left.Add(TileType.AIR_GOD);
        // up.Add(TileType.AIR_GOD);
        // down.Add(TileType.AIR_GOD);
    }
}

