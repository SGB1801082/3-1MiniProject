using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceanLoad : MonoBehaviour
{
    public void DebuginScean()
    {
        GameMgr.single.IsGameLoad(true);
        SceneManager.LoadScene("Town");

        GameUiMgr.single.Receptionist_1();
        Debug.Log("Run Method: Recep_1");
    }
}
