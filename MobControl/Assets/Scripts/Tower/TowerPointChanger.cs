using TMPro;
using UnityEngine;

public class TowerPointChanger : MonoSingleton<TowerPointChanger>
{
    private TextMeshPro _textMesh;
    private TextMeshPro textMesh => _textMesh ?? (_textMesh = GetComponent<TextMeshPro>());

    public void ChangeTowerPoint(int towerPoint)
    {
        Debug.Log("im change tower point");
        textMesh.text = towerPoint.ToString();
    }
}
