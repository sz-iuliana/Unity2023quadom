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
        { //there is a kitchenobject
            if (player.HasKitchenObject())
            { //player is carrying 

            }
            else
            { //player is not carrying
                GetKitchenObject().SetKitchenObjectParent(player);

            }

        }

    }
    
   
}
