using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sensitivity = 2f;

    private void Update()
    {
        // �÷��̾� �̵� ó��
        MovePlayer();

        // ���콺�� ���� �̵� ó��
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

        // ���� ȸ�� (���콺 Y ��)
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float newRotationX = currentRotation.x - mouseY * sensitivity;
        transform.rotation = Quaternion.Euler(newRotationX, currentRotation.y, 0f);

        // ���� ȸ�� (���콺 X ��)
        transform.Rotate(Vector3.up * mouseX * sensitivity);
    }
}
