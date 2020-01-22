using System.Collections;
using UnityEngine;

public class CloudPoof : MonoBehaviour
{
    public Sprite[] cloudSprites;
    private bool animationStarted = false;
    private SpriteRenderer spr;

    private IEnumerator CloudPoofAnimation()
    {
        spr.sprite = cloudSprites[0];
        yield return new WaitForSecondsRealtime(.1f);
        spr.sprite = cloudSprites[1];
        yield return new WaitForSecondsRealtime(.1f);
        spr.sprite = cloudSprites[2];
        yield return new WaitForSecondsRealtime(.1f);
        Destroy(gameObject);
        yield return null;
    }

    // Start is called before the first frame update
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!animationStarted)
        {
            StartCoroutine(CloudPoofAnimation());
            animationStarted = true;
        }
    }
}