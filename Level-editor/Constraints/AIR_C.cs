using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIR_C : Constraints
{
    public AIR_C(){
        type = TileType.AIR_C;

        // Tiles allowed above this tile
        up.Add(TileType.AIR_TOP);
        up.Add(TileType.AIR_C);
        up.Add(TileType.AIR_L);
        up.Add(TileType.AIR_R);

        // Tiles allowed below this tile
        down.Add(TileType.AIR_BTM);
        down.Add(TileType.AIR_C);
        down.Add(TileType.AIR_L);
        down.Add(TileType.AIR_R);

        // Tiles allowed to the left of this tile
        left.Add(TileType.AIR_L);
        left.Add(TileType.AIR_C);
        left.Add(TileType.AIR_TOP);
        left.Add(TileType.AIR_BTM);

        // Tiles allowed to the right of this tile
        right.Add(TileType.AIR_R);
        right.Add(TileType.AIR_C);
        right.Add(TileType.AIR_TOP);
        right.Add(TileType.AIR_BTM);

        right.Add(TileType.AIR_GOD);
        left.Add(TileType.AIR_GOD);
        up.Add(TileType.AIR_GOD);
        down.Add(TileType.AIR_GOD);
    }
}
