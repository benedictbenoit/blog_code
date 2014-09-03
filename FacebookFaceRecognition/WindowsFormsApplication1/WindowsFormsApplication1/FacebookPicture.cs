using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class FacebookPicture
    {
        public string Url { get; set; }
        public List<FacebookPictureTag> tags = new List<FacebookPictureTag>();

        public FacebookPicture(string Url) { this.Url = Url; }
        public void AddTag(string name, float x, float y){
            tags.Add(new FacebookPictureTag(name, x, y));
        }

        public List<FacebookPictureTag> getTags()
        {
            return this.tags;
        }

    }

    class FacebookPictureTag {
        public string Name { get; set; }
        public float x {get; set;}
        public float y {get; set;}

        public FacebookPictureTag(string Name, float x, float y) {
            this.Name = Name;
            this.x = x;
            this.y = y;
        }
    }
}
