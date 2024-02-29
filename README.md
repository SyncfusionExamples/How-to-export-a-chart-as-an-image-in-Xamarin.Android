# How to export a chart as an image in Xamarin.Android

This article explains how to export the [Syncfusion Xamarin.Android SfChart](https://help.syncfusion.com/xamarin-android/sfchart/getting-started) as an image. 

The SfChart image has been saved inside the Pictures directory of the deployed device. To save the chart as an image as per in the below code snippet.

[C#]

```

public void SaveAsImage(string filename) 
{ 
    chart.DrawingCacheEnabled = true; 
    chart.SaveEnabled = true; 
    Bitmap bitmap = null; 
    using (bitmap = chart.DrawingCache) 
    { 
        var extension = filename.Split('.'); 
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
                    case "jpg":  case "jpeg": default: 
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

```

In order to save the image in Android, you have to enable the permission in the Android.Manifest file for device storage.

[XML]

```

<manifest xmlns:android="http://schemas.android.com/apk/res/android" 
          android:versionCode="1" 
          android:versionName="1.0" 
          package="ExportChartAsImage.ExportChartAsImage">

          …

	<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
</manifest>

```

## See Also

[How to export chart to PDF in Xamarin.Android](https://www.syncfusion.com/kb/9370/how-to-export-chart-to-pdf-in-xamarin-android)

[What are the types of SfChart](https://help.syncfusion.com/xamarin-android/sfchart/charttypes)



