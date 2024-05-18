using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace ytdl_gui
{
    public partial class Form3 : Form
    {
        private Form1 _form1;
        int languageSelected;
        string iniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/translate/en.ini");
        Dictionary<string, string> translations = new Dictionary<string, string>();
        private string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/config.ini");
        int langSel;

        public Form3(Form1 form1)
        {
            _form1 = form1;
            InitializeComponent();
            LoadConfig();

            if (langSel == 1)
            {
                languageSelected = 1;
                this.Text = "Support";
            }
            if (langSel == 0)
            {
                languageSelected = 0;
            }

            SetLanguage();
        }

        private void LoadConfig()
        {
            if (File.Exists(configPath))
            {
                string[] lines = File.ReadAllLines(configPath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        switch (parts[0])
                        {
                            case "langSel":
                                langSel = int.Parse(parts[1]);
                                break;
                        }
                    }
                }
            }
        }

        public void SetLanguage()
        {
            if (languageSelected == 1)
            {
                Dictionary<string, string> translations = ReadIniFile(iniFilePath);
                ApplyTranslationsToControls(this, translations, this.Name);

                panel1.Enabled = false;
                panel1.Visible= false;
                panel2.Enabled = true;
                panel2.Visible = true;

                this.Refresh();
            }

            if (languageSelected == 0)
            {
                panel1.Enabled = true;
                panel1.Visible = true;
                panel2.Enabled = false;
                panel2.Visible = false;
                // do nothing...
            }


        }

        private void ApplyTranslationsToControls(Control parent, Dictionary<string, string> translations, string formName)
        {
            foreach (Control c in parent.Controls)
            {
                if (formName.StartsWith("Form") && (c is TextBox || c is CheckBox ||
                    c is System.Windows.Forms.Label || c is System.Windows.Forms.GroupBox ||
                    c is System.Windows.Forms.Button || c is RichTextBox))
                {
                    string key = formName + "." + c.Name;
                    if (translations.ContainsKey(key))
                    {
                        c.Text = translations[key];
                    }
                }
                else if (c is ListBox)
                {
                    ListBox listBox = c as ListBox;
                    for (int i = 0; i < listBox.Items.Count; i++)
                    {
                        string key = formName + "." + c.Name + "[" + i + "]";
                        if (translations.ContainsKey(key))
                        {
                            listBox.Items[i] = translations[key];
                        }
                    }
                }
                if (c.HasChildren)
                {
                    ApplyTranslationsToControls(c, translations, formName);
                }
            }
        }



        private Dictionary<string, string> ReadIniFile(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();
                            translations[key] = value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _form1.richTextBox1.AppendText($"Wystąpił błąd podczas ustawiania języka programu. Can't set your destination language. (Error: {ex.Message})");
            }

            return translations;
        }










    }
}
