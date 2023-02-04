using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEffectController : MonoBehaviour
{
    [SerializeField] GameObject player;//プレイヤー
    [SerializeField] GameObject goalEffectPrefab;//ゴールエフェクト
    [SerializeField] GameObject goalEffectPool;//goalEffectオブジェクト格納
    [SerializeField] GameObject cameraController;//カメラコントローラー
    [SerializeField] int goalEffectActiveQuantity;//ゴールエフェクトの生成数
    [SerializeField] float effectIntervalPlus;

    private GameObject goalEffect;//ゴールエフェクト代入用
    private float effectInterval;
    private List<GameObject> listOfgoalEffect = new List<GameObject>();//goalEffectを格納する用


    private void Start()
    {
        GoalEffectInstantiate();
    }

    private void FixedUpdate()
    {
        GoalEffectActive();
    }

    /// <summary>
    /// goalEffectを生成
    /// </summary>
    private void GoalEffectInstantiate()
    {
        for (int i = 0; i < goalEffectActiveQuantity; i++)
        {
            goalEffect = Instantiate(goalEffectPrefab, transform.position, Quaternion.identity, goalEffectPool.transform);
            goalEffect.SetActive(false);
            listOfgoalEffect.Add(goalEffect);
        }
    }

    /// <summary>
    /// 0から順に非アクティブならアクティブにする処理
    /// </summary>
    /// <returns></returns>
    private GameObject GetGoalEffect()
    {
        for (int i = 0; i < listOfgoalEffect.Count; i++)
        {
            if (listOfgoalEffect[i].activeInHierarchy == false)
                return listOfgoalEffect[i];
        }

        return null;
    }

    /// <summary>
    /// GoalEffectの発動設定
    /// </summary>
    private void GoalEffectSetting()
    {
        goalEffect = GetGoalEffect();

        float x = Random.Range(-30, 30);
        float y = Random.Range(5, 20);
        float z = Random.Range(-30, 30);

        Vector3 startPosion = new Vector3(x, y, z);

        if (goalEffect == null)
            return;

        goalEffect.transform.position = player.transform.position + startPosion;
        goalEffect.transform.rotation = player.transform.rotation;

        goalEffect.SetActive(true);
    }

    /// <summary>
    /// ameraControllerのgameClearBoolがtrueで作動する
    /// </summary>
    private void GoalEffectActive()
    {
        if (cameraController.GetComponent<CameraController>().gameClearBool)
        {
            effectInterval += effectIntervalPlus;

            if (effectInterval % 100 <= 0)
            {
                GoalEffectSetting();
            }
        }
        else return;

    }
}
