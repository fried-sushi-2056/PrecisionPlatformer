using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MerchantScript : MonoBehaviour
{
    public GameObject shopUI;
    [SerializeField] private PlayerMovementScript player;
    private bool shopUIActive = false;//tracks the state of the ui
    private bool canShowShop = false;
    
    public void ShowShop(){//Reverses state of this merchant's UI
        if(!shopUIActive){
        shopUI.SetActive(true);
        }
        else{
            shopUI.SetActive(false);
        }
        shopUIActive = !shopUIActive;
    }

    public void ShowShop(bool canShow){
        canShowShop = canShow;
    }

    void Update(){
        if(Input.GetKeyDown("f") && !shopUIActive && canShowShop){
            shopUI.SetActive(true);
            shopUIActive = true;
        }
        else if(Input.GetKeyDown("f") && shopUIActive){
            shopUI.SetActive(false);
            shopUIActive = false;
        }
        else if(canShowShop == false){
            shopUI.SetActive(false);
        }
    }
}
