using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Sprite[] birdSprites;

    Animator ani;
    Rigidbody2D rb2D;
    SpriteRenderer spr;

    public GameObject gameOverText;

    public float speed = 2;

    public bool isShooting = false;

    public bool isDead = false;

    bool playingDyingAnimation = false;

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
        gameObject.layer = 9;
    }

    // Update is called once per frame
    void Update()
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

    void CheckInput()
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

            if (Input.GetKeyUp(KeyCode.Space))
                tongue.GetComponent<Tongue>().retracting = true;
            GetComponent<SpriteRenderer>().sprite = birdSprites[2];
        }
    }

    void Shoot()
    {
        isShooting = true;

        if (Input.GetKeyUp(KeyCode.Space))
            tongue.GetComponent<Tongue>().retracting = true;

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
