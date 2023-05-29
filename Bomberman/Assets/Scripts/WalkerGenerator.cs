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
        Empty
    }
    //variable declarations
        public Grid grid;
        public List<WalkerObject> Walkers;
        public Tilemap tileMapDestroyable;
        public Tilemap tileMapUndestroyable;
        public Tile Destructible;
        public Tile Indestructible;
        public Tile Walkable;
        public Tile WalkableShadow;
        public Tile Empty;
        public bool haveIndestructible = true; 
    // Start is called before the first frame update
    void Start()
    {
    for (int j = 0; j < 20; j++)
        {
            CreateNewLine();
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
                        tileMapDestroyable.SetTile(new Vector3Int(lastColumn, i, 0), WalkableShadow);
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
            for (int i = -5; i < 7; i++)
            {
                //create destructible tile with 80% chance

                tileMapDestroyable.SetTile(new Vector3Int(lastColumn, i, 0), Walkable);
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
    void Update()
    {
        
    }
}
