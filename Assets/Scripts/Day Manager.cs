using UnityEngine;
using UnityEngine.Events;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    public float DayStartHour => dayStartHour;
    public float DayEndHour => dayEndHour;

    [Header("Game Progress")]
    [SerializeField] private int currentDay = 1;
    [SerializeField] private int maxDays = 14;

    [Header("Season")]
    [SerializeField] private Season currentSeason = Season.Spring;

    [Header("Farm Time")]
    [SerializeField] private float currentHour = 13f;
    [SerializeField] private float farmStartHour = 13f;
    [SerializeField] private float farmEndHour = 27f;

    [SerializeField] private float dayStartHour = 13f;
    [SerializeField] private float dayEndHour = 27f;

    // How many real seconds = one in-game hour
    [SerializeField] private float secondsPerGameHour = 30f;

    public static UnityAction<int> OnDayChanged;

    public int CurrentDay => currentDay;
    public float CurrentHour => currentHour;
    public Season CurrentSeason => currentSeason;

    private bool farmTimeOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (farmTimeOver)
            return;

        currentHour += Time.deltaTime / secondsPerGameHour;

        if (currentHour >= farmEndHour)
        {
            EndFarmTime();
        }
    }

    private void EndFarmTime()
    {
        farmTimeOver = true;
        currentHour = farmEndHour;

        Debug.Log("Farm time is over.");

        EndDay();
    }

    public void StartFarmTime()
    {
        currentHour = farmStartHour;
        farmTimeOver = false;
    }

    public void EndDay()
    {
        if (currentDay >= maxDays)
        {
            EndGame();
            return;
        }

        currentDay++;

        OnDayChanged?.Invoke(currentDay);

        Debug.Log("Day " + currentDay);
        StartFarmTime();
    }

    public void SetSeason(Season season)
    {
        currentSeason = season;
    }

    private void EndGame()
    {
        Debug.Log("The two weeks are over!");
    }
}

public enum Season
{
    Spring,
    Summer,
    Fall,
    Winter
}