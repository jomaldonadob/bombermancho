using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode bombKey = KeyCode.Space; //tiempo que tarda en explotar la bomba
    public float bombFuseTime = 3f; //cantidad de bombas que puede tener el jugador
    public int bombAmount = 1; //bombs remaining es la cantidad de bombas que le quedan al jugador
    public int bombsRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefab; //prefab de la explosion
    public LayerMask explosionLayerMask; //mascara de la explosion
    public float explosionDuration = 1f; //tiempo que dura la explosion
    public int explosionRadius = 1; //radio de la explosion

    [Header("Destructible")]
    public Tilemap destructibleTiles; //tilemap de los destructibles
    public Destructible destructiblePrefab; //prefab de los destructibles

    //metodo que se ejecuta cuando se activa el objeto
    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    //metodo que se ejecuta cada frame
    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(bombKey)) //si quedan bombas y se presiona la tecla de bomba
        {
            //llamo al metodo PlaceBomb para que coloque la bomba
            StartCoroutine(PlaceBomb());
        }
    }

    //metodo que se encarga de colocar la bomba
    private IEnumerator PlaceBomb()
    {
        //instancio la bomba
        Vector2 position = transform.position;
        //get the position of the neartest cell
        Vector3Int cellPosition = destructibleTiles.WorldToCell(position);
        Vector3 cellCenter = destructibleTiles.GetCellCenterWorld(cellPosition);



        //Quaternion.identity es la rotacion de la bomba
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--; //resto una bomba
        bomb.transform.position = cellCenter;
        //move bomb to the center of the cell

        yield return new WaitForSeconds(bombFuseTime); //espero a que explote la bomba

        position = bomb.transform.position;

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity); //instancio la explosion
        explosion.SetActiveRenderer(explosion.start); //activo el sprite de inicio de la explosion
        explosion.DestroyAfert(explosionDuration); //destruyo la explosion despues de explosionDuration segundos

        Explode(position,Vector2.up, explosionRadius); //llamo al metodo Explode para que explote la bomba
        Explode(position,Vector2.down, explosionRadius); 
        Explode(position,Vector2.left, explosionRadius); 
        Explode(position,Vector2.right, explosionRadius); 

        Destroy(bomb); //destruyo la bomba
        bombsRemaining++; //sumo una bomba
    }

    private void OnTriggerExit2D(Collider2D other) //metodo que se ejecuta cuando un objeto sale del trigger
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) //si el objeto que sale del trigger es una bomba
        {
            other.isTrigger = false;
        }
    }

    private void Explode(Vector2 position, Vector2 direction, int length)//metodo que se encarga de explotar la bomba
    {
        if(length <= 0){//si el radio es menor o igual a 0, no hago nada
            return;
        }

        position += direction; //sumo la direccion a la posicion

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask)) //si hay una pared en la posicion
        {
            ClearDestructible(position); //llamo al metodo ClearDestructible para que destruya el destructible
            return; //no hago nada
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity); //instancio la explosion}

        //activo el sprite del medio o el final de la explosion
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end); 
        explosion.SetDirection(direction); //roto la explosion
        explosion.DestroyAfert(explosionDuration); //destruyo la explosion despues de explosionDuration segundos

        Explode(position,direction, length - 1); //llamo al metodo Explode para que explote la bomba (recursividad)

    } 

    private void ClearDestructible(Vector2 position)//metodo que se encarga de destruir el destructible
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position); //obtengo la posicion de la celda
        TileBase tile = destructibleTiles.GetTile(cell); //obtengo el tile de la celda

        if( tile != null) //
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity); //instancio el destructible
            destructibleTiles.SetTile(cell, null); //destruyo el tile
        }
    
    }

    public void AddBomb() //metodo que se encarga de sumar una bomba
    {
        bombAmount++;
        bombsRemaining++;
    }
}
