using UnityEngine;

public class Tongue : MonoBehaviour
{
    private Vector2 startPos;

    private Rigidbody2D rb2D;

    public bool goLeft;

    public bool retracting = false;

    public float speed = 2;

    private GameManager GM;

    // Start is called before the first frame update
    private void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rb2D = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        gameObject.layer = 11;
    }

    // Update is called once per frame
    private void Update()
    {
        if (goLeft)
        {
            if (transform.position.x >= -7.3435 && retracting == false)
            {
                transform.position = transform.position + new Vector3(GM.GameSpeed * -speed * Time.deltaTime, GM.GameSpeed * speed * Time.deltaTime);
            }
            else
            {
                retracting = true;
                if (transform.position.x <= startPos.x)
                {
                    transform.position = transform.position + new Vector3(GM.GameSpeed * speed * Time.deltaTime, GM.GameSpeed * -speed * Time.deltaTime);
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
                transform.position = transform.position + new Vector3(GM.GameSpeed * speed * Time.deltaTime, GM.GameSpeed * speed * Time.deltaTime);
            }
            else
            {
                retracting = true;
                if (transform.position.x >= startPos.x)
                {
                    transform.position = transform.position + new Vector3(GM.GameSpeed * -speed * Time.deltaTime, GM.GameSpeed * -speed * Time.deltaTime);
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
            int scoreToAdd = 1000;
            if (transform.position.y <= 3 + .15625)
            {
                scoreToAdd = 500;
                if (transform.position.y <= 1.5 + .15625)
                {
                    scoreToAdd = 250;
                    if (transform.position.y <= 0 + .15625)
                    {
                        scoreToAdd = 100;
                        if (transform.position.y <= -1.5 + .15625)
                        {
                            scoreToAdd = 50;
                            if (transform.position.y <= -3 + .15625)
                            {
                                scoreToAdd = 10;
                            }
                        }
                    }
                }
            }

            GM.AddToScore(scoreToAdd);
            Destroy(collision.gameObject);
            retracting = true;
        }
    }

    private void OnDestroy()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().isShooting = false;
    }
}