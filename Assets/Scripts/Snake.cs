using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float moveRate = 0.2f; // Intervalle entre les mouvements
    private float timer;
    private Vector2 direction = Vector2.right;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public FoodSpawner foodSpawner;

    // Position de grille actuelle du serpent
    private int currentX;
    private int currentY;

    private void Start()
    {
        segments.Add(this.transform);

        // Initialiser la position de grille
        Vector2 startPos = transform.position;
        currentX = Mathf.RoundToInt(startPos.x + GridManager.Instance.gridWidth / 2 - 0.5f);
        currentY = Mathf.RoundToInt(startPos.y + GridManager.Instance.gridHeight / 2 - 0.5f);

        foodSpawner.SpawnFood();
    }

    private void Update()
    {
        HandleInput();
        timer += Time.deltaTime;
        if (timer >= moveRate)
        {
            Move();
            timer = 0;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
            direction = Vector2.right;
    }

    private void Move()
    {
        // Calculer la nouvelle position de grille
        currentX += Mathf.RoundToInt(direction.x);
        currentY += Mathf.RoundToInt(direction.y);

        // Vérifier les limites de la grille
        if (!GridManager.Instance.IsWithinGrid(currentX, currentY))
        {
            GameOver();
            return;
        }

        // Convertir la position de grille en position du monde
        Vector2 newPosition = GridManager.Instance.GridToWorld(currentX, currentY);

        // Déplacer le serpent
        Vector2 previousPosition = transform.position;
        transform.position = newPosition;

        for (int i = 1; i < segments.Count; i++)
        {
            Vector2 temp = segments[i].position;
            segments[i].position = previousPosition;
            previousPosition = temp;
        }
    }

    private void Grow()
    {
        Transform newSegment = Instantiate(segmentPrefab);
        // Initialiser le nouveau segment à une position temporaire en dehors de la grille
        newSegment.position = new Vector2(-1000, -1000);
        segments.Add(newSegment);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
            GameManager.instance.AddScore(10);
            Destroy(other.gameObject);
            foodSpawner.SpawnFood();
        }
        else
        {
            // Collision avec un segment du serpent ou obstacle
            GameOver();
        }
    }

    private void GameOver()
    {
        // Afficher le game over, réinitialiser le jeu
        Debug.Log("Game Over!");
        GameManager.instance.ResetGame();
        // Rechargez la scène, réinitialisez les positions, etc.
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}