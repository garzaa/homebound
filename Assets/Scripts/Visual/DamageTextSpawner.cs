using UnityEngine;
using UnityEngine.UI;

public class DamageTextSpawner : MonoBehaviour {

    public GameObject textPrefab;
    
    Canvas canvas;
    static DamageTextSpawner dt;
    static GameObject staticTextPrefab;

    void Start() {
        dt = this;
        canvas = dt.GetComponent<Canvas>();
        staticTextPrefab = textPrefab;
    }

    public static void WriteText(string text, Vector2 position) {
        Instantiate(staticTextPrefab, position, Quaternion.identity, dt.transform).GetComponentInChildren<Text>().text = text;
    }

}