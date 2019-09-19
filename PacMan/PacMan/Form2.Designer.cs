namespace PacMan
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GetLocation = new System.Windows.Forms.Button();
            this.FileLocation = new System.Windows.Forms.TextBox();
            this.Submit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GetLocation
            // 
            this.GetLocation.Location = new System.Drawing.Point(86, 30);
            this.GetLocation.Name = "GetLocation";
            this.GetLocation.Size = new System.Drawing.Size(209, 54);
            this.GetLocation.TabIndex = 0;
            this.GetLocation.Text = "Get Location";
            this.GetLocation.UseVisualStyleBackColor = true;
            this.GetLocation.Click += new System.EventHandler(this.GetLocation_Click);
            // 
            // FileLocation
            // 
            this.FileLocation.Location = new System.Drawing.Point(12, 109);
            this.FileLocation.Name = "FileLocation";
            this.FileLocation.Size = new System.Drawing.Size(357, 20);
            this.FileLocation.TabIndex = 1;
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(86, 163);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(209, 53);
            this.Submit.TabIndex = 2;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(381, 336);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.FileLocation);
            this.Controls.Add(this.GetLocation);
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FileLoc;
        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.Button Get_Location;
        private System.Windows.Forms.Button GetLocation;
        private System.Windows.Forms.TextBox FileLocation;
        private System.Windows.Forms.Button Submit;
    }
}