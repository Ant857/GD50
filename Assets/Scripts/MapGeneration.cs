using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] int width = 100;
    [SerializeField] int height = 100;
    [SerializeField] Tilemap Collidable;
    [SerializeField] Tilemap Terrain;
    [SerializeField] Tile[] grass;
    [SerializeField] Tile[] plants;
    [SerializeField] Tile[] rocks;
    [SerializeField] Tile[] crates;
    [Tooltip("Rarities should be around 40-80.")]
    [SerializeField] int plantsRarity;
    [SerializeField] int rocksRarity;
    [SerializeField] int cratesRarity;

    public NavMeshSurface2d[] surfaces;
    public GameObject[] EnemyAI;
    public int enemyCount;
    public LayerMask obstacleMask;
    public LayerMask circleMask;
    public LayerMask playerMask;
    public GameObject Player;
    public GameObject Circle;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Circle = GameObject.Find("Circle");
        Generate();
    }

    // Update is called once per frame
    void Generate()
    {
        for (int x = -(width / 2); x < (width / 2); x++)
        {
            for (int y = -(height / 2); y < (height / 2); y++)
            {
                Terrain.SetTile(new Vector3Int(x, y, 0), grass[Random.Range(0, grass.Length)]);
                if (Random.Range(1, rocksRarity) == 1)
                {
                    Collidable.SetTile(new Vector3Int(x, y, 0), rocks[Random.Range(0, rocks.Length)]);
                }
                if (Random.Range(1, plantsRarity) == 1)
                {
                    Collidable.SetTile(new Vector3Int(x, y, 0), plants[Random.Range(0, plants.Length)]);
                }
                if (Random.Range(1, cratesRarity) == 1)
                {
                    Collidable.SetTile(new Vector3Int(x, y, 0), crates[Random.Range(0, crates.Length)]);
                }
            }
        }
        BakeMap();
        SpawnPlayer();
        SpawnEnemyAI();
    }
    private void BakeMap()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
    private void SpawnEnemyAI()
    {
        int enemyVariant = 0;
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 SpawnLocation = new Vector2(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2));

            while (Physics2D.OverlapCircle(SpawnLocation, 1f, obstacleMask) != null || Physics2D.OverlapCircle(SpawnLocation, 1f, circleMask) == null || Physics2D.OverlapCircle(SpawnLocation, 30f, playerMask) != null)
            {
                SpawnLocation = new Vector2(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2));
            }

            Instantiate(EnemyAI[enemyVariant], SpawnLocation, Quaternion.identity);
            if (enemyVariant < 2)
            {
                enemyVariant++;
            }
            else
            {
                enemyVariant = 0;
            }
        }
    }

    private void SpawnPlayer()
    {
        Vector2 SpawnLocation = new Vector2(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2));

        while (Physics2D.OverlapCircle(SpawnLocation, 1f, obstacleMask) != null || Physics2D.OverlapCircle(SpawnLocation, 2f, circleMask) == null)
        {
            SpawnLocation = new Vector2(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2));
        }

        Player.transform.position = SpawnLocation;
    }
}
