using System.Net;
using DisplayPDF;
using DisplayPDF.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer (typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace DisplayPDF.Droid
{
	public class CustomWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<WebView> e)
		{
			base.OnElementChanged (e);

			if (e.NewElement != null) {
				var customWebView = Element as CustomWebView;
				Control.Settings.AllowUniversalAccessFromFileURLs = true;

				//1 From LIB
				Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}",
												string.Format("file:///android_asset/Content/{0}",
														   WebUtility.UrlEncode(customWebView.Uri))));
				//2 Custom Connect with URL for Display PDF
				//Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}",
				//                              string.Format("{0}",
				//                             WebUtility.UrlEncode(customWebView.Uri))));

				//3 Save and Display
				//this.SaveAndDisplay(customWebView.Uri);
			}
		}
		private void SaveAndDisplay(string Uri)
		{
			string pdfPath = this.SavePdf("test.pdf", Uri);
			Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}",
										  string.Format("{0}", WebUtility.UrlEncode(pdfPath))));
		}

		private string SavePdf(string filename, string link)
		{
			WebClient client = new WebClient();
			string documentsPath = System.IO.Path.Combine((Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)).Path, filename);
			client.DownloadFile(link, documentsPath);

			return documentsPath;

		}
	}
}
