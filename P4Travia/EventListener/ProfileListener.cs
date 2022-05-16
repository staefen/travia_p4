using Android.Gms.Tasks;
using Android.Runtime;
using P4Travia.Datamodels;
using P4Travia.Helpers;
using Firebase.Firestore;
using System;
using System.Collections.Generic;

namespace P4Travia.EventListeners
{
    public class ProfileListener : Java.Lang.Object, IOnSuccessListener, IEventListener
    {
        public List<UserDataStorage> listOfList = new List<UserDataStorage>();

        public event EventHandler<ProfileEventArgs> OnProfileRetrieved;

        public class ProfileEventArgs : EventArgs
        {
            public List<UserDataStorage> userList { get; set; }
        }

        //  public DocumentSnapshot item;

        public void FetchProfile()
        {
           AppDataHelper.GetFirestore().Collection("users").AddSnapshotListener(this);
        }

        public void RemoveProfileListener()
        {
            var listener = AppDataHelper.GetFirestore().Collection("users").AddSnapshotListener(this);
            listener.Remove();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            OrganizeData(result);
        }

        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            OrganizeData(value);
        }

        void OrganizeData(Java.Lang.Object Value)
        {
            var snapshot = (QuerySnapshot)Value;

                foreach (DocumentSnapshot item in snapshot.Documents)
                {
                    UserDataStorage userCopy = new UserDataStorage();
                    userCopy.UserId = item.Id;

            //Hvis en bruger ikke har mangler en eller flere parametre til en post, kan vi risikere at appen crasher
            //Det kan vi håndtere med følgende:

                    userCopy.UserName = item.Get("username") != null ? item.Get("username").ToString() : "";
                    userCopy.Email = item.Get("email") != null ? item.Get("email").ToString() : "";
                    userCopy.Nationality = item.Get("nationality") != null ? item.Get("nationality").ToString() : "";


                    //Efter al data er hentet fra databasen, smider vi det ind i en list
                    listOfList.Add(userCopy);
                }

                OnProfileRetrieved?.Invoke(this, new ProfileEventArgs { userList = listOfList });
            }

        }
    }