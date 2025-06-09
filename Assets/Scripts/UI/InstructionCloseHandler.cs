using UnityEngine;

public class InstructionCloseHandler : MonoBehaviour
{
    [SerializeField] private LocationTimer locationTimer;

    public void OnCloseInstruction()
    {
        locationTimer?.StartTimer();
        gameObject.SetActive(false); 
    }
}
