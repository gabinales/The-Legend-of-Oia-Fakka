using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void mudaCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }
}
