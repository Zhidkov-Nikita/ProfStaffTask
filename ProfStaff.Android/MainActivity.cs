using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using System.Threading.Tasks;
using ProfStaff.Droid;

[Activity(Label = "Дерево локаций", MainLauncher = true)]
public class MainActivity : AppCompatActivity
{
    private RecyclerView _recyclerView;
    private ProgressBar _progressBar;

    protected override async void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.main);

        _recyclerView = FindViewById<RecyclerView>(Resource.Id.locations_recycler_view);
        _progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar);
        _recyclerView.SetLayoutManager(new LinearLayoutManager(this));

        await LoadLocations();
    }

    private async Task LoadLocations()
    {
        try
        {
            _progressBar.Visibility = ViewStates.Visible;

            var apiService = new ApiService();
            var locations = await apiService.GetLocationsAsync();

            _recyclerView.SetAdapter(new LocationTreeAdapter(locations));
        }
        finally
        {
            _progressBar.Visibility = ViewStates.Gone;
        }
    }
}