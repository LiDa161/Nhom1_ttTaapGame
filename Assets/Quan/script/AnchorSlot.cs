using UnityEngine;

public class AnchorSlot : MonoBehaviour
{
    private GameObject currentCharacter;

    public void AssignCharacter(GameObject character)
    {
        currentCharacter = character;
    }

    public void ClearCharacter()
    {
        currentCharacter = null;
    }

    public GameObject GetCharacter()
    {
        return currentCharacter;
    }

    public bool HasCharacter()
    {
        return currentCharacter != null;
    }
}
