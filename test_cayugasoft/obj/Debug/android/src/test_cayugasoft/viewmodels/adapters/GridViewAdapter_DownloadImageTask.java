package test_cayugasoft.viewmodels.adapters;


public class GridViewAdapter_DownloadImageTask
	extends android.os.AsyncTask
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_doInBackground:([Ljava/lang/Object;)Ljava/lang/Object;:GetDoInBackground_arrayLjava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("test_cayugasoft.ViewModels.Adapters.GridViewAdapter/DownloadImageTask, test_cayugasoft, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GridViewAdapter_DownloadImageTask.class, __md_methods);
	}


	public GridViewAdapter_DownloadImageTask () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GridViewAdapter_DownloadImageTask.class)
			mono.android.TypeManager.Activate ("test_cayugasoft.ViewModels.Adapters.GridViewAdapter/DownloadImageTask, test_cayugasoft, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.Object doInBackground (java.lang.Object[] p0)
	{
		return n_doInBackground (p0);
	}

	private native java.lang.Object n_doInBackground (java.lang.Object[] p0);

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
