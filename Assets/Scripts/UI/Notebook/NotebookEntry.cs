using System;

[Serializable]
public class NotebookEntry
{
    public string person;
    public string place;
    public string eventDescription;
    public DateTime date;

    public bool IsComplete =>
        !string.IsNullOrEmpty(person) &&
        !string.IsNullOrEmpty(place) &&
        !string.IsNullOrEmpty(eventDescription);
}
