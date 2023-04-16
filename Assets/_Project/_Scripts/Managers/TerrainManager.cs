using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainManager : MonoBehaviour
{   
    [SerializeField] Tilemap tilemap;
    [SerializeField] List<Tile> terrains;
    [SerializeField] List<Tile> buildings;
    [SerializeField] int cityGrowth = 10000;
    [SerializeField] int metropolisGrowth = 1000000;
    (int row, int column) boundaries;
    List<Vector3Int> terrainPositions = new List<Vector3Int>();
    List<Vector3Int> cityPositions = new List<Vector3Int>();
    int cityCount = 0;
    int metropolisCount = 0;

    public void InitializeTilemap()
    {
        boundaries = ScreenBoundaries();
        for (int row = -boundaries.row; row <= boundaries.row; row++)
        {
            for (int column = -boundaries.column; column <= boundaries.column; column++)
            {
                Vector3Int position = new Vector3Int(row, column);
                tilemap.SetTile(position, terrains[Random.Range(0, terrains.Count)]);
                terrainPositions.Add(position);
            }
        }
    }

    (int row, int column) ScreenBoundaries()
    {
        int xPos = 0;
        for (int row = 0; row < int.MaxValue; row++)
        {
            Vector3Int position = new Vector3Int(row, 0);
            Vector3 tilePos = tilemap.CellToWorld(position);
            Vector3 screenSpace = Camera.main.WorldToScreenPoint(tilePos);

            if (screenSpace.x > Screen.width) {
                xPos = row;
                break;
            }
        }
        
        int yPos = 0;
        for (int column = 0; column < int.MaxValue; column++)
        {
            Vector3Int position = new Vector3Int(0, column);
            Vector3 tilePos = tilemap.CellToWorld(position);
            Vector3 screenSpace = Camera.main.WorldToScreenPoint(tilePos);

            if (screenSpace.y > Screen.height) {
                yPos = column;
                break;
            }
        }
        return (xPos, yPos);
    }

    public void UpdateTiles(int population)
    {
        if (cityPositions.Count > 0 && (population / metropolisGrowth) > metropolisCount)
        {
            metropolisCount = (population / metropolisGrowth);
            Vector3Int position = cityPositions[Random.Range(0, cityPositions.Count)];
            TileBase cityTile = tilemap.GetTile(position);
            int indexPos = buildings.FindIndex((c) => c == cityTile) / 8;
            Tile metropolisTile = buildings[Random.Range(3 * 7, 4 * 7)];
            tilemap.SetTile(position, metropolisTile);
            cityPositions.Remove(position);
        }
        else if (terrainPositions.Count > 0 && (population / cityGrowth) + 1 > cityCount)
        {
            cityCount = (population / cityGrowth) + 1;
            Vector3Int position = terrainPositions[Random.Range(0, terrainPositions.Count)];
            TileBase terrainTile = tilemap.GetTile(position);
            int indexPos = terrains.FindIndex((t) => t == terrainTile) / 8;
            Tile cityTile = buildings[Random.Range(indexPos * 7, (indexPos + 1) * 7)];
            tilemap.SetTile(position, cityTile);
            terrainPositions.Remove(position);
            cityPositions.Add(position);
        }
    }
}
