package test_cayugasoft.viewmodels.activities;


public class ImageContainerActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("test_cayugasoft.ViewModels.Activities.ImageContainerActivity, test_cayugasoft, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ImageContainerActivity.class, __md_methods);
	}


	public ImageContainerActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ImageContainerActivity.class)
			mono.android.TypeManager.Activate ("test_cayugasoft.ViewModels.Activities.ImageContainerActivity, test_cayugasoft, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
