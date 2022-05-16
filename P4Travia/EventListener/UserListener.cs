using Android.Gms.Tasks;
using P4Travia.Helpers;
using Firebase.Firestore;
namespace P4Travia.EventListeners
{
    public class UserListener : Java.Lang.Object, IOnSuccessListener
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
                string username = snapshot.Get("username").ToString();
                AppDataHelper.SaveName(username);
            }
        }
    }
}