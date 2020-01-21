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

            // do a coroutine, rapidly cycle RGB colors for magic block replenishing bean

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 8;
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = new Vector2(0, -1);
        CheckYPosition();
    }

    void CheckYPosition()
    {
        if (Mathf.Abs(transform.position.y) >= 5.5)
        {
            BeanType = 0;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject cGB = collision.gameObject;
        print("Bean: Collided with something.");

        // If we collide with the player, kill the player
        if (cGB.name == "Player")
        {
            print("Bean: Hit the player.");
            cGB.GetComponent<PlayerController>().isDead = true;
            Destroy(gameObject);
        }
        else if (cGB.name.Contains("block"))
        {
            print("Bean: Hit a block.");
            int hitBlockIndex = int.Parse(cGB.name.Replace("block", ""));
            print($"Bean: Hit block number {hitBlockIndex}");
            GameObject.Find("Game Manager").GetComponent<GameManager>().activeTiles[hitBlockIndex] = false;
            cGB.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
        }
        else if (cGB.name.Contains("Tongue") && cGB.GetComponent<Tongue>().retracting)
        {
            StartCoroutine("DisableCollisionTemp");
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

    // Only called when the bean touches the retracting tongue
    private IEnumerator DisableCollisionTemp()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(.25f);
        GetComponent<CircleCollider2D>().isTrigger = false;
        yield return null;
    }
}
