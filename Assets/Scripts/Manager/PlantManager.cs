using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance { get; private set; }
    public Tilemap interactableMap;
    public Tilemap interactableMapPlus;
    public GameObject [] objectsToGenerate;
    public Tile tiles_623;//可以更改的瓦片
    public Tile groundHonedTile;//tiles_442，锄地之后的瓦片
    public List<Tile> carrotlist = new List<Tile>();
    public List<Tile> cabbagelist = new List<Tile>();
    public List<Tile> cornlist = new List<Tile>();
    public List<Tile> eggplantlist = new List<Tile>();
    public List<Tile> mushroomlist = new List<Tile>();
    public List<Tile> potatolist = new List<Tile>();
    public List<Tile> pumkinlist = new List<Tile>();
    public List<Tile> strawberrylist = new List<Tile>();
    public List<Tile> tomatolist = new List<Tile>();
    public List<Tile> watermelonlist = new List<Tile>();
    public List<Tile> whitecarrotlist = new List<Tile>();
    //public GenerateMineral mineral;
    public List<int> waterdayseed;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitInteractableMap();
        for (int i = 0; i < 100; i++)
        {
            waterdayseed.Add(0);
        }
    }

    public TileBase GetTile(Vector3 position)
    {
        Vector3Int tilePosition=interactableMap.WorldToCell(position);
        TileBase tile = interactableMapPlus.GetTile(tilePosition);
        return tile;
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
                    interactableMap.SetTile(position, tiles_623);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        
    }
    

    //耕种
    public void HoeGround(Vector3 position)
    {
        Vector3Int tilePosition=interactableMap.WorldToCell(position);
        TileBase tile=interactableMap.GetTile(tilePosition);
        if (tile!=null&&tile.name == tiles_623.name)
        {
            interactableMap.SetTile(tilePosition,groundHonedTile);
        }
    }
    //播种
    public void SowSeed(Vector3 position,ItemData selected)
    {
        try
        {
            Vector3Int tilePosition = interactableMap.WorldToCell(position);
            TileBase tile = interactableMap.GetTile(tilePosition);
            //如果已经锄地了，可以播种了
            if (tile != null && tile.name == groundHonedTile.name)
            {
                switch (selected.type)
                {
                    case ItemType.CarrotSeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, carrotlist[0]);
                        break;
                    case ItemType.CabbageSeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, cabbagelist[0]);
                        break;
                    case ItemType.CornSeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, cornlist[0]);
                        break;
                    case ItemType.EggplantSeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, eggplantlist[0]);
                        break;
                    case ItemType.MushroomSeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, mushroomlist[0]);
                        break;
                    case ItemType.PotatoSeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, potatolist[0]);
                        break;
                    case ItemType.PumpkinSeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, pumkinlist[0]);
                        break;
                    case ItemType.StrawberrySeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, strawberrylist[0]);
                        break;
                    case ItemType.TomatoSeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, tomatolist[0]);
                        break;
                    case ItemType.WatermelonSeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, watermelonlist[0]);
                        break;
                    case ItemType.WhiteCarrotSeed:
                        interactableMap.SetTile(tilePosition, groundHonedTile);
                        interactableMapPlus.SetTile(tilePosition, whitecarrotlist[0]);
                        break;
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        
    }
    //成长
    public void Water(Vector3 position,Tile t)
    {
        try
        {
            Vector3Int tilePosition = interactableMap.WorldToCell(position);
            TileBase tile = interactableMap.GetTile(tilePosition);
            TileBase tileplus = interactableMapPlus.GetTile(tilePosition);
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == carrotlist[0].name || tileplus.name == carrotlist[1].name || tileplus.name == carrotlist[2].name || tileplus.name == carrotlist[3].name || tileplus.name == carrotlist[4].name || tile.name == PlantManager.Instance.carrotlist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == cabbagelist[0].name || tileplus.name == cabbagelist[1].name || tileplus.name == cabbagelist[2].name || tileplus.name == cabbagelist[3].name || tileplus.name == cabbagelist[4].name || tile.name == PlantManager.Instance.cabbagelist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == cornlist[0].name || tileplus.name == cornlist[1].name || tileplus.name == cornlist[2].name || tileplus.name == cornlist[3].name || tileplus.name == cornlist[4].name || tile.name == PlantManager.Instance.cornlist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == eggplantlist[0].name || tileplus.name == eggplantlist[1].name || tileplus.name == eggplantlist[2].name || tileplus.name == eggplantlist[3].name || tileplus.name == eggplantlist[4].name || tile.name == PlantManager.Instance.eggplantlist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == mushroomlist[0].name || tileplus.name == mushroomlist[1].name || tileplus.name == mushroomlist[2].name || tileplus.name == mushroomlist[3].name || tileplus.name == mushroomlist[4].name || tile.name == PlantManager.Instance.mushroomlist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == potatolist[0].name || tileplus.name == potatolist[1].name || tileplus.name == potatolist[2].name || tileplus.name == potatolist[3].name || tileplus.name == potatolist[4].name || tile.name == PlantManager.Instance.potatolist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == pumkinlist[0].name || tileplus.name == pumkinlist[1].name || tileplus.name == pumkinlist[2].name || tileplus.name == pumkinlist[3].name || tileplus.name == pumkinlist[4].name || tile.name == PlantManager.Instance.pumkinlist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == strawberrylist[0].name || tileplus.name == strawberrylist[1].name || tileplus.name == strawberrylist[2].name || tileplus.name == strawberrylist[3].name || tileplus.name == strawberrylist[4].name || tile.name == PlantManager.Instance.strawberrylist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == tomatolist[0].name || tileplus.name == tomatolist[1].name || tileplus.name == tomatolist[2].name || tileplus.name == tomatolist[3].name || tileplus.name == tomatolist[4].name || tile.name == PlantManager.Instance.tomatolist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == watermelonlist[0].name || tileplus.name == watermelonlist[1].name || tileplus.name == watermelonlist[2].name || tileplus.name == watermelonlist[3].name || tileplus.name == watermelonlist[4].name || tile.name == PlantManager.Instance.watermelonlist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
            if (tile != null && tile.name == groundHonedTile.name && (tileplus.name == whitecarrotlist[0].name || tileplus.name == whitecarrotlist[1].name || tileplus.name == whitecarrotlist[2].name || tileplus.name == whitecarrotlist[3].name || tileplus.name == whitecarrotlist[4].name || tile.name == PlantManager.Instance.whitecarrotlist[5].name))
            {
                interactableMap.SetTile(tilePosition, groundHonedTile);
                interactableMapPlus.SetTile(tilePosition, t);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        
        
    }
    
    //土地复原
    public void recover(Vector3 position)
    {
        Vector3Int tilePosition=interactableMap.WorldToCell(position);
        TileBase tile = interactableMap.GetTile(tilePosition);
        if (tile!=null)
        {
            interactableMap.SetTile(tilePosition,tiles_623);
            interactableMapPlus.SetTile(tilePosition,tiles_623);
        }
    }
    //挖矿
    public void Mineral(Vector3 position)
    {
        Vector3Int tilePosition=interactableMap.WorldToCell(position);
        TileBase tile=interactableMap.GetTile(tilePosition);
        int randomIndex = Random. Range (0, objectsToGenerate.Length);
        GameObject objectToGenerate = objectsToGenerate [randomIndex];
        if (tile!=null&&tile.name == tiles_623.name)
        {
            interactableMap.SetTile(tilePosition,groundHonedTile);
            Instantiate (objectToGenerate,position, Quaternion.identity);
        }
        
    }

}
