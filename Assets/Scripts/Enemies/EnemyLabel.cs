using UnityEngine;
using TMPro;

public class EnemyLabel : MonoBehaviour
{
    [Header("Label Settings")]
    public string enemyLabel = "[MALWARE.EXE]";
    public Color labelColor = Color.red;
    public float floatHeight = 1f;

    private GameObject labelObj;
    private TextMeshPro labelText;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

        // Create a world space text object above enemy
        labelObj = new GameObject("EnemyLabel");
        labelObj.transform.SetParent(transform);
        labelObj.transform.localPosition = new Vector3(0, floatHeight, 0);

        labelText = labelObj.AddComponent<TextMeshPro>();
        labelText.text = enemyLabel;
        labelText.fontSize = 2f;
        labelText.color = labelColor;
        labelText.alignment = TextAlignmentOptions.Center;
        labelText.fontStyle = FontStyles.Bold;
    }

    void Update()
    {
        // Always face the camera
        if (mainCam != null)
            labelObj.transform.rotation = mainCam.transform.rotation;
    }
}