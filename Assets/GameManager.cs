using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_InputField userInput;
    [SerializeField] AudioSource soundPlayer;
    [SerializeField] AudioClip textSound;
    [SerializeField] AudioClip typingSound;
    [SerializeField] bool loggedIn;
    [SerializeField] bool firstTime;
    [SerializeField] bool connectedToServer; //this boolean is for connection to a dedicated game server
    [SerializeField] bool restarting;
    string command;
    string commandAttributes;


    List<string> storedText = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        loggedIn = false;
        OnPowerOn();
      
    }

    // Update is called once per frame
    void Update()
    {
        OnEnterPress();
    }

    void OnPowerOn()
    {
        StartCoroutine(OnPowerUp(.5f));
        if(firstTime == true)
        {
            Register();
        }
    }

    void OnEnterPress()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            soundPlayer.PlayOneShot(textSound);
            text.text += "\n" + "nroot64>: " + userInput.text;

            ReadCommand();

            userInput.text = "";
            userInput.ActivateInputField();
            userInput.Select();
        }
    }

    void ConnectToServer(string ip, string port, string password)
    {

    }

    void Register()
    {

    }

    void ReadCommand()
    {
        storedText.Clear();
        string firstWord = userInput.text.Split(' ').First();
        command = firstWord;

        commandAttributes = userInput.text.Remove(0, command.Length);
        Debug.Log(commandAttributes);

        Debug.Log(command + " " + firstWord);

        if (loggedIn == false)
        {
            if (userInput.text == "admin")
            {
                storedText.Add("[admin] logged in");
                storedText.Add("access rights <all>");
                storedText.Add("enter command");
                loggedIn = true;
                StartCoroutine(DelayText(1));
            }
            else
            {
                storedText.Add("<unrecognized username [0000x00001]>");
                storedText.Add("<log in to execute commands [0000x00002]>");
                storedText.Add("<type username to log in [0000x00003]>");

                StartCoroutine(DelayText(1));
            }
        }
        else
        {
            switch (command)
            {
                case "help":
                    storedText.Add("< [execute]   initiates a process stated by the user>");
                    storedText.Add("< [connect]   initiates a connection to the desiganted location>");
                    storedText.Add("< [setting]   sets a new terminal UI setting state>");
                    storedText.Add("< [show]      shows associated atributes to the written attribute>");
                    storedText.Add("< [terminate] terminates a process invoked by a user>");
                    storedText.Add("< [open]      opens directorys or ports>");
                    storedText.Add("< [close]     closes files, directorys or ports>");
                    storedText.Add("< [tstate]    scans terminal completly and displays the results>");
                    storedText.Add("< [netconf]   returns detailed information about your current network adapter connection>");
                    storedText.Add("< [clrscr]    clears the screen from all text>");
                    storedText.Add("< [rstt]      restarts the terminal>");
                    storedText.Add("< [scannet]   scans the to connected network and displays all devices found online>");
                    StartCoroutine(DelayText(.5f));
                    break;

                case "execute":
                    storedText.Add("<no process attached to the execute command [0000x00005]>");
                    StartCoroutine(DelayText(1));
                    break;

                case "connect":
                    storedText.Add("<no location attached to the connect command [0000x00006]>");
                    storedText.Add("<connection to a network requires data such as network ip, network port and network password>");
                    storedText.Add("<e.g. 'connect 10.107.32.3, 1044, password1'>");
                    StartCoroutine(DelayText(1));
                    break;

                case "setting":
                    storedText.Add("<no attribute attached to the setting command [0000x00007]>");
                    StartCoroutine(DelayText(1));
                    break;

                case "show":
                    storedText.Add("<no attribute attached to the show command [0000x00008]>");
                    StartCoroutine(DelayText(1));
                    break;

                case "terminate":
                    storedText.Add("<no process attached to the terminate command [0000x00009]>");
                    StartCoroutine(DelayText(1));
                    break;

                case "open":
                    storedText.Add("<no attribute attached to the open command [0000x00010]>");
                    StartCoroutine(DelayText(1));
                    break;

                case "close":
                    storedText.Add("<no attribute attached to the close command [0000x00011]>");
                    StartCoroutine(DelayText(1));
                    break;

                case "clrscr":
                    text.text = "";
                    storedText.Add("");
                    StartCoroutine(DelayText(1));
                    break;

                case "rstt":
                    loggedIn = false;
                    restarting = true;
                    storedText.Add("restarting terminal kernel...");
                    storedText.Add("restarting terminal kernel...");
                    storedText.Add("restarting terminal kernel...");
                    storedText.Add("restarting terminal kernel...");
                    storedText.Add("restarting terminal kernel...");
                    storedText.Add("restarting terminal kernel...");
                    storedText.Add("restarting terminal kernel...");
                    storedText.Add("restarting terminal kernel...");
                    StartCoroutine(DelayText(1));

                    break;

                case "netconf":
                    storedText.Add("LAN/WLAN adapter");
                    storedText.Add("hostname..........H92JSD-terminal");
                    storedText.Add("physical address...R8-KS-29-J5-F1-J2");
                    storedText.Add("autoconfig.........on");
                    storedText.Add("static ip..........yes");
                    storedText.Add("ipv4...............192.168.0.4");
                    StartCoroutine(DelayText(2f));
                    break;

                case "tstate":

                    storedText.Add("<initiating component scans...>");
                    storedText.Add("<CPU...>");
                    storedText.Add("<RAM...>");
                    storedText.Add("<ports...>");
                    storedText.Add("<network activity...>");
                    storedText.Add("<discs...>");
                    storedText.Add("<os file integirty...>");
                    storedText.Add("<devices...>");
                    storedText.Add("<general errors...>");
                    storedText.Add("<finishing...>");
                    storedText.Add("\n");

                    storedText.Add("\nCPU [Asterixx2 @350 hZ]\n    temperature: "+Random.Range(40,100)+ "°C\n    usage: " + Random.Range(0, 100) + "%\n    clock: " + Random.Range(50, 350) + " hZ\n    turbo: off\n");
                    storedText.Add("\nRAM [Phoenix RSD 16mb @100 hZ]\n    temperature: " + Random.Range(35, 75) + "°C\n    usage: " + Random.Range(0, 16) + "mb\n    clock: " + Random.Range(10, 100) + " hZ\n");
                    storedText.Add("\nports\n    port[1] closed \n    port[2] closed\n    port[3] closed\n    port[4] open\n");
                    storedText.Add("<no other errors found>\n");
                    StartCoroutine(DelayText(2f));

                    break;

                default:
                    storedText.Add("<unrecognized command [0000x0004]>");
                    StartCoroutine(DelayText(1));
                    break;
            }
        }
    }

    IEnumerator DelayText(float time)
    {
        for (int i = 0; i < storedText.Count; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, time));
            soundPlayer.PlayOneShot(textSound);
            text.text += "\n" + "nroot64>: " + storedText[i];

            if(i==storedText.Count-1 && restarting == true)
            {
                OnPowerOn();
            }
        }
    }

    IEnumerator OnPowerUp(float time)
    {
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> starting kernel subroutine x00064 POST\n" +
    "__________________________________________________________________________________________________ \n" +
    "|    _________  ________  _______     ____    ____  _____  ____  _____       _       _____       | \n" +
    "|   |  _   _  ||_   __  ||_   __ \\   |_   \\  /   _||_   _||_   \\|_   _|     / \\     |_   _|      | \n" +
    "|   |_/ | | \\_|  | |_ \\_|  | |__) |    |   \\/   |    | |    |   \\ | |      / _ \\      | |        | \n" +
    "|       | |      |  _| _   |  __ /     | |\\  /| |    | |    | |\\ \\| |     / ___ \\     | |   _    | \n" +
    "|      _| |_    _| |__/ | _| |  \\ \\_  _| |_\\/_| |_  _| |_  _| |_\\   |_  _/ /   \\ \\_  _| |__/  |  | \n" +
    "|     |_____|  |________||____| |___||_____||_____||_____||_____|\\____||____| |____||________|   | \n" +
    "|________________________________________________________________________________________________|";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> executing x00004...@<driver\\sroot\\krnlsys.exe>";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> <driver\\sroot\\krnlsys.exe> [running x00004]";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> executing x00016...@<driver\\sroot\\krnlos.exe>";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> <driver\\sroot\\krnlos.exe> [running x00016]";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> executing x00032...@<driver\\sroot\\krnlboot.exe>";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> <driver\\sroot\\krnlboot.exe> [running x00032]";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> loading x00064n01...@<sysfile\\strt\\sydrive.mss>";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> <sysfile\\strt\\sydrive.mss> [state: loaded x00064n01]";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> loading x00064n02...@<sysfile\\strt\\sydev.mss>";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> <sysfile\\strt\\sydev.mss> [state: loaded x00064n02]";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> loading x00064n03...@<sysfile\\strt\\synet.mss>";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> <sysfile\\strt\\synet.mss> [state: failed on load]";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> terminal POST successfull";

        yield return new WaitForSeconds(time);
        soundPlayer.PlayOneShot(textSound);
        text.text += "\nnroot64:> type username to log in";

        userInput.ActivateInputField();
        userInput.Select();

    }
}
