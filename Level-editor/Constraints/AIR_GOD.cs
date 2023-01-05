using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIR_GOD : Constraints
{
    public AIR_GOD(){
        type = TileType.AIR_GOD;

        // All tiles are allowed above this tile
        up.Add(TileType.WALL_BTM);
        up.Add(TileType.WALL_BTM_L_CRNR);
        up.Add(TileType.WALL_BTM_R_CRNR);
        up.Add(TileType.WALL_TOP);
        up.Add(TileType.WALL_TOP_L_CRNR);
        up.Add(TileType.WALL_TOP_R_CRNR);
        up.Add(TileType.WALL_L);
        up.Add(TileType.WALL_R);
        up.Add(TileType.WALL_C);
        up.Add(TileType.PLATFORM_L);
        up.Add(TileType.PLATFORM_C);
        up.Add(TileType.PLATFORM_R);
        up.Add(TileType.AIR_TOP);
        up.Add(TileType.AIR_TOP_L_CRNR);
        up.Add(TileType.AIR_TOP_R_CRNR);
        up.Add(TileType.AIR_BTM_L_CRNR);
        up.Add(TileType.AIR_BTM_R_CRNR);
        up.Add(TileType.AIR_C);
        up.Add(TileType.AIR_BTM);
        up.Add(TileType.AIR_L);
        up.Add(TileType.AIR_R);
        up.Add(TileType.AIR_GOD);

        // All tiles are allowed below this tile
        down.Add(TileType.WALL_BTM);
        down.Add(TileType.WALL_BTM_L_CRNR);
        down.Add(TileType.WALL_BTM_R_CRNR);
        down.Add(TileType.WALL_TOP);
        down.Add(TileType.WALL_TOP_L_CRNR);
        down.Add(TileType.WALL_TOP_R_CRNR);
        down.Add(TileType.WALL_L);
        down.Add(TileType.WALL_R);
        down.Add(TileType.WALL_C);
        down.Add(TileType.PLATFORM_L);
        down.Add(TileType.PLATFORM_C);
        down.Add(TileType.PLATFORM_R);
        down.Add(TileType.AIR_TOP);
        down.Add(TileType.AIR_TOP_L_CRNR);
        down.Add(TileType.AIR_TOP_R_CRNR);
        down.Add(TileType.AIR_BTM_L_CRNR);
        down.Add(TileType.AIR_BTM_R_CRNR);
        down.Add(TileType.AIR_C);
        down.Add(TileType.AIR_BTM);
        down.Add(TileType.AIR_L);
        down.Add(TileType.AIR_R);
        down.Add(TileType.AIR_GOD);

        // All tiles are allowed to the left of this tile
        left.Add(TileType.WALL_BTM);
        left.Add(TileType.WALL_BTM_L_CRNR);
        left.Add(TileType.WALL_BTM_R_CRNR);
        left.Add(TileType.WALL_TOP);
        left.Add(TileType.WALL_TOP_L_CRNR);
        left.Add(TileType.WALL_TOP_R_CRNR);
        left.Add(TileType.WALL_L);
        left.Add(TileType.WALL_R);
        left.Add(TileType.WALL_C);
        left.Add(TileType.PLATFORM_L);
        left.Add(TileType.PLATFORM_C);
        left.Add(TileType.PLATFORM_R);
        left.Add(TileType.AIR_TOP);
        left.Add(TileType.AIR_TOP_L_CRNR);
        left.Add(TileType.AIR_TOP_R_CRNR);
        left.Add(TileType.AIR_BTM_L_CRNR);
        left.Add(TileType.AIR_BTM_R_CRNR);
        left.Add(TileType.AIR_C);
        left.Add(TileType.AIR_BTM);
        left.Add(TileType.AIR_L);
        left.Add(TileType.AIR_R);
        left.Add(TileType.AIR_GOD);

        right.Add(TileType.WALL_BTM);
        right.Add(TileType.WALL_BTM_L_CRNR);
        right.Add(TileType.WALL_BTM_R_CRNR);
        right.Add(TileType.WALL_TOP);
        right.Add(TileType.WALL_TOP_L_CRNR);
        right.Add(TileType.WALL_TOP_R_CRNR);
        right.Add(TileType.WALL_L);
        right.Add(TileType.WALL_R);
        right.Add(TileType.WALL_C);
        right.Add(TileType.PLATFORM_L);
        right.Add(TileType.PLATFORM_C);
        right.Add(TileType.PLATFORM_R);
        right.Add(TileType.AIR_TOP);
        right.Add(TileType.AIR_TOP_L_CRNR);
        right.Add(TileType.AIR_TOP_R_CRNR);
        right.Add(TileType.AIR_BTM_L_CRNR);
        right.Add(TileType.AIR_BTM_R_CRNR);
        right.Add(TileType.AIR_C);
        right.Add(TileType.AIR_BTM);
        right.Add(TileType.AIR_L);
        right.Add(TileType.AIR_R);
        right.Add(TileType.AIR_GOD);
    }
}
