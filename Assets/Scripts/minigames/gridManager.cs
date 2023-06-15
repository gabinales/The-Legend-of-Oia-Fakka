using UnityEngine;

public class gridManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    public Sprite cellSprite;

    void Start()
    {
        CreateGrid();
        Spawn(1);
    }

    void CreateGrid()
    {
        for (int row = 0; row < 7; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                GameObject cell = new GameObject("Cell (" + row + ", " + col + ")");
                cell.transform.parent = transform;
                cell.transform.position = new Vector3(col, row, 0);
                SpriteRenderer spriteRenderer = cell.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = cellSprite;
            }
        }
    }

    public void Spawn(int entity)
    {
        GameObject spawnCell = transform.Find("Cell (6, 0)").gameObject;

        // 1 = Jogador
        if (entity == 1)
        {
            GameObject playerSprite = Instantiate(PlayerPrefab, spawnCell.transform.position, Quaternion.identity);
        }
        else
        // 2 = Inimigo
        if (entity == 2)
        {
            GameObject enemySprite = Instantiate(EnemyPrefab, spawnCell.transform.position, Quaternion.identity);
            Debug.Log("enemy spawned");
            Vector3 randomVector = new Vector3(
             Random.Range(0, 6),
             Random.Range(0, 6),
             Random.Range(0, 6)
             );
            enemySprite.transform.position = randomVector;
            //sprite.AddComponent<EnemyMovement>();
        }
    }

    void Update()
    {
    }


}