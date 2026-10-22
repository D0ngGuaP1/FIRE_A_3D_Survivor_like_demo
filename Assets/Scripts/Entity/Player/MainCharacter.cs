using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private Animator animator;
    private CharacterController cc;
    
    public float speed = 10f;
    public GameObject target;

    private float vertical;
    private float horizontal;
    private static readonly int VerticalHash = Animator.StringToHash("Vertical");
    private static readonly int HorizontalHash = Animator.StringToHash("Horizontal");

    private Weapon weapon;
    [SerializeField] Transform firePosition;
    
    private PlayerHealth health;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        weapon = GetComponentInChildren<Weapon>();
        health = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 localMove = transform.InverseTransformDirection(move);
        
        animator.SetFloat(VerticalHash, localMove.z, 0.1f, Time.deltaTime);
        animator.SetFloat(HorizontalHash, localMove.x, 0.1f, Time.deltaTime);
        
        cc.Move(move * (speed * Time.deltaTime));

        if (target)
        {
            Quaternion lookRotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                lookRotation,
                540f * Time.deltaTime
            );
            weapon.TryFire(firePosition);
        }
    }
    
    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtWeight(
            weight: 1f,
            bodyWeight: 0.5f, // 上半身跟着转
            headWeight: 1f,
            eyesWeight: 1f,
            clampWeight: 0.5f
        );
        if(target) animator.SetLookAtPosition(target.transform.position);
    }

    public void TakeDamage(float damage)
    {
        health.TakeFloatDamage(damage);
    }
    
}
