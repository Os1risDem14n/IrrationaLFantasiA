using System.Collections;
using UnityEngine;
using Firebase.Database;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

public class DatabaseManager : Singleton<DatabaseManager>
{
    //Firebase variables
    [Header("Firebase")] public DependencyStatus dependencyStatus;
    public DatabaseReference DBreference;
    public FirebaseAuth Auth;    
    public FirebaseUser User;
    

    public void InitializeFirebaseDatabase()
    {
        Debug.Log("Setting up Firebase Database");
        //Set the authentication instance object
        Auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = Auth.CurrentUser;
    }

    public void SaveData()
    {
        string userDataJson = UserData.Instance.SaveToJSON();
        DBreference.Child("Users").Child(User.UserId).SetRawJsonValueAsync(userDataJson);
    }

    public IEnumerator LoadData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("Users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            UserData.Instance.InitData();
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            string data = snapshot.GetRawJsonValue();
            UserData.Instance.LoadFromJSON(data);
        }
    }
}

