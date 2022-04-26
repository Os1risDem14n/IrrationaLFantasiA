using UnityEngine;
using Firebase.Database;
using Firebase;
using Firebase.Auth;

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
        string userDataJson = JsonUtility.ToJson(UserData.Instance);
        DBreference.Child("Users").Child(User.UserId).SetRawJsonValueAsync(userDataJson);
    }
}

