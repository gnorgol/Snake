using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int gridWidth = 20;
    public int gridHeight = 20;
    public float cellSize = 1f;
    public SpriteRenderer gridSprite;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        AdjustCamera();
        gridSprite.size = new Vector2(gridWidth * cellSize, gridHeight * cellSize);
    }

    // Convertir les coordonnées de grille en positions du monde
    public Vector2 GridToWorld(int x, int y)
    {
        float worldX = (x - gridWidth / 2) * cellSize + cellSize / 2;
        float worldY = (y - gridHeight / 2) * cellSize + cellSize / 2;
        return new Vector2(worldX, worldY);
    }

    // Vérifier si une position est à l'intérieur de la grille
    public bool IsWithinGrid(int x, int y)
    {
        return x >= 0 && x < gridWidth && y >= 0 && y < gridHeight;
    }

    // Obtenir une position aléatoire sur la grille
    public Vector2 GetRandomGridPosition()
    {
        int x = Random.Range(0, gridWidth);
        int y = Random.Range(0, gridHeight);
        return GridToWorld(x, y);
    }

    // Ajuster la caméra pour qu'elle cadre la grille et soit centrée
    private void AdjustCamera()
    {
        Camera camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("Aucune caméra principale trouvée.");
            return;
        }

        // Calculer le ratio d'aspect de l'écran
        float screenAspect = (float)Screen.width / Screen.height;
        // Ratio de la grille
        float gridAspect = (float)gridWidth / gridHeight;

        if (screenAspect >= gridAspect)
        {
            // L'écran est plus large que la grille
            camera.orthographicSize = (gridHeight / 2f) * cellSize;
        }
        else
        {
            // L'écran est plus étroit que la grille
            camera.orthographicSize = (gridWidth / 2f) / screenAspect * cellSize;
        }

        // Centrer la caméra sur la grille
        camera.transform.position = new Vector3(0, 0, camera.transform.position.z);
    }
}