package crc64707a1cf70e1e4d41;


public class Signup4
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("P4Travia.Signup.Signup4, P4Travia", Signup4.class, __md_methods);
	}


	public Signup4 ()
	{
		super ();
		if (getClass () == Signup4.class)
			mono.android.TypeManager.Activate ("P4Travia.Signup.Signup4, P4Travia", "", this, new java.lang.Object[] {  });
	}


	public Signup4 (int p0)
	{
		super (p0);
		if (getClass () == Signup4.class)
			mono.android.TypeManager.Activate ("P4Travia.Signup.Signup4, P4Travia", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
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
