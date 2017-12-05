using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    [SerializeField]
    private Transform gunTransform;

    public bool IsTriggered
    {
        get;
        private set;
    }

    protected void Update()
    {
        if (IsTriggered)
        {
            IsTriggered = false;
        }
        if (Input.GetMouseButton(0))
        {
            IsTriggered = true;
        }

        Vector3 mousePos = Camera.main.WorldToScreenPoint(gunTransform.position);
        Vector3 dir = Input.mousePosition - mousePos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
