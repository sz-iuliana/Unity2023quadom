using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
       
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeComplated;
        DeliveryManager.Instance.OnRecipeComplated += DeliveryManager_OnRecipeSpawned;

        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeComplated(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }
    private void DeliveryManager_OnRecipeSpawned(object sender ,System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container){

            if (child == recipeTemplate) continue;
            {
                Destroy(child.gameObject);
            }
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
                recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);

        }
    }
}
