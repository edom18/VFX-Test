using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDFTest : MonoBehaviour
{
    [SerializeField] private int _count = 100;
    [SerializeField] private float _distance = 1f;
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private float _scale = 0.01f;
    [SerializeField] private Vector3 _origin = Vector3.zero;

    private Transform[] _particles = null;

    private void Start()
    {
        _particles = new Transform[_count];

        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i] = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            _particles[i].localScale = Vector3.one * _scale;
            _particles[i].position = Random.insideUnitSphere * 0.1f;
        }
    }

    private void Update()
    {
        UpdateParticles();
    }

    private void UpdateParticles()
    {
        foreach (var p in _particles)
        {
            Vector3 delta = p.position - _origin;
            float len = delta.magnitude;
            float sd = len - _distance;

            if (Mathf.Abs(sd) < 0.01f)
            {
                Vector3 tan = Vector3.Cross(delta, Vector3.up).normalized;
                p.position += tan * _speed;
            }
            else
            {
                p.position += (delta / len) * -Mathf.Sign(sd) * _speed;
            }
        }
    }
}

