using UnityEngine;
using UnityEngine.UIElements;
public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    private VisualElement m_HealthBar;

    //called as soon as the object the script is attached to is created
    private void Awake()
    {
        instance = this; //ref to the current instance of the UIHandler class executing the Awake fnc
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets UI Document component that's on the same UI Document GameObject as this script
        UIDocument uiDocument = GetComponent<UIDocument>();
        /*
            - The root of the UI Hierarchy is accessed through rootVisualElement.
            - As you’ve encountered previously, the VisualElement is accessed using the dot operator.
            - The Q function is short for Query. You can use this function to find a particular VisualElement in 
            the Hierarchy window that matches multiple search parameters — in this case you are using the element name.
            - Query is a generic function, because you can use it to query lots of different types. 
            The specific type you are looking for is provided in angle brackets — in this case, 
            it’s a VisualElement named “HealthBar”.
         */
        m_HealthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthBar(1.0f);
    }

    public void SetHealthBar(float percentage)
    {
        m_HealthBar.style.width = Length.Percent(100 * percentage);
    }
    
}
