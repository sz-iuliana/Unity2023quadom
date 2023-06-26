using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;
using System;

public class StoveCounter : BaseCounter ,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
    
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,

    }

   

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private float burningTimer;  
    private BurningRecipeSO burningRecipeSO;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTimerMax;

    private void Start()
    {
        state = State.Idle;
    }


    private void Update() {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;

                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {

                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax 
                    });

                   
                 

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //fried
                        KitchenObject kitchenObject = GetKitchenObject();

                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        Debug.Log("Object fried !");

                       state = State.Fried;
                        
                        burningTimer = 0f;
                       // burningRecipeSO = GetBurningRecipeSOWithInput(kitchenObject);
                        burningTimerMax=burningRecipeSO.burningTimerMax;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        

                            state = state

                        }); 


                         
                    }
                    break;
                  case State.Fried:

                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {

                        progressNormalized = burningTimer / burningTimerMax

                    });


                   

                    if (burningTimer > burningTimerMax)
                    {
                        //fried


                        GetKitchenObject().DestroySelf();
                        

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                       

                       Debug.Log("Object burned !");
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                       

                            state = state

                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {

                            progressNormalized = 0f

                        });

                    }

                    break;
                case State.Burned:
                    break;

            }
            Debug.Log(state);

        }

    }

    

        
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        { //there is no  kitchenobject

            if (player.HasKitchenObject())
            { //player is carrying something


                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {

                    //player carrying something that can be fried

                    player.GetKitchenObject().SetKitchenObjectParent(this);

                     fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                   // fryingTimerMax = RryingRecipeSO.fryingTimerMax;
                   // burningTimerMax = BurningRecipeSO.burningTimerMax;

                    state =State.Frying;
                    fryingTimer=0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    

                        state = state

                    });

                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs {

                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax

                    });
                }
            }
            else
            {
                //player not carrying anything
            }
        }
        else
        { //there is a kitchenobject
            if (player.HasKitchenObject())
            { //player is carrying something


                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate 

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {


                            state = state

                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {

                            progressNormalized = 0f

                        });
                    }



                }


            }
            else
            { //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

                state=State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                

                    state = state

                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                

                    progressNormalized = 0f

                });

            }

        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
       FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {

       FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }


    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {

        foreach (FryingRecipeSO friyingRecipeSO in fryingRecipeSOArray)
        {
            if (friyingRecipeSO.input == inputKitchenObjectSO)
            {
                return friyingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {                                                   

        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
