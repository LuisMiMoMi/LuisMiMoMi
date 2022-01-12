using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : Character
{
    int playerLayer = 6, enemyLayer = 7, bulletLayer = 8;
    Vector3 lastDirection;
    public float maxResistance, actualResistance;
    [SerializeField] float dashCost, resistanceRegen;
    float lastSpeed, dashSpeed;
    public bool dashing;
    [SerializeField] public PlayerHealth playerHealth;
    public static GameObject mainCharacterInstance;
    public event Action OnPlayerDeath;

    //Mete el mainCharacter en el DontDestroyOnLoad, cambia el sprite en caso de ser female y setea parametros
    void Start()
    {
        if (mainCharacterInstance == null)
        {
            mainCharacterInstance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if (GameManager.isFemale)
        {
            GetComponent<Animator>().SetBool("female", GameManager.isFemale);
        }
        lastSpeed = speed;
        dashSpeed = speed + 2;
        actualResistance = maxResistance;
        playerHealth.RefreshHearts(actualHealth, health);
    }
    
    //Mueve el personaje segun las teclas pulsadas y guarda la ultima posicion para hacer el dash en esa direccion
    void FixedUpdate()
    {
        if (!dashing)
        {
            Movement(speed, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if(dashing)
        {
            Movement(speed, lastDirection.x, lastDirection.y);
        }
        if (!dashing && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            lastDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

    //Hace flip al personaje segun la posicion del raton, y al apretar el espacio, si se puede, hace el dash, ademas de regenerar la resistencia
    protected override void Update()
    {
        if (!dead || !hitted)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos) + Vector3.back * Camera.main.transform.position.z;
            if ((mousePos - transform.position).normalized.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if ((mousePos - transform.position).normalized.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if ((actualResistance - dashCost) >= 0 && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Dash());
            }
        }

        if (actualResistance < maxResistance)
        {
            if (!(actualResistance + (Time.deltaTime * resistanceRegen) > maxResistance))
            {
                actualResistance += Time.deltaTime * resistanceRegen;
            }
            else
            {
                actualResistance = 10;
            }

        }
    }

    //Hace el dash
    IEnumerator Dash()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        Physics2D.IgnoreLayerCollision(playerLayer, bulletLayer, true);
        actualResistance -= dashCost;
        speed = dashSpeed;
        dashing = true;
        yield return new WaitForSeconds(0.3f);
        speed = lastSpeed;
        dashing = false;
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        Physics2D.IgnoreLayerCollision(playerLayer, bulletLayer, false);
    }

    //Comprueba si se esta haciendo el dash para no recibir daño, y si no recibe daño y refresca los corazones en pantalla
    public override void RecibirDaño(int damage, float stun)
    {
        if (dashing)
        {
            return;
        }
        base.RecibirDaño(damage, stun);
        playerHealth.RefreshHearts(actualHealth, health);
    }

    //En caso de morir
    protected override void Muerte()
    {
        rb2d.velocity = Vector2.zero;
        if (characterAnim)
        {
            characterAnim.Play("Death");
        }
    }

    public void RestartPlayer()
    {
        OnPlayerDeath?.Invoke();
        OnPlayerDeath = null;
        actualHealth = health;
        actualResistance = maxResistance;
        transform.position = GameObject.FindWithTag("Respawn").transform.position;
        playerHealth.RefreshHearts(actualHealth, health);
        dead = false;
    }
}
