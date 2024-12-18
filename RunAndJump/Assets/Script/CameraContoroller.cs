using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoroller : MonoBehaviour
{
    public Transform player;  // �ǂ��Ώۂ̃v���C���[
    public float smoothSpeed = 0.125f;  // �X���[�Y�ɒǂ����x
    public Vector3 offset;  // �v���C���[�Ƃ̑��ΓI�Ȉʒu

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate() // �����ɏ����܂�
    {
        // �v���C���[�̈ʒu�ɃI�t�Z�b�g���������ʒu�����߂�
        Vector3 desiredPosition = player.position + offset;

        // ���݂̃J�����ʒu�ƖڕW�ʒu�����炩�ɕ��
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // �J�����̈ʒu���X�V
        transform.position = smoothedPosition;
    }
}
