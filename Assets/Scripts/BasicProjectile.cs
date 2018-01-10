using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float coolingStrength;
    [SerializeField] private float projectileOffset;

    // Use this for initialization
    void Start()
    {
        transform.position += projectileOffset * transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += Time.deltaTime * movementSpeed * transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
         
        HitzeBlockClass hit = collision.GetComponent<HitzeBlockClass>()
        
        if(hit != null)
        {
            hit->AddCooling(coolingStrength);
        }

        */

        Destroy(gameObject);
    }
}
