using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public int health = 50;
    public float timeBetweenAttacks = 0.5f; //
    public int attackDamage = 10; // 

    public float monsterSpeed;
    public Transform target;
    public Animator anim;

    public GameObject player; //
    public PlayerController playerController; //
    public bool playerInRange; //
    public float timer;

    public float deathTime = 1f;

    public bool isDead;

    // Use this for initialization
    void Awake()
    {
        target = GameObject.Find("Target").transform.GetComponent<Transform>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>(); //
        player = GameObject.Find("Player").transform.GetComponent<PlayerController>().gameObject;
    }


    //
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.SetBool("isAttacking", true);
            playerInRange = true;
        }
    }

    //
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {

            anim.SetBool("isAttacking", false);
            playerInRange = false;
        }

    }


    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (isDead)
        {
            return;
        }

        if (health <= 0f)
        {
            StartCoroutine(Die());
            return;
        }
        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, monsterSpeed * Time.deltaTime);
        transform.LookAt(target);

    }

    void Attack()
    {
        timer = 0f;
        if (playerController.currentHealth > 0)
        {
            playerController.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
    IEnumerator Die()
    {
        anim.SetBool("isDead", true);
        isDead = true;
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject, 2f);
    }
}