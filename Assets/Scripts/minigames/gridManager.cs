using UnityEngine;

public class gridManager : MonoBehaviour
{
    public GameObject spritePrefab;  // Reference to the sprite prefab you want to place in the middle

    public Sprite cellSprite;

    void Start()
    {
        CreateGrid();
        Spawn();
    }

    void CreateGrid()
    {
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject cell = new GameObject("Cell (" + row + ", " + col + ")");
                cell.transform.parent = transform;
                cell.transform.position = new Vector3(col, row, 0);
                SpriteRenderer spriteRenderer = cell.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = cellSprite;
            }
        }
    }

    void Spawn()
    {
        GameObject middleCell = transform.Find("Cell (2, 2)").gameObject;
        GameObject sprite = Instantiate(spritePrefab, middleCell.transform.position, Quaternion.identity);
        sprite.transform.parent = middleCell.transform;

        sprite.AddComponent<SpriteMovement>();
    } 

    
}