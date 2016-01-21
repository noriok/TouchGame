using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class MainSystem : MonoBehaviour {
    public GameObject _circle;

    private int _score = 0;

	// Use this for initialization
	void Start () {
        UpdateScore();
	}

	// Update is called once per frame
	void Update () {
        // 画面上に表示されている円の個数
        if (0 == CountCircles()) {
            var o = (GameObject)Instantiate(_circle, Vector3.zero, Quaternion.identity);

            var c = o.GetComponent<Circle>();
            c.SetTouchedCallback(AddScore);
            c.MoveStrategy();
        }
	}

    private int CountCircles() {
        return GameObject.FindGameObjectsWithTag("Circle").Count();
    }

    void OnGUI() {
        int n = CountCircles();
        GUILayout.Label("circle: " + n);
    }

    public void AddScore(int score) {
        _score += score;
        UpdateScore();
    }

    public void UpdateScore() {
        var o = GameObject.Find("Canvas/Text");
        if (o != null) {
            var t = o.GetComponent<Text>();
            t.text = "Score: " + _score;
        }
    }
}
