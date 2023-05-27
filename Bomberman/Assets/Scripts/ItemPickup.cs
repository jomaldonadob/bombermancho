using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType //enum que contiene los tipos de items
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,     

    }

    public ItemType type; //tipo de item
    
    private void OnItemPickup(GameObject player)
    {
        switch (type) //dependiendo del tipo de item
        {
            case ItemType.ExtraBomb: //si es un item de bomba extra
                player.GetComponent<BombController>().AddBomb(); //aumento la cantidad de bombas extra
                break;
            case ItemType.BlastRadius: //si es un item de rango de explosion
                player.GetComponent<BombController>().explosionRadius++; //aumento el rango de explosion
                break;
            case ItemType.SpeedIncrease: //si es un item de velocidad
                player.GetComponent<MovementController>().speed++; //aumento la velocidad
                break;
        }

        Destroy(gameObject); //destruyo el item
    }

    private void OnTriggerEnter2D(Collider2D other) //metodo que se ejecuta cuando el objeto colisiona con otro
    {
        if (other.CompareTag("Player")) //si el objeto con el que colisiona es el jugador
        {
            OnItemPickup(other.gameObject); //ejecuto el metodo OnItemPickup
        }
    }
}
