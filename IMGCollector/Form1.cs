using IMGCollector.Modules.Danbooru;
using IMGCollector.Modules.Danbooru.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMGCollector
{
    public partial class Form1 : Form
    {
        public List<DanbooruPostModel> loadedPosts = new List<DanbooruPostModel>();
        public int ShuffleIndex = 0;
        public Thread shufflerMode = new Thread(shuffle);

        private static void shuffle(object parent)
        {
            Form1 updateForm = (Form1)parent;
            while (true)
            {
                if (updateForm.loadedPosts.Count != 0)
                {
                    updateForm.ShuffleIndex++;
                    if (updateForm.ShuffleIndex >= updateForm.loadedPosts.Count)
                        updateForm.ShuffleIndex = 0;
                    updateForm.Invoke(new Action(() =>
                    {
                        updateForm.pictureBox1.Load(updateForm.loadedPosts[updateForm.ShuffleIndex].LargeFileUrl.OriginalString);
                    }));
                    Thread.Sleep(1000);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        public void UpdatePictureBox()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DanbooruSharp t = new DanbooruSharp("pp7txHP5VerjmnxroThXFFyg", "theTobby");
            loadedPosts = t.GetPopularImages(DateTime.Now);
            shufflerMode.Start(Application.OpenForms[0]);
        }
    }
}
