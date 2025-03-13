using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform coinTextContainer;
    [SerializeField] private float duration;
    [SerializeField] private Ease animationCurve;

    private float containerInitPositon;
    private float moveAmount;
    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("0");
        toUpdate.SetText("0");
        containerInitPositon = coinTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }

    public void UpdateScore(int score)
    {
        // set score to masked text UI
        toUpdate.SetText($"{score}");
        // add .SetEase(animationCurve) lets us select different
        // animation curves to the dotween animation
        coinTextContainer.DOLocalMoveY(containerInitPositon + moveAmount, duration).SetEase(animationCurve);

        StartCoroutine(ResetCoinContainer(score));
    }

    private IEnumerator ResetCoinContainer(int score)
    {
        // tells editor to wait a given period of time
        yield return new WaitForSeconds(duration);
        // use duration since that's the same time as the animation
        current.SetText($"{score}"); // update original score
        Vector3 localPosition = coinTextContainer.localPosition;
        coinTextContainer.localPosition = new Vector3(localPosition.x, containerInitPositon, localPosition.z);
        // then reset y-localPosition of coinTextContainer
    }
}
