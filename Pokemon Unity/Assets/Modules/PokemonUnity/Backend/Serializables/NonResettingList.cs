/// Source: Pokémon Unity Redux
/// Purpose: Non-resetting overword events to be saved for Pokémon Unity backend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
[System.Serializable]
public class NonResettingList
{
    public string sceneName;

    public bool[] sceneTrainers;
    public bool[] sceneItems;
    public bool[] sceneEvents;

    public NonResettingList(string sceneName, bool[] sceneTrainers, bool[] sceneItems, bool[] sceneEvents)
    {
        this.sceneName = sceneName;
        this.sceneTrainers = sceneTrainers;
        this.sceneItems = sceneItems;
        this.sceneEvents = sceneEvents;
    }
}