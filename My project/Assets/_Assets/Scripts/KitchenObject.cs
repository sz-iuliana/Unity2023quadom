using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenoOjectSO;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenoOjectSO;
    }
}
