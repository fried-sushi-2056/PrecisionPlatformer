using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFriction : MonoBehaviour
{
    [SerializeField] private float friction;
    public float GetFriction() { return friction; }
}
