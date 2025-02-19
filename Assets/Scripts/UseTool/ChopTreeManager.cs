using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ChopTreeManager : MonoBehaviour
{
    public static ChopTreeManager Instance { get; private set; }
    public Tilemap interactableMap;
    //public Tilemap interactableMapPlus;

    public Vector3 tileposition;

    public Tile tiles_1094,tiles_1095,tiles_1096;//可以更改的瓦片 1094,1095,1096
    public Tile chopedTile;//tiles_48，锄地之后的瓦片
    
    public List<Tile> Tileslist = new List<Tile>();
    private void Awake()
    {
        Instance = this;
        //Tileslist.Add(tiles_1094);
        //Tileslist.Add(tiles_1095);
        //Tileslist.Add(tiles_1096);
        
    }

    private void Start()
    {
        InitInteractableMap();
    }
    //初始化
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
                    //int i=UnityEngine.Random.Range(0, Tileslist.Count-1);
                    interactableMap.SetTile(position, tiles_1094);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        
    }
    //
    public Vector3Int getTilePos(Vector3 position)
    {
        Vector3Int tilepos=interactableMap.WorldToCell(position);
        return tilepos;
    }
    //
    public void Chop(Vector3 position)
    {
        try
        {
            Vector3Int tilePosition = interactableMap.WorldToCell(position);
            TileBase tile = interactableMap.GetTile(tilePosition);

            if (tile != null && (tile.name == tiles_1094.name || tile.name == tiles_1095.name || tile.name == tiles_1096.name))
            {
                interactableMap.SetTile(tilePosition, chopedTile);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        
    }

    //土地复原
    public void Recover(Vector3 position)
    {
        try
        {
            Vector3Int tilePosition = interactableMap.WorldToCell(position);
            TileBase tile = interactableMap.GetTile(tilePosition);
            if (tile != null)
            {
                interactableMap.SetTile(tilePosition, tiles_1094);
                //interactableMapPlus.SetTile(tilePosition, tiles_623);
            }
        }
        catch(NullReferenceException e)
        {
            Debug.LogError(e);
        }
        
    }
}
