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
using Android.Content;
using P4Travia.Fragments;
using Android;
using Firebase.Storage;
using Plugin.Media;
using Android.Graphics;
using Android.Views;

namespace P4Travia.Signup
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Signup6 : AppCompatActivity
    {
        Button signup6;
        FirebaseFirestore database;
        FirebaseAuth mAuth;
        ProgressDialogFragment progressDialogue;
        ImageView profileImage;


        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        TaskCompletionListeners downloadUrlListener = new TaskCompletionListeners();

        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        byte[] fileBytes;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.signup_6);

            signup6 = FindViewById<Button>(Resource.Id.btnNext6);
            signup6.Click += Signup6_Click;

            profileImage = (ImageView)FindViewById(Resource.Id.addProfilePic);
            profileImage.Click += ProfileImage_Click;

            database = AppDataHelper.GetFirestore();
            mAuth = AppDataHelper.GetFirebaseAuth();

            RequestPermissions(permissionGroup, 0);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }


        private void Signup6_Click(object sender, EventArgs e)
        {

            //// info inn i klassen
            Datamodels.UserDataStorage user = new Datamodels.UserDataStorage();
            user.Email = Intent.GetStringExtra("Email");
            user.Password = Intent.GetStringExtra("Password");
            user.Birthday = Intent.GetIntExtra("Birthday", 1000);
            user.UserName = Intent.GetStringExtra("Name");
            user.Nationality = Intent.GetStringExtra("Nationality");
            user.Gender = Intent.GetStringExtra("Gender");
            user.Language = Intent.GetStringArrayListExtra("Language");
            user.Bio = Intent.GetStringExtra("Bio");
            user.Location = Intent.GetStringExtra("Location");

            HashMap userMap = new HashMap();

            userMap.Put("mail", user.Email);
            userMap.Put("birthday", user.Birthday);
            userMap.Put("username", user.UserName);
            userMap.Put("nationality", user.Nationality);
            userMap.Put("gender", user.Gender);
            userMap.Put("language", (Java.Lang.Object)user.Language);
            userMap.Put("bio", user.Bio);
            userMap.Put("location", user.Location);


            // user inn i databasen
            ShowProgressDialogue("Registering...");

            StorageReference storageReference = null;
            if (fileBytes != null)
            {
                storageReference = FirebaseStorage.Instance.GetReference("profileImages/" + postKey);
                storageReference.PutBytes(fileBytes)
                    .AddOnSuccessListener(taskCompletionListeners)
                    .AddOnFailureListener(taskCompletionListeners);
            }

            //mAuth.CreateUserWithEmailAndPassword(user.Email, user.Password).AddOnSuccessListener(this, taskCompletionListeners)
            //    .AddOnFailureListener(this, taskCompletionListeners);


            taskCompletionListeners.Success += (success, args) =>
            {

                DocumentReference userReference = database.Collection("users").Document(mAuth.CurrentUser.Uid);
                string postKey = userReference.Id;
                userMap.Put("image_id", postKey);

                if (storageReference != null)
                {
                    storageReference.GetDownloadUrl().AddOnSuccessListener(downloadUrlListener);
                }

                userReference.Set(userMap);

            };

            // Image Download URL Callback
            downloadUrlListener.Success += (obj, args) =>
            {
                string downloadUrl = args.Result.ToString();
                userMap.Put("download_url", downloadUrl);

                // Save post to Firebase Firestore
                CloseProgressDialogue();
                StartActivity(typeof(MainActivity));
                Finish();

            };

            // Registration Failure Callback
            taskCompletionListeners.Failure += (failure, args) =>
            {
                CloseProgressDialogue();
                Toast.MakeText(this, "Registration Failed : " + args.Cause, ToastLength.Short).Show();
            };

        }

        private void ProfileImage_Click(object sender, EventArgs e)
        {
            AndroidX.AppCompat.App.AlertDialog.Builder photoAlert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            photoAlert.SetMessage("Change Photo");

            photoAlert.SetNegativeButton("Take Photo", (thisalert, args) =>
            {
                // Capture Image
                TakePhoto();
            });

            photoAlert.SetPositiveButton("Upload Photo", (thisAlert, args) =>
            {
                // Choose Image
                SelectPhoto();
            });

            photoAlert.Show();
        }

        async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 20,
                Directory = "Sample",
                Name = GenerateRandomString(6) + "travia.jpg"
            });

            if (file == null)
            {
                return;
            }

            //Converts file.path to byte array and set the resulting bitmap to imageview
            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            fileBytes = imageArray;

            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            profileImage.SetImageBitmap(bitmap);

        }

        async void SelectPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Toast.MakeText(this, "Upload not supported", ToastLength.Short).Show();
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 30,
            });

            if (file == null)
            {
                return;
            }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            fileBytes = imageArray;

            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            profileImage.SetImageBitmap(bitmap);

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

        string GenerateRandomString(int lenght)
        {
            System.Random rand = new System.Random();
            char[] allowchars = "ABCDEFGHIJKLOMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            string sResult = "";
            for (int i = 0; i <= lenght; i++)
            {
                sResult += allowchars[rand.Next(0, allowchars.Length)];
            }

            return sResult;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}