using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLF
{
    class KategorieErstellerPanel : Panel
    {
        
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.TextBox textBox;

        private int id;
        private ArrayList kepList;
        private Form parentForm;

        private int height;
        private int width;
        private int padding;

        public KategorieErstellerPanel(int id, ArrayList kepList, Form parentForm, int width, int height, int padding)
        {

            this.id = id;
            this.kepList = kepList;
            this.parentForm = parentForm;

            this.height = height;
            this.width = width;
            this.padding = padding;

            this.button = new Button();
            this.textBox = new TextBox();

            this.SuspendLayout();

            Controls.Add(button);
            Controls.Add(textBox);

            this.Location = new System.Drawing.Point(padding, padding+id*height);
            this.Name = "kategorieErstellerPanel" + id;
            this.Size = new System.Drawing.Size(width, height);
            this.TabIndex = id;

            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(3, 6);
            this.textBox.Name = "textbox";
            this.textBox.Size = new System.Drawing.Size(236, 20);
            this.textBox.TabIndex = 0;

            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(245, 4);
            this.button.Name = "hinzufuegenBtn";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 1;
            this.button.Text = "Hinzufügen";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);

        }

        public KategorieErstellerPanel Disable()
        {

            this.button.Enabled = false;
            this.textBox.Enabled = false;

            return this;

        }

        public KategorieErstellerPanel Enable()
        {

            this.button.Enabled = true;
            this.textBox.Enabled = true;

            return this;

        }

        public KategorieErstellerPanel Editable()
        {

            this.button.Text = "Bearbeiten";
            this.textBox.Enabled = false;

            return this;

        }

        private void button_Click(object sender, EventArgs e)
        {

            if (textBox.Text.Trim() != "")
            {
                if (button.Text == "Hinzufügen")
                {

                    Editable();
                    ((KategorieErstellerPanel)kepList[id + 1]).Enable();
                    kepList.Add(new KategorieErstellerPanel(kepList.Count, kepList, parentForm, 323, 32, 12).Disable());
                    ((AddCategoriesWindow)parentForm).Reinit();

                }else if(button.Text == "Bearbeiten")
                {

                    button.Text = "Fertig";
                    textBox.Enabled = true;

                }else if(button.Text == "Fertig")
                {

                    button.Text = "Bearbeiten";
                    textBox.Enabled = false;

                }
            }

        }

        public int getHeight()
        {
            return height;
        }

        public int getWidth()
        {
            return width;
        }

        public int getPadding()
        {
            return padding;
        }

        public string getButtonText()
        {
            return button.Text;
        }

        public string getText()
        {
            return textBox.Text.Trim();
        }

    }

}
