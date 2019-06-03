using UnityEngine;
using UnityEngine.UI;

public class DamageTextSpawner : MonoBehaviour {

    public GameObject textPrefab;
    public GameObject critTextPrefab;
    
    Canvas canvas;
    static DamageTextSpawner dt;
    void Start() {
        dt = this;
        canvas = dt.GetComponent<Canvas>();
    }

    public static void WriteText(string text, Vector2 position) {
        Instantiate(dt.textPrefab, position, Quaternion.identity, dt.transform).GetComponentInChildren<Text>().text = text;
    }

    public static void WriteCrit(string text, Vector2 position) {
        Instantiate(dt.critTextPrefab, position, Quaternion.identity, dt.transform).GetComponentInChildren<Text>().text = text;
    }

}