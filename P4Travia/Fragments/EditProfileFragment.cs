using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
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

namespace P4Travia
{
    public class EditProfileFragment : AndroidX.Fragment.App.Fragment
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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.edit_profile, container, false);
            ConnectView(view);
            return view;


            RequestPermissions(permissionGroup, 0);

        }

        private void ConnectView(View view)
        {
            Button saveEditButton = (Button)view.FindViewById(Resource.Id.saveEditButton);
            saveEditButton.Click += Save_Click;

            ImageView editProfilePic = (ImageView)view.FindViewById(Resource.Id.editProfilePic);
            editProfilePic.Click += EditProfilePicture_Click;

            editBioText = (EditText)view.FindViewById(Resource.Id.editBioText);
            editBioText.Text = thisProfile.Bio;

            GetImage(thisProfile.DownloadUrl, editProfilePic);

        }

        void GetImage(string url, ImageView imageView)
        {
            ImageService.Instance.LoadUrl(url)
                .Retry(3, 200)
                .DownSample(400, 400)
                .Into(imageView);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            DocumentReference reference = AppDataHelper.GetFirestore().Collection("users").Document(thisProfile.UserId);
            reference.Update("bio", editBioText.Text);

            string postKey = reference.Id;
            reference.Update("image_id", postKey);

            ShowProgressDialogue("Saving...");

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
                return;
            };

            // Image Upload Failure Callback
            taskCompletionListeners.Failure += (obj, args) =>
            {
                Toast.MakeText(Activity, "Upload was not completed", ToastLength.Short).Show();
            };
        }


            private void EditProfilePicture_Click(object sender, EventArgs e)
            {
                AndroidX.AppCompat.App.AlertDialog.Builder photoAlert = new AndroidX.AppCompat.App.AlertDialog.Builder(Activity);
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
                Toast.MakeText(Activity, "Upload not supported", ToastLength.Short).Show();
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
            progressDialogue.Show(trans, "Saving");
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