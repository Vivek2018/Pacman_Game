using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PacMan
{
    public partial class Form2 : Form
    {
        public string BatchLoc; 
        public Form2()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {



        }



        private void Get_Location_Click()
        {
            //String s = "tt";

            /*
           SaveFileDialog save = new SaveFileDialog();

           BatchLoc = save.FileName.ToString();
           //MessageBox.Show(BatchLoc); 
           if (save.ShowDialog() == DialogResult.OK)
           {
               BatchLoc = save.FileName.ToString();
              // MessageBox.Show(BatchLoc); 

               FileLoc.Text = BatchLoc; */

        }

        private void Form2_Load(object sender, EventArgs e)
        {
                
        }

        private void GetLocation_Click(object sender, EventArgs e)
        {
             SaveFileDialog save = new SaveFileDialog();

           //BatchLoc = save.FileName.ToString();
           //MessageBox.Show(BatchLoc); 
             if (save.ShowDialog() == DialogResult.OK)
             {
                 BatchLoc = save.FileName.ToString();
                 // MessageBox.Show(BatchLoc); 

                 FileLocation.Text = BatchLoc;

             }
             BatchLoc = FileLocation.Text; 


        }

        private void Submit_Click(object sender, EventArgs e)
        {
            BatchLoc = FileLocation.Text;
            this.Close(); 
        }
    }
}