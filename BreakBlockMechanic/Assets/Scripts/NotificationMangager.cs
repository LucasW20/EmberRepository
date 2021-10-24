using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationMangager : MonoBehaviour
{
    public static NotificationMangager instance;


    public static NotificationMangager Instance {
        get {
            if (instance != null) {
                return instance;
            }

            instance = FindObjectOfType<NotificationMangager>();

            if (instance != null) {
                return instance;
            }

            CreateNewInstance();

            return instance;
        }
    }

    private static NotificationMangager CreateNewInstance() {
        NotificationMangager notificationManagerPrefab = Resources.Load<NotificationMangager>("NotificationManager");
        instance = Instantiate(notificationManagerPrefab);

        return instance;
    }

    private void Awake() {
        if (Instance != this) {
            Destroy(gameObject);
        }
    }

    [SerializeField] private Text notificationText;
    [SerializeField] private float fadeTime;

    private IEnumerator notificationCoroutine;
    public void SetNewNotification(string message) {
        if (notificationCoroutine != null) {
            StopCoroutine(notificationCoroutine);
        }

        notificationCoroutine = FadeOutNotification(message);
        StartCoroutine(notificationCoroutine);
    }

    private IEnumerator FadeOutNotification(string message) {
        notificationText.text = message;
        float time = 0;

        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;
            notificationText.color = new Color(notificationText.color.r, notificationText.color.g, notificationText.color.b, Mathf.Lerp(1f, 0f, time / fadeTime));
            yield return null;
        }
    }
}
