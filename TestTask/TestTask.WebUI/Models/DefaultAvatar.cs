using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TestTask.WebUI.Models {
    public class DefaultAvatar {
        private string fileNoAvatar = "~/ProfileImages/noavatar.jpg";

        public byte[] Avatar { get; private set; }
        public string MimeType { get; private set; }

        public DefaultAvatar() {
            var defaultImage = new WebImage(fileNoAvatar);
            if (defaultImage != null) {
                Avatar = defaultImage.GetBytes();
                MimeType = defaultImage.ImageFormat;
            }
        }
    }
}