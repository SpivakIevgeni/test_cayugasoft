package test_cayugasoft.viewmodels.activities;


public class GalleryActivity_DownloadImageTask
	extends android.os.AsyncTask
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onPreExecute:()V:GetOnPreExecuteHandler\n" +
			"n_doInBackground:([Ljava/lang/Object;)Ljava/lang/Object;:GetDoInBackground_arrayLjava_lang_Object_Handler\n" +
			"n_onPostExecute:(Ljava/lang/Object;)V:GetOnPostExecute_Ljava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("test_cayugasoft.ViewModels.Activities.GalleryActivity/DownloadImageTask, test_cayugasoft, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GalleryActivity_DownloadImageTask.class, __md_methods);
	}


	public GalleryActivity_DownloadImageTask () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GalleryActivity_DownloadImageTask.class)
			mono.android.TypeManager.Activate ("test_cayugasoft.ViewModels.Activities.GalleryActivity/DownloadImageTask, test_cayugasoft, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public GalleryActivity_DownloadImageTask (android.widget.ImageView p0, android.widget.ProgressBar p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == GalleryActivity_DownloadImageTask.class)
			mono.android.TypeManager.Activate ("test_cayugasoft.ViewModels.Activities.GalleryActivity/DownloadImageTask, test_cayugasoft, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Widget.ImageView, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Widget.ProgressBar, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public void onPreExecute ()
	{
		n_onPreExecute ();
	}

	private native void n_onPreExecute ();


	public java.lang.Object doInBackground (java.lang.Object[] p0)
	{
		return n_doInBackground (p0);
	}

	private native java.lang.Object n_doInBackground (java.lang.Object[] p0);


	public void onPostExecute (java.lang.Object p0)
	{
		n_onPostExecute (p0);
	}

	private native void n_onPostExecute (java.lang.Object p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
