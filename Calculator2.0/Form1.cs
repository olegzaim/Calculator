using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator2._0
{
    public partial class Form1 : Form  
                                       
    {
        private Double result = 0;  
        private String operation = "";        
        private bool isOperationPerformed = false; //to clean textbox, to input a new value
        bool isNumberPerformed = false;            //to avoid conflict of operations (last clicked operator is priority)
        bool isDivideByZeroPerformed = false;      //to stop operations after trying to divide by zero
        private double memory;
        private bool memFlag;

        // getters and setters
        public double Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }

        public string Operation
        {
            get
            {
                return operation;
            }

            set
            {
                operation = value;
            }
        }

        public bool IsOperationPerformed
        {
            get
            {
                return isOperationPerformed;
            }

            set
            {
                isOperationPerformed = value;
            }
        }

        public bool IsNumberPerformed
        {
            get
            {
                return isNumberPerformed;
            }

            set
            {
                isNumberPerformed = value;
            }
        }

        public bool IsDivideByZeroPerformed
        {
            get
            {
                return isDivideByZeroPerformed;
            }

            set
            {
                isDivideByZeroPerformed = value;
            }
        }
        //


        public Form1()
        {

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US"); //setting language culture
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            btnMC.Enabled = false;
            btnMR.Enabled = false;
        }

        private void numberButtonClick(object sender, EventArgs e)
        {
            
            Button button = (Button)sender;
            IsNumberPerformed = true;

            if (IsOperationPerformed)
            {

                textBoxResult.Clear();

            }
            IsOperationPerformed = false;

            if (textBoxResult.Text.Length <= 15 ) 
            {
                
                if (button.Text == "." ) //to avoid too many points in the number
                {
                    
                    if (!(textBoxResult.Text.Contains(".")))
                    {
                        if (textBoxResult.Text == "")  // add zero before point
                        {
                            textBoxResult.Text = "0" + button.Text;
                        }
                        else
                        {
                            textBoxResult.Text = textBoxResult.Text + button.Text;
                        }

                    }
                }
                else if (button.Text == "0" )   //to avoid too many zeros at the beginning
                {
                    if (textBoxResult.Text != "0" )
                    {
                        textBoxResult.Text = textBoxResult.Text + button.Text;
                        
                    }
                    
                }
                else
                {
                    if (textBoxResult.Text == "0"|| memFlag == true) //to avoid zero before number
                    {
                        textBoxResult.Text = button.Text;
                        memFlag = false;
                    }
                    else
                    {
                        textBoxResult.Text = textBoxResult.Text + button.Text;
                    }
                }
               
            }


        }

        private void operationButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (IsNumberPerformed)
            {

                switch (Operation)
                {
                   
                    case "+":

                        textBoxResult.Text = (Result + Double.Parse(textBoxResult.Text)).ToString();
                        break;
                    case "-":

                        textBoxResult.Text = (Result - Double.Parse(textBoxResult.Text)).ToString();
                        break;
                    case "x":
                        textBoxResult.Text = (Result * Double.Parse(textBoxResult.Text)).ToString();
                        break;
                    case "/":
                        if (Double.Parse(textBoxResult.Text) == 0)
                        {
                            MessageBox.Show("You can't divide by zero!");
                            buttonC.PerformClick();
                            IsDivideByZeroPerformed = true;
                            break;
                        }
                        else
                        {
                            textBoxResult.Text = (Result / Double.Parse(textBoxResult.Text)).ToString();
                            break;
                        }
                    
                    default:
                        break;

                }

                IsNumberPerformed = false;

            }

            if(!IsDivideByZeroPerformed)
            {
                if (button.Text == "=")
                {
                    Operation = "";

                    try
                    {
                        Result = Double.Parse(textBoxResult.Text);
                    }
                    catch (FormatException)
                    {

                        Result = 0;
                        textBoxResult.Text = Result.ToString();

                    }

                    IsNumberPerformed = true;
                    labelPreview.Text = "";
                    IsOperationPerformed = false;

                }

                else
                {
                    Operation = button.Text;       // operator is chosen
                    
                    try
                    {
                        Result = Double.Parse(textBoxResult.Text);
                    }
                    catch (FormatException)
                    {

                        Result = 0;
                        textBoxResult.Text = Result.ToString();

                    }

                    labelPreview.Text = textBoxResult.Text + " " + Operation;
                    IsOperationPerformed = true;
                }


            }


            IsDivideByZeroPerformed = false;

        }

      
        private void clearAllButtonClick(object sender, EventArgs e)
        {
           
            textBoxResult.Text = "";
            Result = 0;
            Operation= "";
            IsNumberPerformed = false;
            IsOperationPerformed = false;
            labelPreview.Text = "";

        }

        private void negationButtonClick(object sender, EventArgs e)
        {
                   
            try
            {
                textBoxResult.Text = (-1 * Double.Parse(textBoxResult.Text)).ToString();
                IsNumberPerformed = true;
            }
            catch (FormatException)
            {
                Result = 0;
                textBoxResult.Text = Result.ToString();
            }

        }
        private void buttonProcent_Click(object sender, EventArgs e)
        {
          

        }
        private void btnSqrt_Click(object sender,EventArgs e)
        {
            labelPreview.Text = textBoxResult.Text + "√ " + Operation;
            
            textBoxResult.Text = Math.Sqrt(Double.Parse(textBoxResult.Text)).ToString();
        }
        private void btnRecip_Click(object sender, EventArgs e)
        {
            labelPreview.Text = " 1/ "+textBoxResult.Text  + Operation;
            textBoxResult.Text = (1 / Double.Parse(textBoxResult.Text)).ToString();

        }

        private void btnSqr_Click(object sender, EventArgs e)
        {
            labelPreview.Text = textBoxResult.Text + " ²" + Operation;
            textBoxResult.Text = (Double.Parse(textBoxResult.Text) * Double.Parse(textBoxResult.Text)).ToString();
        }
        private void textBoxResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = "0";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            
            textBoxResult.Text = textBoxResult.Text.Remove(textBoxResult.Text.Length - 1);
            if (textBoxResult.Text.Length == 0)
                textBoxResult.Text = "0";
        }
        //Memory Save
        private void btnMS_Click(object sender, EventArgs e)
        {
            memory = Double.Parse(textBoxResult.Text);
            btnMC.Enabled = true;
            btnMR.Enabled = true;
            memFlag = true;
        }
        //Memory Read
        private void btnMR_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = memory.ToString();
            memFlag = true;
        }
        //Memory Clear
        private void btnMC_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = "0";
            memory = 0;
            btnMR.Enabled = false;
            btnMC.Enabled = false;
        }

        //M+
        private void btnMPlus_Click(object sender, EventArgs e)
        {
            memory += Double.Parse(textBoxResult.Text);
        }
        private void btnMMinus_Click(object sender, EventArgs e)
        {
            memory -= Double.Parse(textBoxResult.Text);

        }

       
       
    }
}
