using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum WhatHappened
{
    PlayerCaught, EndReached
}

public class HeadsUpDisplayMessages : MonoBehaviour
{
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private float displayedMessageDuration = 1f;
    [SerializeField] private CanvasGroup exitBackgroundCanvasGroup;
    [SerializeField] private CanvasGroup caughtBackgroundCanvasGroup;

    private Dictionary<WhatHappened, CanvasGroup> PossibleMessages;
    private float timer;

    private void Awake()
    {
        PossibleMessages = new Dictionary<WhatHappened, CanvasGroup>() {
            { WhatHappened.PlayerCaught, caughtBackgroundCanvasGroup },
            { WhatHappened.EndReached, exitBackgroundCanvasGroup }
        };
    }

    private void Start()
    {
        LineOfSight.CaughtPlayer += DisplayMessage;
        GameEndingTrigger.EndReached += DisplayMessage;
    }

    private void DisplayMessage(WhatHappened whatHappened)
    {
        CanvasGroup message = PossibleMessages[whatHappened];
        timer += Time.deltaTime;
        StartCoroutine(MessageFadeOut(message));
    }

    private IEnumerator MessageFadeOut(CanvasGroup message)
    {
        message.alpha = timer / fadeOutDuration;
        yield return new WaitForSeconds(fadeOutDuration + displayedMessageDuration);
        SceneManager.LoadScene(0);
        Application.Quit();
    }

}
