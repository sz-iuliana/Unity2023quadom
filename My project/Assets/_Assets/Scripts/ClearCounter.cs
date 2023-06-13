using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour ,IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] bool testing;
    public KitchenObject kitchenObject;

    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null) {
                kitchenObject.SetClearCounter(secondClearCounter);
               // Debug.Log(kitchenObject.GetClearCounter());
            
            }


        }
    }

    public void Interact(Player player)
    {
        // Debug.Log("Interact "+transform.gameObject.name);

        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
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
    
    public Transform GetKitchenObjectFollowtransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject= kitchenObject;
    }
    public KittchenObject GetKitchenObject()
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
