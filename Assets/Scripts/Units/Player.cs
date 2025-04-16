using UnityEngine;

public class Player : MonoBehaviour
{
    public AttackData selectedAttack;
    private Vector2Int aimedTile;
    private GridManager gridManager;
    public Vector2Int gridPos;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    public void SelectAttack(AttackData attack)
    {
        selectedAttack = attack;
        Debug.Log($"Selected attack: {selectedAttack.attackName} ({selectedAttack.elementType})");
    }

    public void AimAtTile(Vector2Int tilePos)
    {
        aimedTile = tilePos;
        Debug.Log($"Aiming at tile: {tilePos}");
        // Optional: Show attack preview pattern on grid
    }

    public void ExecuteAttack()
    {
        if (selectedAttack == null)
        {
            Debug.LogWarning("No attack selected.");
            return;
        }

        foreach (var offset in selectedAttack.patternOffsets)
        {
            Vector2Int target = aimedTile + offset;

            // Grid bounds check
            if (target.x < 0 || target.x >= gridManager.columns || 
                target.y < 0 || target.y >= gridManager.rows)
            {
                Debug.Log($"Target {target} out of bounds.");
                continue;
            }

            Debug.Log($"Attacking {target} with {selectedAttack.attackName}");

            // Hit check (placeholder logic)
            Enemy enemy = EnemyAt(target);
            if (enemy != null)
            {
                enemy.TakeDamage(selectedAttack.elementType, selectedAttack.power);
            }
        }

        selectedAttack = null; // Clear current attack after use
    }

    private Enemy EnemyAt(Vector2Int pos)
    {
        // Replace with proper tracking system (e.g. EnemyManager or 2D array map)
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy e in allEnemies)
        {
            if (e.gridPos == pos)
                return e;
        }
        return null;
    }
}
