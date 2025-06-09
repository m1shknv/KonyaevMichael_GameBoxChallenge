using UnityEngine;

public class InteractiveObject : MonoBehaviour, IInteractable
{
    private ReplaceController _replace;
    private LootSpawner _lootSpawner;

    private void Start()
    {
        TryGetComponent(out _replace);
        TryGetComponent(out _lootSpawner);
    }

    public void OnInteract()
    {
        _lootSpawner?.SpawnLoot();  
        _replace.ReplacePrefab();
        AudioManager.Instance?.PlayOpenSound();
    }
}
