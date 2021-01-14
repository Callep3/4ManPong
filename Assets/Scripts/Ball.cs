using UnityEngine;
using Mirror;
using Random = UnityEngine.Random;

public class Ball : NetworkBehaviour
{
    private Vector3 direction;
    [SerializeField] private float ballSpeed;

    private void Start()
    {
        //TODO fix so that Y is randomized when you have more than 2 players
        int x = 0;
        int y = 0;
        while (x == 0)
        {
            x = (int)Random.Range(-1f, 2f);
        }
        direction = new Vector3(x, y, 0);
        ballSpeed = 4f;
    }

    [Server]
    private void Update()
    {
        BallMovement();
    }

    [Server]
    private void BallMovement()
    {
        transform.position += direction.normalized * (ballSpeed * Time.deltaTime);
    }

    [Server]
    private void OnTriggerEnter2D(Collider2D other)
    {
        BallBounce(other);
    }

    [Server]
    private void BallBounce(Collider2D other)
    {
        Debug.Log("Bounce");
        if (other.CompareTag("Player"))
        {
            direction = (transform.position - other.transform.position).normalized;
        }
        else
        {
            if (other.CompareTag("HorizontalWall")) direction.y *= -1;
            if (other.CompareTag("VerticalWall")) direction.x *= -1;
        }
    }
}
