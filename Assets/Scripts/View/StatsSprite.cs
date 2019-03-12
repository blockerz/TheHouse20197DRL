using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSprite : MonoBehaviour
{
    public Actor actor;
    public Sprite[] numbers;
    SpriteRenderer attack10;
    SpriteRenderer attack1;
    SpriteRenderer health10;
    SpriteRenderer health1;
    SpriteRenderer move10;
    SpriteRenderer move1;

    // Start is called before the first frame update
    void Start()
    {
        attack10 = transform.Find("attack10").GetComponent<SpriteRenderer>();
        attack1 = transform.Find("attack1").GetComponent<SpriteRenderer>();
        health10 = transform.Find("health10").GetComponent<SpriteRenderer>();
        health1 = transform.Find("health1").GetComponent<SpriteRenderer>();
        move10 = transform.Find("move10").GetComponent<SpriteRenderer>();
        move1 = transform.Find("move1").GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * 0);

        int attack = actor.Attack;
        int health = actor.Health;
        int move = actor.Moves - actor.MovesCompleted;

        if (attack > 99)
            attack = 99;

        if (attack < 0)
            attack = 0;

        if (health > 99)
            health = 99;

        if (health < 0)
            health = 0;

        if (move > 99)
            move = 99;

        if (move < 0)
            move = 0;

        if (attack < 10)
        {
            attack10.sprite = null;
            //attack1.transform.position = new Vector3(-0.377f, attack1.transform.position.y, attack1.transform.position.z);
        }
        else
        {
            attack10.sprite = numbers[attack / 10];
            //attack1.transform.position = new Vector3(-0.337f, attack1.transform.position.y, attack1.transform.position.z);
        }

        attack1.sprite = numbers[attack % 10];

        if (health < 10)
        {
            health10.sprite = null;
            //health1.transform.position = new Vector3(0.388f, health1.transform.position.y, health1.transform.position.z);
        }
        else
        {
            health10.sprite = numbers[health / 10];
           // health1.transform.position = new Vector3(0.428f, health1.transform.position.y, health1.transform.position.z);
        }
        health1.sprite = numbers[health % 10];

        if (move < 10)
        {
            move10.sprite = null;
            //health1.transform.position = new Vector3(0.388f, health1.transform.position.y, health1.transform.position.z);
        }
        else
        {
            move10.sprite = numbers[move / 10];
            // health1.transform.position = new Vector3(0.428f, health1.transform.position.y, health1.transform.position.z);
        }
        move1.sprite = numbers[move % 10];

    }
}
