package crc64707a1cf70e1e4d41;


public class SignupAndLogin
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onRequestPermissionsResult:(I[Ljava/lang/String;[I)V:GetOnRequestPermissionsResult_IarrayLjava_lang_String_arrayIHandler\n" +
			"";
		mono.android.Runtime.register ("P4Travia.Signup.SignupAndLogin, P4Travia", SignupAndLogin.class, __md_methods);
	}


	public SignupAndLogin ()
	{
		super ();
		if (getClass () == SignupAndLogin.class)
			mono.android.TypeManager.Activate ("P4Travia.Signup.SignupAndLogin, P4Travia", "", this, new java.lang.Object[] {  });
	}


	public SignupAndLogin (int p0)
	{
		super (p0);
		if (getClass () == SignupAndLogin.class)
			mono.android.TypeManager.Activate ("P4Travia.Signup.SignupAndLogin, P4Travia", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2)
	{
		n_onRequestPermissionsResult (p0, p1, p2);
	}

	private native void n_onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2);

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
