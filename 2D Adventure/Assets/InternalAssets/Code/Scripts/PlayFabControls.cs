using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayFabControls : MonoBehaviour
{
    [SerializeField] GameObject signUpTab, logInTab, backupTab,startPanel, HUD;
    public TextMeshProUGUI username, userEmail, userPassword, userEmailLogIn, userPasswordLogIn, errorSignUp, errorLogIn, emailBackup;
    string encryptedPassword;


    private void Start()
    {
        errorLogIn.gameObject.SetActive(false);
        errorSignUp.gameObject.SetActive(false);
    }

    public void SwitchToSignUpTab()
    {
        signUpTab.SetActive(true);  
        logInTab.SetActive(false);
        errorSignUp.text = "";
        errorLogIn.text = "";
    }

    public void SwitchToLogInTab() 
    {
        signUpTab.SetActive(false);
        logInTab.SetActive(true);
        errorSignUp.text = "";
        errorLogIn.text = "";
    }

    public void SwitchToBackupTab()
    {
        signUpTab.SetActive(false);
        logInTab.SetActive(false);
        backupTab.SetActive(true);
    }


    string Encrypt(string pass)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach(byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString();
    }

    public void SignUp()
    {
        Debug.Log(userEmail.text + username.text);
        var usernameNoWhiteSpace = username.text.Remove(username.text.Length - 1);
        var registerRequest = new RegisterPlayFabUserRequest{Email = userEmail.text, Password = Encrypt(userPassword.text), Username = usernameNoWhiteSpace};
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSuccess, RegisterError);
        
    }

    public void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log(result.ToString());
        errorSignUp.text = "";
        errorLogIn.text = "";
        StartGame();
    }

    public void RegisterError(PlayFabError error)
    {
        errorSignUp.text = error.GenerateErrorReport();
        Debug.Log(error.ErrorDetails);
        errorSignUp.gameObject.SetActive(true);
    }

    public void LogIn()
    {
        var request = new LoginWithEmailAddressRequest { Email = userEmailLogIn.text, Password = Encrypt(userPasswordLogIn.text) };
        PlayFabClientAPI.LoginWithEmailAddress(request, LogInSuccess, LogInSuccess);
    }

    public void LogInSuccess(LoginResult result)
    {
        errorSignUp.text = "";
        errorLogIn.text = "";
        HUD.SetActive(true);
        StartGame();
    }

    /*
    public void LogInSuccess(PlayFabError error)
    {
        //errorLogIn.text = error.GenerateErrorReport();
        errorLogIn.gameObject.SetActive(true);
    }
    */
    private void LogInSuccess(PlayFabError error)
    {
        StartCoroutine(ShowErrorAndHide(error));
    }



    private IEnumerator ShowErrorAndHide(PlayFabError error)
    {
        float duration = 3f;



        // Attiva il Canvas
        errorLogIn.gameObject.SetActive(true);



        // Attendere per la durata specificata
        yield return new WaitForSeconds(duration);



        // Disattiva il Canvas dopo il ritardo
        errorLogIn.gameObject.SetActive(false);
    }

    void StartGame()
    {
        startPanel.SetActive(false);
        HUD.SetActive(true);
    }

    public void StartScene()
    {
        SceneManager.LoadScene("Menu");
    }

     public void RecoverPassword()
     {
         var request = new SendAccountRecoveryEmailRequest
         {
             Email = emailBackup.text,
             TitleId = "9D448",
         };
         PlayFabClientAPI.SendAccountRecoveryEmail(request, ResultCallback, ErrorCallback);
     }
     private void ErrorCallback(PlayFabError error)
     {
         Debug.LogError(error.ErrorMessage);
     }

     private void ResultCallback(SendAccountRecoveryEmailResult result)
     {
         Debug.Log("Please check your email");
     }

    public void SwitchToLogInTab2()
    {
        backupTab.SetActive(false);
        logInTab.SetActive(true);
        errorSignUp.text = "";
        errorLogIn.text = "";
    }

}
