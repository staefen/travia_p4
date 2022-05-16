using Android.OS;
using Android.Views;

namespace P4Travia
{
    public class MainPageFragment : AndroidX.Fragment.App.Fragment
    {

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.mainpage, container, false);
            return view;
        }
    }
}