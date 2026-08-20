using UnityEngine;
using UnityEngine.UI;

namespace rlmg.Tools.MediaPlayers
{
    /// <summary>
    /// Play/pause UI button for video player.
    /// </summary>
    public class VideoPlayerUI_PlayPause : VideoPlayerUI_Base
    {
        /// <summary>
        /// the button component
        /// </summary>
        public Button button;

        /// <summary>
        /// the image UI for the dynamically swapped play/pause icons
        /// </summary>
        public Image buttonIconImage;

        /// <summary>
        /// the dynamically swapped play icon sprite
        /// </summary>
        public Sprite playIcon;

        /// <summary>
        /// an optional override for the play icon if the video has reached its end
        /// </summary>
        public Sprite replayIcon;

        /// <summary>
        /// the dynamically swapped pause icon sprite
        /// </summary>
        public Sprite pauseIcon;

        /// <summary>
        /// is the pause state of the button interactable?
        /// </summary>
        public bool allowPausing = true;

        protected override void Start()
        {
            base.Start();

            if (button == null)
            {
                button = GetComponent<Button>();
            }

            if (button != null)
            {
                if (buttonIconImage == null)
                {
                    buttonIconImage = button.GetComponentInChildren<Image>();
                }

                button.onClick.AddListener(() => OnClick());
            }
        }

        private void OnClick()
        {
            if (player == null)
            {
                return;
            }

            if (player.isPlaying)
            {
                player.Pause();
            }
            else
            {
                player.Play();
            }
        }

        private void Update()
        {
            if (player == null || buttonIconImage == null)
            {
                return;
            }

            if (player.isPlaying)
            {
                buttonIconImage.sprite = pauseIcon;

                button.interactable = allowPausing;
            }
            else
            {
                if (Mathf.Abs(Duration - (float)player.time) < 0.1f && replayIcon != null)
                {
                    buttonIconImage.sprite = replayIcon;
                }
                else
                {
                    buttonIconImage.sprite = playIcon;
                }

                button.interactable = true;
            }
        }
    }
}