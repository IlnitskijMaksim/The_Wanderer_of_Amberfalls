using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Animator animatorSword;
    public float delay = 0.3f;
    private bool attackBlocked;

    public Transform circleOrigin;
    public float radius;


    private void Start()
    {
        attackBlocked = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            SwordAttack();
        }
    }

    public void SwordAttack()
    {
        if (attackBlocked)
            return;

        animatorSword.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    public void CancelAttack()
    {
        StopAllCoroutines();
        attackBlocked = false;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }
}