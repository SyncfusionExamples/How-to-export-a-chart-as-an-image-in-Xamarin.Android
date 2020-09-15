using Android.App;
using Android.OS;
using Com.Syncfusion.Charts;
using System.Collections.ObjectModel;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using System.IO;

namespace Chart_GettingStarted
{
    [Activity(Label = "Line Chart", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        SfChart chart;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            LinearLayout mainlayout = (LinearLayout)FindViewById(Resource.Id.RootLayout);

            chart = FindViewById(Resource.Id.chart) as SfChart;
            chart.Title.Text = "Chart";
            chart.SetBackgroundColor(Color.White);

            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title.Text = "Name";
            chart.PrimaryAxis = primaryAxis;

            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title.Text = "Height (in cm)";
            chart.SecondaryAxis = secondaryAxis;

            LineSeries series = new LineSeries();

            ViewModel viewModel = new ViewModel();

            series.ItemsSource = viewModel.Data;
            series.XBindingPath = "Name";
            series.YBindingPath = "Height";

            series.DataMarker.ShowLabel = true;
            series.Label = "Heights";
            series.TooltipEnabled = true;

            chart.Series.Add(series);
            chart.Legend.Visibility = Visibility.Visible;

            Button button = (Button)FindViewById(Resource.Id.button);
            button.Text = "Export ";
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            SaveAsImage("ChartImage.jpg");
        }

        public void SaveAsImage(string filename)
        {
            chart.DrawingCacheEnabled = true;
            chart.SaveEnabled = true;
            Bitmap bitmap = null;
            using (bitmap = chart.DrawingCache)
            {
                var extension = filename.Split('.');
                filename = extension.Length == 1 ? filename + ".jpg" : filename;

                var imagePath =
                    new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory + "/Pictures/" + filename);
                imagePath.CreateNewFile();
                using (Stream fileStream = new FileStream(imagePath.AbsolutePath, System.IO.FileMode.OpenOrCreate))
                {
                    try
                    {
                        string imageExtension = extension.Length > 1 ? extension[1].Trim().ToLower() : "jpg";

                        switch (imageExtension)
                        {
                            case "png":
                                bitmap.Compress(Bitmap.CompressFormat.Png, 100, fileStream);
                                break;
                            case "jpg":
                            case "jpeg":
                            default:
                                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, fileStream);
                                break;
                        }
                    }
                    finally
                    {
                        fileStream.Flush();
                        fileStream.Close();
                        chart.DrawingCacheEnabled = false;
                    }
                }
            }
        }
    }
}

