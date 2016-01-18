using UnityEngine;
using System.Collections;
using System.Linq;

public class MainSystem : MonoBehaviour {
    public GameObject _circle;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
        // 画面上に表示されている円の個数
        if (0 == CountCircles()) {
            var o = (GameObject)Instantiate(_circle, Vector3.zero, Quaternion.identity);

            var c = o.GetComponent<Circle>();
            // StartCoroutine(c.MoveLeftToRight());
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
}
