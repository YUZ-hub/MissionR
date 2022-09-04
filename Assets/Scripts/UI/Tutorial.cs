using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Dialogue tutorial1;
    [SerializeField] private Dialogue tutorial2;
    [SerializeField] private Dialogue tutorial3;
    [SerializeField] private Dialogue tutorial4;
    [SerializeField] private GameObject supply;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameEvent endTutorial;

    private void Start()
    {
        StartCoroutine(TutorialSquence());
    }
    IEnumerator TutorialSquence()
    {
        yield return new WaitForSeconds(1f);
        DialogueController.Instance.Play(tutorial1);
        while (DialogueController.Instance.IsPlaying)
        {
            yield return null;
        }
        supply.SetActive(true);
        DialogueController.Instance.Play(tutorial2);
        while (DialogueController.Instance.IsPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(7f);
        DialogueController.Instance.Play(tutorial3);
        while (DialogueController.Instance.IsPlaying)
        {
            yield return null;
        }
        boss.SetActive(true);
        DialogueController.Instance.Play(tutorial4);
        while (DialogueController.Instance.IsPlaying)
        {
            yield return null;
        }
        endTutorial.Raise();
    }
}
