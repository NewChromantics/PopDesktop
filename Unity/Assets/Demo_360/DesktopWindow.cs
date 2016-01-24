using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DesktopWindow : MonoBehaviour {

	private string				mFilename;
	PopMovie					mMovie;
	[Range(1,2048)]
	public int					mTextureWidth = 1024;
	[Range(1,2048)]
	public int					mTextureHeight = 1024;
	private Texture				mTexture;

	private string				mRawImageChildName = "RawImage";
	private string				mTextChildName = "PlayButtonText";


	void AssignTexture()
	{
		var TextChild = transform.FindChild(mRawImageChildName);
		if ( TextChild == null )
		{
			Debug.LogWarning("DesktopWindow: no child named " + mRawImageChildName + ", on " + this.name );
			return;
		}

		var Image = TextChild.GetComponent<RawImage>();
		if ( Image != null )
		{
			Image.texture = mTexture;
		}


	}

	void SetText(string TextString)
	{
		//	setup gui
		var TextChild = transform.FindChild(mTextChildName);
		if ( TextChild == null )
		{
			Debug.LogWarning("DesktopWindow: " + TextString );
			return;
		}

		var Text = TextChild.GetComponent<Text>();
		if ( Text != null )
		{
			Text.text = TextString;
		}

	}

	void ShowError(string Error)
	{
		SetText( Error );
	}

	public void SetFilename(string Filename)
	{
		mFilename = Filename;

		//	init
		try
		{
			PopMovieParams Params = new PopMovieParams();
			mMovie = new PopMovie( Filename, Params, true );
		}
		catch(System.Exception e)
		{
			mMovie = null;
			ShowError( e.Message );
		}
	
		mTexture = new RenderTexture( mTextureWidth, mTextureHeight, 0 );
		AssignTexture();
	}

	void Update()
	{
		if ( mMovie != null )
		{
			mMovie.Update();
			mMovie.UpdateTexture( mTexture );
		}
	}


}
