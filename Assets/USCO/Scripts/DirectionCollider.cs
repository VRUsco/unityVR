using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirectionCollider : MonoBehaviour
{
    public string Street;
    public string Career;
    [SerializeField] private TMP_Text StreetUI;
    [SerializeField] private TMP_Text CareerUI;

    private void OnTriggerEnter(Collider obj)
    {
        SaveManager save = FindObjectOfType<SaveManager>();

        if (obj.tag == "Player")
        {
            GameManager.Instance.StreetUI.text = Street;
            GameManager.Instance.CareerUI.text = Career;
            //save.updateDirection(direction);
        }
    }
}
