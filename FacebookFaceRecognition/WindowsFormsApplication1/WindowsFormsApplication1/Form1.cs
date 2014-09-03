using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;
using System.Net;
using System.IO;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<FacebookPicture> pictures = new List<FacebookPicture>();

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new FacebookClient("CAACEdEose0cBAJc3zwNHXrytA4nQv1HPRS950zC1vbyqao1CjQem3lVxBjHcvmUQkZAaTZAGVLT3PJKBMcClVZBCCSG3ZCQZBfpjz2ZCVTZBsRd6wxF3LfWeoGQ9x6J3HmZCv0TC7SbepZBmwZA347jcsIqZBm2EwOZBPuuweWNFgtvFsuepCbt2jDP1qZBWk31DwDhiQ7gU4p0D2ZA6HNaD1hiDSRQjbq8ZAqGiuAZD");
            dynamic me = client.Get("me");
            dynamic photos = client.Get(me["id"] + "/photos?limit=500"); //get the first 500 pictures

            //loop trough the facebook pictures
            foreach (dynamic photo in photos[0])
            {
                //check if picture does indeed have a tag
                if(photo["tags"].Count != 0) {
                    FacebookPicture picture = new FacebookPicture(photo["images"][0][2]);
                    foreach(dynamic tag in photo["tags"]["data"]) {
                        picture.AddTag(tag["name"], (float)tag["x"], (float)tag["y"]);
                    }
                    pictures.Add(picture);
                }
            }


            photos = client.Get(me["id"] + "/photos/uploaded?limit=500"); //get the first 500 pictures

            //loop trough the facebook pictures
            foreach (dynamic photo in photos[0])
            {
                //check if picture does indeed have a tag
                if (photo.ContainsKey("tags") && photo["tags"].Count != 0)
                {
                    FacebookPicture picture = new FacebookPicture(photo["images"][0][2]);
                    foreach (dynamic tag in photo["tags"]["data"])
                    {
                        if (tag.ContainsKey("name") && tag.ContainsKey("x") && tag.ContainsKey("y"))
                        {
                            picture.AddTag(tag["name"], (float)tag["x"], (float)tag["y"]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (picture.tags.Count != 0) {
                        pictures.Add(picture);
                    }
                }
            }

            photos = client.Get(me["id"] + "/photos/tagged?limit=1000"); //get the first 500 pictures

            //loop trough the facebook pictures
            foreach (dynamic photo in photos[0])
            {
                //check if picture does indeed have a tag
                if (photo["tags"].Count != 0)
                {
                    FacebookPicture picture = new FacebookPicture(photo["images"][0][2]);
                    foreach (dynamic tag in photo["tags"]["data"])
                    {
                        picture.AddTag(tag["name"], (float)tag["x"], (float)tag["y"]);
                    }
                    pictures.Add(picture);
                }
            }


            DownloadFacebookPicture(pictures);
        }

        private void DownloadFacebookPicture(List<FacebookPicture> list)
        {
            int i = 1; //avoid problems with filenames
            foreach (FacebookPicture picture in list)
            {   
                string filename = Path.Combine("c:\\Temp\\pictures",  i.ToString() + ".jpg");
                using (Image img = DownloadImage(picture.Url))
                {
                    img.Save(filename);
                }
                i++;
            }
        }

        public static Image DownloadImage(string url)
        {
            Image result = null;
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
                WebResponse webResponse = webRequest.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    using (Image tempImage = Image.FromStream(webStream))
                    {
                        result = new Bitmap(tempImage);
                    }
                }
                webResponse.Close();
            }
            catch (Exception e)
            {
                return null;
            }

            return result;
        }
    }
}
