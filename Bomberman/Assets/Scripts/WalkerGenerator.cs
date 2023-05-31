using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : MonoBehaviour
{

    public enum GRID
    {
        Indestructible,
        Destructible,
        Walkable,
        WalkableShadow,
    }
    //variable declarations
        public Grid grid;
        public Tilemap tileMapDestroyable;
        public Tilemap tileMapUndestroyable;
        public Tile Destructible;
        public Tile Indestructible;
        public Tile Walkable;
        public Tile WalkableShadow;
        public bool haveIndestructible = false; 
        //declare camera
        public Camera cam;
        //declare screen width

    // Start is called before the first frame update
    void Update()
    {       
        BoundsInt bounds = tileMapUndestroyable.cellBounds;
        Vector3Int cellPosition = bounds.position;
        cellPosition.x += 1; //to check the second column
        //check if the last column of Undestroyable tilemap is visible
        Vector3 worldPosition = tileMapUndestroyable.CellToWorld(cellPosition);
               
        Vector3 viewportPosition = cam.WorldToViewportPoint(worldPosition);
        
        if (!(viewportPosition.x > 0 && viewportPosition.x < 1))
        {
            CreateNewLine();
            DeleteLine();
        }

    }
    void CreateNewLine()
    {
        int lastColumn = tileMapDestroyable.cellBounds.xMax;
        //creating upper border
        tileMapUndestroyable.SetTile(new Vector3Int(lastColumn, -7, 0), Indestructible);
        //if haveIndestructible is true, then we create a new line with indestructible tiles and destructible tiles
            if (haveIndestructible)
            {
                //define if this tile will be indestructible
                bool have = false; 
                for (int i = -6; i < 8; i++)
                { 
                    if (have){
                    tileMapUndestroyable.SetTile(new Vector3Int(lastColumn, i, 0), Indestructible);
                    have = false;
                    }
                    else{
                        tileMapUndestroyable.SetTile(new Vector3Int(lastColumn, i, 0), WalkableShadow);
                        //create destructible tile with 80% chance
                        if (Random.Range(0, 100) < 80)
                        {
                            tileMapDestroyable.SetTile(new Vector3Int(lastColumn, i, 0), Destructible);
                        }
                        have = true;
                    }
                }
                haveIndestructible = false;
            }
        //if haveIndestructible is false, then we create a new line with destructible tiles
        else
        {
            tileMapDestroyable.SetTile(new Vector3Int(lastColumn, 6, 0), WalkableShadow);
            for (int i = -6; i < 7; i++)
            {
                //create destructible tile with 80% chance
                if (i == 6)
                {
                    tileMapUndestroyable.SetTile(new Vector3Int(lastColumn, i, 0), WalkableShadow);
                }
                else
                {
                    tileMapUndestroyable.SetTile(new Vector3Int(lastColumn, i, 0), Walkable);
                }

    
                if (Random.Range(0, 100) < 80)
                {
                    tileMapDestroyable.SetTile(new Vector3Int(lastColumn, i, 0), Destructible);
                }


            }
            haveIndestructible = true;
        }
        //creating lower border
        tileMapUndestroyable.SetTile(new Vector3Int(lastColumn, 7, 0), Indestructible);
 
    }
    // Update is called once per frame
    void DeleteLine(){
        int firstColumn = tileMapUndestroyable.cellBounds.xMin;
        for (int i = -7; i < 8; i++)
        {
            tileMapDestroyable.SetTile(new Vector3Int(firstColumn, i, 0), null);
            tileMapUndestroyable.SetTile(new Vector3Int(firstColumn, i, 0), null);
        }
        //resize the tilemap
        tileMapDestroyable.CompressBounds();
        tileMapUndestroyable.CompressBounds();
    }
}
