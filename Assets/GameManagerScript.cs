using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private Tile[] Tiles;
    [SerializeField] public int rows;
    [SerializeField] public int collumns;
    public Tile[,] tileGrid;

    [Serializable]
    public struct Coordinates
    {
        public int row;
        public int column;
    }

    [SerializeField] public Coordinates[] coordinates;


    void Awake()
    {
        tileGrid = new Tile[rows, collumns];
    }

    void Start()
    {
        int a = 0;
       for(int i=0; i<rows; i++)
       {
            for(int j=0; j<collumns; j++)
            {
                tileGrid[i,j] = Tiles[a];
                tileGrid[i, j].tilesRow = i;
                tileGrid[i, j].tilesColumn = j;
                a++;
            }
       }

        for(int i =0; i<coordinates.Length; i++)
        {
            tileGrid[coordinates[i].row, coordinates[i].column].colorOfTile = Tile.Colors.Box;
        }

        BundleTiles();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < collumns; j++)
            {
                tileGrid[i, j].UpdateColors();
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    public void BundleTiles()
    {
        //all tiles will only check the tile at their right and buttom, edge cases will be the last row and the last collumn. Tiles in last row will only check to their right and last column will only check to their bottom.
        //the bottom right corner will not check anywhere as the last edge case
        //if two adjacent tiles are the same color and neither are in a list a list will be crated and they will be both added to this list. Also the lists reference will also be assigned to the tiles list field.
        //if one of them is in a list the other one will be added to the list and its list reference will be assigned
        //if both of them are in seperate lists than the lists will be merged and tiles list field will be updated with the new lists reference
        for(int i=0; i<rows; i++)
        {
            for (int j = 0; j < collumns; j++)
            {
                if (i == rows - 1 && j == collumns - 1) {
                    CheckUpside(i, j);
                    break;
                }
                else if(i==0 && j == collumns -1){
                    CheckDownside(i, j);
                    continue;
                }
                else if(i == 0)
                {
                    //first row, check right and downside only
                    CheckRighthandSide(i, j);
                    CheckDownside(i, j);
                }
                else if(i == rows - 1)
                {
                    //last row, check right and upside only
                    CheckRighthandSide(i, j);
                    CheckUpside(i, j);
                }
                else if(j == collumns - 1)
                {
                    //last columm, check upside and downside only
                    CheckUpside(i, j);
                    CheckDownside(i, j);
                }
                else
                {
                    CheckUpside(i, j);
                    CheckDownside(i, j);
                    CheckRighthandSide(i, j);
                }
            }
        }

    }

    private void CheckRighthandSide(int i, int j)
    {
        if (tileGrid[i, j].colorOfTile == tileGrid[i, j + 1].colorOfTile)
            ListOperations(tileGrid[i, j], tileGrid[i, j + 1]); //edge case of first row, check right side only
    }

    private void CheckDownside(int i, int j)
    {
        if (tileGrid[i, j].colorOfTile == tileGrid[i + 1, j].colorOfTile)
            ListOperations(tileGrid[i, j], tileGrid[i + 1, j]); //edge case of last collumn, check downside only
    }

    private void CheckUpside(int i, int j)
    {
        if (tileGrid[i, j].colorOfTile == tileGrid[i - 1, j].colorOfTile)
            ListOperations(tileGrid[i, j], tileGrid[i - 1, j]); //edge case of last collumn, check downside only
    }


    private void ListOperations(Tile tile1, Tile tile2)
    {
        if(!tile1.isInAList && !tile2.isInAList)
        {
            CreateListAndAddAdjacentTiles(tile1, tile2);
            tile1.isInAList = true;
            tile2.isInAList = true;
        }
        else if(!tile1.isInAList && tile2.isInAList)
        {
            AddTileToOtherTilesList(tile1, tile2);
            tile1.isInAList = true;
            for (int i = 0; i < tile2.adjacentTiles.Count; i++)
                tile2.adjacentTiles[i].UpdateColors();
        }
        else if(tile1.isInAList && !tile2.isInAList)
        {
            AddTileToOtherTilesList(tile2, tile1);
            tile2.isInAList = true;
            for (int i = 0; i < tile1.adjacentTiles.Count; i++)
                tile1.adjacentTiles[i].UpdateColors();
        }
        else if(tile1.isInAList && tile2.isInAList)
        {
            MergeListsOfTiles(tile1, tile2);
            for (int i = 0; i < tile1.adjacentTiles.Count; i++)
                tile1.adjacentTiles[i].UpdateColors();

        }
    }

    private void CreateListAndAddAdjacentTiles(Tile tile1, Tile tile2)
    {
        List<Tile> adjList = new List<Tile>();
        adjList.Add(tile1);
        adjList.Add(tile2);
        tile1.adjacentTiles = adjList;
        tile2.adjacentTiles = adjList;       
    }

    private void AddTileToOtherTilesList(Tile tile1, Tile tile2)
    {
        tile2.adjacentTiles.Add(tile1);
        tile1.adjacentTiles = tile2.adjacentTiles;
    }

    private void MergeListsOfTiles(Tile tile1, Tile tile2)
    {
        if(tile1.adjacentTiles != tile2.adjacentTiles)
        {
            tile1.adjacentTiles.AddRange(tile2.adjacentTiles);
            tile2.adjacentTiles = tile1.adjacentTiles;
        }
    }

    //color adjusting and state resetting is done within the same loop for better performance
    public void ResetState()
    {
        for(int i=0; i<rows; i++)
        {
            for(int j =0; j<collumns; j++)
            {
                tileGrid[i, j].isInAList = false;
                tileGrid[i, j].adjacentTiles = null;                
            }
        }
    }

    public void UpdateTileColors()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < collumns; j++)
            {
                tileGrid[i, j].UpdateColors();
            }
        }
    }



    

    


}

