using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite[] birdSprites;

    private Animator ani;
    private Rigidbody2D rb2D;
    private SpriteRenderer spr;

    public GameObject gameOverText;

    public float speed = 2;

    public bool isShooting = false;

    public bool isDead = false;

    private bool playingDyingAnimation = false;

    public GameObject tonguePrefab;

    private GameObject tongue = null;

    private GameManager GM;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameObject.layer = 9;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isDead)
        {
            CheckInput();
        }
        else
        {
            isShooting = false;
            Destroy(tongue);
            ani.enabled = false;
            if (!playingDyingAnimation)
            {
                playingDyingAnimation = true;
                StartCoroutine("PlayDeathAnimation");
            }
        }

        if (transform.position.y <= -4.75)
        {
            gameOverText.SetActive(true);
        }
    }

    private void CheckInput()
    {
        if (isShooting == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb2D.velocity = new Vector2(0, 0);
                GetComponent<Animator>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = birdSprites[2];
                isShooting = true;
                Shoot();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                GetComponent<SpriteRenderer>().sprite = birdSprites[0];
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rb2D.velocity = new Vector2(1 * speed * GM.GameSpeed, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                GetComponent<Animator>().enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rb2D.velocity = new Vector2(-1 * speed * GM.GameSpeed, 0);
                transform.localScale = new Vector3(1, 1, 1);
                GetComponent<Animator>().enabled = true;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                rb2D.velocity = new Vector2(0, 0);
                GetComponent<Animator>().enabled = false;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space))
                tongue.GetComponent<Tongue>().retracting = true;
            GetComponent<SpriteRenderer>().sprite = birdSprites[2];
        }
    }

    private void Shoot()
    {
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
        tongue.layer = 11;

        if (Input.GetKeyUp(KeyCode.Space))
            tongue.GetComponent<Tongue>().retracting = true;
    }

    private IEnumerator PlayDeathAnimation()
    {
        gameObject.layer = 12;
        spr.sprite = birdSprites[4];
        transform.position = new Vector3(transform.position.x, transform.position.y, -.5f);
        while (transform.position.y > -4.75)
        {
            transform.position -= new Vector3(0, .01f);
            yield return new WaitForSeconds(1.0f / 60.0f);
        }
        yield return null;
    }
}