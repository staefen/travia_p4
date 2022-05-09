using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Firestore;
using System;
using DocumentReference = Firebase.Firestore.DocumentReference;
using Java.Util;
using Firebase.Auth;
using P4Travia.EventListeners;
//using P4Travia.Helpers;
using Android.Runtime;
using Android.Content;


namespace P4Travia.Signup
{
    [Activity(Label = "Signup6")]
    public class Signup6 : Activity
    {
        Button signup6;
       // FirebaseFirestore database;
       // FirebaseAuth mAuth;
        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        EditText locationText;
        string location;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.signup_6);

            signup6 = FindViewById<Button>(Resource.Id.btnNext6);
            signup6.Click += Signup6_Click;

            locationText = (EditText)FindViewById(Resource.Id.signuplocation);

 //           database = AppDataHelper.GetFirestore();
 //           mAuth = AppDataHelper.GetFirebaseAuth();
        }


        private void Signup6_Click(object sender, EventArgs e)
        {
            location = locationText.Text;

            // info inn i klassen
            Datamodels.UserDataStorage user = new Datamodels.UserDataStorage();
            user.Email = Intent.GetStringExtra("Email");
            user.Password = Intent.GetStringExtra("Password");
            user.Birthday = Intent.GetIntExtra("Birthday", 1000);
            user.UserName = Intent.GetStringExtra("Name");
            user.Nationality = Intent.GetStringExtra("Nationality");
            user.Gender = Intent.GetStringExtra("Gender");
            user.Language = Intent.GetStringExtra("Language");
            user.Bio = Intent.GetStringExtra("Bio");
            user.Location = location;

            // user inn i databasen
            /*  mAuth.CreateUserWithEmailAndPassword(user.Email, user.Password).AddOnSuccessListener(this, taskCompletionListeners)
                 .AddOnFailureListener(this, taskCompletionListeners);

              taskCompletionListeners.Success += (success, args) =>
              {
                  HashMap userMap = new HashMap();
                  userMap.Put("mail", user.Email);
                  userMap.Put("birthday", user.Birthday);
                  userMap.Put("username", user.UserName);
                  userMap.Put("nationality", user.Nationality);
                  userMap.Put("gender", user.Gender);
                  userMap.Put("language", user.Language);
                  userMap.Put("bio", user.Bio);

                  DocumentReference userReference = database.Collection("users").Document(mAuth.CurrentUser.Uid);
                  userReference.Set(userMap);

                  StartActivity(typeof(MainActivity));
                  Finish();
              };
              // Registration Failure Callback
              taskCompletionListeners.Failure += (failure, args) =>
              {
                  Toast.MakeText(this, "Registration Failed : " + args.Cause, ToastLength.Short).Show();
              };
            */
          }
           

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}