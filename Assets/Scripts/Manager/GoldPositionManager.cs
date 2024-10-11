using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GoldPositionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    void Update()
    {
        gameObject.transform.localPosition = new Vector3(-40 + goldText.text.Length * -18, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
    }
}
