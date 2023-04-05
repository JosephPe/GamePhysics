using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject spawnObject;
    Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(spawnObject, transform.localPosition, transform.localRotation);
        }
    }
}
