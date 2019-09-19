using System;

namespace PacMan
{
    partial class Form1
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
            this.LBL_Score = new System.Windows.Forms.Label();
            this.BTN_Save_Ghost = new System.Windows.Forms.Button();
            this.BTN_Upload_Ghost = new System.Windows.Forms.Button();
            this.BTN_Save_Pac = new System.Windows.Forms.Button();
            this.BTN_Upload_Pac = new System.Windows.Forms.Button();
            this.CBX_Train = new System.Windows.Forms.CheckBox();
            this.LBL_Round = new System.Windows.Forms.Label();
            this.LBL_Gen = new System.Windows.Forms.Label();
            this.CBX_Render = new System.Windows.Forms.CheckBox();
            this.BTN_Training = new System.Windows.Forms.Button();
            this.BTN_Upload_Train = new System.Windows.Forms.Button();
            this.Breed = new System.Windows.Forms.Button();
            this.FastSave = new System.Windows.Forms.Button();
            this.SaveLoc = new System.Windows.Forms.Button();
            this.AutoSave = new System.Windows.Forms.CheckBox();
            this.Reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LBL_Score
            // 
            this.LBL_Score.AutoSize = true;
            this.LBL_Score.Location = new System.Drawing.Point(13, 13);
            this.LBL_Score.Name = "LBL_Score";
            this.LBL_Score.Size = new System.Drawing.Size(38, 13);
            this.LBL_Score.TabIndex = 25;
            this.LBL_Score.Text = "Score:";
            // 
            // BTN_Save_Ghost
            // 
            this.BTN_Save_Ghost.Location = new System.Drawing.Point(15, 30);
            this.BTN_Save_Ghost.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_Save_Ghost.Name = "BTN_Save_Ghost";
            this.BTN_Save_Ghost.Size = new System.Drawing.Size(90, 30);
            this.BTN_Save_Ghost.TabIndex = 26;
            this.BTN_Save_Ghost.Text = "Save Ghost";
            this.BTN_Save_Ghost.UseVisualStyleBackColor = true;
            this.BTN_Save_Ghost.Click += new System.EventHandler(this.BTN_Save_Click);
            // 
            // BTN_Upload_Ghost
            // 
            this.BTN_Upload_Ghost.Location = new System.Drawing.Point(110, 30);
            this.BTN_Upload_Ghost.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_Upload_Ghost.Name = "BTN_Upload_Ghost";
            this.BTN_Upload_Ghost.Size = new System.Drawing.Size(89, 30);
            this.BTN_Upload_Ghost.TabIndex = 27;
            this.BTN_Upload_Ghost.Text = "Upload Ghost";
            this.BTN_Upload_Ghost.UseVisualStyleBackColor = true;
            this.BTN_Upload_Ghost.Click += new System.EventHandler(this.BTN_Upload_Click);
            // 
            // BTN_Save_Pac
            // 
            this.BTN_Save_Pac.Location = new System.Drawing.Point(203, 29);
            this.BTN_Save_Pac.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_Save_Pac.Name = "BTN_Save_Pac";
            this.BTN_Save_Pac.Size = new System.Drawing.Size(99, 30);
            this.BTN_Save_Pac.TabIndex = 28;
            this.BTN_Save_Pac.Text = "Save Pacman";
            this.BTN_Save_Pac.UseVisualStyleBackColor = true;
            this.BTN_Save_Pac.Click += new System.EventHandler(this.BTN_Save_Pac_Click);
            // 
            // BTN_Upload_Pac
            // 
            this.BTN_Upload_Pac.Location = new System.Drawing.Point(307, 29);
            this.BTN_Upload_Pac.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_Upload_Pac.Name = "BTN_Upload_Pac";
            this.BTN_Upload_Pac.Size = new System.Drawing.Size(110, 30);
            this.BTN_Upload_Pac.TabIndex = 29;
            this.BTN_Upload_Pac.Text = "Upload Pacman";
            this.BTN_Upload_Pac.UseVisualStyleBackColor = true;
            this.BTN_Upload_Pac.Click += new System.EventHandler(this.BTN_Upload_Pac_Click);
            // 
            // CBX_Train
            // 
            this.CBX_Train.AutoSize = true;
            this.CBX_Train.Location = new System.Drawing.Point(307, 12);
            this.CBX_Train.Margin = new System.Windows.Forms.Padding(2);
            this.CBX_Train.Name = "CBX_Train";
            this.CBX_Train.Size = new System.Drawing.Size(50, 17);
            this.CBX_Train.TabIndex = 30;
            this.CBX_Train.Text = "Train";
            this.CBX_Train.UseVisualStyleBackColor = true;
            this.CBX_Train.CheckedChanged += new System.EventHandler(this.CBX_Train_Toggle);
            // 
            // LBL_Round
            // 
            this.LBL_Round.AutoSize = true;
            this.LBL_Round.Location = new System.Drawing.Point(107, 14);
            this.LBL_Round.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBL_Round.Name = "LBL_Round";
            this.LBL_Round.Size = new System.Drawing.Size(45, 13);
            this.LBL_Round.TabIndex = 31;
            this.LBL_Round.Text = "Round: ";
            // 
            // LBL_Gen
            // 
            this.LBL_Gen.AutoSize = true;
            this.LBL_Gen.Location = new System.Drawing.Point(201, 14);
            this.LBL_Gen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBL_Gen.Name = "LBL_Gen";
            this.LBL_Gen.Size = new System.Drawing.Size(33, 13);
            this.LBL_Gen.TabIndex = 32;
            this.LBL_Gen.Text = "Gen: ";
            // 
            // CBX_Render
            // 
            this.CBX_Render.AutoSize = true;
            this.CBX_Render.Location = new System.Drawing.Point(358, 12);
            this.CBX_Render.Margin = new System.Windows.Forms.Padding(2);
            this.CBX_Render.Name = "CBX_Render";
            this.CBX_Render.Size = new System.Drawing.Size(61, 17);
            this.CBX_Render.TabIndex = 33;
            this.CBX_Render.Text = "Render";
            this.CBX_Render.UseVisualStyleBackColor = true;
            this.CBX_Render.CheckedChanged += new System.EventHandler(this.CBX_Render_Toggle);
            // 
            // BTN_Training
            // 
            this.BTN_Training.Location = new System.Drawing.Point(15, 65);
            this.BTN_Training.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_Training.Name = "BTN_Training";
            this.BTN_Training.Size = new System.Drawing.Size(90, 31);
            this.BTN_Training.TabIndex = 34;
            this.BTN_Training.Text = "Save Training";
            this.BTN_Training.UseVisualStyleBackColor = true;
            this.BTN_Training.Click += new System.EventHandler(this.BTN_Save_Training);
            // 
            // BTN_Upload_Train
            // 
            this.BTN_Upload_Train.Location = new System.Drawing.Point(110, 67);
            this.BTN_Upload_Train.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_Upload_Train.Name = "BTN_Upload_Train";
            this.BTN_Upload_Train.Size = new System.Drawing.Size(89, 29);
            this.BTN_Upload_Train.TabIndex = 35;
            this.BTN_Upload_Train.Text = "Upload Train";
            this.BTN_Upload_Train.UseVisualStyleBackColor = true;
            this.BTN_Upload_Train.Click += new System.EventHandler(this.BTN_Upload_Training);
            // 
            // Breed
            // 
            this.Breed.Location = new System.Drawing.Point(206, 67);
            this.Breed.Name = "Breed";
            this.Breed.Size = new System.Drawing.Size(151, 30);
            this.Breed.TabIndex = 36;
            this.Breed.Text = "Breed";
            this.Breed.UseVisualStyleBackColor = true;
            this.Breed.Click += new System.EventHandler(this.Breed_Click);
            // 
            // FastSave
            // 
            this.FastSave.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.FastSave.Location = new System.Drawing.Point(363, 67);
            this.FastSave.Name = "FastSave";
            this.FastSave.Size = new System.Drawing.Size(132, 30);
            this.FastSave.TabIndex = 37;
            this.FastSave.Text = "Quick Save/Batch Save";
            this.FastSave.UseVisualStyleBackColor = true;
            this.FastSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // SaveLoc
            // 
            this.SaveLoc.Location = new System.Drawing.Point(422, 28);
            this.SaveLoc.Name = "SaveLoc";
            this.SaveLoc.Size = new System.Drawing.Size(112, 31);
            this.SaveLoc.TabIndex = 38;
            this.SaveLoc.Text = "Set Save Location";
            this.SaveLoc.UseVisualStyleBackColor = true;
            this.SaveLoc.Click += new System.EventHandler(this.SaveLoc_Click);
            // 
            // AutoSave
            // 
            this.AutoSave.AutoSize = true;
            this.AutoSave.Location = new System.Drawing.Point(458, 12);
            this.AutoSave.Margin = new System.Windows.Forms.Padding(2);
            this.AutoSave.Name = "AutoSave";
            this.AutoSave.Size = new System.Drawing.Size(76, 17);
            this.AutoSave.TabIndex = 39;
            this.AutoSave.Text = "Auto Save";
            this.AutoSave.UseVisualStyleBackColor = true;
            this.AutoSave.CheckedChanged += new System.EventHandler(this.AutoSave_CheckedChanged);
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(459, 103);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(75, 23);
            this.Reset.TabIndex = 40;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 512);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.AutoSave);
            this.Controls.Add(this.SaveLoc);
            this.Controls.Add(this.FastSave);
            this.Controls.Add(this.Breed);
            this.Controls.Add(this.BTN_Upload_Train);
            this.Controls.Add(this.BTN_Training);
            this.Controls.Add(this.CBX_Render);
            this.Controls.Add(this.LBL_Gen);
            this.Controls.Add(this.LBL_Round);
            this.Controls.Add(this.CBX_Train);
            this.Controls.Add(this.BTN_Upload_Pac);
            this.Controls.Add(this.BTN_Save_Pac);
            this.Controls.Add(this.BTN_Upload_Ghost);
            this.Controls.Add(this.BTN_Save_Ghost);
            this.Controls.Add(this.LBL_Score);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        #endregion

        private System.Windows.Forms.Label LBL_Score;
        private System.Windows.Forms.Button BTN_Save_Ghost;
        private System.Windows.Forms.Button BTN_Upload_Ghost;
        private System.Windows.Forms.Button BTN_Save_Pac;
        private System.Windows.Forms.Button BTN_Upload_Pac;
        private System.Windows.Forms.CheckBox CBX_Train;
        private System.Windows.Forms.Label LBL_Round;
        private System.Windows.Forms.Label LBL_Gen;
        private System.Windows.Forms.CheckBox CBX_Render;
        private System.Windows.Forms.Button BTN_Training;
        private System.Windows.Forms.Button BTN_Upload_Train;
        private System.Windows.Forms.Button Breed;
        private System.Windows.Forms.Button FastSave;
        private System.Windows.Forms.Button SaveLoc;
        private System.Windows.Forms.CheckBox AutoSave;
        private System.Windows.Forms.Button Reset;
    }
}

