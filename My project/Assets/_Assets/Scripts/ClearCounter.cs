using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour ,IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private IKitchenObjectParent kitchenObjectParent;
   
    private KitchenObject kitchenObject;


    public void Interact(Player player)
    {
        // Debug.Log("Interact "+transform.gameObject.name);

        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
           // kitchenObjectTransform.localPosition = Vector3.zero;


            //Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
           // kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
          //  kitchenObject.SetClearCounter(this);
        }
        else
        { //Give object to the player

            //  kitchenObject.SetClearCounter(player);
            // Debug.Log(kitchenObject.GetClearCounter());

            kitchenObject.SetKitchenObjectParent(player);
        }
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject= kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    
}
