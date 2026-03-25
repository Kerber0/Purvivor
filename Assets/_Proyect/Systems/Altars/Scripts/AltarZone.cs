using UnityEngine;

public class AltarZone : MonoBehaviour
{
    [Header("Buff")]
    [SerializeField] private TemporaryBuffData buffData;

    [Header("Activation")]
    [SerializeField] private float activationTime = 3f;
    [SerializeField] private bool oneUseOnly = true;
    [SerializeField] private bool resetProgressOnExit = true;

    private float currentProgress = 0f;
    private bool playerInside = false;
    private bool activated = false;

    private PlayerTemporaryBuffs currentPlayerBuffs;

    private void Update()
    {
        if (activated && oneUseOnly) return;

        if (playerInside && currentPlayerBuffs != null)
        {
            currentProgress += Time.deltaTime;

            if (currentProgress >= activationTime)
            {
                ActivateAltar();
            }
        }
        else
        {
            if (resetProgressOnExit)
            {
                currentProgress = 0f;
            }
            else
            {
                currentProgress = Mathf.Max(0f, currentProgress - Time.deltaTime);
            }
        }
    }

    private void ActivateAltar()
    {
        if (currentPlayerBuffs == null || buffData == null) return;

        currentPlayerBuffs.ApplyTemporaryBuff(buffData);

        activated = true;
        currentProgress = 0f;

        Debug.Log("Altar activado: " + buffData.buffName);

        if (oneUseOnly)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = true;
        currentPlayerBuffs = other.GetComponent<PlayerTemporaryBuffs>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        currentPlayerBuffs = null;

        if (resetProgressOnExit)
        {
            currentProgress = 0f;
        }
    }

    public float GetProgressNormalized()
    {
        if (activationTime <= 0f) return 0f;
        return currentProgress / activationTime;
    }

    public bool IsActivated()
    {
        return activated;
    }
}