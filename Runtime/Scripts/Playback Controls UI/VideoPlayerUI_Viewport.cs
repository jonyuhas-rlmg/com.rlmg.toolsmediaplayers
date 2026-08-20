using UnityEngine.UI;

namespace rlmg.Tools.MediaPlayers
{
	/// <summary>
	/// Raw image display output for video player.
	/// </summary>
	public class VideoPlayerUI_Viewport : VideoPlayerUI_Base
	{
		private RawImage viewportImage;

		protected override void Start()
		{
			base.Start();

			viewportImage = GetComponent<RawImage>();

			if (viewportImage != null && player != null && player.targetTexture != null)
			{
				//todo: generate render texture at same dimensions as video if one doesn't already exist

				viewportImage.texture = player.targetTexture;
			}
		}
	}
}