using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RelationShipCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txt_secondName_TextChanged(object sender, EventArgs e)
        {
            txt_firstName_TextChanged(sender, e);
        }

        private void txt_firstName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_firstName.Text) && !string.IsNullOrEmpty(txt_secondName.Text))
            {
                RelationshipCalculator calc = new RelationshipCalculator(txt_firstName.Text, txt_secondName.Text);
                lbl_result.Text = calc.RelationshipChance;
            }
        }
    }
}
