using UnityEngine;
using TMPro;

public class EffectController : MonoBehaviour
{
    [SerializeField] private GameObject _fragmentation;
    [SerializeField] private Transform _effectsParentTrnsf;

    [Header("UI")]
    [SerializeField] private GameObject _pointTextParent;

    public void CandyFragmentationEffect(GameObject dot)
    {
        GameObject _effect = Instantiate(_fragmentation, dot.transform.position, Quaternion.identity);
        _effect.transform.parent = _effectsParentTrnsf;
        ParticleSystem.MainModule mainModule = _effect.GetComponent<ParticleSystem>().main;
        mainModule.startColor = GetColor(dot.tag);
        Destroy(_effect, 1.5f);
    }

    private Color GetColor(string tag)
    {
        Color newColor;
        if (tag == "RedCandy")
            newColor = new Color(0.941f, 0.141f, 0.361f, 1f);
        else if (tag == "YellowCandy")
            newColor = new Color(0.988f, 0.725f, 0.192f, 1f);
        else if (tag == "BlueCandy")
            newColor = new Color(0.059f, 0.588f, 1f, 1f);
        else if (tag == "BrownCandy")
            newColor = new Color(0.733f, 0.349f, 0.094f, 1f);
        else if (tag == "GreenCandy")
            newColor = new Color(0.376f, 0.686f, 0.094f, 1f);
        else if (tag == "PinkCandy")
            newColor = new Color(0.941f, 0.141f, 0.361f, 1f);
        else if (tag == "PurpleCandy")
            newColor = new Color(0.663f, 0.247f, 0.878f, 1f);
        else
            newColor = new Color(1f, 1f, 1f, 1f);

        return newColor;
    }

    public void PointTextEffect(int point, GameObject dot)
    {
        GameObject _pointTextPrefab = Instantiate(_pointTextParent, dot.transform.position, Quaternion.identity);
        
        _pointTextPrefab.transform.SetParent(_effectsParentTrnsf);
        _pointTextPrefab.GetComponentInChildren<TMP_Text>().text = "+" + point.ToString();
        Destroy(_pointTextPrefab.gameObject, 1.5f);
    }
}
