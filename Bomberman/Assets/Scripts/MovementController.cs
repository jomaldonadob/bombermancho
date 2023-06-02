using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //Clase que se encarga de mover el objeto
    private new Rigidbody2D rigidbody;
    private Vector2 direction = Vector2.down;
    public float speed = 5f;
    
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    //clase que se encarga de cambiar el sprite del objeto
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    private void Awake()
    {
        //Obtengo el rigidbody del objeto
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    //metodo que se ejecuta cada frame
    private void Update()
    {
        //if player position in x less than -12.50f call death animation
        if (transform.position.x < -12.50f)
        {
            DeathSequence();

        }
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }

    //metodo que se ejecuta cada cierto tiempo
    private void FixedUpdate()
    {
        //Muevo el objeto en la direccion que se le indico
        Vector2 position = rigidbody.position;
        Vector2 traslation = direction * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(position + traslation);
        
    }

    //metodo que se encarga de cambiar la direccion del objeto
    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;
        //cambio el sprite del objeto dependiendo de la direccion
        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //si el objeto colisiona con un objeto que tenga el tag "Explosion"
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        //metodo que se encarga de la secuencia de muerte del objeto
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);

    }

    private void OnDeathSequenceEnded()
    {
        //metodo que se encarga de la secuencia de muerte del objeto
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }

}
