using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using static Android.Manifest;

namespace fchvpn
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {

        public static MainActivity Instance;


        public ProgressBar progressLoading;
        public TextView loadingInfo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;

            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            if (!CheckPermissionGranted(Permission.Internet))
                this.RequestPermissions(new string[] { Permission.Internet }, 1);
            if (!CheckPermissionGranted(Permission.BindAccessibilityService))
                this.RequestPermissions(new string[] { Permission.BindAccessibilityService }, 1);
            if (!CheckPermissionGranted(Permission.ForegroundService))
                this.RequestPermissions(new string[] { Permission.ForegroundService }, 1);

            progressLoading = FindViewById<ProgressBar>(Resource.Id.progressInfo);
            loadingInfo = FindViewById<TextView>(Resource.Id.loadingInfo);

            startServiceAndroid service = new startServiceAndroid();
            service.StartForegroundServiceCompat();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool CheckPermissionGranted(string perm)
        {
            try
            {
                return (this.CheckSelfPermission(perm) == Android.Content.PM.Permission.Granted);
            }
            catch{}
            return false;
        }

    }
}