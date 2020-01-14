using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanFall : MonoBehaviour
{
    public readonly float[] validXpositions = { 7.75f, 7.25f, 6.75f, 6.25f, 5.75f, 5.25f, 4.75f, 4.25f, 3.75f, 3.25f, 2.75f, 2.25f, 1.75f, 1.25f, 0.75f, 0.25f };

    enum BeanTypes { Nothing = 0, RegenOne = 1, RegenTen = 2 };

    public int BeanType = 0;

    Animator ani;
    Rigidbody2D rb2D;
    SpriteRenderer spr;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        if (BeanType == 0 || BeanType == 1)
        {
            ani.SetInteger("BeanType", BeanType);
        }
        else //if (BeanType == 2)
        {
            // Tell animation state machine to use white bean sprite animation
            ani.SetInteger("BeanType", 1);

            // do a coroutine, rapidly cycle RGB colors for magic bean
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckYPosition()
    {
        if (transform.position.y <= -3.75)
        {
            for (int i = 0; i < validXpositions.Length; ++i)
            {
                if (transform.position.x == validXpositions[i])
                {
                    DestroyTile(i);
                    break;
                }
                else if (transform.position.x == validXpositions[i] * -1)
                {
                    DestroyTile(i);
                    break;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("Bean: Collided with something.");
        // If we collide with the player, kill the player
        if (collision.gameObject.name == "Player")
        {
            print("Bean: Hit the player.");
        }
        if (collision.gameObject.name.Contains("block"))
        {
            print("Bean: Hit a block.");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (BeanType == (int)BeanTypes.RegenOne)
        {
            RegenerateTile(0);
        }
        else
        {
            for (int i = 0; i < 10; ++i)
                RegenerateTile(0);
        }
    }

    void RegenerateTile(int tileIndex)
    {

    }
    void DestroyTile(int tileIndex)
    {

    }
}
