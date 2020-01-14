using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPoof : MonoBehaviour
{
    Animator ani;
    Rigidbody2D rb2D;
    SpriteRenderer spr;

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
    void Start()
    {
        ani.StartPlayback();
        ani.enabled = true;
        ani.Play("CloudPoof");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
