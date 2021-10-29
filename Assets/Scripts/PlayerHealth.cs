using UnityEngine;
using UnityEngine.UI; // UI ���� �ڵ�

// �÷��̾� ĳ������ ����ü�μ��� ������ ���
public class PlayerHealth : LifeEntity
{
    public Slider healthSlider; // ü���� ǥ���� UI �����̴�

    public AudioClip deathClip; // ��� �Ҹ�
    public AudioClip hitClip; // �ǰ� �Ҹ�
    public AudioClip itemPickupClip; // ������ ���� �Ҹ�

    private AudioSource playerAudioPlayer; // �÷��̾� �Ҹ� �����
    private Animator playerAnimator; // �÷��̾��� �ִϸ�����

    private PlayerMovement playerMovement; // �÷��̾� ������ ������Ʈ
    private PlayerShooter playerShooter; // �÷��̾� ���� ������Ʈ

    private void Awake()
    {
        // ����� ������Ʈ�� ��������
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();

    }

    protected override void OnEnable()
    {
        // LivingEntity�� OnEnable() ���� (���� �ʱ�ȭ)
        base.OnEnable();
        healthSlider.gameObject.SetActive(true); // ü�� �����̴� Ȱ��ȭ
        healthSlider.maxValue = startingHealth; // ü�� �ʱ�ȭ(100)
        healthSlider.value = health; // ü�� �����̴� ���� ���� ü�°����� ����

        playerMovement.enabled = true; // �÷��̾� ������ �޴� ������Ʈ Ȱ��ȭ
        playerShooter.enabled = true;
    }

    // ü�� ȸ��
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity�� RestoreHealth() ���� (ü�� ����)
        base.RestoreHealth(newHealth);
        healthSlider.value = health; // ���ŵ� ü������ ü�� �����̴� ����
    }

    // ������ ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // LivingEntity�� OnDamage() ����(������ ����)
        base.OnDamage(damage, hitPoint, hitDirection);
        healthSlider.value = health;
    }

    // ��� ó��
    public override void Die()
    {
        // LivingEntity�� Die() ����(��� ����)
        base.Die();
        healthSlider.gameObject.SetActive(false); // ü�� �����̴� ��Ȱ��ȭ
        //playerAudioPlayer.PlayOneShot(deathClip); // ����� ���
        playerAnimator.SetTrigger("Die"); // ��� �ִϸ��̼� ���
        playerMovement.enabled = false; // �÷��̾� ������ �޴� ������Ʈ ��Ȱ��ȭ
        playerShooter.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����۰� �浹�� ��� �ش� �������� ����ϴ� ó��
        if (!dead)
        {
            // �浹�� �������κ��� Iitem ������Ʈ�� �������µ� �����ߴٸ�,
            IItem item = other.GetComponent<IItem>();
            if (item != null)
            {
                // Use �޼ҵ带 �����Ͽ� �������� ���.
                item.Use(gameObject);
                // ������ ���� �Ҹ� ���.
                playerAudioPlayer.PlayOneShot(itemPickupClip);
            }
        }
    }
}