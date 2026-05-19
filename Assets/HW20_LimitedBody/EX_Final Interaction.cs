using UnityEngine;
using UnityEngine.InputSystem;

public class EX_FinalInteraction : MonoBehaviour
{
    private Camera mainCamera;
    private Renderer objectRenderer;

    // Tap
    private bool firstTap = false;

    // Drag
    private bool isDragging = false;
    private float zDistance;

    // Lerp
    private Vector3 targetPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
        objectRenderer = GetComponent<Renderer>();

        targetPosition = transform.position;
    }

    void Update()
    {
        var pointer = Pointer.current;

        if (pointer == null) return;

        // =========================
        // Tap + Drag 시작
        // =========================
        if (pointer.press.wasPressedThisFrame)
        {
            Vector2 screenPos =
                pointer.position.ReadValue();

            Ray ray =
                mainCamera.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    // 첫 탭 시 색 변경
                    if (!firstTap)
                    {
                        objectRenderer.material.color = Color.red;

                        firstTap = true;
                    }

                    // Drag 시작
                    isDragging = true;

                    zDistance =
                        mainCamera.WorldToScreenPoint(
                            transform.position
                        ).z;
                }
            }
        }

        // =========================
        // Drag 종료
        // =========================
        if (pointer.press.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        // =========================
        // Drag 중
        // =========================
        if (isDragging)
        {
            Vector2 screenPos =
                pointer.position.ReadValue();

            Vector3 mousePoint =
                new Vector3(
                    screenPos.x,
                    screenPos.y,
                    zDistance
                );

            Vector3 worldPos =
                mainCamera.ScreenToWorldPoint(mousePoint);

            // Y축 낮게 고정
            targetPosition =
                new Vector3(
                    worldPos.x,
                    -0.05f,
                    worldPos.z
                );
        }

        // =========================
        // Lerp 이동
        // =========================
        transform.position =
            Vector3.Lerp(
                transform.position,
                targetPosition,
                Time.deltaTime * 5f
            );
    }
}