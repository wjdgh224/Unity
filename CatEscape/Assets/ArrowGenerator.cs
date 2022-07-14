using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;//설계도
    float span = 0.5f;
    float delta = 0;

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if(this.delta > this.span) {
            this.delta = 0;
            GameObject go = Instantiate(arrowPrefab) as GameObject;//객체(인스턴스) 생성, Object -> GameObject
            int px = Random.Range(-13, 14);
            go.transform.position = new Vector3(px, 7, 0);
        }
    }
}
