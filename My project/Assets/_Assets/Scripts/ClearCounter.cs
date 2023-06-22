using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClearCounter : BaseCounter 
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   
   
    


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        { //there is no a kitchenobject
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player carrying object
            }
        }
        else
        { //there is a kitchenobject here
            if (player.HasKitchenObject())
            { //player is carrying something


                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate 
                   
                   if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
                else
                {
                    //player is not carrying Plate but something else
                    if(GetKitchenObject().TryGetPlate(out  plateKitchenObject))
                    {
                        //counter is holding a plate

                      if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                           player.GetKitchenObject().DestroySelf();
                        }

                    }
                }

            }
            else
            { //player is not carrying
                GetKitchenObject().SetKitchenObjectParent(player);

            }

        }

    }
    
   
}
