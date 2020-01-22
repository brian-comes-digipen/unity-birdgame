using UnityEngine;

public class BlockObject : MonoBehaviour
{
    public bool blockPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        blockPlayer = false;
        gameObject.layer = 10;
        transform.GetChild(0).GetComponent<Transform>().gameObject.layer = 8;
    }

    // Update is called once per frame
    private void Update()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = !blockPlayer;
        gameObject.GetComponent<SpriteRenderer>().enabled = !blockPlayer;
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = blockPlayer;
        //transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = blockPlayer;
    }
}