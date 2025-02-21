using UnityEngine;
using UnityEngine.InputSystem;
public class player : MonoBehaviour
{
    [SerializeField] float movespeed = 5f;
    Vector2 playerInput;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    Vector2 minBound;
    Vector2 maxBound;

    Shooting shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooting>();
    }

    private void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Movement();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    private void Movement()
    {
        Vector3 delta = playerInput * movespeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBound.x + paddingLeft, maxBound.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBound.y +paddingBottom, maxBound.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
