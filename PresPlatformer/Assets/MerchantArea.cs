using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantArea : MonoBehaviour
{
    public MerchantScript merchant;
    void OnTriggerEnter2D(Collider2D collider){
        merchant.ShowShop();
    }
}
