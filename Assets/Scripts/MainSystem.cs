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
        if (0 == GameObject.FindGameObjectsWithTag("Circle").Count()) {
            var x = Random.Range(-8, 8);
            var y = Random.Range(-4, 4);
            Instantiate(_circle, new Vector3(x, y, 0), Quaternion.identity);
        }
	}
}
