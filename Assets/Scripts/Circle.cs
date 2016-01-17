using UnityEngine;
using UnityEngine.EventSystems;

public class Circle : MonoBehaviour, IPointerClickHandler {

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.clickCount == 1) {
            // クリックされたら自身を消す
            Destroy(gameObject);
        }
    }
}
