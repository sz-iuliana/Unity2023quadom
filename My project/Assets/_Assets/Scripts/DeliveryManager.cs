using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeComplated;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax=4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

              
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }

        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
          
            
            bool recipeOK = false;
            if(plateKitchenObject.GetKitchenObjectSOList().Count== waitingRecipeSO.kitchenObjectSOList.Count)
            {
                recipeOK = true;
                //daca are nr de elemente reteta
                foreach (var ingredient in plateKitchenObject.GetKitchenObjectSOList())
                {
                    //daca are acelasi ingrediente ca si reteta
                    if (!waitingRecipeSO.kitchenObjectSOList.Contains(ingredient))
                    {
                        recipeOK = true;
                        
                    }
                }
            }

            if (recipeOK)
            {
                Debug.Log("Player served correct recipe" + waitingRecipeSO.recipeName);
                waitingRecipeSOList.RemoveAt(i);

                OnRecipeComplated?.Invoke(this, EventArgs.Empty);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                return ;
            }


        }
        



    } 

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;

    }


}
