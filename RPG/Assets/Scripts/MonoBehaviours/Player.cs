using System.Collections;
using UnityEngine;

public class Player : Character
{
    public HitPoints hitPoints;

    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public Inventory inventoryPrefab;
    Inventory inventory;

    public void Start() {
        

    }

    public override void ResetCharacter() {
        inventory = Instantiate(inventoryPrefab); //프리펩 생성하면 화면에 나옴

        hitPoints.value = startingHitPoints; //현재 체력
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;//HealthBar에 있는 Player characer로 현재 객체... 시작 체력, 최대 체력 정보 설정된 상태로...
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("CanBePickedUp")) {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item; //코인, 하트 객체 가져오기(충동 판별)

            if(hitObject != null) {
                bool shouldDisappear = false;

                switch(hitObject.itemType) {
                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.AddItem(hitObject);

                        shouldDisappear = true;
                        break;
                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }
                if(shouldDisappear) {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    public bool AdjustHitPoints(int amount) { //최대 체력 제한
        if(hitPoints.value < maxHitPoints) {
            hitPoints.value = hitPoints.value + amount;
        
            print("Adjusted HP by: " + amount + ". New value: " + hitPoints.value);

            return true;
        }
        return false;
    }

    public override IEnumerator DamageCharacter(int damage, float interval) {
        while(true) {
            StartCoroutine(FlickerCharacter());
            hitPoints.value = hitPoints.value - damage;

            if(hitPoints.value <= float.Epsilon) {
                KillCharacter();
                break;
            }

            if(interval > float.Epsilon) {
                yield return new WaitForSeconds(interval);
            }
            else {
                break;
            }
        }
    }

    public override void KillCharacter() {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }

    private void OnEnable() {
        ResetCharacter();   
    }
}
