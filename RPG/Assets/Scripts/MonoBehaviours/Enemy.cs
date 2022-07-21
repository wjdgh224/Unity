using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    float hitPoints;
    public int damageStrength;
    Coroutine damageCoroutine;

    public override IEnumerator DamageCharacter(int damage, float interval) {
        while(true) {
            StartCoroutine(FlickerCharacter());
            hitPoints = hitPoints - damage;

            if(hitPoints <= float.Epsilon) {
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

    public override void ResetCharacter() {
        hitPoints = startingHitPoints;
    }

    private void OnEnable() {
        ResetCharacter();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            Player player = collision.gameObject.GetComponent<Player>();

            if(damageCoroutine == null) {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            if(damageCoroutine != null) {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
}
