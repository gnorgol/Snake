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

    // Convertir les coordonn�es de grille en positions du monde
    public Vector2 GridToWorld(int x, int y)
    {
        float worldX = (x - gridWidth / 2) * cellSize + cellSize / 2;
        float worldY = (y - gridHeight / 2) * cellSize + cellSize / 2;
        return new Vector2(worldX, worldY);
    }

    // V�rifier si une position est � l'int�rieur de la grille
    public bool IsWithinGrid(int x, int y)
    {
        return x >= 0 && x < gridWidth && y >= 0 && y < gridHeight;
    }

    // Obtenir une position al�atoire sur la grille
    public Vector2 GetRandomGridPosition()
    {
        int x = Random.Range(0, gridWidth);
        int y = Random.Range(0, gridHeight);
        return GridToWorld(x, y);
    }

    // Ajuster la cam�ra pour qu'elle cadre la grille et soit centr�e
    private void AdjustCamera()
    {
        Camera camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("Aucune cam�ra principale trouv�e.");
            return;
        }

        // Calculer le ratio d'aspect de l'�cran
        float screenAspect = (float)Screen.width / Screen.height;
        // Ratio de la grille
        float gridAspect = (float)gridWidth / gridHeight;

        if (screenAspect >= gridAspect)
        {
            // L'�cran est plus large que la grille
            camera.orthographicSize = (gridHeight / 2f) * cellSize;
        }
        else
        {
            // L'�cran est plus �troit que la grille
            camera.orthographicSize = (gridWidth / 2f) / screenAspect * cellSize;
        }

        // Centrer la cam�ra sur la grille
        camera.transform.position = new Vector3(0, 0, camera.transform.position.z);
    }
}