using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class Circle : MonoBehaviour, IPointerClickHandler {
    private bool _enable = true;

    private Action<int> _touchedCallback;

	// Use this for initialization
	void Start () {
        _enable = true;
	}

	// Update is called once per frame
	void Update () {

	}

    public void SetTouchedCallback(Action<int> callback) {
        _touchedCallback = callback;
    }

    private bool OutOfCamera() {
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1) return true;
        return false;
    }

    private Vector3 GetRandomPosition() {
        var x = UnityEngine.Random.Range(-8, 8);
        var y = UnityEngine.Random.Range(-4, 4);
        return new Vector3(x, y, 0);
    }

    // ランダムな位置に表示。その場に留まる
    private IEnumerator Stay() {
        float duration = 3;

        transform.position = GetRandomPosition();
        yield return new WaitForSeconds(duration);
        Die();
    }

    // 垂直方向に移動する
    private IEnumerator MoveVertical() {
        float duration = 3;

        float elapsedTime = 0;
        var startPos = new Vector3(UnityEngine.Random.Range(-8, 8), 4, 0);
        transform.position = startPos;
        while (_enable) {
            elapsedTime += Time.deltaTime;
            var pos = transform.position;
            pos.y = Mathf.Lerp(startPos.y, startPos.y - 8, elapsedTime / duration);
            transform.position = pos;

            if (elapsedTime >= duration) break;
            yield return null;
        }
        Die();
    }

    // 画面左から右へ移動する
    private IEnumerator MoveHorizontal() {
        float duration = 5; // duration 秒で画面を横切る

        float elapsedTime = 0;
        var startPos = new Vector3(-8, UnityEngine.Random.Range(-4, 4), 0);
        transform.position = startPos;
        while (_enable) {
            elapsedTime += Time.deltaTime;
            var pos = transform.position;
            pos.x = Mathf.Lerp(startPos.x, startPos.x + 16, elapsedTime / duration);
            transform.position = pos;

            if (elapsedTime >= duration) break;
            yield return null;
        }
    }

    // 円周起動でぐるぐるまわりながら水平移動
    private IEnumerator MoveHorizontalWithRevolution() {
        float elapsedTime = 0;
        var pos = new Vector3(-8, 0, 0); // 回転の中心
        float r = 1.3f; // 円の半径
        var speed = 3f; // 移動スピード
        while (_enable) {
            elapsedTime += Time.deltaTime;

            // t 秒で一回転する
            var t = 2f;
            var a = 2 * Mathf.PI * elapsedTime / t;
            var rx = r * Mathf.Sin(a);
            var ry = r * Mathf.Cos(a);

            pos.x += speed * Time.deltaTime;
            transform.position = new Vector3(pos.x + rx, pos.y + ry, 0);

            if (OutOfCamera()) break;
            yield return null;
        }
        Die();
    }

    // TODO:画面上から物理演算で落下

    public void MoveStrategy() {
        Func<IEnumerator>[] xs = {
            Stay, MoveVertical, MoveHorizontal, MoveHorizontalWithRevolution,
        };
        Util.Shuffle(xs);
        StartCoroutine(xs[0]());
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.clickCount == 1) {
            // クリックされたら自身を消す
            int score = 1;
            if (_touchedCallback != null) _touchedCallback(score);
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
