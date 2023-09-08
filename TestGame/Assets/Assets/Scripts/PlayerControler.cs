using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Horizontal");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                var targetPosition = transform.position;
                targetPosition.x = input.x;
                targetPosition.y = input.y;

                StartCoroutine(Move(targetPosition));
            }
        }
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;

        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {     
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;

        isMoving = false;
    }
}