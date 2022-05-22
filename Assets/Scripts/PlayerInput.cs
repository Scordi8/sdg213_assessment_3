using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private EngineBase movement;
    [SerializeField] private bool CanMove = true;
    [SerializeField] private float MovementSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EngineBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove && movement)
        {
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movement.Move(direction * MovementSpeed);
        }

    }
}
