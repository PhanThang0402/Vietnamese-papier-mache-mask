using UnityEngine;
using UnityEngine.UI; // Thêm thư viện UI

public class ButtonHandler : MonoBehaviour
{
    public GameObject cube;
    public Button vbutton; // Sử dụng UnityEngine.UI.Button

    void Start()
    {
        cube.SetActive(true);
    }
}