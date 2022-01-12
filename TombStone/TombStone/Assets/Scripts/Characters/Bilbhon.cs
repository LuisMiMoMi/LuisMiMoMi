using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilbhon : Enemy
{
    float timer;
    float prevSpeed;
    Transform hammer;
    Vector3 hammerPos;
    [SerializeField] float hitRecover;
    [SerializeField] float nSpikes;
    [SerializeField] GameObject spike;
    [SerializeField] BilBhonAttacks[] attacks = new BilBhonAttacks[3];

    //Ejecuto el Start del que hace override y le añado comportamiento extra
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<MainCharacter>().gameObject;
        prevSpeed = speed;
        hammer = transform.GetChild(0);
        hammerPos = hammer.localPosition;
    }

    protected void FixedUpdate()
    {
        //Deja de ejecutarse al ser golpeado o al morir
        if (!dead && !hitted)
        {
            //Si ve al personaje, se mueve
            if (CheckVision())
            {
                Movement(speed, direction.x, direction.y);
            }

            //Timer para el tiempo entre ataques
            if (timer >= hitCD)
            {
                Attack(ChooseAttack());
                timer = 0;
            }
            //Cuando empieza el timer del tiempo entre ataques
            if (canHit && CheckVision())
            {
                timer += Time.deltaTime;
            }

            //Para el ataque a melee
            if (canHit)
            {
                if (CheckSound(hitRadius))
                {
                    StartCoroutine(MeleeAttack());
                }
            }

            //Para cambiar el porcentaje de la regeneracion
            if (attacks[2].prob == 0 && health/actualHealth >= 2)
            {
                ChangeAttackPercentaje(2, attacks[2].probIncrement);
            }
            else if (attacks[2].prob != 0 && health / actualHealth < 2)
            {
                ChangeAttackPercentaje(2, -attacks[2].probIncrement);
            }

        }
    }

    //Listado de los ataques
    void Attack(int indice)
    {
        if (indice == -1)
        {
            return;
        }
        switch (indice)
        {
            case 0:
                speed *= 2;
                break;

            case 1:
                StartCoroutine(RangedAttack());
                break;

            case 2:
                StartCoroutine(Regeneration());
                break;

            default:
                break;
        }
    }
    //Para cambiar el porcentaje de cualquier ataque
    void ChangeAttackPercentaje(float attack, float percentaje)
    {
        for (int i = 0; i < attacks.Length; i++)
        {
            if (attacks[i].attack == attack)
            {
                attacks[i].prob += percentaje;
            }
        }
    }

    //Ataque a melee
    private IEnumerator MeleeAttack()
    {
        speed = 0;
        canHit = false;
        if (hammer.GetComponent<Animator>())
        {
            if (GetComponent<Animator>())
            {
                GetComponent<Animator>().SetFloat("speed", speed);
            }
            if (this.gameObject.GetComponent<SpriteRenderer>().flipX == false)
            {
                hammer.GetComponent<Animator>().Play("MeleeAttackRight");
            }
            if (this.gameObject.GetComponent<SpriteRenderer>().flipX == true)
            {
                hammer.GetComponent<Animator>().Play("MeleeAttackLeft");
            }
        }
        yield return new WaitForSeconds(hitRecover);
        speed = prevSpeed;
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetFloat("speed", speed);
        }
        canHit = true;
    }

    //Ataque a distancia
    private IEnumerator RangedAttack()
    {
        float ang = 360 / nSpikes;
        Vector3 rotation;
        Vector3 pos;
        speed = 0;
        canHit = false;
        if (hammer.GetComponent<Animator>())
        {
            if (GetComponent<Animator>())
            {
                GetComponent<Animator>().SetFloat("speed", speed);
            }
            if (this.gameObject.GetComponent<SpriteRenderer>().flipX == false)
            {
                hammer.GetComponent<Animator>().Play("RangedAttack");
            }
            if (this.gameObject.GetComponent<SpriteRenderer>().flipX == true)
            {
                hammer.GetComponent<Animator>().Play("RangedAttackLeft");
            }
            
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < nSpikes; i++)
        {
            rotation = Quaternion.AngleAxis(ang * i, Vector3.forward) * Vector3.up;
            pos = transform.position + (rotation * 0.2f);
            GameObject spke = Instantiate(spike, pos, Quaternion.identity);
            spke.GetComponent<Spike>().damage = damage;
            spke.GetComponent<Spike>().direction = rotation;
            spke.transform.up = rotation;
        }
        yield return new WaitForSeconds(hitRecover/2);
        speed = prevSpeed;
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetFloat("speed", speed);
        }
        canHit = true;
    }

    //Regeneracion del personaje
    private IEnumerator Regeneration()
    {
        speed = 0;
        canHit = false;
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetFloat("speed", speed);
            GetComponent<Animator>().Play("Regen");
        }
        for (int i = 0; i < 8; i++)
        {
            actualHealth += 2;
            yield return new WaitForSeconds(0.5f);
        }
        speed = prevSpeed;
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetFloat("speed", speed);
        }
        canHit = true;
    }

    //Para saber si hay un jugador cerca
    protected override bool CheckSound(float radius)
    {
        RaycastHit2D hitPlayer = Physics2D.CircleCast(c2d.bounds.center, radius, Vector2.zero, 0, playerLayer);
        if (hitPlayer)
        {
            return true;
        }
        return false;
    }

    protected override bool CheckVision()
    {
        RaycastHit2D hit = Physics2D.Linecast(c2d.bounds.center, player.GetComponent<Collider2D>().bounds.center, enemyLayerInv + bulletLayerInv);
        //Debug.DrawLine(c2d.bounds.center, player.GetComponent<Collider2D>().bounds.center, Color.black, .5f);
        if (hit && hit.transform.GetComponent<MainCharacter>() && !hit.transform.GetComponent<MainCharacter>().dead)
        {
            direction = (hit.transform.position - transform.position).normalized;
            if (hit.transform.position.x - transform.position.x > 0 && canHit)
            {
                if (this.gameObject.GetComponent<SpriteRenderer>().flipX == true)
                {
                    hammer.localPosition = new Vector3(hammerPos.x, hammerPos.y);
                }
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (hit.transform.position.x - transform.position.x < 0 && canHit)
            {
                if (this.gameObject.GetComponent<SpriteRenderer>().flipX == false)
                {
                    hammer.localPosition = new Vector3(-hammerPos.x, hammerPos.y);
                }
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            return true;
        }
        return false;
    }

    private int ChooseAttack()
    {
        attacks[0].probTotal = attacks[0].prob;
        float probSum = attacks[0].prob;
        for (int i = 1; i < attacks.Length; i++)
        {
            attacks[i].probTotal = attacks[i].prob + attacks[i - 1].probTotal;
            probSum += attacks[i].prob;
        }
        float random = Random.Range(1, probSum);
        for (int i = 0; i < attacks.Length; i++)
        {
            if (random <= attacks[i].probTotal)
            {
                return i;
            }
        }
        return -1;
    }
}
