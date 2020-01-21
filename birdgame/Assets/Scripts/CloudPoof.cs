using UnityEngine;

public class CloudPoof : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb2D;
    private SpriteRenderer spr;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        ani.StartPlayback();
        ani.enabled = true;
        ani.Play("CloudPoof");
    }

    // Start is called before the first frame update
    private void Start()
    {
        ani.StartPlayback();
        ani.enabled = true;
        ani.Play("CloudPoof");
    }

    // Update is called once per frame
    private void Update()
    {
    }
}