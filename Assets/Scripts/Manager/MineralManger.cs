using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineralManger : MonoBehaviour
{
    public static MineralManger Instance { get; private set; }
    public Tilemap interactableMap;
    public Tilemap interactableMapPlus;

    public Tile changable;//可以更改的瓦片
    public Tile groundHonedTile;//tiles_442，锄地之后的瓦片
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitInteractableMap();
    }
    

    void InitInteractableMap()
    {
        try
        {
            //得到所有瓦片的位置,再去修改瓦片
            foreach (Vector3Int position in interactableMap.cellBounds.allPositionsWithin)
            {
                TileBase tile = interactableMap.GetTile(position);
                if (tile != null)
                {
                    interactableMap.SetTile(position, changable);
                }
            }
        }
        catch(NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
        
    }
    //挖矿
    public void HoeGround(Vector3 position)
    {
        Vector3Int tilePosition=interactableMap.WorldToCell(position);
        TileBase tile=interactableMap.GetTile(tilePosition);

        if (tile!=null&&tile.name == changable.name)
        {
            interactableMap.SetTile(tilePosition,groundHonedTile);
        }
    }
    
    //土地复原
    public void recover(Vector3 position)
    {
        Vector3Int tilePosition=interactableMap.WorldToCell(position);
        TileBase tile = interactableMap.GetTile(tilePosition);
        if (tile!=null)
        {
            interactableMap.SetTile(tilePosition,changable);
            interactableMapPlus.SetTile(tilePosition,changable);
        }
    }
}
