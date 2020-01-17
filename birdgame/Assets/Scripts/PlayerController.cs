using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Sprite[] birdSprites;

    Animator ani;
    Rigidbody2D rb2D;
    SpriteRenderer spr;

    public float speed = 2;

    public bool isShooting = false;

    public GameObject tonguePrefab;

    GameObject tongue = null;

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
        if (isShooting == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<SpriteRenderer>().sprite = birdSprites[2];
                isShooting = true;
                GetComponent<Animator>().enabled = false;
                Shoot();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                GetComponent<SpriteRenderer>().sprite = birdSprites[0];
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                transform.localScale = new Vector3(-1, 1, 1);
                GetComponent<Animator>().enabled = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                transform.localScale = new Vector3(1, 1, 1);
                GetComponent<Animator>().enabled = true;
            }
            else
                GetComponent<Animator>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = birdSprites[2];
        }
    }

    void Shoot()
    {
        // fire a beam/laser at a 45 degree angle from the center of the head
        // raycasting???
        // just have a skinny box that's at a 45 degree angle from the player's center that checks for collisions with beans when the player shoots???? WHAT SHOULD I DO HERE

        isShooting = true;


        if (transform.localScale.x < 0)
        {
            tongue = Instantiate(tonguePrefab, transform.position + new Vector3(.28125f, .125f), new Quaternion(0, 0, 0, 0));
            tongue.GetComponent<Tongue>().goLeft = false;
        }
        else if (transform.localScale.x > 0)
        {
            tongue = Instantiate(tonguePrefab, transform.position + new Vector3(-.28125f, .125f), new Quaternion(0, 0, 0, 0));
            tongue.GetComponent<Tongue>().goLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
            tongue.GetComponent<Tongue>().retracting = true;
        
        
    }
}
