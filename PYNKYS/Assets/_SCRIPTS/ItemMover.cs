using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{

    Rigidbody _rig;
    Vector3 _velocity;
    public float _speed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _velocity = new Vector3(_speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _rig.velocity = _velocity;
    }
}
