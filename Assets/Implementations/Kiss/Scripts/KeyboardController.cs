using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiss
{
    public class KeyboardController : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private float maxVelocity;
        [SerializeField] private float acceleration;
        [SerializeField] private float rotation;

        [Header("Movement")]
        [SerializeField] private KeyCode keyUp = KeyCode.W;
        [SerializeField] private KeyCode keyLeft = KeyCode.A;
        [SerializeField] private KeyCode keyDown = KeyCode.S;
        [SerializeField] private KeyCode keyRight = KeyCode.D;

        [Header("Rotation")]
        [SerializeField] private KeyCode keyRotateLeft = KeyCode.LeftArrow;
        [SerializeField] private KeyCode keyRotateRight = KeyCode.RightArrow;

        private Vector2 velocity;

        private void Update()
        {
            ApplyAcceleration(KeysToVector2(keyUp, keyLeft, keyDown, keyRight));
            ApplyRotation(KeysToScalar(keyRotateLeft, keyRotateRight));
        }

        private void ApplyAcceleration(Vector2 a)
        {
            velocity += a * acceleration * Time.deltaTime;

            // Clamp
            if (velocity.sqrMagnitude > maxVelocity * maxVelocity)
            {
                velocity = velocity.normalized * maxVelocity;
            }

            transform.localPosition += new Vector3(velocity.x, velocity.y, 0f);
        }

        private void ApplyRotation(float dir)
        {
            if (dir != 0f)
            {
                transform.Rotate(0f, 0f, dir * rotation * Time.deltaTime);
            }
        }

        private static Vector2 KeysToVector2(KeyCode up, KeyCode left, KeyCode down, KeyCode right)
        {
            Vector2 v = new Vector2();
            if (Input.GetKey(up))
            {
                v += new Vector2(0f, 1f);
            }
            if (Input.GetKey(left))
            {
                v += new Vector2(-1f, 0f);
            }
            if (Input.GetKey(down))
            {
                v += new Vector2(0f, -1f);
            }
            if (Input.GetKey(right))
            {
                v += new Vector2(1f, 0f);
            }

            return v.normalized;
        }

        private static float KeysToScalar(KeyCode left, KeyCode right)
        {
            float rot = 0f;
            if (Input.GetKey(left))
            {
                rot += 1f;
            }
            if (Input.GetKey(right))
            {
                rot += -1f;
            }
            return rot;
        }
    }
}