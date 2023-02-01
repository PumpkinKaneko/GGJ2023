using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSoundManager : MonoBehaviour
{

    [SerializeField] AudioSource titleBGM;//タイトルBGM
    [SerializeField] AudioSource gamePlayBGM;//ゲームシーンBGM
    [SerializeField] AudioSource dangerBGM;//ゲームオーバーの危険ありBGM
    [SerializeField] AudioSource gameOverBGM;//ゲームオーバーBGM
    [SerializeField] AudioSource gameClearBGM;//ゲームクリアBGM


    private string bgm_StageSlect = "";//現在のシーンをテキストで記憶する用

    private void Awake()
    {
        //Awakeで一番最初に呼び出さないとエラーになる
        SoundManagerSystem();
    }

    private void Start()
    {
        //シーンが切り替わった時にシーン毎にBGMを再生/停止する
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    //シーンが切り替わった時に条件に応じてBGMを変更し再生/停止する
    private void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {


        //シーンがどう変わったかで判定

        //ゲームシーンからタイトルシーンへ
        if (bgm_StageSlect != "TitleScene" && nextScene.name == "TitleScene")
        {
            gamePlayBGM.Stop();
            dangerBGM.Stop();
            gameClearBGM.Stop();
            titleBGM.Play();
        }

        //タイトルシーンからゲームシーンへ
        if (bgm_StageSlect != "GameScene" && nextScene.name == "GameScene")
        {
            titleBGM.Stop();
            gamePlayBGM.Play();
        }

        //ゲームシーンリセット時
        if (bgm_StageSlect == "GameScene" && nextScene.name == "GameScene")
        {
            dangerBGM.Stop();
            gameClearBGM.Stop();
            gamePlayBGM.Play();
        }

        //遷移後のシーン名を「１つ前のシーン名」として保持
        bgm_StageSlect = nextScene.name;
    }

    /// <summary>
    /// シーン遷移時にFindで呼び出しているオブジェクトを破棄させない/1以上存在もさせない
    /// </summary>
    private void SoundManagerSystem()
    {
        int numMusicPlayers = FindObjectsOfType<MainSoundManager>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// デンジャーBGMをONに
    /// </summary>
    public void DangerBGMActive()
    {
        gamePlayBGM.Stop();
        dangerBGM.Play();
    }

    /// <summary>
    /// ゲームオーバーBGMをONに
    /// </summary>
    public void GameOverBGMActive()
    {
        gamePlayBGM.Stop();
        dangerBGM.Stop();
        gameOverBGM.Play();
    }

    /// <summary>
    /// ゲームクリアBGMをONに
    /// </summary>
    public void GameClearBGMActive()
    {
        gamePlayBGM.Stop();
        dangerBGM.Stop();
        gameClearBGM.Play();
    }
}

