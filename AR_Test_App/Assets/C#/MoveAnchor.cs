using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnchor : MonoBehaviour
{
    public GameObject model;

    void Start() 
    {
        model = GameObject.Find("Root");
        model.transform.parent = this.transform;
    }

    void Update()
    {
        model.transform.position = this.transform.position;
    }
}
