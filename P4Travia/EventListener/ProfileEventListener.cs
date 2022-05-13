﻿using Android.Gms.Tasks;
using P4Travia.Helpers;
using Firebase.Firestore;
namespace P4Travia.EventListeners
{
    public class ProfileEventListener : Java.Lang.Object, IOnSuccessListener
    {
        public void FetchUser()
        {
            AppDataHelper.GetFirestore().Collection("users").Document(AppDataHelper.GetFirebaseAuth().CurrentUser.Uid).Get()
                .AddOnSuccessListener(this);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            DocumentSnapshot snapshot = (DocumentSnapshot)result;
            if (snapshot.Exists())
            {
                string name = snapshot.Get("name").ToString();
                AppDataHelper.SaveName(name);
            }
        }
    }
}