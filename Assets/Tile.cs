using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Tile : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteOfTile;

    [SerializeField] private Sprite[] blueSprites;
    [SerializeField] private Sprite[] yellowSprites;
    [SerializeField] private Sprite[] pinkSprites;
    [SerializeField] private Sprite[] purpleSprites;
    [SerializeField] private Sprite[] greenSprites;
    [SerializeField] private Sprite[] redSprites;
    [SerializeField] public Sprite[] boxSprites;

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
    [SerializeField] private int boxHits = 0;

    public enum Colors
    {
        Blue,
        Yellow,
        Pink,
        Purple,
        Green,
        Red,
        None,
        Box
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
        if (adjacentTiles != null && colorOfTile != Colors.None)
        {
            //identify the columns that has popped blocks
            //identify the deepest hole and match it with the closest existing block
            tilesWithHoles.Clear();
            Color color = spriteOfTile.color;
            color.a = 0;

            for (int i = 0; i < adjacentTiles.Count; i++)
            {
                adjacentTiles[i].spriteOfTile.color = color;
                adjacentTiles[i].colorOfTile = Colors.None;
                gameManager.HandleHoles(adjacentTiles[i]);
            }
            

            for (int i = 0; i < adjacentTiles.Count; i++)
            {
                bool breakOuter = false;
                for (int j = 0; j < gameManager.coordinates.Length; j++)
                {
                    
                    if (gameManager.coordinates[j].row>0 && gameManager.tileGrid[gameManager.coordinates[j].row - 1, gameManager.coordinates[j].column] == adjacentTiles[i])
                    { //top of the box              
                        gameManager.tileGrid[gameManager.coordinates[j].row, gameManager.coordinates[j].column].boxHits++;
                        breakOuter = true;
                    }
                    else if (gameManager.coordinates[j].row < gameManager.rows-1 && gameManager.tileGrid[gameManager.coordinates[j].row + 1, gameManager.coordinates[j].column] == adjacentTiles[i])
                    {   
                        //bottom of the box
                        gameManager.tileGrid[gameManager.coordinates[j].row, gameManager.coordinates[j].column].boxHits++;
                        breakOuter = true;
                    }
                    else if (gameManager.coordinates[j].column < gameManager.collumns-1  && gameManager.tileGrid[gameManager.coordinates[j].row, gameManager.coordinates[j].column + 1] == adjacentTiles[i])
                    {
                        //right of the box
                        gameManager.tileGrid[gameManager.coordinates[j].row, gameManager.coordinates[j].column].boxHits++;
                        breakOuter = true;
                    }              
                    else if (gameManager.coordinates[j].column > 0  && gameManager.tileGrid[gameManager.coordinates[j].row, gameManager.coordinates[j].column - 1] == adjacentTiles[i])
                    {   
                        //left of the box
                        gameManager.tileGrid[gameManager.coordinates[j].row, gameManager.coordinates[j].column].boxHits++;
                        breakOuter = true;
                    }    
                }
                if (breakOuter)
                    break;
            }


            gameManager.UpdateColors();
            gameManager.ResetState();           
            gameManager.CreateNewTiles();
            //gameManager.BundleTiles();  BundleTiles is instead called within the CreateNewTiles function
            //gameManager.UpdateColors(); UpdateColors is instead called within the CreateNewTiles function
        }

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
            else if (adjacentTiles.Count >= 4)
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
        else if (colorOfTile == Colors.Box)
        {
            if (boxHits <= 1)
            {
                spriteOfTile.sprite = boxSprites[boxHits];
                Color color = spriteOfTile.color;
                color.a = 255;
                spriteOfTile.color = color;
            }
            else
            {
                colorOfTile = Colors.None;
                Color color = spriteOfTile.color;
                color.a = 0;
                spriteOfTile.color = color;
                gameManager.HandleHoles(this);
                for(int i=tilesRow+1; i<gameManager.rows; i++)
                {
                    //Debug.Log("kutunun altýnda belirtilen satýrda hole arama: " + i);
                    if (gameManager.tileGrid[i, tilesColumn].colorOfTile == Colors.None)
                        gameManager.HandleHoles(gameManager.tileGrid[i, tilesColumn]);
                }
            }
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

    

   
