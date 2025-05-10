using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    public bool isSmallSlime;
    public float moveSpeed = 2f;
    public int maxHP = 20;
    public int currentHP;
    public float attackRange = 1.5f;
    public int damage = 5;
    public float attackDuration = 2.5f;
    private float attackTimer;

    public GameObject smallSlimePrefab;
    public Transform target;

    public Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;

        if (target == null && GameObject.FindWithTag("Player"))
            target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null) return;
        
        if (target.position.x < transform.position.x)
            animator.GetComponent<SpriteRenderer>().flipX = true;
        else
            animator.GetComponent<SpriteRenderer>().flipX = false;
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackRange)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDuration)
            {
                target.GetComponent<Player>().TakeDamage(damage);
                attackTimer = 0f;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Slime Dead");

        if (maxHP > 5 && smallSlimePrefab != null && isSmallSlime == false)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject clone = Instantiate(smallSlimePrefab, transform.position + new Vector3(i * 0.5f, 0, 0), Quaternion.identity);
                clone.GetComponent<SlimeAI>().maxHP = 5;
            }
        }

        Destroy(gameObject);
    }
}