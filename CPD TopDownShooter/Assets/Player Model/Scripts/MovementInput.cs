
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementInput : MonoBehaviour
{
  
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;


    void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

    private void Update()
    {
        m_animator.SetFloat("MoveSpeed", 6);
    }

}

