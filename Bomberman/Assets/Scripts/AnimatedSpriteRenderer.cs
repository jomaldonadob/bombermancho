using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    //clase que se encarga de cambiar el sprite del objeto
    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite[] animationSprites;

    public float animationTime = 0.25f;
    private int animationFrame;

    public bool loop = true;
    public bool idle = true;

    private void Awake()
    {
        //obtengo el sprite renderer del objeto
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //metodo que se ejecuta cuando se activa el objeto
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    //metodo que se ejecuta cuando se desactiva el objeto
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    //metodo que se ejecuta cuando se inicia el objeto
    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    //metodo que se ejecuta cada frame
    private void NextFrame()
    {
        animationFrame++;
        //si el frame actual es mayor o igual a la cantidad de sprites de la animacion
        if (loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        
        if (idle){
            spriteRenderer.sprite = idleSprite;
        }
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }

    }
}
