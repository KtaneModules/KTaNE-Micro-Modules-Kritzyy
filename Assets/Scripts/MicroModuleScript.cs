using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using System.Text.RegularExpressions;

public class MicroModuleScript : MonoBehaviour
{
    public KMSelectable KeypadTL, KeypadTR, KeypadBL, KeypadBR; //Keypads
    public KMSelectable MorseKey0, MorseKey1, MorseKey2, MorseKey3, MorseKey4, MorseKey5, MorseKey6, MorseKey7, MorseKey8, MorseKey9, MorseKeySend, MorseKeyReceive; //Morse
    public KMSelectable Wire1Sel, Wire2Sel, Wire3Sel, Wire4Sel, Wire5Sel, Wire6Sel; //Wires
    public KMSelectable Password1Next, Password2Next, Password3Next, PasswordSubmit; //Passwords
    public KMSelectable ModuleSubmit, ResetBtn; //General

    public KMBombInfo BombInfo;

    //The ACTUAL module ID
    static int moduleIdCounter = 1;
    int ModuleID;

    //General related
    //Module
    public KMBombModule ThisModule;
        
     //Serial Number
    public string SerialNr = "XXXXX0";
    List<string> PossibleSerialCharacters = new List<string>
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
    };
    int SerialGen;
    public TextMesh SerialNumberTxtMsh;

    //Indicators
    public TextMesh Indicator1Label, Indicator2Label, Indicator3Label;
    public Renderer Indicator1Light, Indicator2Light, Indicator3Light;
    List<string> PossibleIndicators = new List<string>
    {
        "NULL", "EXPL", "INDC", "BOMB", "BOOM", "MINI", "MDLE", "XXXX", "BLNK"
    };
    public List<string> IndicatorsOnBomb;
    public List<Renderer> AllIndicators;
    string Indicator1, Indicator2, Indicator3;
    int IndicatorGen1, IndicatorGen2, IndicatorGen3;
    bool Indc1Lit, Indc2Lit, Indc3Lit;

    //Battery
    public Renderer Battery;
    public Texture BlackBattery, RedBattery, OrangeBattery, YellowBattery, GreenBattery, BlueBattery, PurpleBattery, PinkBattery;
    public string BatteryColor;
    int BatteryColorGen;

    //Module ID
    public TextMesh KeypadsMicroModuleIDTxt, MorseMicroModuleIDTxt, WireMicroModuleIDTxt, PasswordMicroModuleIDTxt;
    public string MicroModuleIDKeypads, MicroModuleIDMorse, MicroModuleIDWires, MicroModuleIDPassword;
    List<string> PossibleModuleIDs = new List<string>
    {
        "1", "2", "3", "4"
    };
    public List<TextMesh> MicroModuleIDList;

    //Status Light related
    public StatusLight KeypadStatusLight, MorseStatusLight, WireStatusLight, PasswordStatusLight, ResetStatusLight;


    //Keypad related
    public Renderer KeypadTLArrow, KeypadTRArrow, KeypadBLArrow, KeypadBRArrow;
    public Renderer KeypadLedTL, KeypadLedTR, KeypadLedBL, KeypadLedBR;
    public Texture ArrowLeft, ArrowRight, ArrowReverse, ArrowClockwise, ArrowSwap, ArrowDiagonalTL, ArrowDiagonalTR, ArrowDiagonalBL, ArrowDiagonalBR;
    public Texture KeypadLedWrong, KeypadLedCorrect, KeypadLedOff;
    public List<Renderer> TotalKeys;
    public List<Renderer> AllArrows = new List<Renderer>
    {

    };
    int KeyArrowGenerator;
    string DiagonalSide;
    public int BaseNr;
    int Offset;
    public string[] ArrowKind;
    public Renderer[] ArrowOrder;
    public string[] Directions;
    int KeyNr = 0;
    public bool Key1Fake, Key2Fake, Key3Fake, Key4Fake;

    //Morse related
    public Renderer MorseLight;
    public TextMesh MorseCodeTxtMsh;
    int MorseDigitGenerator;
    string MorseDigitCode;
    string MorseCode;
    public int MorseIntCode;
    bool MorseReceivingCode;
    string MorseEnteredCode;
    int MorseCharacters;
    bool CodeGenerated = false;

    string DesiredKey;

    //Password related
    public TextMesh PasswordChar1, PasswordChar2, PasswordChar3;
    public TextMesh ExpressionText;
    List<string> PasswordPossibleLetters = new List<string>
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };
    List<string> PasswordPossibleNumbers = new List<string>
    {
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
    };

    public List<char> CharactersList1;
    public List<char> CharactersList2;
    public List<char> CharactersList3;

    char PasswordCharacter1, PasswordCharacter2, PasswordCharacter3;
    string PasswordLetters = "XXX";
    int PasswordLetterGen, PasswordNumberGen;
    int ActionNumber1 = 10, ActionNumber2 = 10, ActionNumber3 = 10;
    public int CharacterA, CharacterB, CharacterC;
    string Expression;
    public int EnteredPasswordDigit1, EnteredPasswordDigit2, EnteredPasswordDigit3;
    public int SolutionPasswordDigit1, SolutionPasswordDigit2, SolutionPasswordDigit3;

    public int ExpressionSolution;

    //Wires related
    public TextMesh ScriptLine1, ScriptLine3, ScriptLine5;
    public Renderer Wire1ColUp, Wire2ColUp, Wire3ColUp, Wire4ColUp, Wire5ColUp, Wire6ColUp, Wire1ColDown, Wire2ColDown, Wire3ColDown, Wire4ColDown, Wire5ColDown, Wire6ColDown;
    public GameObject Wire1UpObj, Wire2UpObj, Wire3UpObj, Wire4UpObj, Wire5UpObj, Wire6UpObj, Wire1DownObj, Wire2DownObj, Wire3DownObj, Wire4DownObj, Wire5DownObj, Wire6DownObj; //Wires
    List<string> PossibleRendererNames = new List<string>
    {
        "BOMB", "EXPL", "MINI", "NULL", "BOB", "MSA", "SIG", "TRN", "DVID", "Parallel", "SRCA", "RJ45", "WireCount", "AllWires", "WireRenderer", "CurrentWire"
    };
    public bool[] DesiredCuts;
    public bool[] CurrentCuts;
    public List<string> ColorList;
    string RendererName;
    string WireCol1, WireCol2, WireCol3, WireCol4, WireCol5, WireCol6;
    public bool WireShouldBeCut1, WireShouldBeCut2, WireShouldBeCut3, WireShouldBeCut4, WireShouldBeCut5, WireShouldBeCut6;
    bool WireWasCut1, WireWasCut2, WireWasCut3, WireWasCut4, WireWasCut5, WireWasCut6;

    //Solve module related
    public List<string> DesiredSolveOrder;
    public List<string> CurrentSolveOrder;

    public Renderer Alarm;
    public GameObject AlarmLightCTRL, SolveAlarmLight;
    public GameObject AlarmSolved, AlarmGameObj;
    public Light AlarmLight1, AlarmLight2, AlarmPointLight;
    public TextMesh ModulesLeftText, CurrentStrikesText;
    public TextMesh[] SolveOrderDisplay;
    float Light1Rot, Light2Rot;
    int ModulesLeft = 4;
    int StrikeCount;
    int CurrentSolve = 0;
    int SolveOffset;
    bool Unicorn;

    //Reset button related
    int ResetTimer, ResetTimeCounter = 20;
    public TextMesh ResetCooldownText;


    void Awake()
    {
        ModuleID = moduleIdCounter++;

        KeypadTL.OnInteract = KeypadTLButton;
        KeypadTR.OnInteract = KeypadTRButton;
        KeypadBL.OnInteract = KeypadBLButton;
        KeypadBR.OnInteract = KeypadBRButton;

        MorseKeySend.OnInteract = MorseSubmitKey;
        MorseKeyReceive.OnInteract = MorseReceiveCode;
        MorseKey0.OnInteract = MorseKey0Press;
        MorseKey1.OnInteract = MorseKey1Press;
        MorseKey2.OnInteract = MorseKey2Press;
        MorseKey3.OnInteract = MorseKey3Press;
        MorseKey4.OnInteract = MorseKey4Press;
        MorseKey5.OnInteract = MorseKey5Press;
        MorseKey6.OnInteract = MorseKey6Press;
        MorseKey7.OnInteract = MorseKey7Press;
        MorseKey8.OnInteract = MorseKey8Press;
        MorseKey9.OnInteract = MorseKey9Press;

        Password1Next.OnInteract = PasswordChar1Next;
        Password2Next.OnInteract = PasswordChar2Next;
        Password3Next.OnInteract = PasswordChar3Next;
        PasswordSubmit.OnInteract = PasswordSubmision;

        Wire1Sel.OnInteract = CuttingWire1;
        Wire2Sel.OnInteract = CuttingWire2;
        Wire3Sel.OnInteract = CuttingWire3;
        Wire4Sel.OnInteract = CuttingWire4;
        Wire5Sel.OnInteract = CuttingWire5;
        Wire6Sel.OnInteract = CuttingWire6;

        ModuleSubmit.OnInteract = BombSolveButton;
        ResetBtn.OnInteract = Reset;

        Button1.OnInteract = Redacted;
        Button2.OnInteract = Nothing;
        Chip.OnInteract = HandleClick;

      
    }

    void Start()
    {
        foreach (char Character in SerialNr)
        {
            if (Character == 'X')
            {
                SerialGen = Random.Range(0, 26);
                SerialNr += PossibleSerialCharacters[SerialGen];
            }
            else
            {
                SerialGen = Random.Range(26, 35);
                SerialNr += PossibleSerialCharacters[SerialGen];
            }
        }
        SerialNr = SerialNr.Remove(0, 6);
        SerialNumberTxtMsh.text = SerialNr;
        Debug.LogFormat("[Micro-Modules #{0}] The serial number is {1}", ModuleID, SerialNr);

        IndicatorGen1 = Random.Range(0, 9);
        IndicatorGen2 = Random.Range(0, 8);
        IndicatorGen3 = Random.Range(0, 7);

        Indicator1 = PossibleIndicators[IndicatorGen1];
        PossibleIndicators.Remove(Indicator1);

        Indicator2 = PossibleIndicators[IndicatorGen2];
        PossibleIndicators.Remove(Indicator2);

        Indicator3 = PossibleIndicators[IndicatorGen3];
        PossibleIndicators.Remove(Indicator3);

        Indicator1Label.text = Indicator1;
        Indicator2Label.text = Indicator2;
        Indicator3Label.text = Indicator3;

        IndicatorsOnBomb.Add(Indicator1);
        IndicatorsOnBomb.Add(Indicator2);
        IndicatorsOnBomb.Add(Indicator3);

        AllIndicators.Add(Indicator1Light);
        AllIndicators.Add(Indicator2Light);
        AllIndicators.Add(Indicator3Light);

        foreach (Renderer Light in AllIndicators)
        {
            int LightGen = Random.Range(0, 2);
            switch (LightGen)
            {
                case 0:
                    {
                        Light.material.color = Color.white;
                        if (Light == Indicator1Light)
                        {
                            Indc1Lit = true;
                        }
                        else if (Light == Indicator2Light)
                        {
                            Indc2Lit = true;
                        }
                        else if (Light == Indicator3Light)
                        {
                            Indc3Lit = true;
                        }
                        break;
                    }
                default:
                    {
                        Light.material.color = new Color32(60, 53, 53, 255);
                        if (Light == Indicator1Light)
                        {
                            Indc1Lit = false;
                        }
                        else if (Light == Indicator2Light)
                        {
                            Indc2Lit = false;
                        }
                        else if (Light == Indicator3Light)
                        {
                            Indc3Lit = false;
                        }
                        break;
                    }
            }
        }
        Debug.LogFormat("[Micro-Modules #{0}] Indicator 1 is {1}", ModuleID, Indc1Lit);
        Debug.LogFormat("[Micro-Modules #{0}] Indicator 2 is {1}", ModuleID, Indc2Lit);
        Debug.LogFormat("[Micro-Modules #{0}] Indicator 3 is {1}", ModuleID, Indc3Lit);
        BatteryColorGen = Random.Range(0, 8);
        switch (BatteryColorGen)
        {
            case 0:
                {
                    BatteryColor = "Black";
                    Battery.material.mainTexture = BlackBattery;
                    break;
                }
            case 1:
                {
                    BatteryColor = "Red";
                    Battery.material.mainTexture = RedBattery;
                    break;
                }
            case 2:
                {
                    BatteryColor = "Orange";
                    Battery.material.mainTexture = OrangeBattery;
                    break;
                }
            case 3:
                {
                    BatteryColor = "Yellow";
                    Battery.material.mainTexture = YellowBattery;
                    break;
                }
            case 4:
                {
                    BatteryColor = "Green";
                    Battery.material.mainTexture = GreenBattery;
                    break;
                }
            case 5:
                {
                    BatteryColor = "Blue";
                    Battery.material.mainTexture = BlueBattery;
                    break;
                }
            case 6:
                {
                    BatteryColor = "Purple";
                    Battery.material.mainTexture = PurpleBattery;
                    break;
                }
            case 7:
                {
                    BatteryColor = "Pink";
                    Battery.material.mainTexture = PinkBattery;
                    break;
                }
        }
        Debug.LogFormat("[Micro-Modules #{0}] The color of the battery is {1}", ModuleID, BatteryColor);
        ModuleIDGenerator();
        KeypadsSetup();
        MorseSetup();
        PasswordSetup();
        WiresSetup();
        
        StartCoroutine("CheckForModulesLeft");
        //StartCoroutine("LightTranslation");
    }

    

    IEnumerator CheckForModulesLeft()
    {
        while (true)
        {
            if (ModulesLeft == 0)
            {
                AlarmLightCTRL.SetActive(true);
                AlarmLight1.enabled = true;
                AlarmLight2.enabled = true;
                Alarm.material.color = new Color32(255, 0, 0, 0);
                ModulesLeftText.color = Color.green;
                StartCoroutine("ContinuousRotation");
            }
            yield return new WaitForSecondsRealtime(0.0001f);
        }
    }
    

    IEnumerator ContinuousRotation()
    {
        for (int x = 0; x < 3; x++)
        {
            if (x == 1)
            {
                AlarmLightCTRL.transform.localEulerAngles = new Vector3(0, Light1Rot, 0);
                Light1Rot = Light1Rot + 7;
            }
            if (x == 2)
            {
                StartCoroutine("ContinuousRotation");
                StopCoroutine("ContinuousRotation");
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    void ModuleIDGenerator()
    {
        int IDGenerator;
        IDGenerator = Random.Range(0, 4);
        MicroModuleIDKeypads = PossibleModuleIDs[IDGenerator];
        KeypadsMicroModuleIDTxt.text = PossibleModuleIDs[IDGenerator];
        PossibleModuleIDs.Remove(MicroModuleIDKeypads);

        IDGenerator = Random.Range(0, 3);
        MicroModuleIDMorse = PossibleModuleIDs[IDGenerator];
        MorseMicroModuleIDTxt.text = PossibleModuleIDs[IDGenerator];
        PossibleModuleIDs.Remove(MicroModuleIDMorse);

        IDGenerator = Random.Range(0, 2);
        MicroModuleIDWires = PossibleModuleIDs[IDGenerator];
        WireMicroModuleIDTxt.text = PossibleModuleIDs[IDGenerator];
        PossibleModuleIDs.Remove(MicroModuleIDWires);

        IDGenerator = Random.Range(0, 1);
        MicroModuleIDPassword = PossibleModuleIDs[IDGenerator];
        PasswordMicroModuleIDTxt.text = PossibleModuleIDs[IDGenerator];
        PossibleModuleIDs.Remove(MicroModuleIDPassword);
        Debug.LogFormat("[Micro-Modules #{0}] Directional Keypads' module ID is {1}, Code Morse's is {2}, Script Wires' is {3}, Math Code's is {4}", ModuleID, MicroModuleIDKeypads, MicroModuleIDMorse, MicroModuleIDWires, MicroModuleIDPassword);
        SolveOrderStartCalculation();
    }


    void SolveOrderStartCalculation()
    {
        if (MorseMicroModuleIDTxt.text == "1" && BombInfo.GetModuleNames().Where((x) => x.Contains("Morse")).Any())
        {
            DesiredSolveOrder.Add("Morse Code");
            SolveOffset = 1;
        }
        else if (KeypadsMicroModuleIDTxt.text == "2" && BombInfo.GetModuleNames().Where((x) => x.Contains("Button")).Any())
        {
            DesiredSolveOrder.Add("Keypads");
            SolveOffset = 2;
        }
        else if (PasswordMicroModuleIDTxt.text == "3")
        {
            if (Indicator1 == "MINI" || Indicator2 == "MINI" || Indicator3 == "MINI" || Indicator1 == "BOMB" || Indicator2 == "BOMB" || Indicator3 == "BOMB")
            {
                DesiredSolveOrder.Add("Passwords");
                SolveOffset = 3;
            }
            else if (WireMicroModuleIDTxt.text == "4")
            {
                if (BatteryColor == "Red" || BatteryColor == "Green" || BatteryColor == "Blue")
                {
                    DesiredSolveOrder.Add("Wires");
                    SolveOffset = 4;
                }

            }
            else if (BombInfo.IsIndicatorOn(Indicator.BOB) || Indicator1 == "BOMB" || Indicator2 == "BOMB" || Indicator3 == "BOMB")
            {
                DesiredSolveOrder.Clear();
                DesiredSolveOrder.Add("Unicorn");
                Unicorn = true;
                return;
            }
            else
            {
                if (MorseMicroModuleIDTxt.text == "1")
                {
                    DesiredSolveOrder.Add("Morse Code");
                }
                else if (KeypadsMicroModuleIDTxt.text == "1")
                {
                    DesiredSolveOrder.Add("Keypads");
                }
                else if (PasswordMicroModuleIDTxt.text == "1")
                {
                    DesiredSolveOrder.Add("Passwords");
                }
                else if (WireMicroModuleIDTxt.text == "1")
                {
                    DesiredSolveOrder.Add("Wires");
                }
                SolveOffset = 1;
            }
        }
        else if (WireMicroModuleIDTxt.text == "4")
        {
            if (BatteryColor == "Red" || BatteryColor == "Green" || BatteryColor == "Blue")
            {
                DesiredSolveOrder.Add("Wires");
                SolveOffset = 4;
            }
            else if (BombInfo.IsIndicatorOn(Indicator.BOB) || Indicator1 == "BOMB" || Indicator2 == "BOMB" || Indicator3 == "BOMB")
            {
                DesiredSolveOrder.Clear();
                DesiredSolveOrder.Add("Unicorn");
                Unicorn = true;
                return;
            }
            else
            {
                if (MorseMicroModuleIDTxt.text == "1")
                {
                    DesiredSolveOrder.Add("Morse Code");
                }
                else if (KeypadsMicroModuleIDTxt.text == "1")
                {
                    DesiredSolveOrder.Add("Keypads");
                }
                else if (PasswordMicroModuleIDTxt.text == "1")
                {
                    DesiredSolveOrder.Add("Passwords");
                }
                else if (WireMicroModuleIDTxt.text == "1")
                {
                    DesiredSolveOrder.Add("Wires");
                }
                SolveOffset = 1;
            }

        }
        else if (BombInfo.IsIndicatorOn(Indicator.BOB) || Indicator1 == "BOMB" || Indicator2 == "BOMB" || Indicator3 == "BOMB")
        {
            Debug.LogFormat("[Micro-Modules #{0}] Unicorn present, so the solve order doesn't matter.", ModuleID);
            DesiredSolveOrder.Clear();
            DesiredSolveOrder.Add("Unicorn");
            Unicorn = true;
            return;
        }
        else
        {
            if (MorseMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Wires");
            }
            SolveOffset = 1;
        }
        SolveOrderFollowingCalculation();
    }

    void SolveOrderFollowingCalculation()
    {
        if (SolveOffset == 1)
        {
            if (MorseMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Wires");
            }

            if (MorseMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Wires");
            }

            if (MorseMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Wires");
            }
        }
        else if (SolveOffset == 2)
        {
            if (MorseMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Wires");
            }

            if (MorseMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Wires");
            }

            if (MorseMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Wires");
            }
        }
        else if (SolveOffset == 3)
        {
            if (MorseMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "4")
            {
                DesiredSolveOrder.Add("Wires");
            }

            if (MorseMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Wires");
            }

            if (MorseMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Wires");
            }
        }
        else if (SolveOffset == 4)
        {
            if (MorseMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "1")
            {
                DesiredSolveOrder.Add("Wires");
            }

            if (MorseMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "2")
            {
                DesiredSolveOrder.Add("Wires");
            }

            if (MorseMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Morse Code");
            }
            else if (KeypadsMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Keypads");
            }
            else if (PasswordMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Passwords");
            }
            else if (WireMicroModuleIDTxt.text == "3")
            {
                DesiredSolveOrder.Add("Wires");
            }
        }
        if (!Unicorn)
        {
            Debug.LogFormat("[Micro-Modules #{0}] The solve order is {1}, {2}, {3} and {4}", ModuleID, DesiredSolveOrder[0], DesiredSolveOrder[1], DesiredSolveOrder[2], DesiredSolveOrder[3]);
        }
    }

    //Solve button
    protected bool BombSolveButton()
    {
        GetComponent<KMSelectable>().AddInteractionPunch();
        if (ModulesLeft == 0)
        {
            if (Enumerable.SequenceEqual(DesiredCuts, CurrentCuts))
            {
                AlarmLight1.gameObject.SetActive(false);
                AlarmLight2.gameObject.SetActive(false);
                AlarmPointLight.color = Color.green;
                AlarmGameObj.SetActive(false);
                SolveAlarmLight.SetActive(true);
                AlarmSolved.SetActive(true);
                GetComponent<KMBombModule>().HandlePass();
            }
            else if (DesiredSolveOrder[1] == "Unicorn")
            {
                AlarmLight1.gameObject.SetActive(false);
                AlarmLight2.gameObject.SetActive(false);
                AlarmPointLight.color = Color.green;
                AlarmGameObj.SetActive(false);
                AlarmLightCTRL.SetActive(true);
                SolveAlarmLight.SetActive(true);
                AlarmSolved.SetActive(true);
                GetComponent<KMBombModule>().HandlePass();
            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        else
        {

        }
        
        return false;
    }

    //Reset Button
    protected bool Reset()
    {
        Debug.LogFormat("[Micro-Modules #{0}] The module was reset.", ModuleID);
        GetComponent<KMSelectable>().AddInteractionPunch();

        //Keypads Reset
        KeypadLedTL.material.mainTexture = KeypadLedOff;
        KeypadLedTR.material.mainTexture = KeypadLedOff;
        KeypadLedBL.material.mainTexture = KeypadLedOff;
        KeypadLedBR.material.mainTexture = KeypadLedOff;
        KeypadTL.OnInteract = KeypadTLButton;
        KeypadTR.OnInteract = KeypadTRButton;
        KeypadBL.OnInteract = KeypadBLButton;
        KeypadBR.OnInteract = KeypadBRButton;

        //Morse Code Reset
        MorseCharacters = 0;
        MorseCodeTxtMsh.text = "____";
        MorseEnteredCode = "";
        MorseKeySend.OnInteract = MorseSubmitKey;
        MorseKeyReceive.OnInteract = MorseReceiveCode;
        MorseKey0.OnInteract = MorseKey0Press;
        MorseKey1.OnInteract = MorseKey1Press;
        MorseKey2.OnInteract = MorseKey2Press;
        MorseKey3.OnInteract = MorseKey3Press;
        MorseKey4.OnInteract = MorseKey4Press;
        MorseKey5.OnInteract = MorseKey5Press;
        MorseKey6.OnInteract = MorseKey6Press;
        MorseKey7.OnInteract = MorseKey7Press;
        MorseKey8.OnInteract = MorseKey8Press;
        MorseKey9.OnInteract = MorseKey9Press;

        //Wires Reset
        WireWasCut1 = false;
        WireWasCut2 = false;
        WireWasCut3 = false;
        WireWasCut4 = false;
        WireWasCut5 = false;
        WireWasCut6 = false;
        Wire1UpObj.transform.localPosition = new Vector3(-0.01298998f, -0.007339936f, 0.01845633f);
        Wire1DownObj.transform.localPosition = new Vector3(0.05786593f, 0.02225007f, 0.04621001f);
        Wire2UpObj.transform.localPosition = new Vector3(-0.01298999f, -0.00734f, 0.005940826f);
        Wire2DownObj.transform.localPosition = new Vector3(0.04177f, 0.02225f, 0.04621f);
        Wire3UpObj.transform.localPosition = new Vector3(-0.01309f, -0.00734f, -0.0058f);
        Wire3DownObj.transform.localPosition = new Vector3(0.02664043f, 0.02216029f, 0.04642617f);
        Wire4UpObj.transform.localPosition = new Vector3(-0.01295f, -0.00734f, -0.01714f);
        Wire4DownObj.transform.localPosition = new Vector3(0.01205628f, 0.02216012f, 0.04596f);
        Wire5UpObj.transform.localPosition = new Vector3(-0.01308f, -0.00734f, -0.02755f);
        Wire5DownObj.transform.localPosition = new Vector3(-0.00133183f, 0.02216f, 0.0465f);
        Wire6UpObj.transform.localPosition = new Vector3(-0.01319199f, -0.00734f, -0.03806f);
        Wire6DownObj.transform.localPosition = new Vector3(-0.01484855f, 0.02216026f, 0.04671f);
        CurrentCuts[0] = false;
        CurrentCuts[1] = false;
        CurrentCuts[2] = false;
        CurrentCuts[3] = false;
        CurrentCuts[4] = false;
        CurrentCuts[5] = false;
        Wire1Sel.OnInteract = CuttingWire1;
        Wire2Sel.OnInteract = CuttingWire2;
        Wire3Sel.OnInteract = CuttingWire3;
        Wire4Sel.OnInteract = CuttingWire4;
        Wire5Sel.OnInteract = CuttingWire5;
        Wire6Sel.OnInteract = CuttingWire6;

        //Passwords Reset
        Password1Next.OnInteract = PasswordChar1Next;
        Password2Next.OnInteract = PasswordChar2Next;
        Password3Next.OnInteract = PasswordChar3Next;
        PasswordSubmit.OnInteract = PasswordSubmision;
        EnteredPasswordDigit1 = 10;
        EnteredPasswordDigit2 = 10;
        EnteredPasswordDigit3 = 10;
        ActionNumber1 = 10;
        ActionNumber2 = 10;
        ActionNumber3 = 10;
        PasswordCharacter1 = CharactersList1[10];
        PasswordCharacter2 = CharactersList2[10];
        PasswordCharacter3 = CharactersList3[10];
        PasswordChar1.color = new Color32(96, 255, 0, 255);
        PasswordChar2.color = new Color32(96, 255, 0, 255);
        PasswordChar3.color = new Color32(96, 255, 0, 255);
        PasswordChar1.text = PasswordCharacter1.ToString();
        PasswordChar2.text = PasswordCharacter2.ToString();
        PasswordChar3.text = PasswordCharacter3.ToString();
        CurrentSolveOrder.Clear();

        //General
        CurrentSolve = 0;
        ModulesLeft = 4;
        ModulesLeftText.text = ModulesLeft.ToString();
        ModulesLeftText.color = Color.red;
        SolveOrderDisplay[0].text = "";
        SolveOrderDisplay[1].text = "";
        SolveOrderDisplay[2].text = "";
        SolveOrderDisplay[3].text = "";
        Alarm.material.color = new Color32(100, 0, 0, 0);
        AlarmLight1.enabled = false;
        AlarmLight2.enabled = false;
        AlarmPointLight.enabled = false;

        //Status Light
        KeypadStatusLight.SetInActive();
        MorseStatusLight.SetInActive();
        WireStatusLight.SetInActive();
        PasswordStatusLight.SetInActive();


        //Cooldown
        ResetTimeCounter = ResetTimeCounter + 10;
        ResetBtn.OnInteract = ResetDeactivated;
        StartCoroutine("ConstantReset");
        return false;
    }

    protected bool ResetDeactivated()
    {
        GetComponent<KMSelectable>().AddInteractionPunch();
        return false;
    }

    IEnumerator ConstantReset()
    {
        ResetTimer = ResetTimeCounter;
        Debug.LogFormat("[Micro-Modules #{0}] Time left until reset is {1}", ModuleID, ResetTimeCounter);
        while (true)
        {
            ResetCooldownText.text = ResetTimer.ToString();
            if (ResetTimer == 0)
            {
                GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                ResetCooldownText.color = Color.green;
                ResetTimer = 0;
                ResetStatusLight.SetInActive();
                ResetBtn.OnInteract = Reset;
                StopCoroutine("ConstantReset");
            }
            else
            {
                ResetCooldownText.color = Color.red;
                ResetStatusLight.FlashStrike();
                ResetTimer--;
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    //Begin of Keypads
    void KeypadsSetup()
    {
        TotalKeys.Add(KeypadTLArrow);
        TotalKeys.Add(KeypadTRArrow);
        TotalKeys.Add(KeypadBLArrow);
        TotalKeys.Add(KeypadBRArrow);

        foreach (Renderer Arrow in TotalKeys)
        {
            KeyArrowGenerator = Random.Range(0, 5);
            switch (KeyArrowGenerator)
            {
                case 0:
                    {
                        if (Arrow == KeypadTLArrow || Arrow == KeypadBLArrow)
                        {
                            Arrow.material.mainTexture = ArrowRight;
                            ArrowKind[KeyNr] = "Right";
                        }
                        else
                        {
                            Arrow.material.mainTexture = ArrowLeft;
                            ArrowKind[KeyNr] = "Left";
                        }
                        break;
                    }
                case 1:
                    {
                        Arrow.material.mainTexture = ArrowReverse;
                        ArrowKind[KeyNr] = "Reverse";
                        break;
                    }
                case 2:
                    {
                        Arrow.material.mainTexture = ArrowClockwise;
                        ArrowKind[KeyNr] = "Clockwise";
                        break;
                    }
                case 3:
                    {
                        Arrow.material.mainTexture = ArrowSwap;
                        ArrowKind[KeyNr] = "Swap";
                        break;
                    }
                case 4:
                    {
                        ArrowKind[KeyNr] = "Diagonal";
                        int DiagonalSideGen = Random.Range(0, 4);
                        switch (DiagonalSideGen)
                        {
                            case 0:
                                {
                                    Arrow.material.mainTexture = ArrowDiagonalTR;
                                    DiagonalSide = "Top Right";
                                    break;
                                }
                            case 1:
                                {
                                    Arrow.material.mainTexture = ArrowDiagonalBR;
                                    DiagonalSide = "Bottom Right";
                                    break;
                                }
                            case 2:
                                {
                                    Arrow.material.mainTexture = ArrowDiagonalBL;
                                    DiagonalSide = "Bottom Left";
                                    break;
                                }
                            case 3:
                                {
                                    Arrow.material.mainTexture = ArrowDiagonalTL;
                                    DiagonalSide = "Top Left";
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
            }
            KeyNr++;
        }
        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top left arrow is a {1}", ModuleID, ArrowKind[0]);
        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top right arrow is a {1}", ModuleID, ArrowKind[1]);
        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom left arrow is a {1}", ModuleID, ArrowKind[2]);
        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom right arrow is a {1}", ModuleID, ArrowKind[3]);
        KeypadOffsetCalculation();
    }

    void KeypadOffsetCalculation()
    {
        string SerialNrNumbers = SerialNr.Last().ToString();
        //Offset first:
        BaseNr = int.Parse(SerialNrNumbers);
        int KeypadCalcModuleID = Convert.ToInt32(KeypadsMicroModuleIDTxt.text);
        BaseNr = BaseNr + KeypadCalcModuleID;
        if (BombInfo.GetOnIndicators().Count() > 0)
        {
            BaseNr = BaseNr * BombInfo.GetOnIndicators().Count();
        }
        StartCoroutine("KeypadSubtraction");
    }

    IEnumerator KeypadSubtraction()
    {
        while (true)
        {
            if (BaseNr >= 5)
            {
                BaseNr = BaseNr - 4;
            }
            else
            {
                Offset = BaseNr;
                if (Offset == 1)
                {
                    Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The offset key is top left", ModuleID);
                }
                else if (Offset == 2)
                {
                    Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The offset key is top right", ModuleID);
                }
                else if (Offset == 3)
                {
                    Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The offset key is bottom left", ModuleID);
                }
                else
                {
                    Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The offset key is bottom right", ModuleID);
                }
                FakeKeypadCalculation();
                StopCoroutine("KeypadSubtraction");
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    void FakeKeypadCalculation()
    {
        foreach (Renderer Arrow in TotalKeys)
        {
            if (Arrow.material.mainTexture == ArrowLeft)
            {
                if (Indc1Lit && Indc2Lit && Indc3Lit)
                {
                    if (Arrow == KeypadTLArrow)
                    {
                        Key1Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadTRArrow)
                    {
                        Key2Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top right key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBLArrow)
                    {
                        Key3Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBRArrow)
                    {
                        Key4Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom right key is fake", ModuleID);
                    }
                }
            }
            else if (Arrow.material.mainTexture == ArrowRight)
            {
                if (IndicatorsOnBomb.Contains("INDC"))
                {
                    if (Arrow == KeypadTLArrow)
                    {
                        Key1Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadTRArrow)
                    {
                        Key2Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top right key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBLArrow)
                    {
                        Key3Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBRArrow)
                    {
                        Key4Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom right key is fake", ModuleID);
                    }
                }
            }
            else if (Arrow.material.mainTexture == ArrowReverse)
            {
                if (SerialNr.Any("AEIOU".Contains) && BombInfo.GetSerialNumberLetters().Any("AEIOU".Contains))
                {
                    if (Arrow == KeypadTLArrow)
                    {
                        Key1Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadTRArrow)
                    {
                        Key2Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top right key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBLArrow)
                    {
                        Key3Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBRArrow)
                    {
                        Key4Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom right key is fake", ModuleID);
                    }
                }
            }
            else if (Arrow.material.mainTexture == ArrowClockwise)
            {
                if (BatteryColor == "Red")
                {
                    if (Arrow == KeypadTLArrow)
                    {
                        Key1Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadTRArrow)
                    {
                        Key2Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top right key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBLArrow)
                    {
                        Key3Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBRArrow)
                    {
                        Key4Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom right key is fake", ModuleID);
                    }
                }
            }
            else if (Arrow.material.mainTexture == ArrowSwap)
            {
                if (KeypadsMicroModuleIDTxt.text == "4")
                {
                    if (Arrow == KeypadTLArrow)
                    {
                        Key1Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadTRArrow)
                    {
                        Key2Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top right key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBLArrow)
                    {
                        Key3Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBRArrow)
                    {
                        Key4Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom right key is fake", ModuleID);
                    }
                }
            }
            else if (Arrow.material.mainTexture == ArrowDiagonalTL || Arrow.material.mainTexture == ArrowDiagonalTR || Arrow.material.mainTexture == ArrowDiagonalBL || Arrow.material.mainTexture == ArrowDiagonalBR)
            {
                if (BombInfo.GetBatteryCount(KModkit.Battery.D) > 2)
                {
                    if (Arrow == KeypadTLArrow)
                    {
                        Key1Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadTRArrow)
                    {
                        Key2Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The top right key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBLArrow)
                    {
                        Key3Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom left key is fake", ModuleID);
                    }
                    else if (Arrow == KeypadBRArrow)
                    {
                        Key4Fake = true;
                        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The bottom right key is fake", ModuleID);
                    }
                }
            }
        }
        KeypadDirectionCalculation();
    }

    void KeypadDirectionCalculation()
    {
        if (Offset == 1)
        {
            ArrowOrder[0] = KeypadTLArrow;
        }
        else if (Offset == 2)
        {
            ArrowOrder[0] = KeypadTRArrow;
        }
        else if (Offset == 3)
        {
            ArrowOrder[0] = KeypadBLArrow;
        }
        else if (Offset == 4)
        {
            ArrowOrder[0] = KeypadBRArrow;
        }


        if (ArrowOrder[0] == KeypadTLArrow)
        {
            if (!Key1Fake)
            {
                if (KeypadTLArrow.material.mainTexture == ArrowRight)
                {
                    Offset = 2;
                    ArrowOrder[1] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 3;
                    ArrowOrder[1] = KeypadBLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 2;
                    ArrowOrder[1] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 4;
                    ArrowOrder[1] = KeypadBRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[1] = KeypadTLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[1] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[1] = KeypadBLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[1] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[0] == KeypadTRArrow)
        {
            if (!Key2Fake)
            {
                if (KeypadTRArrow.material.mainTexture == ArrowLeft)
                {
                    Offset = 1;
                    ArrowOrder[1] = KeypadTLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 4;
                    ArrowOrder[1] = KeypadBRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 4;
                    ArrowOrder[1] = KeypadBRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 3;
                    ArrowOrder[1] = KeypadBLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[1] = KeypadTLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[1] = KeypadTRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[1] = KeypadBLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[1] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[0] == KeypadBLArrow)
        {
            if (!Key3Fake)
            {
                if (KeypadBLArrow.material.mainTexture == ArrowRight)
                {
                    Offset = 4;
                    ArrowOrder[1] = KeypadBRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 1;
                    ArrowOrder[1] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 1;
                    ArrowOrder[1] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 2;
                    ArrowOrder[1] = KeypadTRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[1] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[1] = KeypadTRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[1] = KeypadBLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[1] = KeypadBRArrow;
                }
            }

        }
        else if (ArrowOrder[0] == KeypadBRArrow)
        {
            if (!Key4Fake)
            {
                if (KeypadBRArrow.material.mainTexture == ArrowLeft)
                {
                    Offset = 3;
                    ArrowOrder[1] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 2;
                    ArrowOrder[1] = KeypadTRArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 3;
                    ArrowOrder[1] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 1;
                    ArrowOrder[1] = KeypadTLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[1] = KeypadTLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[1] = KeypadTRArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[1] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[1] = KeypadBRArrow;
                }
            }
        }

        if (ArrowOrder[1] == KeypadTLArrow)
        {
            if (!Key1Fake)
            {
                if (KeypadTLArrow.material.mainTexture == ArrowRight)
                {
                    Offset = 2;
                    ArrowOrder[2] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 3;
                    ArrowOrder[2] = KeypadBLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 2;
                    ArrowOrder[2] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 4;
                    ArrowOrder[2] = KeypadBRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[2] = KeypadTLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[2] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[2] = KeypadBLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[2] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[1] == KeypadTRArrow)
        {
            if (!Key2Fake)
            {
                if (KeypadTRArrow.material.mainTexture == ArrowLeft)
                {
                    Offset = 1;
                    ArrowOrder[2] = KeypadTLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 4;
                    ArrowOrder[2] = KeypadBRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 4;
                    ArrowOrder[2] = KeypadBRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 3;
                    ArrowOrder[2] = KeypadBLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[2] = KeypadTLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[2] = KeypadTRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[2] = KeypadBLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[2] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[1] == KeypadBLArrow)
        {
            if (!Key3Fake)
            {
                if (KeypadBLArrow.material.mainTexture == ArrowRight)
                {
                    Offset = 4;
                    ArrowOrder[2] = KeypadBRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 1;
                    ArrowOrder[2] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 1;
                    ArrowOrder[2] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 2;
                    ArrowOrder[2] = KeypadTRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[2] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[2] = KeypadTRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[2] = KeypadBLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[2] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[1] == KeypadBRArrow)
        {
            if (!Key4Fake)
            {
                if (KeypadBRArrow.material.mainTexture == ArrowLeft)
                {
                    Offset = 3;
                    ArrowOrder[2] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 2;
                    ArrowOrder[2] = KeypadTRArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 3;
                    ArrowOrder[2] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 1;
                    ArrowOrder[2] = KeypadTLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[2] = KeypadTLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[2] = KeypadTRArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[2] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[2] = KeypadBRArrow;
                }
            }
        }

        if (ArrowOrder[2] == KeypadTLArrow)
        {
            if (!Key1Fake)
            {
                if (KeypadTLArrow.material.mainTexture == ArrowRight)
                {
                    Offset = 2;
                    ArrowOrder[3] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 3;
                    ArrowOrder[3] = KeypadBLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 2;
                    ArrowOrder[3] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 4;
                    ArrowOrder[3] = KeypadBRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[3] = KeypadTLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[3] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[3] = KeypadBLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[3] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[2] == KeypadTRArrow)
        {
            if (!Key2Fake)
            {
                if (KeypadTRArrow.material.mainTexture == ArrowLeft)
                {
                    Offset = 1;
                    ArrowOrder[3] = KeypadTLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 4;
                    ArrowOrder[3] = KeypadBRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 4;
                    ArrowOrder[3] = KeypadBRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 3;
                    ArrowOrder[3] = KeypadBLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[3] = KeypadTLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[3] = KeypadTRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[3] = KeypadBLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[3] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[2] == KeypadBLArrow)
        {
            if (!Key3Fake)
            {
                if (KeypadBLArrow.material.mainTexture == ArrowRight)
                {
                    Offset = 4;
                    ArrowOrder[3] = KeypadBRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 1;
                    ArrowOrder[3] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 1;
                    ArrowOrder[3] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 2;
                    ArrowOrder[3] = KeypadTRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[3] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[3] = KeypadTRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[3] = KeypadBLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[3] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[2] == KeypadBRArrow)
        {
            if (!Key4Fake)
            {
                if (KeypadBRArrow.material.mainTexture == ArrowLeft)
                {
                    Offset = 3;
                    ArrowOrder[3] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 2;
                    ArrowOrder[3] = KeypadTRArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 3;
                    ArrowOrder[3] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 1;
                    ArrowOrder[3] = KeypadTLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[3] = KeypadTLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[3] = KeypadTRArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[3] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[3] = KeypadBRArrow;
                }
            }
        }


        if (ArrowOrder[3] == KeypadTLArrow)
        {
            if (!Key1Fake)
            {
                if (KeypadTLArrow.material.mainTexture == ArrowRight)
                {
                    Offset = 2;
                    ArrowOrder[4] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 3;
                    ArrowOrder[4] = KeypadBLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 2;
                    ArrowOrder[4] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 4;
                    ArrowOrder[4] = KeypadBRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[4] = KeypadTLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[4] = KeypadTRArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[4] = KeypadBLArrow;
                }
                else if (KeypadTLArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[4] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[3] == KeypadTRArrow)
        {
            if (!Key2Fake)
            {
                if (KeypadTRArrow.material.mainTexture == ArrowLeft)
                {
                    Offset = 1;
                    ArrowOrder[4] = KeypadTLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 4;
                    ArrowOrder[4] = KeypadBRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 4;
                    ArrowOrder[4] = KeypadBRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 3;
                    ArrowOrder[4] = KeypadBLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[4] = KeypadTLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[4] = KeypadTRArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[4] = KeypadBLArrow;
                }
                else if (KeypadTRArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[4] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[3] == KeypadBLArrow)
        {
            if (!Key3Fake)
            {
                if (KeypadBLArrow.material.mainTexture == ArrowRight)
                {
                    Offset = 4;
                    ArrowOrder[4] = KeypadBRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 1;
                    ArrowOrder[4] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 1;
                    ArrowOrder[4] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 2;
                    ArrowOrder[4] = KeypadTRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[4] = KeypadTLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[4] = KeypadTRArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[4] = KeypadBLArrow;
                }
                else if (KeypadBLArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[4] = KeypadBRArrow;
                }
            }
        }
        else if (ArrowOrder[3] == KeypadBRArrow)
        {
            if (!Key4Fake)
            {
                if (KeypadBRArrow.material.mainTexture == ArrowLeft)
                {
                    Offset = 3;
                    ArrowOrder[4] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowReverse)
                {
                    Offset = 2;
                    ArrowOrder[4] = KeypadTRArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowClockwise)
                {
                    Offset = 3;
                    ArrowOrder[4] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowSwap)
                {
                    Offset = 1;
                    ArrowOrder[4] = KeypadTLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalTL)
                {
                    Offset = 1;
                    ArrowOrder[4] = KeypadTLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalTR)
                {
                    Offset = 2;
                    ArrowOrder[4] = KeypadTRArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalBL)
                {
                    Offset = 3;
                    ArrowOrder[4] = KeypadBLArrow;
                }
                else if (KeypadBRArrow.material.mainTexture == ArrowDiagonalBR)
                {
                    Offset = 4;
                    ArrowOrder[4] = KeypadBRArrow;
                }
            }
        }

        if (ArrowOrder[4] == null)
        {
            if (ArrowOrder[3] == null)
            {
                if (ArrowOrder[2] == null)
                {
                    if (ArrowOrder[1] == null)
                    {
                        DesiredKey = ArrowOrder[0].name;
                    }
                    else
                    {
                        DesiredKey = ArrowOrder[1].name;
                    }
                }
                else
                {
                    DesiredKey = ArrowOrder[2].name;
                }
            }
            else
            {
                DesiredKey = ArrowOrder[3].name;
            }
        }
        else
        {
            DesiredKey = ArrowOrder[4].name;
        }
        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] The correct key is {1}", ModuleID, DesiredKey);
    }

    protected bool KeypadTLButton()
    {
        GetComponent<KMSelectable>().AddInteractionPunch();
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] Key pressed: Top left. Expected: {1}", ModuleID, DesiredKey);
        if (DesiredKey == "ArrowTL")
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] ... Which was correct. Module passed.", ModuleID);
            KeypadStatusLight.SetPass();
            KeypadLedTL.material.mainTexture = KeypadLedCorrect;
            CurrentSolveOrder.Add("Keypads");
            ModulesLeft--;
            ModulesLeftText.text = ModulesLeft.ToString();
            SolveOrderDisplay[CurrentSolve].text = "Keypads";
            CurrentSolve++;

            KeypadTL.OnInteract = KeypadDeactvated;
            KeypadTR.OnInteract = KeypadDeactvated;
            KeypadBL.OnInteract = KeypadDeactvated;
            KeypadBR.OnInteract = KeypadDeactvated;
        }
        else
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] ... Which caused a strike!", ModuleID);
            KeypadLedTL.material.mainTexture = KeypadLedWrong;
            KeypadStatusLight.FlashStrike();
            GetComponent<KMBombModule>().HandleStrike();
            StrikeCount++;
            CurrentStrikesText.text = StrikeCount.ToString();
        }
        return false;
    }

    protected bool KeypadTRButton()
    {
        GetComponent<KMSelectable>().AddInteractionPunch();
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] Key pressed: Top right. Expected: {1}", ModuleID, DesiredKey);
        if (DesiredKey == "ArrowTR")
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] ... Which was correct. Module passed.", ModuleID);
            KeypadStatusLight.SetPass();
            KeypadLedTR.material.mainTexture = KeypadLedCorrect;
            CurrentSolveOrder.Add("Keypads");
            ModulesLeft--;
            ModulesLeftText.text = ModulesLeft.ToString();
            SolveOrderDisplay[CurrentSolve].text = "Keypads";
            CurrentSolve++;

            KeypadTL.OnInteract = KeypadDeactvated;
            KeypadTR.OnInteract = KeypadDeactvated;
            KeypadBL.OnInteract = KeypadDeactvated;
            KeypadBR.OnInteract = KeypadDeactvated;
        }
        else
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] ... Which caused a strike!", ModuleID);
            KeypadLedTR.material.mainTexture = KeypadLedWrong;
            KeypadStatusLight.FlashStrike();
            GetComponent<KMBombModule>().HandleStrike();
            StrikeCount++;
            CurrentStrikesText.text = StrikeCount.ToString();
        }
        return false;
    }

    protected bool KeypadBLButton()
    {
        GetComponent<KMSelectable>().AddInteractionPunch();
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] Key pressed: Bottom left. Expected: {1}", ModuleID, DesiredKey);
        if (DesiredKey == "ArrowBL")
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] ... Which was correct. Module passed.", ModuleID);
            KeypadStatusLight.SetPass();
            KeypadLedBL.material.mainTexture = KeypadLedCorrect;
            CurrentSolveOrder.Add("Keypads");
            ModulesLeft--;
            ModulesLeftText.text = ModulesLeft.ToString();
            SolveOrderDisplay[CurrentSolve].text = "Keypads";
            CurrentSolve++;

            KeypadTL.OnInteract = KeypadDeactvated;
            KeypadTR.OnInteract = KeypadDeactvated;
            KeypadBL.OnInteract = KeypadDeactvated;
            KeypadBR.OnInteract = KeypadDeactvated;
        }
        else
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] ... Which caused a strike!", ModuleID);
            KeypadLedBL.material.mainTexture = KeypadLedWrong;
            KeypadStatusLight.FlashStrike();
            GetComponent<KMBombModule>().HandleStrike();
            StrikeCount++;
            CurrentStrikesText.text = StrikeCount.ToString();
        }
        return false;
    }

    protected bool KeypadBRButton()
    {
        GetComponent<KMSelectable>().AddInteractionPunch();
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] Key pressed: Bottom right. Expected: {1}", ModuleID, DesiredKey);
        if (DesiredKey == "ArrowBR")
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] ... Which was correct. Module passed.", ModuleID);
            KeypadStatusLight.SetPass();
            KeypadLedBR.material.mainTexture = KeypadLedCorrect;
            CurrentSolveOrder.Add("Keypads");
            ModulesLeft--;
            ModulesLeftText.text = ModulesLeft.ToString();
            SolveOrderDisplay[CurrentSolve].text = "Keypads";
            CurrentSolve++;

            KeypadTL.OnInteract = KeypadDeactvated;
            KeypadTR.OnInteract = KeypadDeactvated;
            KeypadBL.OnInteract = KeypadDeactvated;
            KeypadBR.OnInteract = KeypadDeactvated;
        }
        else
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Directional Keypads] ... Which caused a strike!", ModuleID);
            KeypadLedBR.material.mainTexture = KeypadLedWrong;
            KeypadStatusLight.FlashStrike();
            GetComponent<KMBombModule>().HandleStrike();
            StrikeCount++;
            CurrentStrikesText.text = StrikeCount.ToString();
        }
        return false;
    }

    protected bool KeypadDeactvated()
    {
        GetComponent<KMSelectable>().AddInteractionPunch();
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        return false;
    }
    //End of keypad



    //Begin of Morse.  
    void MorseSetup()
    {
        int x = 0;
        while (x < 4)
        {
            if (x == 0)
            {
                MorseDigitGenerator = Random.Range(0, 9);
                switch (MorseDigitGenerator)
                {
                    case 0:
                        {
                            MorseDigitCode += 0;
                            MorseCode += "/----- ";
                            break;
                        }
                    case 1:
                        {
                            MorseDigitCode += 1;
                            MorseCode += "/.---- ";
                            break;
                        }
                    case 2:
                        {
                            MorseDigitCode += 2;
                            MorseCode += "/..--- ";
                            break;
                        }
                    case 3:
                        {
                            MorseDigitCode += 3;
                            MorseCode += "/...-- ";
                            break;
                        }
                    case 4:
                        {
                            MorseDigitCode += 4;
                            MorseCode += "/....- ";
                            break;
                        }
                    case 5:
                        {
                            MorseDigitCode += 5;
                            MorseCode += "/..... ";
                            break;
                        }
                    case 6:
                        {
                            MorseDigitCode += 6;
                            MorseCode += "/-.... ";
                            break;
                        }
                    case 7:
                        {
                            MorseDigitCode += 7;
                            MorseCode += "/--... ";
                            break;
                        }
                    case 8:
                        {
                            MorseDigitCode += 8;
                            MorseCode += "/---.. ";
                            break;
                        }
                    case 9:
                        {
                            MorseDigitCode += 9;
                            MorseCode += "/----. ";
                            break;
                        }
                }
            }
            else if (x == 1 || x == 2)
            {
                MorseDigitGenerator = Random.Range(0, 9);
                switch (MorseDigitGenerator)
                {
                    case 0:
                        {
                            MorseDigitCode += 0;
                            MorseCode += "----- ";
                            break;
                        }
                    case 1:
                        {
                            MorseDigitCode += 1;
                            MorseCode += ".---- ";
                            break;
                        }
                    case 2:
                        {
                            MorseDigitCode += 2;
                            MorseCode += "..--- ";
                            break;
                        }
                    case 3:
                        {
                            MorseDigitCode += 3;
                            MorseCode += "...-- ";
                            break;
                        }
                    case 4:
                        {
                            MorseDigitCode += 4;
                            MorseCode += "....- ";
                            break;
                        }
                    case 5:
                        {
                            MorseDigitCode += 5;
                            MorseCode += "..... ";
                            break;
                        }
                    case 6:
                        {
                            MorseDigitCode += 6;
                            MorseCode += "-.... ";
                            break;
                        }
                    case 7:
                        {
                            MorseDigitCode += 7;
                            MorseCode += "--... ";
                            break;
                        }
                    case 8:
                        {
                            MorseDigitCode += 8;
                            MorseCode += "---.. ";
                            break;
                        }
                    case 9:
                        {
                            MorseDigitCode += 9;
                            MorseCode += "----. ";
                            break;
                        }
                }
            }
            else
            {
                MorseDigitGenerator = Random.Range(0, 9);
                switch (MorseDigitGenerator)
                {
                    case 0:
                        {
                            MorseDigitCode += 0;
                            MorseCode += "-----";
                            break;
                        }
                    case 1:
                        {
                            MorseDigitCode += 1;
                            MorseCode += ".----";
                            break;
                        }
                    case 2:
                        {
                            MorseDigitCode += 2;
                            MorseCode += "..---";
                            break;
                        }
                    case 3:
                        {
                            MorseDigitCode += 3;
                            MorseCode += "...--";
                            break;
                        }
                    case 4:
                        {
                            MorseDigitCode += 4;
                            MorseCode += "....-";
                            break;
                        }
                    case 5:
                        {
                            MorseDigitCode += 5;
                            MorseCode += ".....";
                            break;
                        }
                    case 6:
                        {
                            MorseDigitCode += 6;
                            MorseCode += "-....";
                            break;
                        }
                    case 7:
                        {
                            MorseDigitCode += 7;
                            MorseCode += "--...";
                            break;
                        }
                    case 8:
                        {
                            MorseDigitCode += 8;
                            MorseCode += "---..";
                            break;
                        }
                    case 9:
                        {
                            MorseDigitCode += 9;
                            MorseCode += "----.";
                            break;
                        }
                }
            }
            x++;
        }
        MorseCode += "!";
        Debug.LogFormat("[Micro-Modules #{0}] [Code Morse] The received digits are {1}", ModuleID, MorseDigitCode);
        MorseCalculation();
    }

    void MorseCalculation()
    {
        int MorseModuleID = int.Parse(MorseMicroModuleIDTxt.text);
        MorseIntCode = int.Parse(MorseDigitCode);
        if (BombInfo.GetBatteryCount() > 0)
        {
            MorseIntCode = MorseIntCode + BombInfo.GetBatteryCount() * 10;
        }
        if (BatteryColor == "Red" || BatteryColor == "Blue" || BatteryColor == "Green")
        {
            MorseIntCode = MorseIntCode * 30;
        }
        if (BombInfo.GetSerialNumberNumbers().Last() != 0)
        {
            MorseIntCode = MorseIntCode / BombInfo.GetSerialNumberNumbers().Last();
        }
        else
        {
            MorseIntCode = MorseIntCode * 5;
        }
        if (MorseIntCode % 10 == 1 || MorseIntCode % 10 == 3 || MorseIntCode % 10 == 5 || MorseIntCode % 10 == 7 || MorseIntCode % 10 == 9)
        {
            MorseIntCode = MorseIntCode + 101;
        }
        MorseIntCode = MorseIntCode * MorseModuleID;
        StartCoroutine("MorseSubtraction");
    }


    IEnumerator MorseSubtraction()
    {
        while (true)
        {
            if (MorseIntCode > 9999)
            {
                MorseIntCode = MorseIntCode - 1000;
            }
            else
            {
                CodeGenerated = true;
                MorseCodeTxtMsh.text = "____";
                Debug.LogFormat("[Micro-Modules #{0}] [Code Morse] The code is {1}", ModuleID, MorseIntCode);
                StopCoroutine("MorseSubtraction");
            }
            yield return new WaitForSecondsRealtime(0);
        }
    }

    protected bool MorseReceiveCode()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                MorseReceivingCode = true;
                StartCoroutine("MorseCodeReceive");
            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }

    IEnumerator MorseCodeReceive()
    {
        foreach (char unit in MorseCode)
        {
            if (unit == ' ')
            {
                MorseLight.material.color = Color.magenta;
                yield return new WaitForSecondsRealtime(1);
                MorseLight.material.color = Color.black;
            }
            else if (unit == '.')
            {
                MorseLight.material.color = Color.yellow;
                yield return new WaitForSecondsRealtime(0.25f);
                MorseLight.material.color = Color.black;
            }
            else if (unit == '-')
            {
                MorseLight.material.color = Color.yellow;
                yield return new WaitForSecondsRealtime(0.75f);
                MorseLight.material.color = Color.black;
            }
            else if (unit == '!')
            {
                MorseLight.material.color = Color.red;
                yield return new WaitForSecondsRealtime(1.5f);
                MorseLight.material.color = Color.black;
                MorseReceivingCode = false;
            }
            else if (unit == '/')
            {
                yield return new WaitForSecondsRealtime(0.5f);
                MorseReceivingCode = true;
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    protected bool MorseKey0Press()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                MorseEnteredCode += "0";
                MorseCodeTxtMsh.text = MorseCodeTxtMsh.text.Substring(1);
                MorseCodeTxtMsh.text += "0";
                MorseCharacters++;
            }
            else if (MorseReceivingCode)
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            else if (!MorseReceivingCode && MorseCharacters == 4)
            {

            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }

    protected bool MorseKey1Press()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                if (MorseCharacters < 4)
                {
                    MorseEnteredCode += "1";
                    MorseCodeTxtMsh.text = MorseCodeTxtMsh.text.Substring(1);
                    MorseCodeTxtMsh.text += "1";
                    MorseCharacters++;
                }
            }
            else if (MorseReceivingCode)
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            else if (!MorseReceivingCode && MorseCharacters == 4)
            {

            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }
    protected bool MorseKey2Press()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                if (MorseCharacters < 4)
                {
                    MorseEnteredCode += "2";
                    MorseCodeTxtMsh.text = MorseCodeTxtMsh.text.Substring(1);
                    MorseCodeTxtMsh.text += "2";
                    MorseCharacters++;
                }
            }
            else if (MorseReceivingCode)
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            else if (!MorseReceivingCode && MorseCharacters == 4)
            {

            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }

    protected bool MorseKey3Press()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                if (MorseCharacters < 4)
                {
                    MorseEnteredCode += "3";
                    MorseCodeTxtMsh.text = MorseCodeTxtMsh.text.Substring(1);
                    MorseCodeTxtMsh.text += "3";
                    MorseCharacters++;
                }
            }
            else if (MorseReceivingCode)
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            else if (!MorseReceivingCode && MorseCharacters == 4)
            {

            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }

    protected bool MorseKey4Press()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                if (MorseCharacters < 4)
                {
                    MorseEnteredCode += "4";
                    MorseCodeTxtMsh.text = MorseCodeTxtMsh.text.Substring(1);
                    MorseCodeTxtMsh.text += "4";
                    MorseCharacters++;
                }
            }
            else if (MorseReceivingCode)
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            else if (!MorseReceivingCode && MorseCharacters == 4)
            {

            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }

    protected bool MorseKey5Press()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                if (MorseCharacters < 4)
                {
                    MorseEnteredCode += "5";
                    MorseCodeTxtMsh.text = MorseCodeTxtMsh.text.Substring(1);
                    MorseCodeTxtMsh.text += "5";
                    MorseCharacters++;
                }
            }
            else if (MorseReceivingCode)
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            else if (!MorseReceivingCode && MorseCharacters == 4)
            {

            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }

        return false;
    }

    protected bool MorseKey6Press()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                if (MorseCharacters < 4)
                {
                    MorseEnteredCode += "6";
                    MorseCodeTxtMsh.text = MorseCodeTxtMsh.text.Substring(1);
                    MorseCodeTxtMsh.text += "6";
                    MorseCharacters++;
                }
            }
            else if (MorseReceivingCode)
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            else if (!MorseReceivingCode && MorseCharacters == 4)
            {

            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }

    protected bool MorseKey7Press()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                if (MorseCharacters < 4)
                {
                    MorseEnteredCode += "7";
                    MorseCodeTxtMsh.text = MorseCodeTxtMsh.text.Substring(1);
                    MorseCodeTxtMsh.text += "7";
                    MorseCharacters++;
                }
            }
            else if (MorseReceivingCode)
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            else if (!MorseReceivingCode && MorseCharacters == 4)
            {

            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }

    protected bool MorseKey8Press()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                if (MorseCharacters < 4)
                {
                    MorseEnteredCode += "8";
                    MorseCodeTxtMsh.text = MorseCodeTxtMsh.text.Substring(1);
                    MorseCodeTxtMsh.text += "8";
                    MorseCharacters++;
                }
            }
            else if (MorseReceivingCode)
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            else if (!MorseReceivingCode && MorseCharacters == 4)
            {

            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }

    protected bool MorseKey9Press()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode && MorseCharacters != 4)
            {
                if (MorseCharacters < 4)
                {
                    MorseEnteredCode += "9";
                    MorseCodeTxtMsh.text = MorseCodeTxtMsh.text.Substring(1);
                    MorseCodeTxtMsh.text += "9";
                    MorseCharacters++;
                }
            }
            else if (MorseReceivingCode)
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            else if (!MorseReceivingCode && MorseCharacters == 4)
            {

            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }

    protected bool MorseSubmitKey()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(1.5f);
        if (CodeGenerated)
        {
            if (!MorseReceivingCode)
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Code Morse] Submitted: {1}. Desired: {2}", ModuleID, MorseEnteredCode, MorseIntCode);
                if (MorseEnteredCode == MorseIntCode.ToString())
                {
                    Debug.LogFormat("[Micro-Modules #{0}] [Code Morse] ... Which was correct. Module passed.", ModuleID);
                    MorseStatusLight.SetPass();
                    CurrentSolveOrder.Add("Morse Code");
                    ModulesLeft--;
                    ModulesLeftText.text = ModulesLeft.ToString();
                    SolveOrderDisplay[CurrentSolve].text = "Morse Code";
                    CurrentSolve++;

                    MorseKeySend.OnInteract = MorseSendButtonDeactivated;
                    MorseKeyReceive.OnInteract = MorseReceiveButtonDeactivated;
                    MorseKey0.OnInteract = MorseKeyDeactivated;
                    MorseKey1.OnInteract = MorseKeyDeactivated;
                    MorseKey2.OnInteract = MorseKeyDeactivated;
                    MorseKey3.OnInteract = MorseKeyDeactivated;
                    MorseKey4.OnInteract = MorseKeyDeactivated;
                    MorseKey5.OnInteract = MorseKeyDeactivated;
                    MorseKey6.OnInteract = MorseKeyDeactivated;
                    MorseKey7.OnInteract = MorseKeyDeactivated;
                    MorseKey8.OnInteract = MorseKeyDeactivated;
                    MorseKey9.OnInteract = MorseKeyDeactivated;
                }
                else
                {
                    Debug.LogFormat("[Micro-Modules #{0}] [Code Morse] ... Which caused a strike!", ModuleID);
                    MorseStatusLight.FlashStrike();
                    GetComponent<KMBombModule>().HandleStrike();
                    StrikeCount++;
                    CurrentStrikesText.text = StrikeCount.ToString();
                    MorseCharacters = 0;
                    MorseCodeTxtMsh.text = "____";
                    MorseEnteredCode = "";
                }
            }
            else
            {
                MorseStatusLight.FlashStrike();
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
        }
        return false;
    }

    protected bool MorseKeyDeactivated()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        return false;
    }

    protected bool MorseSendButtonDeactivated()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(1.5f);
        return false;
    }

    protected bool MorseReceiveButtonDeactivated()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        return false;
    }
    //End of Morse

    //Begin of Passwords
    void PasswordSetup()
    {
        foreach (char Character in PasswordLetters)
        {
            PasswordLetterGen = Random.Range(0, 25);
            PasswordLetters += PasswordPossibleLetters[PasswordLetterGen];
            PasswordLetters = PasswordLetters.Substring(1);
        }
        PasswordCharacter1 = PasswordLetters[0];
        PasswordCharacter2 = PasswordLetters[1];
        PasswordCharacter3 = PasswordLetters[2];

        PasswordChar1.text = PasswordCharacter1.ToString();
        PasswordChar2.text = PasswordCharacter2.ToString();
        PasswordChar3.text = PasswordCharacter3.ToString();

        CharactersList1.Add(PasswordCharacter1);
        CharactersList2.Add(PasswordCharacter2);
        CharactersList3.Add(PasswordCharacter3);
        
        CharacterA = PasswordCharacter1 % 32;
        CharacterB = PasswordCharacter2 % 32;
        CharacterC = PasswordCharacter3 % 32;

        Debug.LogFormat("[Micro-Modules #{0}] [Math Code] Character A is a(n) {1}, B is a(n) {2} and C is a(n) {3}", ModuleID, PasswordCharacter1, PasswordCharacter2, PasswordCharacter3);

        Expression = ExpressionText.text;

        foreach (char Character in Expression)
        {
            int OperatorGen;
            if (Character == '?')
            {
                OperatorGen = Random.Range(0, 4);
                switch (OperatorGen)
                {
                    case 0:
                        {
                            Expression = Expression.Replace('?', '+');
                            break;
                        }
                    case 1:
                        {
                            Expression = Expression.Replace('?', '-');
                            break;
                        }
                    case 2:
                        {
                            Expression = Expression.Replace('?', '*');
                            break;
                        }
                    case 3:
                        {
                            Expression = Expression.Replace('?', '/');
                            break;
                        }
                }
            }
            else if (Character == '!')
            {
                OperatorGen = Random.Range(0, 4);
                switch (OperatorGen)
                {
                    case 0:
                        {
                            Expression = Expression.Replace('!', '+');
                            break;
                        }
                    case 1:
                        {
                            Expression = Expression.Replace('!', '-');
                            break;
                        }
                    case 2:
                        {
                            Expression = Expression.Replace('!', '*');
                            break;
                        }
                    case 3:
                        {
                            Expression = Expression.Replace('!', '/');
                            break;
                        }
                }
            }

            ExpressionText.text = Expression;
        }
        Debug.LogFormat("[Micro-Modules #{0}] [Math Code] The expression is {1}", ModuleID, Expression);
        if (BatteryColor == "Red" || BatteryColor == "Blue" || BatteryColor == "Green")
        {
            CharacterA = CharacterA + 5;
        }
        if (PasswordLetters.Any("aAeEiIoOuU".Contains))
        {
            CharacterB = CharacterB * 3;
        }
        else
        {
            CharacterB = CharacterB - 3;
        }
        if (CharacterA == 0)
        {
            CharacterA = 1;
        }
        if (CharacterB == 0)
        {
            CharacterB = 1;
        }
        if (CharacterC == 0)
        {
            CharacterC = 1;
        }
        CharacterC = CharacterC + BombInfo.GetBatteryCount();
        if (BombInfo.GetOnIndicators().Count() > 0)
        {
            CharacterA = CharacterA * BombInfo.GetOnIndicators().Count();
            CharacterB = CharacterB * BombInfo.GetOnIndicators().Count();
            CharacterC = CharacterC * BombInfo.GetOnIndicators().Count();
        }
        Debug.LogFormat("[Micro-Modules #{0}] [Math Code] The digits for A, B and C before calculation are {1}, {2} and {3}", ModuleID, CharacterA, CharacterB, CharacterC);
        PasswordCalculation();
    }

    void PasswordCalculation()
    {
        if (Expression[2] == '+' || Expression[3] == '+')
        {
            if (Expression[6] == '+' || Expression[7] == '+' || Expression[8] == '+')
            {
                //A + B + C
                ExpressionSolution = CharacterA + CharacterB + CharacterC;
            }
            else if (Expression[6] == '-' || Expression[7] == '-' || Expression[8] == '-')
            {
                //A + B - C
                ExpressionSolution = CharacterA + CharacterB - CharacterC;
            }
            else if (Expression[6] == '*' || Expression[7] == '*' || Expression[8] == '*')
            {
                //A + B * C
                ExpressionSolution = CharacterA + CharacterB * CharacterC;
            }
            else if (Expression[6] == '/')
            {
                //A + B / C
                if (CharacterC == 0)
                {
                    CharacterC++;
                }
                ExpressionSolution = CharacterA + CharacterB / CharacterC;
            }
        }
        else if (Expression[2] == '-' || Expression[3] == '-')
        {
            if (Expression[6] == '+' || Expression[7] == '+' || Expression[8] == '+')
            {
                //A - B + C
                ExpressionSolution = CharacterA - CharacterB + CharacterC;
            }
            else if (Expression[6] == '-' || Expression[7] == '-' || Expression[8] == '-')
            {
                //A - B - C
                ExpressionSolution = CharacterA - CharacterB - CharacterC;
            }
            else if (Expression[6] == '*' || Expression[7] == '*' || Expression[8] == '*')
            {
                //A - B * C
                ExpressionSolution = CharacterA - CharacterB * CharacterC;
            }
            else if (Expression[6] == '/')
            {
                //A - B / C
                if (CharacterC == 0)
                {
                    CharacterC++;
                }
                ExpressionSolution = CharacterA - CharacterB / CharacterC;
            }
        }
        else if (Expression[2] == '*' || Expression[3] == '*')
        {
            if (Expression[6] == '+' || Expression[7] == '+' || Expression[8] == '+')
            {
                //A * B + C
                ExpressionSolution = CharacterA * CharacterB + CharacterC;
            }
            else if (Expression[6] == '-' || Expression[7] == '-' || Expression[8] == '-')
            {
                //A * B - C
                ExpressionSolution = CharacterA * CharacterB - CharacterC;
            }
            else if (Expression[6] == '*' || Expression[7] == '*' || Expression[8] == '*')
            {
                //A * B * C
                ExpressionSolution = CharacterA * CharacterB * CharacterC;
            }
            else if (Expression[6] == '/')
            {
                //A * B / C
                if (CharacterC == 0)
                {
                    CharacterC++;
                }
                ExpressionSolution = CharacterA * CharacterB / CharacterC;
            }
        }
        else if (Expression[2] == '/' || Expression[3] == '/')
        {
            if (Expression[6] == '+' || Expression[7] == '+' || Expression[8] == '+')
            {
                //A / B + C
                if (CharacterB == 0)
                {
                    CharacterB++;
                }
                ExpressionSolution = CharacterA / CharacterB + CharacterC;
            }
            else if (Expression[6] == '-' || Expression[7] == '-' || Expression[8] == '-')
            {
                //A / B - C
                if (CharacterB == 0)
                {
                    CharacterB++;
                }
                ExpressionSolution = CharacterA / CharacterB - CharacterC;
            }
            else if (Expression[6] == '*' || Expression[7] == '*' || Expression[8] == '*')
            {
                //A / B * C
                if (CharacterB == 0)
                {
                    CharacterB++;
                }
                ExpressionSolution = CharacterA / CharacterB * CharacterC;
            }
            else if (Expression[6] == '/')
            {
                //A / B / C
                if (CharacterB == 0)
                {
                    CharacterB++;
                }
                if (CharacterC == 0)
                {
                    CharacterC++;
                }
                ExpressionSolution = CharacterA / CharacterB / CharacterC;
            }
        }
        StartCoroutine("PasswordSubtraction");
    }

    IEnumerator PasswordSubtraction()
    {
        Debug.LogFormat("[Micro-Modules #{0}] [Math Code] The solution is {1}", ModuleID, ExpressionSolution);
        while (true)
        {
            if (ExpressionSolution < 100)
            {
                ExpressionSolution = ExpressionSolution + 100;
            }
            else if (ExpressionSolution > 999)
            {
                ExpressionSolution = ExpressionSolution - 100;
            }
            else
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Math Code] The code is {1}", ModuleID, ExpressionSolution);
                SolutionPasswordDigit1 = int.Parse(ExpressionSolution.ToString()[0].ToString());
                SolutionPasswordDigit2 = int.Parse(ExpressionSolution.ToString()[1].ToString());
                SolutionPasswordDigit3 = int.Parse(ExpressionSolution.ToString()[2].ToString());
                StopCoroutine("PasswordSubtraction");
            }
            yield return new WaitForSecondsRealtime(0.001f);
        }
    }

    protected bool PasswordChar1Next()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        ActionNumber1++;
        if (ActionNumber1 > 10)
            ActionNumber1 = 0;
        switch (ActionNumber1)
        {
            case 0:
                {
                    PasswordChar1.color = new Color32(0, 213, 0, 255);
                    PasswordCharacter1 = CharactersList1[0];
                    EnteredPasswordDigit1 = 0;
                    break;
                }
            case 1:
                {
                    PasswordCharacter1 = CharactersList1[1];
                    EnteredPasswordDigit1 = 1;
                    break;
                }
            case 2:
                {
                    PasswordCharacter1 = CharactersList1[2];
                    EnteredPasswordDigit1 = 2;
                    break;
                }
            case 3:
                {
                    PasswordCharacter1 = CharactersList1[3];
                    EnteredPasswordDigit1 = 3;
                    break;
                }
            case 4:
                {
                    PasswordCharacter1 = CharactersList1[4];
                    EnteredPasswordDigit1 = 4;
                    break;
                }
            case 5:
                {
                    PasswordCharacter1 = CharactersList1[5];
                    EnteredPasswordDigit1 = 5;
                    break;
                }
            case 6:
                {
                    PasswordCharacter1 = CharactersList1[6];
                    EnteredPasswordDigit1 = 6;
                    break;
                }
            case 7:
                {
                    PasswordCharacter1 = CharactersList1[7];
                    EnteredPasswordDigit1 = 7;
                    break;
                }
            case 8:
                {
                    PasswordCharacter1 = CharactersList1[8];
                    EnteredPasswordDigit1 = 8;
                    break;
                }
            case 9:
                {
                    PasswordCharacter1 = CharactersList1[9];
                    EnteredPasswordDigit1 = 9;
                    break;
                }
            case 10:
                {
                    PasswordChar1.color = new Color32(96, 255, 0, 255);
                    PasswordCharacter1 = CharactersList1[10];
                    EnteredPasswordDigit1 = 10;
                    break;
                }
        }
        PasswordChar1.text = PasswordCharacter1.ToString();
        return false;
    }

    protected bool PasswordChar2Next()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        ActionNumber2++;
        if (ActionNumber2 > 10)
            ActionNumber2 = 0;
        switch (ActionNumber2)
        {
            case 0:
                {
                    PasswordChar2.color = new Color32(0, 213, 0, 255);
                    PasswordCharacter2 = CharactersList2[0];
                    EnteredPasswordDigit2 = 0;
                    break;
                }
            case 1:
                {
                    PasswordCharacter2 = CharactersList2[1];
                    EnteredPasswordDigit2 = 1;
                    break;
                }
            case 2:
                {
                    PasswordCharacter2 = CharactersList2[2];
                    EnteredPasswordDigit2 = 2;
                    break;
                }
            case 3:
                {
                    PasswordCharacter2 = CharactersList2[3];
                    EnteredPasswordDigit2 = 3;
                    break;
                }
            case 4:
                {
                    PasswordCharacter2 = CharactersList2[4];
                    EnteredPasswordDigit2 = 4;
                    break;
                }
            case 5:
                {
                    PasswordCharacter2 = CharactersList2[5];
                    EnteredPasswordDigit2 = 5;
                    break;
                }
            case 6:
                {
                    PasswordCharacter2 = CharactersList2[6];
                    EnteredPasswordDigit2 = 6;
                    break;
                }
            case 7:
                {
                    PasswordCharacter2 = CharactersList2[7];
                    EnteredPasswordDigit2 = 7;
                    break;
                }
            case 8:
                {
                    PasswordCharacter2 = CharactersList2[8];
                    EnteredPasswordDigit2 = 8;
                    break;
                }
            case 9:
                {
                    PasswordCharacter2 = CharactersList2[9];
                    EnteredPasswordDigit2 = 9;
                    break;
                }
            case 10:
                {
                    PasswordChar2.color = new Color32(96, 255, 0, 255);
                    PasswordCharacter2 = CharactersList2[10];
                    EnteredPasswordDigit2 = 10;
                    break;
                }
        }
        PasswordChar2.text = PasswordCharacter2.ToString();
        return false;
    }

    protected bool PasswordChar3Next()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch(0.25f);
        ActionNumber3++;
        if (ActionNumber3 > 10)
            ActionNumber3 = 0;
        switch (ActionNumber3)
        {
            case 0:
                {
                    PasswordChar3.color = new Color32(0, 213, 0, 255);
                    PasswordCharacter3 = CharactersList3[0];
                    EnteredPasswordDigit3 = 0;
                    break;
                }
            case 1:
                {
                    PasswordCharacter3 = CharactersList3[1];
                    EnteredPasswordDigit3 = 1;
                    break;
                }
            case 2:
                {
                    PasswordCharacter3 = CharactersList3[2];
                    EnteredPasswordDigit3 = 2;
                    break;
                }
            case 3:
                {
                    PasswordCharacter3 = CharactersList3[3];
                    EnteredPasswordDigit3 = 3;
                    break;
                }
            case 4:
                {
                    PasswordCharacter3 = CharactersList3[4];
                    EnteredPasswordDigit3 = 4;
                    break;
                }
            case 5:
                {
                    PasswordCharacter3 = CharactersList3[5];
                    EnteredPasswordDigit3 = 5;
                    break;
                }
            case 6:
                {
                    PasswordCharacter3 = CharactersList3[6];
                    EnteredPasswordDigit3 = 6;
                    break;
                }
            case 7:
                {
                    PasswordCharacter3 = CharactersList3[7];
                    EnteredPasswordDigit3 = 7;
                    break;
                }
            case 8:
                {
                    PasswordCharacter3 = CharactersList3[8];
                    EnteredPasswordDigit3 = 8;
                    break;
                }
            case 9:
                {
                    PasswordCharacter3 = CharactersList3[9];
                    EnteredPasswordDigit3 = 9;
                    break;
                }
            case 10:
                {
                    PasswordChar3.color = new Color32(96, 255, 0, 255);
                    PasswordCharacter3 = CharactersList3[10];
                    EnteredPasswordDigit3 = 10;
                    break;
                }
        }
        PasswordChar3.text = PasswordCharacter3.ToString();
        return false;
    }

    protected bool PasswordSubmision()
    {
        GetComponent<KMSelectable>().AddInteractionPunch();
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        Debug.LogFormat("[Micro-Modules #{0}] [Math Code] Submitted: {1}, {2} and {3}. Desired: {4}, {5} and {6}", ModuleID, EnteredPasswordDigit1, EnteredPasswordDigit2, EnteredPasswordDigit3, SolutionPasswordDigit1, SolutionPasswordDigit2, SolutionPasswordDigit3);
        if (EnteredPasswordDigit1 == SolutionPasswordDigit1)
        {
            if (EnteredPasswordDigit2 == SolutionPasswordDigit2)
            {
                if (EnteredPasswordDigit3 == SolutionPasswordDigit3)
                {
                    Debug.LogFormat("[Micro-Modules #{0}] [Math Code] ... Which was correct. Module passed.", ModuleID);
                    PasswordStatusLight.SetPass();
                    CurrentSolveOrder.Add("Passwords");
                    ModulesLeft--;
                    ModulesLeftText.text = ModulesLeft.ToString();
                    SolveOrderDisplay[CurrentSolve].text = "Passwords";
                    CurrentSolve++;

                    Password1Next.OnInteract = KeypadDeactvated;
                    Password2Next.OnInteract = KeypadDeactvated;
                    Password3Next.OnInteract = KeypadDeactvated;
                    PasswordSubmit.OnInteract = PasswordDeactivation;
                }
                else
                {
                    Debug.LogFormat("[Micro-Modules #{0}] [Math Code] ... Which caused a strike!", ModuleID);
                    PasswordStatusLight.FlashStrike();
                    GetComponent<KMBombModule>().HandleStrike();
                    StrikeCount++;
                    CurrentStrikesText.text = StrikeCount.ToString();

                    PasswordChar1.color = new Color32(96, 255, 0, 255);
                    PasswordCharacter1 = CharactersList1[10];
                    EnteredPasswordDigit1 = 10;
                    PasswordChar2.color = new Color32(96, 255, 0, 255);
                    PasswordCharacter2 = CharactersList2[10];
                    EnteredPasswordDigit2 = 10;
                    PasswordChar3.color = new Color32(96, 255, 0, 255);
                    PasswordCharacter3 = CharactersList3[10];
                    EnteredPasswordDigit3 = 10;
                    PasswordChar1.text = PasswordCharacter1.ToString();
                    PasswordChar2.text = PasswordCharacter2.ToString();
                    PasswordChar3.text = PasswordCharacter3.ToString();
                    ActionNumber1 = 10;
                    ActionNumber2 = 10;
                    ActionNumber3 = 10;
                }
            }
            else
            {
                PasswordStatusLight.FlashStrike();
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
                PasswordChar1.color = new Color32(96, 255, 0, 255);
                PasswordCharacter1 = CharactersList1[10];
                EnteredPasswordDigit1 = 10;
                PasswordChar2.color = new Color32(96, 255, 0, 255);
                PasswordCharacter2 = CharactersList2[10];
                EnteredPasswordDigit2 = 10;
                PasswordChar3.color = new Color32(96, 255, 0, 255);
                PasswordCharacter3 = CharactersList3[10];
                EnteredPasswordDigit3 = 10;
                PasswordChar1.text = PasswordCharacter1.ToString();
                PasswordChar2.text = PasswordCharacter2.ToString();
                PasswordChar3.text = PasswordCharacter3.ToString();
                ActionNumber1 = 10;
                ActionNumber2 = 10;
                ActionNumber3 = 10;
            }
        }
        else
        {
            PasswordStatusLight.FlashStrike();
            GetComponent<KMBombModule>().HandleStrike();
            StrikeCount++;
            CurrentStrikesText.text = StrikeCount.ToString();
            PasswordChar1.color = new Color32(96, 255, 0, 255);
            PasswordCharacter1 = CharactersList1[10];
            EnteredPasswordDigit1 = 10;
            PasswordChar2.color = new Color32(96, 255, 0, 255);
            PasswordCharacter2 = CharactersList2[10];
            EnteredPasswordDigit2 = 10;
            PasswordChar3.color = new Color32(96, 255, 0, 255);
            PasswordCharacter3 = CharactersList3[10];
            EnteredPasswordDigit3 = 10;
            PasswordChar1.text = PasswordCharacter1.ToString();
            PasswordChar2.text = PasswordCharacter2.ToString();
            PasswordChar3.text = PasswordCharacter3.ToString();
            ActionNumber1 = 10;
            ActionNumber2 = 10;
            ActionNumber3 = 10;
        }
        return false;
    }

    protected bool PasswordDeactivation()
    {
        GetComponent<KMSelectable>().AddInteractionPunch();
        return false;
    }
        //End of Password.

        //Begin of Wires.
    void WiresSetup()
    {
        int RendererGen = Random.Range(0, 16);
        RendererName = PossibleRendererNames[RendererGen];
        Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] The MeshRenderer's name is {1}", ModuleID, RendererName);
        ScriptLine1.text = "<color=#2721D3FF>public</color> <color=#4EC97FFF>Renderer</color>[] " + RendererName + ";";
        ScriptLine3.text = "<color=#2721D3FF>foreach</color> (<color=#4EC97FFF>Renderer</color> Wire <color=#2721D3FF>in</color> " + RendererName + ")";
        ScriptLine5.text = "	<color=#2721D3FF>if</color> (Wire.color.ToString() == Colors[WireNr])";

        if (RendererName == "BOMB" || RendererName == "EXPL" || RendererName == "MINI" || RendererName == "NULL")
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Using List 1", ModuleID);
            ColorList.Add("Red");
            ColorList.Add("Yellow");
            ColorList.Add("Green");
            ColorList.Add("Blue");
            ColorList.Add("White");
            ColorList.Add("Black");
        }
        else if (RendererName == "BOB" || RendererName == "MSA" || RendererName == "SIG" || RendererName == "TRN")
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Using List 2", ModuleID);
            ColorList.Add("Black");
            ColorList.Add("White");
            ColorList.Add("Blue");
            ColorList.Add("Green");
            ColorList.Add("Yellow");
            ColorList.Add("Red");
        }
        else if (RendererName == "DVID" || RendererName == "Parallel" || RendererName == "SRCA" || RendererName == "RJ45")
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Using List 3", ModuleID);
            ColorList.Add("Green");
            ColorList.Add("Blue");
            ColorList.Add("White");
            ColorList.Add("Black");
            ColorList.Add("Red");
            ColorList.Add("Yellow");
        }
        else
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Using List 4", ModuleID);
            ColorList.Add("White");
            ColorList.Add("Green");
            ColorList.Add("Red");
            ColorList.Add("Black");
            ColorList.Add("Yellow");
            ColorList.Add("Blue");
        }

        int WireCol1Gen = Random.Range(0, 6), WireCol2Gen = Random.Range(0, 6), WireCol3Gen = Random.Range(0, 6), WireCol4Gen = Random.Range(0, 6), WireCol5Gen = Random.Range(0, 6), WireCol6Gen = Random.Range(0, 6);
        switch (WireCol1Gen)
        {
            case 0:
                {
                    Wire1ColUp.material.color = Color.red;
                    Wire1ColDown.material.color = Color.red;
                    WireCol1 = "Red";
                    break;
                }
            case 1:
                {
                    Wire1ColUp.material.color = Color.yellow;
                    Wire1ColDown.material.color = Color.yellow;
                    WireCol1 = "Yellow";
                    break;
                }
            case 2:
                {
                    Wire1ColUp.material.color = Color.green;
                    Wire1ColDown.material.color = Color.green;
                    WireCol1 = "Green";
                    break;
                }
            case 3:
                {
                    Wire1ColUp.material.color = Color.blue;
                    Wire1ColDown.material.color = Color.blue;
                    WireCol1 = "Blue";
                    break;
                }
            case 4:
                {
                    Wire1ColUp.material.color = Color.white;
                    Wire1ColDown.material.color = Color.white;
                    WireCol1 = "White";
                    break;
                }
            case 5:
                {
                    Wire1ColUp.material.color = new Color32(21, 21, 21, 255);
                    Wire1ColDown.material.color = new Color32(21, 21, 21, 255);
                    WireCol1 = "Black";
                    break;
                }
        }
        switch (WireCol2Gen)
        {
            case 0:
                {
                    Wire2ColUp.material.color = Color.red;
                    Wire2ColDown.material.color = Color.red;
                    WireCol2 = "Red";
                    break;
                }
            case 1:
                {
                    Wire2ColUp.material.color = Color.yellow;
                    Wire2ColDown.material.color = Color.yellow;
                    WireCol2 = "Yellow";
                    break;
                }
            case 2:
                {
                    Wire2ColUp.material.color = Color.green;
                    Wire2ColDown.material.color = Color.green;
                    WireCol2 = "Green";
                    break;
                }
            case 3:
                {
                    Wire2ColUp.material.color = Color.blue;
                    Wire2ColDown.material.color = Color.blue;
                    WireCol2 = "Blue";
                    break;
                }
            case 4:
                {
                    Wire2ColUp.material.color = Color.white;
                    Wire2ColDown.material.color = Color.white;
                    WireCol2 = "White";
                    break;
                }
            case 5:
                {
                    Wire2ColUp.material.color = new Color32(21, 21, 21, 255);
                    Wire2ColDown.material.color = new Color32(21, 21, 21, 255);
                    WireCol2 = "Black";
                    break;
                }
        }
        switch (WireCol3Gen)
        {
            case 0:
                {
                    Wire3ColUp.material.color = Color.red;
                    Wire3ColDown.material.color = Color.red;
                    WireCol3 = "Red";
                    break;
                }
            case 1:
                {
                    Wire3ColUp.material.color = Color.yellow;
                    Wire3ColDown.material.color = Color.yellow;
                    WireCol3 = "Yellow";
                    break;
                }
            case 2:
                {
                    Wire3ColUp.material.color = Color.green;
                    Wire3ColDown.material.color = Color.green;
                    WireCol3 = "Green";
                    break;
                }
            case 3:
                {
                    Wire3ColUp.material.color = Color.blue;
                    Wire3ColDown.material.color = Color.blue;
                    WireCol3 = "Blue";
                    break;
                }
            case 4:
                {
                    Wire3ColUp.material.color = Color.white;
                    Wire3ColDown.material.color = Color.white;
                    WireCol3 = "White";
                    break;
                }
            case 5:
                {
                    Wire3ColUp.material.color = new Color32(21, 21, 21, 255);
                    Wire3ColDown.material.color = new Color32(21, 21, 21, 255);
                    WireCol3 = "Black";
                    break;
                }
        }
        switch (WireCol4Gen)
        {
            case 0:
                {
                    Wire4ColUp.material.color = Color.red;
                    Wire4ColDown.material.color = Color.red;
                    WireCol4 = "Red";
                    break;
                }
            case 1:
                {
                    Wire4ColUp.material.color = Color.yellow;
                    Wire4ColDown.material.color = Color.yellow;
                    WireCol4 = "Yellow";
                    break;
                }
            case 2:
                {
                    Wire4ColUp.material.color = Color.green;
                    Wire4ColDown.material.color = Color.green;
                    WireCol4 = "Green";
                    break;
                }
            case 3:
                {
                    Wire4ColUp.material.color = Color.blue;
                    Wire4ColDown.material.color = Color.blue;
                    WireCol4 = "Blue";
                    break;
                }
            case 4:
                {
                    Wire4ColUp.material.color = Color.white;
                    Wire4ColDown.material.color = Color.white;
                    WireCol4 = "White";
                    break;
                }
            case 5:
                {
                    Wire4ColUp.material.color = new Color32(21, 21, 21, 255);
                    Wire4ColDown.material.color = new Color32(21, 21, 21, 255);
                    WireCol4 = "Black";
                    break;
                }
        }
        switch (WireCol5Gen)
        {
            case 0:
                {
                    Wire5ColUp.material.color = Color.red;
                    Wire5ColDown.material.color = Color.red;
                    WireCol5 = "Red";
                    break;
                }
            case 1:
                {
                    Wire5ColUp.material.color = Color.yellow;
                    Wire5ColDown.material.color = Color.yellow;
                    WireCol5 = "Yellow";
                    break;
                }
            case 2:
                {
                    Wire5ColUp.material.color = Color.green;
                    Wire5ColDown.material.color = Color.green;
                    WireCol5 = "Green";
                    break;
                }
            case 3:
                {
                    Wire5ColUp.material.color = Color.blue;
                    Wire5ColDown.material.color = Color.blue;
                    WireCol5 = "Blue";
                    break;
                }
            case 4:
                {
                    Wire5ColUp.material.color = Color.white;
                    Wire5ColDown.material.color = Color.white;
                    WireCol5 = "White";
                    break;
                }
            case 5:
                {
                    Wire5ColUp.material.color = new Color32(21, 21, 21, 255);
                    Wire5ColDown.material.color = new Color32(21, 21, 21, 255);
                    WireCol5 = "Black";
                    break;
                }
        }
        switch (WireCol6Gen)
        {
            case 0:
                {
                    Wire6ColUp.material.color = Color.red;
                    Wire6ColDown.material.color = Color.red;
                    WireCol6 = "Red";
                    break;
                }
            case 1:
                {
                    Wire6ColUp.material.color = Color.yellow;
                    Wire6ColDown.material.color = Color.yellow;
                    WireCol6 = "Yellow";
                    break;
                }
            case 2:
                {
                    Wire6ColUp.material.color = Color.green;
                    Wire6ColDown.material.color = Color.green;
                    WireCol6 = "Green";
                    break;
                }
            case 3:
                {
                    Wire6ColUp.material.color = Color.blue;
                    Wire6ColDown.material.color = Color.blue;
                    WireCol6 = "Blue";
                    break;
                }
            case 4:
                {
                    Wire6ColUp.material.color = Color.white;
                    Wire6ColDown.material.color = Color.white;
                    WireCol6 = "White";
                    break;
                }
            case 5:
                {
                    Wire6ColUp.material.color = new Color32(21, 21, 21, 255);
                    Wire6ColDown.material.color = new Color32(21, 21, 21, 255);
                    WireCol6 = "Black";
                    break;
                }
        }

        if (WireCol1 == ColorList[0])
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 1 should be cut", ModuleID);
            DesiredCuts[0] = true;
            WireShouldBeCut1 = true;
        }
        if (WireCol2 == ColorList[1])
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 2 should be cut", ModuleID);
            DesiredCuts[1] = true;
            WireShouldBeCut2 = true;
        }
        if (WireCol3 == ColorList[2])
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 3 should be cut", ModuleID);
            DesiredCuts[2] = true;
            WireShouldBeCut3 = true;
        }
        if (WireCol4 == ColorList[3])
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 4 should be cut", ModuleID);
            DesiredCuts[3] = true;
            WireShouldBeCut4 = true;
        }
        if (WireCol5 == ColorList[4])
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 5 should be cut", ModuleID);
            DesiredCuts[4] = true;
            WireShouldBeCut5 = true;
        }
        if (WireCol6 == ColorList[5])
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 6 should be cut", ModuleID);
            DesiredCuts[5] = true;
            WireShouldBeCut6 = true;
        }

        if (!WireShouldBeCut1 && !WireShouldBeCut2 && !WireShouldBeCut3 && !WireShouldBeCut4 && !WireShouldBeCut5 && !WireShouldBeCut6)
        {
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 6 should be cut", ModuleID);
            DesiredCuts[5] = true;
            WireShouldBeCut6 = true;
        }
    }

    protected bool CuttingWire1()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSnip, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 1 was cut", ModuleID);
        if (!WireWasCut1)
        {
            Wire1UpObj.transform.localPosition = new Vector3(-0.01149f, -0.00775f, 0.0184f);
            Wire1DownObj.transform.localPosition = new Vector3(0.05773786f, 0.02175019f, 0.0525f);
            if (WireShouldBeCut1)
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which should have been cut", ModuleID);
                WireWasCut1 = true;
                CurrentCuts[0] = true;
                CheckWires();
            }
            else
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which caused a strike!", ModuleID);
                WireStatusLight.FlashStrike();
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            Wire1Sel.OnInteract = WireDeactivated;
        }
        return false;
    }
    protected bool CuttingWire2()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSnip, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 2 was cut", ModuleID);
        if (!WireWasCut2)
        {
            Wire2UpObj.transform.localPosition = new Vector3(-0.01144f, -0.00734f, 0.005859944f);
            Wire2DownObj.transform.localPosition = new Vector3(0.04177f, 0.02225f, 0.05252f);
            if (WireShouldBeCut2)
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which should have been cut", ModuleID);
                WireWasCut2 = true;
                CurrentCuts[1] = true;
                CheckWires();
            }
            else
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which caused a strike!", ModuleID);
                WireStatusLight.FlashStrike();
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            Wire2Sel.OnInteract = WireDeactivated;
        }
        return false;
    }
    protected bool CuttingWire3()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSnip, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 3 was cut", ModuleID);
        if (!WireWasCut3)
        {
            Wire3UpObj.transform.localPosition = new Vector3(-0.01139f, -0.00734f, -0.0058f);
            Wire3DownObj.transform.localPosition = new Vector3(0.02664043f, 0.02216012f, 0.0529f);
            if (WireShouldBeCut3)
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which should have been cut", ModuleID);
                WireWasCut3 = true;
                CurrentCuts[2] = true;
                CheckWires();
            }
            else
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which caused a strike!", ModuleID);
                WireStatusLight.FlashStrike();
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            Wire3Sel.OnInteract = WireDeactivated;
        }
        return false;
    }
    protected bool CuttingWire4()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSnip, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 4 was cut", ModuleID);
        if (!WireWasCut4)
        {
            Wire4UpObj.transform.localPosition = new Vector3(-0.01136f, -0.00734f, -0.01714f);
            Wire4DownObj.transform.localPosition = new Vector3(0.01205628f, 0.02216012f, 0.05256f);
            if (WireShouldBeCut4)
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which should have been cut", ModuleID);
                WireWasCut4 = true;
                CurrentCuts[3] = true;
                CheckWires();
            }
            else
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which caused a strike!", ModuleID);
                WireStatusLight.FlashStrike();
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            Wire4Sel.OnInteract = WireDeactivated;
        }
        return false;
    }
    protected bool CuttingWire5()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSnip, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 5 was cut", ModuleID);
        if (!WireWasCut5)
        {
            Wire5UpObj.transform.localPosition = new Vector3(-0.01132f, -0.007339831f, -0.02755f);
            Wire5DownObj.transform.localPosition = new Vector3(-0.00133183f, 0.02216034f, 0.05238f);
            if (WireShouldBeCut5)
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which should have been cut", ModuleID);
                WireWasCut5 = true;
                CurrentCuts[4] = true;
                CheckWires();
            }
            else
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which caused a strike!", ModuleID);
                WireStatusLight.FlashStrike();
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            Wire5Sel.OnInteract = WireDeactivated;
        }
        return false;
    }
    protected bool CuttingWire6()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSnip, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Wire 6 was cut", ModuleID);
        if (!WireWasCut6)
        {
            Wire6UpObj.transform.localPosition = new Vector3(-0.01128f, -0.00734f, -0.03806f);
            Wire6DownObj.transform.localPosition = new Vector3(-0.0148485f, 0.02216026f, 0.0529f);
            if (WireShouldBeCut6)
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which should have been cut", ModuleID);
                WireWasCut6 = true;
                CurrentCuts[5] = true;
                CheckWires();
            }
            else
            {
                Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] ... Which caused a strike!", ModuleID);
                WireStatusLight.FlashStrike();
                GetComponent<KMBombModule>().HandleStrike();
                StrikeCount++;
                CurrentStrikesText.text = StrikeCount.ToString();
            }
            Wire6Sel.OnInteract = WireDeactivated;
        }
        return false;
    }

    void CheckWires()
    {
        if (Enumerable.SequenceEqual(DesiredCuts, CurrentCuts))
        {
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
            Debug.LogFormat("[Micro-Modules #{0}] [Script Wires] Every wire was cut. Module passed.", ModuleID);
            WireStatusLight.SetPass();
            CurrentSolveOrder.Add("Wires");
            ModulesLeft--;
            ModulesLeftText.text = ModulesLeft.ToString();
            SolveOrderDisplay[CurrentSolve].text = "Wires";
            CurrentSolve++;
        }
    }

    protected bool WireDeactivated()
    {
        return false;
    }
    //End of Wires
    public string TwitchHelpMessage = "Wires: Use '!{0} cut 6' to cut the 6th wire! If you can't see the script wires's screen well, use '!{0} scriptinfo' to get the renderer's name written down in chat! Keypad: Use '!{0} press 3' to press the bottom-left button (1-tl, 2-tr, 3-bl, 4-br)! Morse: Use '!{0} receive' to receive the message and use '!{0} send 1 2 3 4' to send code 1234! Math: Use '!{0} answer 1 2 3' to submit 123! General: Use '!{0} submit' to submit the module and use '!{0} reset' to reset the module!";
    IEnumerator ProcessTwitchCommand(string command)
    {
        if(command.Equals("submit", StringComparison.InvariantCultureIgnoreCase)){      //submit module
            yield return null;
            yield return ModuleSubmit;
        }
        if(command.Equals("reset", StringComparison.InvariantCultureIgnoreCase)){  //reset module
            yield return null;     
            yield return ResetBtn;
        }
        if(command.Contains("cut")){       //wires
            string commfinal=command.Replace("cut ", "");
            string[] digitstring = commfinal.Split(' ');
            int tried;
            int index =1;
            if (int.TryParse(digitstring[0], out tried))
            {
                tried = int.Parse(digitstring[0]);
                if (tried / 10 >= 1)
                {
                    yield return null;
                    yield return "sendtochaterror Please separate the digits by spaces!";
                    yield break;
                }
            }
            foreach (string digit in digitstring){
                if(int.TryParse(digit, out tried)){
                    if(index<=6){
                        tried=int.Parse(digit);
                        index+=1;
                        if(tried==1){
                            yield return null;
                            yield return Wire1Sel;
                            yield return Wire1Sel;
                        }
                        if(tried==2){
                            yield return null;
                            yield return Wire2Sel;
                            yield return Wire2Sel;
                        }
                        if(tried==3){
                            yield return null;
                            yield return Wire3Sel;
                            yield return Wire3Sel;
                        }
                        if(tried==4){
                            yield return null;
                            yield return Wire4Sel;
                            yield return Wire4Sel;
                        }
                        if(tried==5){
                            yield return null;
                            yield return Wire5Sel;
                            yield return Wire5Sel;
                        }
                        if(tried==6){
                            yield return null;
                            yield return Wire6Sel;
                            yield return Wire6Sel;
                        }
                    }
                    else{
                        yield return null;
                        yield return "sendtochaterror Too many digits!";
                        yield break;
                    }
                }
                else{
                    yield return null;
                    yield return "sendtochaterror Digit not valid.";
                    yield break;
                }
            }
        }
        if(command.Contains("press")){  //keypad
            string commfinal=command.Replace("press ", "");
            int tried;
            if(int.TryParse(commfinal, out tried)){
                tried=int.Parse(commfinal);
                if(tried==1){
                    yield return null;
                    yield return KeypadTL;
                    yield return KeypadTL;
                }
                if(tried==2){
                    yield return null;
                    yield return KeypadTR;
                    yield return KeypadTR;
                }
                if(tried==3){
                    yield return null;
                    yield return KeypadBL;
                    yield return KeypadBL;
                }
                if(tried==4){
                    yield return null;
                    yield return KeypadBR;
                    yield return KeypadBR;
                }
            }
            else{
                    yield return null;
                    yield return "sendtochaterror Digit not valid.";
                    yield break;
                }
        }
        //morse
        if(command.Equals("receive")){   
            yield return null;  
            yield return MorseKeyReceive;
            yield return MorseKeyReceive;
        }
        if(command.Contains("send")){
        
        string commfinal=command.Replace("send ", "");
            string[] digitstring = commfinal.Split(' ');
            int tried;
            int index =1;
            if (int.TryParse(digitstring[0], out tried))
            {
                tried = int.Parse(digitstring[0]);
                if (tried / 10 >= 1)
                {
                    yield return null;
                    yield return "sendtochaterror Please separate the digits by spaces!";
                    yield break;
                }
            }
            foreach (string digit in digitstring){
                if(int.TryParse(digit, out tried)){
                    if(index<=4){
                        tried=int.Parse(digit);
                        index+=1;
                        switch (tried){
                            case 1:
                                yield return null;
                                yield return MorseKey1;
                                yield return MorseKey1;
                                break;
                            case 2:
                                yield return null;
                                yield return MorseKey2;
                                yield return MorseKey2;
                                break;
                            case 3:
                                yield return null;
                                yield return MorseKey3;
                                yield return MorseKey3;
                                break;
                            case 4:
                                yield return null;
                                yield return MorseKey4;
                                yield return MorseKey4;
                                break;
                            case 5:
                                yield return null;
                                yield return MorseKey5;
                                yield return MorseKey5;
                                break;
                            case 6:
                                yield return null;
                                yield return MorseKey6;
                                yield return MorseKey6;
                                break;
                            case 7:
                                yield return null;
                                yield return MorseKey7;
                                yield return MorseKey7;
                                break;
                            case 8:
                                yield return null;
                                yield return MorseKey8;
                                yield return MorseKey8;
                                break;
                            case 9:
                                yield return null;
                                yield return MorseKey9;
                                yield return MorseKey9;
                                break;
                            case 0:
                                yield return null;
                                yield return MorseKey0;
                                yield return MorseKey0;
                                break;
                        }

                    }
                    else{
                        yield return null;
                        yield return "sendtochaterror Too many digits!";
                    }
                }
                else{
                    yield return null;
                    yield return "sendtochaterror Digit not valid.";
                }
                
            }
            yield return null;
            yield return MorseKeySend;
            yield return MorseKeySend;
        }
        if(command.Contains("answer")){
            string commfinal=command.Replace("answer ", "");
            string[] digitstring = commfinal.Split(' ');
            int tried;
            int index =1;
            foreach(string digit in digitstring){
                if(int.TryParse(digit, out tried)){
                    if(index<=3){
                        tried=int.Parse(digit);
                        tried+=1;
                        
                        if(tried<=10){
                            if(tried>=1){
                                for(int i=0;i<tried;i++){
                                    if(index==1){
                                        yield return null;
                                        yield return Password1Next;
                                        yield return Password1Next;
                                    }
                                    if(index==2){
                                        yield return null;
                                        yield return Password2Next;
                                        yield return Password2Next;
                                    }
                                    if(index==3){
                                        yield return null;
                                        yield return Password3Next;
                                        yield return Password3Next;
                                    }
                                }
                            }
                        }
                        
                        index+=1;
                    }
                    else{
                        yield return null;
                        yield return "sendtochaterror Too many digits!";
                    }
                }
                else{
                    yield return null;
                    yield return "sendtochaterror Digit not valid.";
                }
            }
            yield return null;
            yield return PasswordSubmit;
            yield return PasswordSubmit;        
        }
        if (command.Contains("scriptinfo"))
        {
            yield return null;
            yield return "sendtochat The renderer's name: " + RendererName;
            yield break;
        }
    }

    public KMSelectable Button1, Button2, Chip;
    public GameObject Module;
    bool Flipped = false;
    protected bool Redacted()
    {
        if (!Flipped)
        {
            Button1.OnInteract = Nothing;
            Button2.OnInteract = Redacted;
            Flipped = true;
        }
        else
        {
            Button1.OnInteract = Redacted;
            Button2.OnInteract = Nothing;
            Flipped = false;
        }
        StartCoroutine(Flip());
        return false;
    }
    protected bool Nothing()
    {
        return false;
    }
    int Rot = 0;
    IEnumerator Flip()
    {
        for (int T = 0; T < 10; T++)
        {
            Rot += 18;
            Module.transform.localEulerAngles = new Vector3(0, 0, Rot);
            yield return new WaitForSecondsRealtime(0.025f);
        }
    }
    int Clicks;
    protected bool HandleClick()
    {
        GetComponent<KMSelectable>().AddInteractionPunch(0.01f);
        Clicks++;
        if (CodeGenerated)
        {
            if (Clicks == 7)
            {
                Debug.LogFormat("[Micro-Modules #{0}] Uh oh, that wasn't supposed to happen.", ModuleID);
                StartCoroutine(Foo());
            }
        }
        return false;
    }
    string Error;
    public KMAudio Glitch;
    IEnumerator Foo()
    {
        Glitch.PlaySoundAtTransform("Sound", transform);
        for (int T = 0; T < 120; T++)
        {
            int ErrorDigit1 = Random.Range(0, 10);
            int ErrorDigit2 = Random.Range(0, 10);
            int ErrorDigit3 = Random.Range(0, 10);
            int ErrorDigit4 = Random.Range(0, 10);
            MorseCodeTxtMsh.text = ErrorDigit1.ToString() + ErrorDigit2.ToString() + ErrorDigit3.ToString() + ErrorDigit4.ToString();

            if (T == 0 || T == 7 || T == 14 || T == 21 || T == 28 || T == 35 || T == 42 || T == 49 || T == 56 || T == 63 || T == 70 || T == 77 || T == 84 || T == 91 || T == 98 || T == 105 || T == 112)
                MorseCodeTxtMsh.color = Color.Lerp(new Color32(255, 0, 0, 255), new Color32(255, 165, 0, 255), 0.01f);
            else if (T == 1 || T == 8 || T == 15 || T == 22 || T == 29 || T == 36 || T == 43 || T == 50 || T == 57 || T == 64 || T == 71 || T == 78 || T == 85 || T == 92 || T == 99 || T == 106 || T == 113)
                MorseCodeTxtMsh.color = Color.Lerp(new Color32(255, 165, 0, 255), new Color32(255, 255, 0, 255), 0.01f);
            else if (T == 2 || T == 9 || T == 16 || T == 23 || T == 30 || T == 37 || T == 44 || T == 51 || T == 58 || T == 65 || T == 72 || T == 79 || T == 86 || T == 93 || T == 100 || T == 107 || T == 114)
                MorseCodeTxtMsh.color = Color.Lerp(new Color32(255, 255, 0, 255), new Color32(0, 255, 0, 255), 0.01f);
            else if (T == 3 || T == 10 || T == 17 || T == 24 || T == 31 || T == 38 || T == 45 || T == 52 || T == 59 || T == 66 || T == 73 || T == 80 || T == 87 || T == 94 || T == 101 || T == 108 || T == 115)
                MorseCodeTxtMsh.color = Color.Lerp(new Color32(0, 255, 0, 255), new Color32(0, 0, 255, 255), 0.01f);
            else if (T == 4 || T == 11 || T == 18 || T == 25 || T == 32 || T == 39 || T == 46 || T == 53 || T == 60 || T == 67 || T == 74 || T == 81 || T == 88 || T == 95 || T == 102 || T == 109 || T == 116)
                MorseCodeTxtMsh.color = Color.Lerp(new Color32(0, 0, 255, 255), new Color32(255, 0, 255, 255), 0.01f);
            else if (T == 5 || T == 12 || T == 19 || T == 26 || T == 33 || T == 40 || T == 47 || T == 54 || T == 61 || T == 68 || T == 75 || T == 82 || T == 89 || T == 96 || T == 103 || T == 110 || T == 117)
                MorseCodeTxtMsh.color = Color.Lerp(new Color32(255, 0, 255, 255), new Color32(255, 20, 147, 255), 0.01f);
            else if (T == 6 || T == 13 || T == 20 || T == 27 || T == 34 || T == 41 || T == 48 || T == 55 || T == 62 || T == 69 || T == 76 || T == 83 || T == 90 || T == 97 || T == 104 || T == 111 || T == 118)
                MorseCodeTxtMsh.color = Color.Lerp(new Color32(255, 20, 147, 255), new Color32(255, 0, 0, 255), 0.01f);
            else
            {
                if (MorseIntCode.ToString().Length == 1)
                {
                    MorseCodeTxtMsh.text = "000" + MorseIntCode;
                }
                else if (MorseIntCode.ToString().Length == 2)
                {
                    MorseCodeTxtMsh.text = "00" + MorseIntCode;
                }
                else if (MorseIntCode.ToString().Length == 3)
                {
                    MorseCodeTxtMsh.text = "0" + MorseIntCode;
                }
                else
                {
                    MorseCodeTxtMsh.text = MorseIntCode.ToString();
                }
                MorseEnteredCode = MorseIntCode.ToString();
                MorseCodeTxtMsh.color = new Color32(0, 255, 0, 255);
            }
               
            yield return new WaitForSecondsRealtime(0.01f);
        }
       
    }
}