using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    [SerializeField]
    private Transform gunTransform;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float shotCooldown;
    private float currentShotCooldown;
    [SerializeField] private float weaponLength;

    public bool IsTriggered
    {
        get;
        private set;
    }

    private void Awake()
    {
        currentShotCooldown = 0f;
    }

    protected void Update()
    {
        currentShotCooldown -= Time.deltaTime;

        IsTriggered = Input.GetMouseButton(0);

        if(IsTriggered && currentShotCooldown <= 0f)
        {
            currentShotCooldown = shotCooldown;
            Shoot();
        }

        Vector3 mousePos = Camera.main.WorldToScreenPoint(gunTransform.position);
        Vector3 dir = Input.mousePosition - mousePos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Shoot()
    {
        Instantiate(projectile, gunTransform.position + (weaponLength * gunTransform.right), gunTransform.rotation);
    }
}
