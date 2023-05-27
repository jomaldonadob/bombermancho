using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer start; //sprite de inicio de la explosion
    public AnimatedSpriteRenderer middle; //sprite del medio de la explosion
    public AnimatedSpriteRenderer end; //sprite del final de la explosion

    public void SetActiveRenderer(AnimatedSpriteRenderer renderer) 
    //metodo que se encarga de cambiar el sprite de la explosion
    {
        start.enabled = renderer == start; //si el sprite es el de inicio, lo activo
        middle.enabled = renderer == middle; //si el sprite es el del medio, lo activo
        end.enabled = renderer == end;      //si el sprite es el final, lo activo
    }

    public void SetDirection(Vector2 direction) //metodo que se encarga de rotar la explosion
    {
        float angle = Mathf.Atan2(direction.y, direction.x); //obtengo el angulo de la direccion
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward); //roto la explosion
    }
    
    public void DestroyAfert(float seconds)
    {
        Destroy(gameObject, seconds); //destruyo la explosion despues de -- segundos
    }
}
