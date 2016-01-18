using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
// using System.Linq;

public class Circle : MonoBehaviour, IPointerClickHandler {
    private bool _enable = true;

	// Use this for initialization
	void Start () {
        _enable = true;
	}

	// Update is called once per frame
	void Update () {

	}

    private Vector3 GetRandomPosition() {
        var x = Random.Range(-8, 8);
        var y = Random.Range(-4, 4);
        return new Vector3(x, y, 0);
    }

    // ランダムな位置に表示。その場に留まる
    private IEnumerator Stay() {
        float duration = 3;

        transform.position = GetRandomPosition();
        yield return new WaitForSeconds(duration);
        Die();
    }

    // 画面左から右へ移動する
    private IEnumerator MoveLeftToRight() {
        float duration = 5; // duration 秒で画面を横切る

        float elapsedTime = 0;
        var startPos = new Vector3(-8, Random.Range(-4, 4), 0);
        transform.position = startPos;
        while (true) {
            if (!_enable) yield break;

            elapsedTime += Time.deltaTime;
            var pos = transform.position;
            pos.x = Mathf.Lerp(startPos.x, startPos.x + 16, elapsedTime / duration);
            transform.position = pos;

            if (elapsedTime >= duration) {
                Die();
                yield break;
            }
            yield return null;
        }
    }

    public void MoveStrategy() {
        if (5 <= Random.Range(0, 9)) {
            StartCoroutine(Stay());
        }
        else {
            StartCoroutine(MoveLeftToRight());
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.clickCount == 1) {
            // クリックされたら自身を消す
            Die();
        }
    }

    private void Die() {
        if (_enable) {
            _enable = false;
            Destroy(gameObject);
        }
    }
}
