using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    public void RemoveObjects() {
        for(int i = 0; i < objects.Length; i++) {
            if(objects != null) Destroy(objects[i]);
        }
    }
}
