using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        float stoppingDistance = .1f;

        if(Vector3.Distance(transform.position , targetPosition) > stoppingDistance)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += direction * moveSpeed * Time.deltaTime;
            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward , direction , Time.deltaTime * rotateSpeed);

            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }

    }
    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
