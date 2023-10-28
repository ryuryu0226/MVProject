using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks; 
using Immersal;
using Immersal.REST;
using MVProject.MyREST;
using System.Linq;

public class GetCameraPose : MonoBehaviour
{

    private async UniTask<string> LoginRoutine() 
    {
        SDKLoginRequest loginRequest = new SDKLoginRequest();
        loginRequest.login = System.Environment.GetEnvironmentVariable("email", System.EnvironmentVariableTarget.User);
        loginRequest.password = System.Environment.GetEnvironmentVariable("email_password", System.EnvironmentVariableTarget.User);

        string jsonString = JsonUtility.ToJson(loginRequest);
        SDKLoginResult loginResult = new SDKLoginResult();
        using (UnityWebRequest request = UnityWebRequest.Put(string.Format(ImmersalHttp.URL_FORMAT, "https://api.immersal.com", SDKLoginRequest.endpoint), jsonString))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.useHttpContinue = false;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
            await request.SendWebRequest();

            loginResult = JsonUtility.FromJson<SDKLoginResult>(request.downloadHandler.text);
            if (loginResult.error == "none")
            {
                Debug.Log($"token: {loginResult.token}");
            }
        }

        return loginResult.token;
    }

    async UniTask GetOldCameraPose(string token, int mapId) 
    {
        SDKGetPosesRequest getPosesRequest = new SDKGetPosesRequest();
        getPosesRequest.token = token;
        getPosesRequest.id = mapId;

        string jsonString = JsonUtility.ToJson(getPosesRequest);
        SDKGetPosesReslt getPosesResult = new SDKGetPosesReslt();
        using (UnityWebRequest request = UnityWebRequest.Put(string.Format(ImmersalHttp.URL_FORMAT, "https://api.immersal.com", SDKGetPosesRequest.endpoint), jsonString))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.useHttpContinue = false;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
            await request.SendWebRequest();

            getPosesResult = JsonUtility.FromJson<SDKGetPosesReslt>(request.downloadHandler.text);
            // Debug.Log(request.downloadHandler.text);
            // Debug.Log(getPosesResult.count);
            Debug.Log($"count: {getPosesResult.poses.Count}");
        }
    }

    async void Start()
    {
        string token = await LoginRoutine();
        int mapId = 85113;
        await GetOldCameraPose(token, mapId);
    }
}
