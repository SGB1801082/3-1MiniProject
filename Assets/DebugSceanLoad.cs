using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceanLoad : MonoBehaviour
{
    public void DebuginScean()
    {
        GameMgr.single.IsGameLoad(true);
        SceneManager.LoadScene("Town");

        GameUiMgr.single.TutorialDungeonClear();
    }
}
