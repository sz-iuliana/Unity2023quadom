using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
   private void Awake
    {
        Player.Instance.OnSelectedCounterChanged += PlayerectedCounterChanged;
    }
    private void Instance_OnSelectedCounterChanged(object sender,Player,OnSelectedCounterChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}
