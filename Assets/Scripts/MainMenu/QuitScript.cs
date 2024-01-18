using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScript : MonoBehaviour
{

    [SerializeField] private GameObject QuitPage;
    private void Update()
    {
        if (!QuitPage.activeSelf && this.gameObject.activeSelf)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    QuitPage.SetActive(true);

                    return;
                }
            }
        }
    }
}
