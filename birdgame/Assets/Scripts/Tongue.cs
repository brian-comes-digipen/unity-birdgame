using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    Vector2 startPos;

    Rigidbody2D rb2D;

    public bool goLeft;

    public bool retracting = false;

    public float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        gameObject.layer = 11;
    }

    // Update is called once per frame
    void Update()
    {
        if (goLeft)
        {
            if (transform.position.x >= -7.3435 && retracting == false)
            {
                transform.position = transform.position + new Vector3(-speed * Time.deltaTime, speed * Time.deltaTime);
            }
            else
            {
                retracting = true;
                if (transform.position.x <= startPos.x)
                {
                    transform.position = transform.position + new Vector3(speed * Time.deltaTime, -speed * Time.deltaTime);
                }
                else if (transform.position.x >= startPos.x)
                {
                    Destroy(gameObject);
                    GameObject.Find("Player").GetComponent<PlayerController>().isShooting = false;
                    GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = GameObject.Find("Player").GetComponent<PlayerController>().birdSprites[0];
                }
            }
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            if (transform.position.x <= 7.3435 && retracting == false)
            {
                transform.position = transform.position + new Vector3(speed * Time.deltaTime, speed * Time.deltaTime);
            }
            else
            {
                retracting = true;
                if (transform.position.x >= startPos.x)
                {
                    transform.position = transform.position + new Vector3(-speed * Time.deltaTime, -speed * Time.deltaTime);
                }
                else if (transform.position.x <= startPos.x)
                {
                    Destroy(gameObject);
                    GameObject.Find("Player").GetComponent<PlayerController>().isShooting = false;
                    GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = GameObject.Find("Player").GetComponent<PlayerController>().birdSprites[0];
                }
            }
        }

        if (transform.position.y >= 4.3435)
        {
            retracting = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bean") && !retracting)
        {
            int scoreToAdd = 0;
            if (gameObject.transform.position.y <= -2.09375)
            {
                print("should be adding 10");
                scoreToAdd = 10;
            }
            else if (gameObject.transform.position.y <= -0.09375)
            {
                print("should be adding 50");
                scoreToAdd = 50;
            }
            else if (gameObject.transform.position.y <= 2 - 0.09375)
            {
                print("should be adding 100");
                scoreToAdd = 100;
            }
            else if (gameObject.transform.position.y <= 5 - 0.09375)
            {
                print("should be adding 500");
                scoreToAdd = 500;
            }

            GameObject.Find("Game Manager").GetComponent<GameManager>().AddToScore(scoreToAdd);
            Destroy(collision.gameObject);
            retracting = true;
        }
    }

    private void OnDestroy()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().isShooting = false;
    }
}
