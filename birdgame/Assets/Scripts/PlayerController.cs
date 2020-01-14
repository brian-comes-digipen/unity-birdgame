using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Sprite[] birdSprites;

    Animator ani;
    Rigidbody2D rb2D;
    SpriteRenderer spr;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckInputAndMove();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<SpriteRenderer>().sprite = birdSprites[2];
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<SpriteRenderer>().sprite = birdSprites[0];
        }
    }

    void CheckInputAndMove()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<SpriteRenderer>().sprite = birdSprites[2];
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<SpriteRenderer>().sprite = birdSprites[0];
            {

            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector2(transform.position.x + 2 * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            GetComponent<Animator>().enabled = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector2(transform.position.x - 2 * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Animator>().enabled = true;
        }
        else
            GetComponent<Animator>().enabled = false;
    }

    void Shoot()
    {
        //fire a beam/laser at a 45 degree angle from the center of the head
        // raycasting???
        // just have a skinny box that's at a 45 degree angle from the player's center that checks for collisions with beans when the player shoots???? WHAT SHOULD I DO HERE
    }
}
