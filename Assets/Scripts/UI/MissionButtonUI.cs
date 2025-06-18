using UnityEngine;
using UnityEngine.UI;

public class MissionButtonUI : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject slidingHighlight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        slidingHighlight.SetActive(!slidingHighlight.activeSelf);
        PlayerController.Instance.autoMode = slidingHighlight.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
