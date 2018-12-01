using Assets.Scripts.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Housing : MonoBehaviour {

    public int populationSupply;

    private void Start()
    {
        GameplayController.instance.maxPopulation += populationSupply;
    }

    private void OnDestroy()
    {
        if (GameplayController.instance != null)
        {
            GameplayController.instance.maxPopulation -= populationSupply;
        }
    }
}
