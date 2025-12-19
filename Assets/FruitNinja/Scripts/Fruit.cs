using UnityEngine;
using System.Collections.Generic;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;

    public int points = 1;
    public int fruitType;
    public bool isEntangle;

    public bool isTunnel;

    [Header("Entanglement Settings")]
    public Fruit entangledPartner;
    private bool hasBeenSliced = false;
    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        Object.FindAnyObjectByType<GameManager>().IncreaseScore(points);

        if (hasBeenSliced) return;
        hasBeenSliced = true;

        if (entangledPartner != null && entangledPartner.gameObject.activeInHierarchy)
        {
            entangledPartner.Slice(direction, transform.position, force);
        }

    
        whole.SetActive(false);

        Entangle(position);
        Tunnel(position);
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        
        foreach (Rigidbody slice in slices)
        {
            slice.linearVelocity = fruitRigidbody.linearVelocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void Entangle(Vector3 position)
    {
        if (fruitType != 1 || isEntangle)
        {
            sliced.SetActive(true);
            fruitCollider.enabled = false;
            juiceParticleEffect.Play();
        }
        else if (fruitType == 1)
        {
            int chanceEngtangle = Random.Range(0, 10);
            if (chanceEngtangle < 2)
            {
                sliced.SetActive(true);
                fruitCollider.enabled = false;
                juiceParticleEffect.Play();
            }
            else
        {
            Rigidbody[] entangleParts = sliced.GetComponentsInChildren<Rigidbody>();
            
            List<Fruit> spawnedFruits = new List<Fruit>();
            
            Spawner spawner = Object.FindAnyObjectByType<Spawner>();

            foreach (Rigidbody slice in entangleParts)
            {
                GameObject newObj = spawner.SpawnFruit(1, 2, position[0]-1, position[0]+1,
                                            position[1]+2, position[1]+2,
                                            position[2], position[2], 13f, 17f, true, false);
                
                Fruit fScript = newObj.GetComponent<Fruit>();
                if(fScript != null)
                {
                    spawnedFruits.Add(fScript);
                }
            }

            if (spawnedFruits.Count == 2)
            {
                spawnedFruits[0].entangledPartner = spawnedFruits[1];
                spawnedFruits[1].entangledPartner = spawnedFruits[0];
            }

            fruitCollider.enabled = false;
            Destroy(fruitRigidbody);
            whole.SetActive(false);
            sliced.SetActive(false);
        }
        }
    }

    private void Tunnel(Vector3 position)
    {
        if (fruitType != 2 || isTunnel)
        {
            sliced.SetActive(true);
            fruitCollider.enabled = false;
            juiceParticleEffect.Play();
        }
        else if (fruitType == 2)
        {
            int chanceTunnel = Random.Range(0, 10);
            if (chanceTunnel < 2)
            {
                sliced.SetActive(true);
                fruitCollider.enabled = false;
                juiceParticleEffect.Play();
            }
            else
            {
                Object.FindAnyObjectByType<Spawner>().SpawnFruit(2, 3, position[0]-7, position[0]+7,
                                                            position[1]-10, position[1]+10,
                                                            position[2], position[2], 6f, 10f, false, true);
                
                Rigidbody[] tunnel = sliced.GetComponentsInChildren<Rigidbody>();

                foreach (Rigidbody slice in tunnel)
                {
                    fruitCollider.enabled = false;
                    Destroy(fruitRigidbody);
                    whole.SetActive(false);
                    sliced.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenSliced)
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
