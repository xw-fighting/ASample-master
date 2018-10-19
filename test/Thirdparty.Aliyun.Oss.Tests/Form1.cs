using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thirdparty.Aliyun.Oss.Tests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new ImageOperationCenter();
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var key = "20181211141059123.jpg";
            var fs = File.Open($"{path}/{key}", FileMode.Open);
            client.Upload(fs, key);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var client = new ImageOperationCenter();
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var key = "20181211141059123.jpg";
            var stream = client.Download(key);
            var img = Image.FromStream(stream);
            img.Save($"{path}/download/down-{key}");
            MessageBox.Show("下载完成");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var client = new ImageOperationCenter();
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var key = "20181211141059123.jpg";
            var list = new List<string>();
            list.Add("20180119160656371.jpg");
            client.Deletes(list);
        }
    }
}
