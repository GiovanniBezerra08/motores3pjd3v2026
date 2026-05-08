using System.Collections;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    private IEnumerator Start()
    {
        // 2segundos
        yield return new WaitForSeconds(2f);

        // menuprincipal
        GameManager.Instance.LoadScene("MenuPrincipal");
    }
}