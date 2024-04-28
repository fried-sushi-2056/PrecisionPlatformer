using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MerchantScript : MonoBehaviour
{
    public GameObject shopUI;
    [SerializeField] private PlayerMovementScript player;
    private bool shopUIActive = false;
    
    public void ShowShop(){
        if(!shopUIActive){
        shopUI.SetActive(true);
        }
        else{
            shopUI.SetActive(false);
        }
        shopUIActive = !shopUIActive;
    }

    public void ShowShop(bool shop){
        if(shop){
            shopUI.SetActive(true);
            shopUIActive = true;
        }
        else{
            shopUI.SetActive(false);
            shopUIActive = false;
        }
    }
}
