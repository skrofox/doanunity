using System.Collections;
using UnityEngine;

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Buff Details")]
    [SerializeField] private float buffDuration = 5;
    [SerializeField] private bool canBeUsed = true;

    [Header("Floaty Movement")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatRange = .1f;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        float YOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, YOffset);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //dung coroutine de lam hieu ung buff
        if (!canBeUsed) return;

        StartCoroutine(BuffCo(buffDuration));
    }

    private IEnumerator BuffCo(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear;
        Debug.Log("Buff is applied for: " + duration + "second!");
        yield return new WaitForSeconds(duration);

        Debug.Log("Buff is removed!");

        Destroy(gameObject);
    }
}
