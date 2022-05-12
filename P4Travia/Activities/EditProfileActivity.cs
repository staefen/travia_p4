using Android;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using FFImageLoading;
using Firebase.Firestore;
using Firebase.Storage;
using P4Travia.Datamodels;
using P4Travia.EventListeners;
using P4Travia.Fragments;
using P4Travia.Helpers;
using Plugin.Media;
using System;

namespace P4Travia.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class EditProfileActivity : AppCompatActivity
    {

        UserDataStorage thisProfile;
        ImageView profileImage;
        EditText editBioText;

        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        TaskCompletionListeners downloadUrlListener = new TaskCompletionListeners();

        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        byte[] fileBytes;
        ProgressDialogFragment progressDialogue;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.edit_profile);

            Button saveEditButton = (Button)FindViewById(Resource.Id.saveEditButton);
            ImageView profileImageView = (ImageView)FindViewById(Resource.Id.editProfilePic);
            editBioText = (EditText)FindViewById(Resource.Id.editBioText);
            editBioText.Text = thisProfile.Bio;
            GetImage(thisProfile.DownloadUrl, profileImageView);

            profileImage = (ImageView)FindViewById(Resource.Id.editProfilePic);
            profileImage.Click += ProfileImage_Click;

            saveEditButton.Click += SaveEditButton_Click;

            RequestPermissions(permissionGroup, 0);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }

        void GetImage(string url, ImageView imageView)
        {
            ImageService.Instance.LoadUrl(url)
                .Retry(3, 200)
                .DownSample(400, 400)
                .Into(imageView);
        }

        private void SaveEditButton_Click(object sender, EventArgs e)
        {
            DocumentReference reference = AppDataHelper.GetFirestore().Collection("users").Document(thisProfile.UserId);
            reference.Update("bio", editBioText.Text);

            string postKey = reference.Id;

            reference.Update("image_id", postKey);


            ShowProgressDialogue("Saving...");

            // Save Post Image to Firebase Storaage
            StorageReference storageReference = null;
            if (fileBytes != null)
            {
                storageReference = FirebaseStorage.Instance.GetReference("profileImages/" + postKey);
                storageReference.PutBytes(fileBytes)
                    .AddOnSuccessListener(taskCompletionListeners)
                    .AddOnFailureListener(taskCompletionListeners);
            }

            // Image Upload Success Callback
            taskCompletionListeners.Success += (obj, args) =>
            {
                if (storageReference != null)
                {
                    storageReference.GetDownloadUrl().AddOnSuccessListener(downloadUrlListener);
                }
            };

            // Image Download URL Callback
            downloadUrlListener.Success += (obj, args) =>
            {
                string downloadUrl = args.Result.ToString();
                reference.Update("download_url", downloadUrl);


                CloseProgressDialogue();
                Finish();
            };

            // Image Upload Failure Callback
            taskCompletionListeners.Failure += (obj, args) =>
            {
                Toast.MakeText(this, "Upload was not completed", ToastLength.Short).Show();
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