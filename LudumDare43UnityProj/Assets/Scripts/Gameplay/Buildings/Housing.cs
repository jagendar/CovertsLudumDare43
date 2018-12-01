using Assets.Scripts.Gameplay;
using System.Collections;
using UnityEngine;

public class Housing : MonoBehaviour {

    [SerializeField] public GameObject personPrefab;
    [SerializeField] int populationSupply;
    [SerializeField] float spawnChancePerSecond = .01f;
    [SerializeField] Transform spawnSpot;

    private GameObject[] people;

    private void Start()
    {
        people = new GameObject[populationSupply];
        GameplayController.instance.maxPopulation += populationSupply;
        StartCoroutine(SpawnPeople());
    }

    private void OnDestroy()
    {
        if (GameplayController.instance != null)
        {
            GameplayController.instance.maxPopulation -= populationSupply;
        }
    }

    private IEnumerator SpawnPeople()
    {
        while(true)
        {
            if (Random.value < spawnChancePerSecond)
            {
                for (int i = 0; i < people.Length; ++i)
                {
                    if (people[i] == null)
                    {
                        people[i] = Instantiate(personPrefab, spawnSpot.position, spawnSpot.rotation);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
