using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;

    [HideInInspector]
    public Player character;//Player로 부터 받은 객체로 초기화 되어 있음
    public Image meterImage;
    public Text hpText;
    float maxHitPoints;

    void Start()
    {
        maxHitPoints = character.maxHitPoints;

    }

    void Update() //체력바 업데이트, HitPoints를 서로 참조하기 때문에 실시간으로 공유하는 효과
    {
        if(character != null) {
            meterImage.fillAmount = hitPoints.value / maxHitPoints;

            hpText.text = "HP:" + (meterImage.fillAmount * 100);
        }
    }
}
