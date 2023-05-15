using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float minXClimap;
    public float maxXClamp;
    public float minYClimap;
    public float maxYClamp;

    private void LateUpdate()
    {
        Vector3 cameraPos;

        cameraPos = transform.position;
        cameraPos.x = Mathf.Clamp(player.transform.position.x, minXClimap, maxXClamp);
        cameraPos.y = Mathf.Clamp(player.transform.position.y, minYClimap, maxYClamp);

        transform.position = cameraPos;
    }
}
