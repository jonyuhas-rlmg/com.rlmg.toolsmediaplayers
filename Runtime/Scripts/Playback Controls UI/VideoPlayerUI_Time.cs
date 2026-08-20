using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;
using TMPro;

namespace rlmg.Tools.MediaPlayers
{
	/// <summary>
	/// Timecode text UI display for video player.
	/// </summary>
	public class VideoPlayerUI_Time : VideoPlayerUI_Base
	{
		public Action<double,double> OnUpdateTime;

		/// <summary>
		/// Timecode text UI display for video player.
		/// </summary>
		public string formatting = "{0}:{1:00} | {2}:{3:00}";

		/// <summary>
		/// If true, text string will be wrapped with TMP monospace markup.
		/// </summary>
		public bool doMonospaceTMP = true;

		/// <summary>
		/// The em spacing used by the TMP monospace markup.
		/// </summary>
		public float monospacing = 2.75f;

		/// <summary>
		/// Legacy UI text for output
		/// </summary>
		private Text _textLegacy;

		/// <summary>
		/// TMP text for output
		/// </summary>
		private TMP_Text _textTMP;

		private StringBuilder _timeSB;
		private int minDur, minPos, secDur, secPos;

		protected override void Start()
		{
			base.Start();

			_textLegacy = GetComponent<Text>();
			_textTMP = GetComponent<TMP_Text>();
			_timeSB  = new StringBuilder();

			OnUpdateTime = new Action<double, double>((pos, dur) => 
			{
				_timeSB.Length = 0;
				_timeSB.Capacity = 0;

				try
				{
					minPos = (int)Mathf.Floor(Convert.ToInt32(pos) / 60f);
					secPos = (int)(Convert.ToInt32(pos) - (minPos * 60));

					minDur = (int)Mathf.Floor(Convert.ToInt32(dur) / 60f);
					secDur = (int)(Convert.ToInt32(dur) - (minDur * 60));
				}
				catch (Exception)
				{

				};

				_timeSB.AppendFormat(formatting, minPos, secPos, minDur, secDur);

				if (_textLegacy != null)
				{
					_textLegacy.text = _timeSB.ToString();
				}

				if (_textTMP != null)
				{
					if (doMonospaceTMP)
					{
						_textTMP.text = "<mspace=" + monospacing + "em>" + _timeSB.ToString() + "</mspace>";

					}
					else
					{
						_textTMP.text = _timeSB.ToString();
					}
				}
			});
		}

		void Update()
		{
			if (OnUpdateTime != null)
			{
				OnUpdateTime(player.time, Duration);
			}
		}
	}
}