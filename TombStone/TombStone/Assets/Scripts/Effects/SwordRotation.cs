using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRotation : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = Camera.main.transform.position.z * Vector3.back.z;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((mousePos - transform.position).normalized, Vector3.back), speed * Time.deltaTime);
    }
}
