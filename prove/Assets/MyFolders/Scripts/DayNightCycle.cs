using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    public int _days;                                    // Define naming convenction for the days
    public int _hours;                                   // Define naming convenction for the hours
    public int _minutes;                                 // Define naming convenction for the minutes
    public int _seconds;                                 // Define naming convenction for the seconds
    public float _counter;                                // Define naming convenction for the counter

    public int _years;                                     // Define naming convenction for years counter
    public int _leapYearsCounter;                                 // Define naming convenction for leap years counter
    public int _calendarDays;                              // Define naming convenction for days in the month

    public bool _january;                                    //Defines if we are in the month of January
    public bool _february;                                   //Defines if we are in the month of February
    public bool _march;                                     //Defines if we are in the month of March
    public bool _april;                                    //Defines if we are in the month of April
    public bool _may;                                     //Defines if we are in the month of May
    public bool _june;                                   //Defines if we are in the month of June
    public bool _july;                                   //Defines if we are in the month of July
    public bool _august;                                 //Defines if we are in the month of August
    public bool _september;                              //Defines if we are in the month of September
    public bool _october;                                //Defines if we are in the month of October
    public bool _november;                               //Defines if we are in the month of November
    public bool _december;                               //Defines if we are in the month of December

    public bool _spring;                                  //Defines if we are in Spring
    public bool _summer;                                  //Defines if we are in Summer
    public bool _autumn;                                  //Defines if we are in Autumn
    public bool _winter;                                  //Defines if we are in Winter

    public int _dawnStartTime = 6;                         // Defines down start                            
    public int _dayStartTime = 8;                         // Defines day start   
    public int _duskStartTime = 18;                       // Defines dusk start   
    public int _nightStartTime = 20;                     // Defines night start   

    public float _sunDimTime = 0.01f;                    // Speed at which sun dims
    public float _dawnSunIntensity = 0.5f;              // down sun strenght
    public float _daySunIntensity = 1f;                 // day sun strenght
    public float _duskSunIntensity = 0.25f;             // dusk sun strenght
    public float _nightSunIntensity = 0f;               // night sun strenght

    public float _ambientDimTime = 0.001f;               // Defines the speed at which ambient light is adjusted
    public float _dawnAmbientIntensity = 0.5f;           // Defines the ambient intensity for dawn
    public float _dayAmbientIntensity = 1f;              // Defines the ambient intensity for day
    public float _duskAmbientIntensity = 0.25f;          // Defines the ambient intensity for dusk
    public float _nightAmbientIntensity = 0f;          // Defines the ambient intensity for night

    public float _dawnSkyboxBlendFactor = 0.5f;         // Defines dawn skybox blend value
    public float _daySkyboxBlendFactor = 0.5f;         // Defines day skybox blend value
    public float _duskSkyboxBlendFactor = 0.5f;         // Defines dusk skybox blend value
    public float _nightSkyboxBlendFactor = 0.5f;         // Defines night skybox blend value

    public float _skyboxBlendFactor;                     // Defines the current skybox blend value
    public float _skyboxBlendSpeed = 0.01f;              // Defines speed at which the skybox will blend
    public int _guiWidth = 100;                          // Defines GUI label width;
    public int _guiHeight = 20;                          // Defines GUI label height;

    public DayPhases _dayPhases;                        // Defines naming convection for the phases in the day

    public enum DayPhases                               // enum for day phases
    {
        Dawn,Day,Dusk,Night
    }

    void Awake()
    {
        _dayPhases = DayPhases.Night;                                   // Set up day phase on night on start up
        RenderSettings.ambientIntensity = _nightAmbientIntensity;       // Render settings is equal to night on start up
        GetComponent<Light>().intensity = _nightSunIntensity;           // Set sun intensity to night on start up
    }

    // Use this for initialization
    void Start ()
    {
        StartCoroutine("TimeOfDayFiniteStateMachine");      // Starts TimeOfDayFiniteStateMachine on start up
        
        _hours = 5;                                          // Set up hours to 5 on start up
        _minutes = 59;                                       // Set up minutes to 59 on start up
        _counter = 59;                                       // Set up counter to 59 on start up
        _days = 1;                                           // Set up days to 1 on start up
        _calendarDays = 1;                                   // Calendar days equal to 1 on start up
        _october = true;                                     // Starts in the month of October on start up
        _autumn = true;                                     // Starts in the season of Autumn on start up
        _years = 2016;                                      // Year is equal to 2016 on start up
        _leapYearsCounter = 4;                              // Leap year counter is equal to 4 on start up
    }
	
	// Update is called once per frame
	void Update ()
    {
        SecondsCounter();                                    //Starts SecondCounter function
        UpdateSkybox();                                      //Calls UpdateSkybox function
    }

    IEnumerator TimeOfDayFiniteStateMachine()
    {
        while(true)
        {
            switch(_dayPhases)            {
                case DayPhases.Dawn:
                    Dawn();
                    break;
                case DayPhases.Day:
                    Day();
                    break;
                case DayPhases.Dusk:
                    Dusk();
                    break;
                case DayPhases.Night:
                    Night();
                    break;
            }
            yield return null;
        }
    }

    void SecondsCounter()
    {
        Debug.Log("SecondsCounter");

        if (_counter == 60)            // If the counter is equals to 60
            _counter = 0;             // than make the counter equals to 0

        _counter += Time.deltaTime;   // counter plus time sync tp PC speed
        _seconds = (int)_counter;     // seconds equals to counter cast to an int

        if (_counter < 60)            // If the counter is less then 60
            return;                   // then do nothing and return

        if (_counter > 60)            // if counter is greater then 60
            _counter = 60;            // then make counter equals to 60

        if (_counter == 60)            // If the counter is equals to 60
            MinutesCounter();          // then call MinutesCounter function
    }

    void MinutesCounter()
    {
        Debug.Log("MinutesCounter");

        _minutes++;                   // Increase minutes counter

        if(_minutes == 60)            // If minutes counter is equal to 60
        {
            HoursCounter();           // call HoursCounter function
            _minutes = 0;             // and then make minutes equal zero
        }
    }

    void HoursCounter()
    {
        Debug.Log("HoursCounter");

        _hours++;                 // Increase hours counter

        if (_hours == 24)         // If hours counter is equal to 60
        {
            DaysCounter();       // call HoursCounter function
            _hours = 0;         // and then make hours equal zero
        }
    }

    void DaysCounter()
    {
        Debug.Log("DaysCounter");

        _days++;                 // Increase hours counter
    }

    void UpdateCalendarMonth()
    {
        Debug.Log("UpdateCalendarMonth");

        if (_january == true && _calendarDays > 31)           // If we are in January and calendar days is greater than 31
        {
            _january = false;                                // then set January to false
            _february = true;                                // and set February to true
            _calendarDays = 1;                               // and make callendar day equal to 1 (the first day of February)
        }

        if (_leapYearsCounter == 4 &&                         // If leap year counter is equal to 4
            _february == true && _calendarDays > 29)        // and February is equal to true and calendar days is greater than 29
        {
            _february = false;                                // then set February to false
            _march = true;                                   // and set March to true
            _calendarDays = 1;                               // and make callendar day equal to 1 (the first day of March)

            SeasonManager();                                 // Calls SeasonManager function
        }

        if (_leapYearsCounter < 4 &&                         // If leap year counter is less than 4
           _february == true && _calendarDays > 28)          // and February is equal to true and calendar days is greater than 28
        {
            _february = false;                                // then set February to false
            _march = true;                                   // and set March to true
            _calendarDays = 1;                               // and make callendar day equal to 1 (the first day of March

            SeasonManager();                                 // Calls SeasonManager function
        }

        if (_march == true && _calendarDays > 31)            // If we are in March and calendar days is greater than 31
        {
            _march = false;                                  // then set March to false
            _april = true;                                   // and set April to true
            _calendarDays = 1;                               // and make callendar day equal to 1 (the first day of April)
        }

        if (_april == true && _calendarDays > 30)            // If we are in April and calendar days is greater than 30
        {
            _april = false;                                  // then set April to false
            _may = true;                                     // and set May to true
            _calendarDays = 1;                               // and make calendar day equal to 1 (the first day of May)
        }

        if (_may == true && _calendarDays > 31)              // If we are in May and calendar days is greater than 31
        {
            _may = false;                                   // then set March to false
            _june = true;                                   // and set June to true
            _calendarDays = 1;                              // and make calendar day equal to 1 (the first day of June)

            SeasonManager();                                 // Calls SeasonManagerfunction
        }

        if (_june == true && _calendarDays > 30)            // If we are in June and calendar days is greater than 30
        {
            _june = false;                                   // then set June to false
            _july = true;                                   // and set July to true
            _calendarDays = 1;                              // and make calendar day equal to 1 (the first day of July)
        }

        if (_july == true && _calendarDays > 31)             // If we are in July and calendar days is greater than 31
        {
            _july = false;                                   // then set July to false
            _august = true;                                  // and set August to true
            _calendarDays = 1;                               // and make calendar day equal to 1 (the first day of August)
        }

        if (_august == true && _calendarDays > 31)           // If we are in August and calendar days is greater than 31
        {
            _august = false;                                 // then set August to false
            _september = true;                               // and set September to true
            _calendarDays = 1;                               // and make calendar day equal to 1 (the first day of September)

            SeasonManager();                                 // Calls SeasonManagerfunction
        }

        if (_september == true && _calendarDays > 30)        // If we are in September and calendar days is greater than 30
        {
            _september = false;                              // then set September to false
            _october = true;                                 // and set October to true
            _calendarDays = 1;                               // and make calendar day equal to 1 (the first day of October)
        }

        if (_october == true && _calendarDays > 31)          // If we are in October and calendar days is greater than 31
        {
            _october = false;                                // then set October to false
            _november = true;                                // and set November to true
            _calendarDays = 1;                               // and make calendar day equal to 1 (the first day of November)
        }

        if (_november == true && _calendarDays > 30)        // If we are in November and calendar days is greater than 30
        {
            _november = false;                              // then set November to false
            _december = true;                               // and set December to true
            _calendarDays = 1;                              // and make calendar day equal to 1 (the first day of December)

            SeasonManager();                                 // Calls SeasonManagerfunction
        }

        if (_december == true && _calendarDays > 31)         // If we are in December and calendar days is greater than 31
        {
            _december = false;                               // then set December to false
            _january = true;                                 // and set January to true
            _calendarDays = 1;                               // and make calendar day equal to 1 (the first day of January)
        }

        YearCounter();                                      // Calls YearCounter function
    }

    void YearCounter()
    {
        Debug.Log("YearCounter");

        _years++;                               // Increase years
        _leapYearsCounter++;                    // Increase leap years

        if (_leapYearsCounter > 4)               // If leap year counter is greater than 4
            _leapYearsCounter = 1;               // then leap year counter is equal to 1
    }


    void SeasonManager()
    {
        Debug.Log("SeasonManager");

        _spring = false;                         // Set spring to be equal to false
        _summer = false;                         // Set summer to be equal to false
        _autumn = false;                         // Set autumn to be equal to false
        _winter = false;                         // Set winter to be equal to false

        if (_march == true && _calendarDays == 1)  // If we are in March and calendar days is 1
            _spring = true;                        // then set Spring to true

        if (_june == true && _calendarDays == 1)   // If we are in June and calendar days is 1
            _summer = true;                        // then set Summer to true

        if (_september == true && _calendarDays == 1)  // If we are in September and calendar days is 1
            _autumn = true;                           // then set Autumn to true

        if (_december == true && _calendarDays == 1)  // If we are in December and calendar days is 1
            _winter = true;                           // then set Winter to true
    }

    void Dawn()
    {
        Debug.Log("Dawn");

        DawnSunLightManager();                                                        // Call DawnSunLightManbager function

        DawnLightAmbientManager();                                                    // Call DawnLightAmbientManager function

        if (-_hours == _dayStartTime && _hours < _duskStartTime)
        {
            _dayPhases = DayPhases.Day;                        // Set up day phase on day
        }
    }

    void DawnSunLightManager()
    {
        Debug.Log("DawnSunLightManbager");

        if (GetComponent<Light>().intensity == _dawnSunIntensity)                        // If light intensity is equal to dawn intensity
            return;                                                                      // then do nothing and return

        if (GetComponent<Light>().intensity < _dawnSunIntensity)                        // If sun intensity is less than dawn
            GetComponent<Light>().intensity += _sunDimTime * Time.deltaTime;            // then increase sun intensity by the sun dim time

        if (GetComponent<Light>().intensity > _dawnSunIntensity)                        // If sun intensity is greater than dawn
            GetComponent<Light>().intensity = _dawnSunIntensity;                        // then make intensity equal to dawn
    }

    void DawnLightAmbientManager()
    {
        Debug.Log("DawnLightAmbientManager");

        if (RenderSettings.ambientIntensity == _dayAmbientIntensity)                      // If ambient intensity is equal to dawn ambient intensity
            return;                                                                      // then do nothing and return

        if (RenderSettings.ambientIntensity < _dawnAmbientIntensity)                   // If ambient intensity is less than dawn
            RenderSettings.ambientIntensity += _ambientDimTime * Time.deltaTime;       // then increase ambient intensity by the ambient dim time

        if (RenderSettings.ambientIntensity > _dawnAmbientIntensity)                   // If ambient intensity is greater than dawn
            RenderSettings.ambientIntensity = _dawnAmbientIntensity;                   // then make ambient intensity equal to dawn
    }

    void Day()
    {
        Debug.Log("Day");

        DaySunLightManager();                                           // Call DaySunLightManager function
        DayLightAmbientManager();                                       // Call DayLightAmbientManager function

        if (-_hours == _duskStartTime && _hours < _nightStartTime)
        {
            _dayPhases = DayPhases.Dusk;                        // Set up day phase on dusk
        }
    }

    void DaySunLightManager()
    {
        Debug.Log("DaySunLightManager");

        if (GetComponent<Light>().intensity == _daySunIntensity)                        // If light intensity is equal to day intensity
            return;                                                                      // then do nothing and return

        if (GetComponent<Light>().intensity < _dawnSunIntensity)                        // If sun intensity is less than dawn
            GetComponent<Light>().intensity += _sunDimTime * Time.deltaTime;       // then increase sun intensity by the sun dim time

        if (GetComponent<Light>().intensity > _dawnSunIntensity)                        // If sun intensity is greater than dawn
            GetComponent<Light>().intensity = _dawnSunIntensity;                        // then make intensity equal to dawn
    } 

    void DayLightAmbientManager()
    {
        Debug.Log("DayLightAmbientManager");

        if (RenderSettings.ambientIntensity == _dayAmbientIntensity)                      // If ambient intensity is equal to day ambient intensity
            return;                                                                      // then do nothing and return

        if (RenderSettings.ambientIntensity < _dayAmbientIntensity)                   // If ambient intensity is less than day
            RenderSettings.ambientIntensity += _ambientDimTime * Time.deltaTime;      // then increase ambient intensity by the ambient dim time

        if (RenderSettings.ambientIntensity > _dayAmbientIntensity)                   // If ambient intensity is greater than day
            RenderSettings.ambientIntensity = _dayAmbientIntensity;                   // then make ambient intensity equal to day
    }

    void Dusk()
    {
        Debug.Log("Dusk");

        DuskSunLightManager();                                    // Call DuskSunLightManager function
        DuskLightAmbientManager();                                // Call DuskLightAmbientManager function

        if (-_hours == _nightStartTime)
        {
            _dayPhases = DayPhases.Night;                        // Set up day phase on night
        }
    }

    void DuskSunLightManager()
    {
        Debug.Log("DuskSunLightManager");

        if (GetComponent<Light>().intensity == _duskSunIntensity)                        // If light intensity is equal to dusk intensity
            return;                                                                      // then do nothing and return

        if (GetComponent<Light>().intensity > _duskSunIntensity)                        // If sun intensity is grater than dusk
            GetComponent<Light>().intensity -= _sunDimTime * Time.deltaTime;           // then decrease sun intensity by the sun dim time

        if (GetComponent<Light>().intensity < _duskSunIntensity)                        // If sun intensity is less than dusk
            GetComponent<Light>().intensity = _duskSunIntensity;                        // then make intensity equal to dusk
    }

    void DuskLightAmbientManager()
    {
        Debug.Log("DayLightAmbientManager");

        if (RenderSettings.ambientIntensity == _duskAmbientIntensity)                    // If ambient intensity is equal to dusk ambient intensity
            return;                                                                      // then do nothing and return

        if (RenderSettings.ambientIntensity > _duskAmbientIntensity)                   // If ambient intensity is gtreater than dusk
            RenderSettings.ambientIntensity -= _ambientDimTime * Time.deltaTime;      // then decrease ambient intensity by the ambient dim time

        if (RenderSettings.ambientIntensity < _duskAmbientIntensity)                   // If ambient intensity is less than dusk
            RenderSettings.ambientIntensity = _duskAmbientIntensity;                   // then make ambient intensity equal to dusk
    }

    void Night()
    {
        Debug.Log("Night");

        NightSunLightManager();                                   // Call NightSunLightManager function
        NightLightAmbientManager();                               // Call NightLightAmbientManager function

        if (-_hours == _dawnStartTime && _hours < _duskStartTime)
        {
            _dayPhases = DayPhases.Dawn;                        // Set up day phase on dawn
        }
    }

    void NightSunLightManager()
    {
        Debug.Log("NightSunLightManager");

        if (GetComponent<Light>().intensity == _nightSunIntensity)                        // If light intensity is equal to night intensity
            return;                                                                      // then do nothing and return

        if (GetComponent<Light>().intensity > _nightSunIntensity)                        // If sun intensity is grater than night
            GetComponent<Light>().intensity -= _sunDimTime * Time.deltaTime;           // then decrease sun intensity by the sun dim time

        if (GetComponent<Light>().intensity < _nightSunIntensity)                        // If sun intensity is less than night
            GetComponent<Light>().intensity = _nightSunIntensity;                        // then make intensity equal to night
    }

    void NightLightAmbientManager()
    {
        Debug.Log("NightLightAmbientManager");

        if (RenderSettings.ambientIntensity == _duskAmbientIntensity)                    // If ambient intensity is equal to night ambient intensity
            return;                                                                      // then do nothing and return

        if (RenderSettings.ambientIntensity < _nightAmbientIntensity)                   // If ambient intensity is less than night
            RenderSettings.ambientIntensity += _ambientDimTime * Time.deltaTime;      // then increase ambient intensity by the ambient dim time

        if (RenderSettings.ambientIntensity > _nightAmbientIntensity)                   // If ambient intensity is greater than night
            RenderSettings.ambientIntensity = _nightAmbientIntensity;                   // then make ambient intensity equal to night
    }

    void OnGUI()
    {
        // Create GUI Label to display numbers of days
        GUI.Label(new Rect(Screen.width -50, 5, _guiWidth, _guiHeight), "Day " + _days);

        // If minutes is less than 10 dysplay our clock with extra zero
        if(_minutes < 10)
        {
            GUI.Label(new Rect(Screen.width - 50, 25, _guiWidth, _guiHeight), _hours + ":"  + 0 + _minutes + ":" + _seconds);
        }
        // else just display our clock
        else
            GUI.Label(new Rect(Screen.width - 50, 25, _guiWidth, _guiHeight), _hours + ":" + _minutes + ":" + _seconds );
    }

    private void UpdateSkybox()
    {
        Debug.Log("UpdateSkybox");

        if(_dayPhases == DayPhases.Dawn)             // If day phase is equal to dawn
        {
            if (_skyboxBlendFactor == _dawnSkyboxBlendFactor)  // If skybox blend value is equal to dawn
                return;                                        // then do nothing and return

            _skyboxBlendFactor += _skyboxBlendSpeed * Time.deltaTime;   // Increase skybox blend by blend speed

            if (_skyboxBlendFactor > _dawnSkyboxBlendFactor)  // If skybox blend value is greater to dawn
                _skyboxBlendFactor = _dawnSkyboxBlendFactor;  // then make skybox blend factor equal to dawn
        }

        if (_dayPhases == DayPhases.Day)             // If day phase is equal to day
        {
            if (_skyboxBlendFactor == _daySkyboxBlendFactor)  // If skybox blend value is equal to day
                return;                                        // then do nothing and return

            _skyboxBlendFactor += _skyboxBlendSpeed * Time.deltaTime;   // Increase skybox blend by blend speed

            if (_skyboxBlendFactor > _daySkyboxBlendFactor)  // If skybox blend value is greater to day
                _skyboxBlendFactor = _daySkyboxBlendFactor;  // then make skybox blend factor equal to day
        }

        if (_dayPhases == DayPhases.Dusk)             // If day phase is equal to dusk
        {
            if (_skyboxBlendFactor == _duskSkyboxBlendFactor)  // If skybox blend value is equal to dusk
                return;                                        // then do nothing and return

            _skyboxBlendFactor -= _skyboxBlendSpeed * Time.deltaTime;   // Decrease skybox blend by blend speed

            if (_skyboxBlendFactor < _duskSkyboxBlendFactor)  // If skybox blend value is less to day
                _skyboxBlendFactor = _duskSkyboxBlendFactor;  // then make skybox blend factor equal to day
        }

        if (_dayPhases == DayPhases.Night)             // If day phase is equal to night
        {
            if (_skyboxBlendFactor == _nightSkyboxBlendFactor)  // If skybox blend value is equal to night
                return;                                        // then do nothing and return

            _skyboxBlendFactor -= _skyboxBlendSpeed * Time.deltaTime;   // Decrease skybox blend by blend speed

            if (_skyboxBlendFactor < _nightSkyboxBlendFactor)  // If skybox blend value is less to night
                _skyboxBlendFactor = _nightSkyboxBlendFactor;  // then make skybox blend factor equal to night
        }

        RenderSettings.skybox.SetFloat("_Blend", _skyboxBlendFactor);  // Get render for skybox and set float for the blend
    }
}
