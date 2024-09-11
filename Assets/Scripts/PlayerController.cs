using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float screenLimitBuffer = 0.5f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        #if UNITY_ANDROID
        HandleTouchInput(); // 모바일 터치 입력 처리
        #else
        HandleKeyboardAndMouseInput();
        #endif
        ClampPlayerPosition();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 moveDirection = new Vector2(touchPosition.x - transform.position.x, 0).normalized;

                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void HandleKeyboardAndMouseInput()
    {
        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(moveInput * moveSpeed * Time.deltaTime, 0));

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 moveDirection = new Vector2(mousePosition.x - transform.position.x, 0).normalized;

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void ClampPlayerPosition()
    {
        float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        Vector2 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -cameraWidth + screenLimitBuffer, cameraWidth - screenLimitBuffer);
        transform.position = clampedPosition;
    }
}
