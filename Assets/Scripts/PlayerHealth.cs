using UnityEngine;
using UnityEngine.UI; // UI 관련 코드
using TMPro;

// 플레이어 캐릭터의 생명체로서의 동작을 담당
public class PlayerHealth : LifeEntity
{
    public Slider healthSlider; // 체력을 표시할 UI 슬라이더
    public TextMeshProUGUI healthText;
   
    public AudioClip deathClip; // 사망 소리
    public AudioClip hitClip; // 피격 소리
    public AudioClip itemPickupClip; // 아이템 습득 소리

    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    private Animator playerAnimator; // 플레이어의 애니메이터

    private PlayerMovement playerMovement; // 플레이어 움직임 컴포넌트
    private PlayerShooter playerShooter; // 플레이어 슈터 컴포넌트
    private Collider playerCollider;

    private void Awake()
    {
        // 사용할 컴포넌트를 가져오기
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
        playerCollider = GetComponent<CapsuleCollider>();

    }

    protected override void OnEnable()
    {
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();
        healthSlider.gameObject.SetActive(true); // 체력 슬라이더 활성화
        healthSlider.maxValue = startingHealth; // 체력 초기화(100)
        healthSlider.value = health; // 체력 슬라이더 값을 현재 체력값으로 변경
        playerMovement.enabled = true; // 플레이어 조작을 받는 컴포넌트 활성화
        playerShooter.enabled = true;
    }

    // 체력 회복
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity의 RestoreHealth() 실행 (체력 증가)
        base.RestoreHealth(newHealth);
        healthSlider.value = health; // 갱신된 체력으로 체력 슬라이더 갱신
        healthText.text = health + "  /  " + startingHealth;
    }

    // 데미지 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // LivingEntity의 OnDamage() 실행(데미지 적용)
        base.OnDamage(damage, hitPoint, hitDirection);
        playerAudioPlayer.PlayOneShot(hitClip);
        healthSlider.value = health;
        if(health <= 0 )
        {
            health = 0;
        }
        healthText.text = Mathf.Floor(health) + "  /  " + startingHealth; 
    }

    // 사망 처리
    public override void Die()
    {
        // LivingEntity의 Die() 실행(사망 적용)
        base.Die();
        //playerAudioPlayer.PlayOneShot(deathClip); // 사망음 재생
        playerAnimator.SetTrigger("Die"); // 사망 애니메이션 재생
        playerMovement.enabled = false; // 플레이어 조작을 받는 컴포넌트 비활성화
        playerShooter.enabled = false;
        playerCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 아이템과 충돌한 경우 해당 아이템을 사용하는 처리
        if (!dead)
        {
            // 충돌한 상대방으로부터 Iitem 컴포넌트를 가져오는데 성공했다면,
            IItem item = other.GetComponent<IItem>();
            if (item != null)
            {
                // Use 메소드를 실행하여 아이템을 사용.
                item.Use(gameObject);
                // 아이템 습득 소리 재생.
                playerAudioPlayer.PlayOneShot(itemPickupClip);
            }
        }
    }
}