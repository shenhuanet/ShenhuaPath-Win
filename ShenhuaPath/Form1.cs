using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ShenhuaPath
{
    public partial class ShenhuaPath : Form
    {
        public ShenhuaPath()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            String path = Environment.GetCommandLineArgs()[0];
            // 注入文件右键菜单
            Boolean success = createWithIcon("*", path);
            if (success)
            {
                // 注入文件夹右键菜单
                createWithIcon("Folder", path);
            }
            else
            {
                unAuthorized();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Boolean success = delete("*");
            if (success)
            {
                delete("Folder");
            }
            else
            {
                unAuthorized();
            }
        }

        private Boolean createWithIcon(String where, String path)
        {
            try
            {
                RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey(where + "\\shell\\ShenhuaPath");
                registryKey.SetValue("", "神话取文件路径");
                registryKey.SetValue("Icon", path);
                RegistryKey openFileCommand = registryKey.CreateSubKey("command");
                openFileCommand.SetValue("", "\"" + path + "\" " + "%1");

                registryKey.Close();
                openFileCommand.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private Boolean create(String where, String path)
        {
            try
            {
                RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey(where + "\\shell\\ShenhuaPath");
                registryKey.SetValue("", "神话取文件路径");
                RegistryKey openFileCommand = registryKey.CreateSubKey("command");
                openFileCommand.SetValue("", "\"" + path + "\" " + "%1");

                registryKey.Close();
                openFileCommand.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private Boolean delete(String where) {
            try
            {
                RegistryKey shellKey = Registry.ClassesRoot
                    .OpenSubKey(where, true)
                    .OpenSubKey("shell", true);

                shellKey.DeleteSubKeyTree("ShenhuaPath",true);

                //RegistryKey fileKey = shellKey.OpenSubKey("ShenhuaPath", true);
                //fileKey.DeleteSubKey("command", true);
                //shellKey.DeleteSubKey("ShenhuaPath", true);
                //fileKey.Close();
                shellKey.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void unAuthorized()
        {
            MessageBox.Show("请以管理员身份运行", "授权失败");
        }
    }
}
