using UnityEngine;

public class UIController : MonoBehaviour
{
    public void switchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if (_menu != null)
            _menu.SetActive(true);
    }
}
