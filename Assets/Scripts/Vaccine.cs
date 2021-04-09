using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Vaccine : MonoBehaviour
{
    [SerializeField] private float _vaccineSpeed = 7f;
    void Update()
    {
        transform.Translate(Vector3.up * _vaccineSpeed * Time.deltaTime);

        if (transform.position.y > 7f)
        {
            Destroy(this.gameObject);
        }
    }
}
