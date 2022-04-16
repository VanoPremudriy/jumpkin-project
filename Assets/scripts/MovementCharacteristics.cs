using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characteristics", menuName = "Movement/MovementCharacteristics", order = 1)]
public class MovementCharacteristics : ScriptableObject
{
    [SerializeField] private bool visibleCursor = false;

    [SerializeField] private float movementSpeed = 1f;

    [SerializeField] private float angularSpeed = 150f;

    [SerializeField] private float gravity = -9.8f;

    [SerializeField] private float jumpSpeed = 10f;

    public bool VisibleCursor => visibleCursor;
    public float MovementSpeed => movementSpeed;
    public float AngularSpeed => angularSpeed;
    public float Gravity => gravity;

    public float JumpSpeed => jumpSpeed;
    

}
