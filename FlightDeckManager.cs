using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class FlightDeckManager : MonoBehaviour {
    // KeywordRecognizer object.
    KeywordRecognizer keywordRecognizer;

    // Defines which function to call when a keyword is recognized.
    delegate void KeywordAction(PhraseRecognizedEventArgs args);
    Dictionary<string, KeywordAction> keywordCollection;

    private GameObject throttle;
    private GameObject flightDeck;

    // Use this for initialization
    void Start () {
        keywordCollection = new Dictionary<string, KeywordAction>();

        // Add keyword to start manipulation.
        keywordCollection.Add("Set Throttle 100", SetThrottleHigh);
        keywordCollection.Add("Set Throttle 0", SetThrottleLow);
        keywordCollection.Add("Hide Flight Deck", HideFlightDeck);
        keywordCollection.Add("Show Flight Deck", ShowFlightDeck);
        
        // Initialize KeywordRecognizer with the previously added keywords.
        keywordRecognizer = new KeywordRecognizer(keywordCollection.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
        Debug.Log("Started Keyword Recognizer");

        throttle = GameObject.FindGameObjectWithTag("throttle");
        flightDeck = GameObject.FindGameObjectWithTag("FlightDeck");
    }

    private void SetThrottleHigh(PhraseRecognizedEventArgs args)
    {
        float speed = 0.1f;
        throttle.transform.localRotation = Quaternion.Slerp(throttle.transform.rotation, Quaternion.Euler(new Vector3(90F, 0, 0)), Time.time * speed);
    }

    private void SetThrottleLow(PhraseRecognizedEventArgs args)
    {
        float speed = 0.1f;
        throttle.transform.localRotation = Quaternion.Slerp(throttle.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.time * speed);
    }

    private void HideFlightDeck(PhraseRecognizedEventArgs args)
    {
        flightDeck.SetActive(false);
    }

    private void ShowFlightDeck(PhraseRecognizedEventArgs args)
    {
        flightDeck.SetActive(true);
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction keywordAction;

        if (keywordCollection.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke(args);
        }
    }
}
