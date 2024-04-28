using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantArea : MonoBehaviour
{
    public MerchantScript merchant;
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            merchant.ShowShop(true);
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            merchant.ShowShop(false);
        }
    }
}
