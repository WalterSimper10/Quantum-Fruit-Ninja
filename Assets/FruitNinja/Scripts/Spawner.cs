using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;
    [Range(0f,1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 0.75f;
    public float maxSpawnDelay = 1.5f;
    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 3f;
    public int Id {get; set;}

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        StartCoroutine(SpawnLoop());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(6.5f);
        while (enabled)
        {
            SpawnFruit(0, fruitPrefabs.Length, 
                        spawnArea.bounds.min.x, spawnArea.bounds.max.x, 
                        spawnArea.bounds.min.y, spawnArea.bounds.max.y,
                        spawnArea.bounds.min.z, spawnArea.bounds.max.z, minForce, maxForce, false, false);
            yield return new WaitForSeconds(Random.Range(minSpawnDelay,maxSpawnDelay));
        }
    }

    public GameObject SpawnFruit(int idRngMin, int idRngMax, 
                            float Xmin, float Xmax, 
                            float Ymin, float Ymax, 
                            float Zmin, float Zmax, 
                            float Forcemin, float Forcemax, 
                            bool isEntangle, bool isTunnel)
    {
            Id = Random.Range(idRngMin, idRngMax);
            GameObject prefab = fruitPrefabs[Id];

            if(Random.value < bombChance && !isEntangle && !isTunnel){
                prefab = bombPrefab;
            }

            Vector3 position = new Vector3
            {
                x = Random.Range(Xmin, Xmax),
                y = Random.Range(Ymin, Ymax),
                z = Random.Range(Zmin, Zmax)
            };

            Quaternion rotation = Quaternion.Euler(0f,0f,Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, maxLifetime);
            Fruit fruitScript = fruit.GetComponent<Fruit>();

            if (fruitScript != null)
            {
                fruitScript.fruitType = Id;
            }

            if (fruitScript != null)
            {
                fruitScript.isEntangle = isEntangle;
            }

            if (fruitScript != null)
            {
                fruitScript.isTunnel = isTunnel;
            }

            float force = Random.Range(Forcemin, Forcemax);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

        return fruit;
    }
}
