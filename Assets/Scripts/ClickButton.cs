using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class ClickButton : MonoBehaviour
{
    public Collider2D clickButtonCollider;

    private Animator clickButtonAnimator;

    public GameInputs inputActions;

    public CoinManager coinManager;

    private Vector2 clickPosition;

    public AudioManager audioManager;

    public GameObject moneyParticleSystem;

    public bool clickPerformed = false;
    void Start()
    {
        inputActions = new GameInputs();
        clickButtonCollider = GetComponent<Collider2D>();
        clickButtonAnimator = GetComponent<Animator>();
        inputActions.Taps.Enable();
        inputActions.Taps.ButtonClick.performed+=OnButtonClicked;
    }

    void OnDisable()
    {
        inputActions.Taps.Disable();
    }

    
    void Update()
    {
        
    }

    public void OnButtonClicked(InputAction.CallbackContext context)
    {
        
        if(context.performed)
        {
            clickPosition = context.ReadValue<Vector2>();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            if(clickButtonCollider.OverlapPoint(worldPosition))
            {
                clickPerformed=true;
                Debug.Log("Clicked button!");
                coinManager.coins+=coinManager.coinsPerClick;
                clickButtonAnimator.SetTrigger("Clicked");
                audioManager.Play("clickSound1");
                GameObject particles = Instantiate(moneyParticleSystem,new Vector3(0,-2.95f,0),Quaternion.identity);
                ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
                if(particleSystem!=null)
                {
                    particleSystem.Play();
                    Debug.Log("Particle system instantiated!");
                }
                Destroy(particles,0.48f);
            }
            else
            {
                clickPerformed=false;
            }
        }
        
    }
}
