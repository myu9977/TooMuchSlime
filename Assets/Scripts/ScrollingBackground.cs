using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]private float scrollSpeed = 0.5f;
    private Vector2 startPosition;
    private float textureSizeY;

    [SerializeField]private GameObject background1;
    [SerializeField]private GameObject background2;

    void Start()
    {
        startPosition = transform.position;
        textureSizeY = GetComponent<SpriteRenderer>().bounds.size.y;

        background1.transform.position = startPosition + Vector2.up * textureSizeY;
        background2.transform.position = startPosition + Vector2.up * textureSizeY;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, textureSizeY);
        
        background1.transform.position = startPosition + Vector2.down * newPosition;
        background2.transform.position = startPosition + Vector2.down * (newPosition - textureSizeY);

        if (background1.transform.position.y < startPosition.y - textureSizeY)
        {
            background1.transform.position = startPosition + Vector2.up * textureSizeY;
            startPosition = background1.transform.position;
        }

        if (background2.transform.position.y < startPosition.y - textureSizeY)
        {
            background2.transform.position = startPosition + Vector2.up * textureSizeY;
            startPosition = background2.transform.position;
        }
    }
}
