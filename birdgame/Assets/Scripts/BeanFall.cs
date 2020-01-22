using System.Collections;
using UnityEngine;

public class BeanFall : MonoBehaviour
{
    public int BeanType = 0;

    public GameObject cloudPoofPrefab;

    private Animator ani;

    private GameManager GM;

    private Rigidbody2D rb2D;

    private SpriteRenderer spr;

    private enum BeanTypes
    { Nothing = 0, RegenOne = 1, RegenTen = 2 };

    private void Awake()
    {
        ani = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        if (BeanType == 0 || BeanType == 1)
        {
            ani.SetInteger("BeanType", BeanType);
        }
        else if (BeanType == 2)
        {
            // Tell animation state machine to use white bean sprite animation
            ani.SetInteger("BeanType", 1);

            // do a coroutine, rapidly cycle RGB colors for magic block replenishing bean
            StartCoroutine(CycleColors());
        }
    }

    private void CheckYPosition()
    {
        if (Mathf.Abs(transform.position.y) >= 5.5)
        {
            BeanType = 0;
            Destroy(gameObject);
        }
    }

    private IEnumerator CycleColors()
    {
        float h;
        float s = 1;
        float v = 1;
        while (true)
        {
            Color.RGBToHSV(spr.color, out h, out s, out v);
            h += 1f / 90f;
            s = 1;
            v = 1;
            spr.color = Color.HSVToRGB(h, s, v);
            yield return new WaitForSecondsRealtime(1f / 360f);
        }
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
            BeanType = 0;
            print("Bean: Hit a block.");
            int hitBlockIndex = int.Parse(cGB.name.Replace("block", ""));
            print($"Bean: Hit block number {hitBlockIndex}");
            GM.activeTiles[hitBlockIndex] = false;
            Instantiate(cloudPoofPrefab, new Vector2(transform.position.x, -4), new Quaternion(0, 0, 0, 0));
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (BeanType == (int)BeanTypes.RegenOne)
        {
            GM.RegenerateTiles(1);
        }
        else if (BeanType == (int)BeanTypes.RegenTen)
        {
            GM.RegenerateTiles(10);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameObject.layer = 8;
    }

    // Update is called once per frame
    private void Update()
    {
        rb2D.velocity = new Vector2(0, -1 * GM.GameSpeed);
        CheckYPosition();
    }
}