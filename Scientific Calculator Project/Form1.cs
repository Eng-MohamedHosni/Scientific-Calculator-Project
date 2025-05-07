using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scientific_Calculator_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }


        Nullable<double> Num1, Num2,Res;

        enum enOp {  OpClicked ,OpNotClicked};

        enOp OpStatus = enOp.OpNotClicked;

        string Operation,PrevOp;

        bool SpecialCase = false;
        bool IsNewOperation = false;

        private void AddTxt(object sender, MouseEventArgs e)
        {
            Guna2Button btn = (Guna2Button)sender;

            if(Num1!=null&&Num2!=null&&Res!=null)
            {
                Operation = PrevOp = "";
                OpStatus = enOp.OpNotClicked;
                txtDisplay.Text = "0";
                txtSmall.Text = "";
                Num1 = Num2 = Res = null;
                IsNewOperation = true;
            }

            if(OpStatus==enOp.OpClicked)
            {
                IsNewOperation=false;
                OpStatus = enOp.OpNotClicked;
                txtDisplay.Text = "0";
                if (txtDisplay.Text == "0")
                {
                    txtDisplay.Text = btn.Tag.ToString();
                }
                else if (txtDisplay.Text.Contains("."))
                {
                    txtDisplay.Text += btn.Tag.ToString();
                }
                else
                {
                    txtDisplay.Text += btn.Tag.ToString();
                }
            }

            else if(txtDisplay.Text=="0")
            {
                txtDisplay.Text = btn.Tag.ToString();
            }
            else if(txtDisplay.Text.Contains("."))
            {
                txtDisplay.Text += btn.Tag.ToString();
            }
            else
            {
                txtDisplay.Text += btn.Tag.ToString();
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDot_MouseDown(object sender, MouseEventArgs e)
        {
           if (txtDisplay.Text.Contains("."))
            {
                return;
            }
            else if(txtDisplay.Text == "0")
            {
                txtDisplay.Text = "0.";
            } 
            else
            {
                txtDisplay.Text += ".";
            }
        }

        private void btnDelete_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtDisplay.Text=="0")
            {
                return;
            }   
            else
            {
                if(txtDisplay.Text.Length==1||(txtDisplay.Text.Length==2&&txtDisplay.Text.Contains("-")))
                {
                    txtDisplay.Text = "0";
                }
                else
                txtDisplay.Text=txtDisplay.Text.Substring(0, txtDisplay.Text.Length-1);
            }
        }

        private void guna2Button20_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void PositiveNegative_MouseDown(object sender, MouseEventArgs e)
        {
            if(Double.Parse(txtDisplay.Text.ToString())>0)
            {
                txtDisplay.Text="-"+txtDisplay.Text;
            }
            else
            {
                txtDisplay.Text = txtDisplay.Text.Replace("-","");
            }
        }

        private void btnClear_MouseDown(object sender, MouseEventArgs e)
        {
            Res = Num1 = Num2 = null;
            Operation =PrevOp= "";
            OpStatus = enOp.OpNotClicked;
            txtDisplay.Text = "0";
            txtSmall.Text = "";
        }

        private void GetResult(string Op)
        {
           
            switch(Op)
            {
                case "+":
                    {
                        Res= Num1 + Num2;
                        break;
                    }
                 case "-": 
                    {
                        Res = Num1 - Num2;
                        break;
                    }
                 case "*":
                    {
                        Res = Num1 * Num2;
                        break;
                    }
                case "/":
                    {
                        if(Num2!=0)
                        Res = Num1 / Num2;
                        else
                        {
                            SpecialCase = true;
                            txtDisplay.Text = "\u221E"; // this Print Infinity
                            Res = 0;
                        }
                        
                            // Display.Text="Can't divide by zero;
                        break;
                    }
                case "%":
                    {
                        Res = Num1 % Num2;
                        break;
                    }

                default:
                    {
                        
                        break;
                    }

            }

           
        }

        private void SmallDisplay()
        {
           
            if (Num2 == null)
            {
                txtSmall.Text = $"{Num1}{Operation}";
            }
            else 
            {
                txtSmall.Text = $"{Num1}{PrevOp}{Num2}=";
            }
            
        }
        private void PerformOperation(object sender, MouseEventArgs e)
        {
            Guna2Button btn = (Guna2Button)sender;
            
            OpStatus = enOp.OpClicked;

            if (Num1 == null)
            {
                if (btn.Tag.ToString() == "=")
                    return;

                Num1 = Double.Parse(txtDisplay.Text.ToString());
            }
            else if (Num2 == null)
            {
                Num2 = Double.Parse(txtDisplay.Text.ToString());
                GetResult(Operation);
                if(!SpecialCase)
                txtDisplay.Text = Res.ToString();

                if (btn.Tag.ToString() != "=")
                {
                    Num2 = null;
                    Num1 = Res;
                }
               
            }
            else
            {
                if (btn.Tag.ToString() == "=")
                {
                    Num1 = Res;
                    
                    GetResult(PrevOp);
                    txtDisplay.Text = Res.ToString();
                    SmallDisplay();
                    return;
                }
                else
                {
                    Num1 = Res;
                    Num2 = null;
                }

            }
                PrevOp = Operation;
            Operation = btn.Tag.ToString();
            SmallDisplay();

           
        }


    }
}
