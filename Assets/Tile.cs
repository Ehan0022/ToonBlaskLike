using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Tile : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteOfTile;

    [SerializeField] Sprite[] blueSprites;
    [SerializeField] Sprite[] yellowSprites;
    [SerializeField] Sprite[] pinkSprites;
    [SerializeField] Sprite[] purpleSprites;
    [SerializeField] Sprite[] greenSprites;
    [SerializeField] Sprite[] redSprites;

    [SerializeField] GameManagerScript gameManager;

    [SerializeField] private int A;
    [SerializeField] private int B;
    [SerializeField] private int C;

    public bool isInAList;
    public List<Tile> adjacentTiles;
    public Colors colorOfTile;
    public int tilesRow;
    public int tilesColumn;
    public bool isOccupied = true;


    public enum Colors
    {
        Blue,
        Yellow,
        Pink,
        Purple,
        Green,
        Red,
        None
    }



    void Start()
    {
        colorOfTile = (Colors)UnityEngine.Random.Range(0, 6);

        if (colorOfTile == Colors.Red)
            spriteOfTile.sprite = redSprites[0];
        else if (colorOfTile == Colors.Blue)
            spriteOfTile.sprite = blueSprites[0];
        else if (colorOfTile == Colors.Yellow)
            spriteOfTile.sprite = yellowSprites[0];
        else if (colorOfTile == Colors.Pink)
            spriteOfTile.sprite = pinkSprites[0];
        else if (colorOfTile == Colors.Purple)
            spriteOfTile.sprite = purpleSprites[0];
        else
            spriteOfTile.sprite = greenSprites[0];

    }


    public static List<int> tilesWithHoles = new List<int>();
    private void OnMouseDown()
    {
        //identify the columns that has popped blocks
        //identify the deepest hole and match it with the closest existing block
        tilesWithHoles.Clear();
        Color color = spriteOfTile.color;
        color.a = 0;
        //we will only iterate through columns with holes in them to perform block fallings
        tilesWithHoles = new List<int>();
        for (int i = 0; i<adjacentTiles.Count; i++)
        {
            adjacentTiles[i].spriteOfTile.color = color;
            adjacentTiles[i].colorOfTile = Colors.None;
            //I used hashing with the formula rows*10 + columns. This formula will support a maximum of 9x9 grid.
            tilesWithHoles.Add((int)(adjacentTiles[i].tilesRow * 10 + adjacentTiles[i].tilesColumn));
            Debug.Log("Eklenen hash: " + (int)(adjacentTiles[i].tilesRow * 10 + adjacentTiles[i].tilesColumn));
        }

        tilesWithHoles.Sort();
        for (int i=0; i<tilesWithHoles.Count; i++)
        {
            //calculate how many iterasions are needed for tiles to move to the top
            int rowOfHole = (int)tilesWithHoles[i] / 10;           
            int collumnOfHole = tilesWithHoles[i] - rowOfHole*10;
            Debug.Log("Extract edilen bilgi, row: " + rowOfHole + " column: " + collumnOfHole);
            for(int j = rowOfHole; j>0; j--)
            {
                BubbleSwitchTileColors(gameManager.tileGrid[j, collumnOfHole], gameManager.tileGrid[j - 1, collumnOfHole]);
            }
        }

        gameManager.ResetState();
        gameManager.BundleTiles();
        gameManager.UpdateTileColors();
             
    }

    private void BubbleSwitchTileColors(Tile tile1, Tile tile2)
    {
        Colors color = tile1.colorOfTile;
        tile1.colorOfTile = tile2.colorOfTile;
        tile2.colorOfTile = color;
    }



    public void UpdateColors()
    {
        int b = 0;
        if (adjacentTiles != null)
        {
            if (adjacentTiles.Count == 2)
                b = 1;
            else if (adjacentTiles.Count == 3)
                b = 2;
            else if (adjacentTiles.Count == 4)
                b = 3;
        }



        if (colorOfTile == Colors.Red)
        {
            spriteOfTile.sprite = redSprites[b];
            Color color = spriteOfTile.color;
            color.a = 255;
            spriteOfTile.color = color;
        }
        else if (colorOfTile == Colors.Blue)
        {
            spriteOfTile.sprite = blueSprites[b];
            Color color = spriteOfTile.color;
            color.a = 255;
            spriteOfTile.color = color;
        }
        else if (colorOfTile == Colors.Yellow)
        {
            spriteOfTile.sprite = yellowSprites[b];
            Color color = spriteOfTile.color;
            color.a = 255;
            spriteOfTile.color = color;
        }
        else if (colorOfTile == Colors.Pink)
        {
            spriteOfTile.sprite = pinkSprites[b];
            Color color = spriteOfTile.color;
            color.a = 255;
            spriteOfTile.color = color;
        }
        else if (colorOfTile == Colors.Purple)
        {
            spriteOfTile.sprite = purpleSprites[b];
            Color color = spriteOfTile.color;
            color.a = 255;
            spriteOfTile.color = color;
        }
        else if (colorOfTile == Colors.None)
        {
            Color color = spriteOfTile.color;
            color.a = 0;
            spriteOfTile.color = color;
        }
        else
        {
            spriteOfTile.sprite = greenSprites[b];
            Color color = spriteOfTile.color;
            color.a = 255;
            spriteOfTile.color = color;
        }
            
    }

    // Update is called once per frame
    
    void Update()
    {

    }
}

   
