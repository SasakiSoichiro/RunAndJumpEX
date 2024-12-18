using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class kyori : MonoBehaviour
{
    private jamp janmpkyori;
    public GameObject KyoriText;

    // Start is called before the first frame update
    void Start()
    {
        SetText();
        janmpkyori = GetComponent<jamp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetText()
    {
        KyoriText.GetComponent<TextMeshProUGUI>().text = janmpkyori.jumpDistance.ToString();
    }
}
