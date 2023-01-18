using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class test : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject paddle;
    public Boundary boundary;

    bool dragging = false;
    Vector2 mouseStartPos;
    Vector2 paddleStartPos;
    Vector2 mousePos;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x <= boundary.xMin || transform.position.x >= boundary.xMax)
        {
            float xPos = Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax);
            transform.position = new Vector2(xPos, transform.position.y);
        }

        if (transform.position.y <= boundary.yMin || transform.position.y >= boundary.yMax)
        {
            float yPos = Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax);
            transform.position = new Vector2(transform.position.x, yPos);
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x <= boundary.xMin || mousePos.x >= boundary.xMax)
        {
            float xPos = Mathf.Clamp(mousePos.x, boundary.xMin, boundary.xMax);
            mousePos = new Vector2(xPos, mousePos.y);
        }

        if (mousePos.y <= boundary.yMin || mousePos.y >= boundary.yMax)
        {
            float yPos = Mathf.Clamp(mousePos.y, boundary.yMin, boundary.yMax);
            mousePos = new Vector2(mousePos.x, yPos);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(mousePos);
    }
}