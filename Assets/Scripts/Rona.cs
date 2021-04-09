using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rona : MonoBehaviour
{
    [SerializeField] private float _infectionSpeed = 6f;
    void Start()
    {
        transform.position = new Vector3(x: Random.Range(-10f, 10f), y: 7f, z: 0f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _infectionSpeed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(
                x: Random.Range(-10f, 10f), y: 7f, z: 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the object we collide with is player
        if (other.CompareTag("Player"))
        {
            // damage player or destroy them
            // get rigidbody component form player script
            other.GetComponent<Player>().Damage();
            
            Destroy(this.gameObject);
        }
        // if the object is vaccine
        else if (other.CompareTag("Vaccine"))
        {
            // destroy vaccine and virus
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
