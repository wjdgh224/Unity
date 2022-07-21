using UnityEngine;
using Cinemachine;

public class RPGCameraManager : MonoBehaviour
{
    public static RPGCameraManager sharedInstance = null;
    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    void Awake() { //RPGGM에서 public RPGCM변수에 인스턴스를 넣으면 자동으로 만들어 진다. 그 후 PRGGM에서 맴버 사용.
        if(sharedInstance != null && sharedInstance != this) {
            Destroy(gameObject);
        }
        else {
            sharedInstance = this;
        }

        GameObject vCamGameObject = GameObject.FindWithTag("VirtualCamera");

        virtualCamera = vCamGameObject.GetComponent<CinemachineVirtualCamera>();
    }
}
