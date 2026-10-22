using UnityEngine;


public class MainCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform mainCharacter;
    void LateUpdate()
    {
        Vector3 cameraPosition = new Vector3(mainCharacter.position.x, 15, mainCharacter.position.z - 6);
        transform.position = cameraPosition;
    }
}
