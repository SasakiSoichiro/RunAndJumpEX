using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoroller : MonoBehaviour
{
    public Transform player;  // 追う対象のプレイヤー
    public float smoothSpeed = 0.125f;  // スムーズに追う速度
    public Vector3 offset;  // プレイヤーとの相対的な位置

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate() // ここに書きます
    {
        // プレイヤーの位置にオフセットを加えた位置を求める
        Vector3 desiredPosition = player.position + offset;

        // 現在のカメラ位置と目標位置を滑らかに補間
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // カメラの位置を更新
        transform.position = smoothedPosition;
    }
}
