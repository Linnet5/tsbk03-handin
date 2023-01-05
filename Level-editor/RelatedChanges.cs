using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelatedChanges : MonoBehaviour
{
    List<Vector2Int> positions;
    List<List<TileType>> deletedOptions;
    public RelatedChanges(){
        positions = new List<Vector2Int>();
        deletedOptions = new List<List<TileType>>();
    }

    public void AddChange(Vector2Int position, List<TileType> options){
        positions.Add(position);
        deletedOptions.Add(options);
    }

    // A tile has been reset, so we need to reset the related tiles with the changes that occurred from this tile
    public void UndoChanges(ref Cell[,] cells){
        for(int i = 0; i < positions.Count; i++){
            cells[positions[i].x, positions[i].y].options.AddRange(deletedOptions[i]);

            if(cells[positions[i].x, positions[i].y].options.Count > 1) cells[positions[i].x, positions[i].y].collapsed = false;
        }
        positions.Clear();
        deletedOptions.Clear();
    }
}
