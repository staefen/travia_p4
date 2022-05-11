using Android.App;
using Android.OS;
using Android.Widget;
using Firebase.Firestore;
using System;
using AndroidX.AppCompat.App;
using DocumentReference = Firebase.Firestore.DocumentReference;
using Java.Util;
using Firebase.Auth;
using P4Travia.EventListeners;
using P4Travia.Helpers;
using Android.Runtime;
using Android.Content;
using P4Travia.Fragments;


namespace P4Travia.Signup
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Signup6 : AppCompatActivity
    {
        Button signup6;
        FirebaseFirestore database;
        FirebaseAuth mAuth;
        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        ProgressDialogFragment progressDialogue;
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

            database = AppDataHelper.GetFirestore();
            mAuth = AppDataHelper.GetFirebaseAuth();
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
            ShowProgressDialogue("Registering...");
            mAuth.CreateUserWithEmailAndPassword(user.Email, user.Password).AddOnSuccessListener(this, taskCompletionListeners)
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
                  CloseProgressDialogue();
                  StartActivity(typeof(Activities.ViewProfile));
                  Finish();
              };


              // Registration Failure Callback
              taskCompletionListeners.Failure += (failure, args) =>
              {
                  CloseProgressDialogue();
                  Toast.MakeText(this, "Registration Failed : " + args.Cause, ToastLength.Short).Show();
              };
            
          }


        void ShowProgressDialogue(string status)
        {
            progressDialogue = new ProgressDialogFragment(status);
            var trans = SupportFragmentManager.BeginTransaction();
            progressDialogue.Cancelable = false;
            progressDialogue.Show(trans, "Progress");
        }

        void CloseProgressDialogue()
        {
            if (progressDialogue != null)
            {
                progressDialogue.Dismiss();
                progressDialogue = null;
            }
        }
    }
}