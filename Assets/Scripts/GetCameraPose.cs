using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Immersal;
using Immersal.REST;

public class GetCameraPose : MonoBehaviour
{
    SDKLoginRequest loginRequest = new SDKLoginRequest();
    
    IEnumerator LoginRoutine() 
    {
        loginRequest.login = System.Environment.GetEnvironmentVariable("email", System.EnvironmentVariableTarget.User);
        loginRequest.password = System.Environment.GetEnvironmentVariable("email_password", System.EnvironmentVariableTarget.User);

        string jsonString = JsonUtility.ToJson(loginRequest);
        using (UnityWebRequest request = UnityWebRequest.Put(string.Format(ImmersalHttp.URL_FORMAT, "https://api.immersal.com", SDKLoginRequest.endpoint), jsonString))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.useHttpContinue = false;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
            yield return request.SendWebRequest();

            SDKLoginResult loginResult = JsonUtility.FromJson<SDKLoginResult>(request.downloadHandler.text);
            if (loginResult.error == "none")
            {
                Debug.Log(loginResult.token);
            }
        }
    }

    void Start()
    {
        StartCoroutine(LoginRoutine());
    }
}
