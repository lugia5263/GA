using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sensitivity = 2f;

    private void Update()
    {
        // 플레이어 이동 처리
        MovePlayer();

        // 마우스로 시점 이동 처리
        RotateCamera();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 수직 회전 (마우스 Y 축)
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float newRotationX = currentRotation.x - mouseY * sensitivity;
        transform.rotation = Quaternion.Euler(newRotationX, currentRotation.y, 0f);

        // 수평 회전 (마우스 X 축)
        transform.Rotate(Vector3.up * mouseX * sensitivity);
    }
}
