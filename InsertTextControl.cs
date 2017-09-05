using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;

namespace WordDocument1
{
    partial class InsertTextControl : UserControl
    {
        public InsertTextControl()
        {
            InitializeComponent();                      
        }

        int flag6 = 0,flag7=0;
        private void Generate_Table_Click(object sender, EventArgs e) //print the table into word document
        {
            //textbox1.Enabled = true;
            if (comboBox1.SelectedIndex == 0 || comboBox2.SelectedIndex == 0 )
            {
                MessageBox.Show("Please select a valid range", "Warning!!");
                flag6 = 1;
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid range", "Warning!!");
                flag6 = 1;
            }
            //if (comboBox2.Visible = true && comboBox2.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please select a valid range", "Warning!!");
            //    flag6 = 1;
            //}
            if (flag6 == 1)
            {
                flag6 = 0;
              //  button6_Click(sender, e);
            }
            else
            {
                if (checkBox1.Checked == true) //cross checking to see whether check box is checked or not
                {
                    whether_checkbox_is_checked_or_not(sender, e);
                }
                Generate_Bv_Click(sender, e); //call to bv table generator
                if (flag7 == 1)
                {
                    flag7 = 0;
                }
                else
                {
                    button4_Click(sender, e);

                    if (textBox12.Text.Length >= 1)
                    {
                        addText_Click(sender, e);
                    }
                    else
                    {
                        addBv2_Click(sender, e);
                    }
                    button6_Click(sender, e);
                    textbox1.Focus();
                }
            }
        }

        int flag2 = 1;
        private void button4_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                button6_Click(sender, e);
                flag = 1;
                flag2 = 0;
            }
            else
            {
                var para1 = (dynamic)Globals.ThisDocument.Paragraphs.Add();
                para1.Range.Text = "";
                para1.Range.InsertParagraphAfter();
                para1.Range.Text = "The equivalence classes for " + textbox1.Text + " are:";
                para1.Range.Font.Name = "Arial";
                para1.Range.Font.Size = 11;
                para1.Range.InsertParagraphAfter();

                Microsoft.Office.Interop.Word.Table table = AddColumns();

                if (table != null)
                {
                    // write data to the created table
                    // TODO: Write relevant values    
                    if (textBox7.Text.Length >= 1)
                    {
                        three_Eqs_boxes_Click(sender, e);
                        AddData(box32.Text, textBox5.Text, textBox8.Text, table);
                        AddData(box33.Text, textBox6.Text, textBox9.Text, table);
                        AddData(box34.Text, textBox7.Text, textBox10.Text, table);
                    }
                    else
                    {
                        two_Eqs_boxes_Click(sender, e);
                        AddData(box30.Text, textBox5.Text, textBox8.Text, table);
                        AddData(box31.Text, textBox6.Text, textBox9.Text, table);
                    }
                    // Add a page break to the document
                    ((dynamic)Globals.ThisDocument.Paragraphs).Last.Range.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdLineBreak);

                }
            }
        }


        public Microsoft.Office.Interop.Word.Table AddColumns()
        {
           
                object missing = System.Type.Missing;
                Microsoft.Office.Interop.Word.Table tbl = null;

                try
                {
                    // Create a table.
                    object miss = System.Type.Missing;
                    var paragraph = Globals.ThisDocument.Paragraphs.Add(ref miss);
                    tbl = Globals.ThisDocument.Tables.Add(paragraph.Range, 1, 3, ref missing, ref missing);

                    // set the border line style
                    tbl.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    tbl.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    tbl.PreferredWidth = 432.18f;
                    tbl.Columns[1].PreferredWidth = 62.2f;
                    tbl.Columns[2].PreferredWidth = 358.34f;
                    tbl.Columns[3].PreferredWidth = 49.7f;
                    tbl.Rows.SetHeight(12.14f, WdRowHeightRule.wdRowHeightExactly);
                    // Insert headings.
                    SetHeadings(tbl.Cell(1, 1), "Equivalence Class");
                    SetHeadings(tbl.Cell(1, 2), "Condition");
                    SetHeadings(tbl.Cell(1, 3), "Validity");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem creating Products table: " + ex.Message,
                        "Actions Pane", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return tbl;
            
        }

        private void AddData(string Eq, string Condition, string validity, Microsoft.Office.Interop.Word.Table table)
        {
            object miss = System.Type.Missing;
            Microsoft.Office.Interop.Word.Row newRow = table.Rows.Add(ref miss);
            newRow.Range.Font.Bold = 0;
            newRow.Range.Font.Name = "Arial";
            newRow.Range.Font.Size = 8;
            newRow.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            newRow.Cells[3].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            newRow.Cells[2].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            newRow.Cells[1].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            newRow.Cells[1].Range.Text = Eq;
            newRow.Cells[2].Range.Text = Condition;
            newRow.Cells[3].Range.Text = validity;
        }
        static void SetHeadings(Microsoft.Office.Interop.Word.Cell tblCell, string text)
        {
            tblCell.Range.Text = text;          
            tblCell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            tblCell.Range.Font.Bold = 1;
            tblCell.Range.Font.Size = 8;
            tblCell.Range.Font.Name = "Arial";
            tblCell.Range.ParagraphFormat.Alignment =
                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
        }

        //second table(bv table1) starts  here
        private void addText_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                button6_Click(sender, e);
                flag = 1;
            }
            else
            {
                var para1 = (dynamic)Globals.ThisDocument.Paragraphs.Add();
                para1.Range.Text = "";
                para1.Range.InsertParagraphAfter();
                para1.Range.Text = "The boundary values for " + textbox1.Text + " are:";
                para1.Range.InsertParagraphAfter();
                Microsoft.Office.Interop.Word.Table table2 = AddColumns2();
                if (table2 != null)
                {
                    // write data to the created table
                    // TODO: Write relevant values              
                    if (textBox7.Text.Length == 0)
                    {
                        if (textBox14.Text == "")
                        {
                            three_boxes_Click(sender, e);
                            AddBv(box30.Text, box35.Text, textBox11.Text, "", textBox14.Text, table2);
                            AddBv(box31.Text, box36.Text, textBox12.Text, box37.Text, textBox15.Text, table2);
                        }
                        else if (textBox14.Text != "")
                        {
                            four_boxes_Click(sender, e);
                            AddBv(box30.Text, box7.Text, textBox11.Text, box8.Text, textBox14.Text, table2);
                            AddBv(box31.Text, box9.Text, textBox12.Text, box10.Text, textBox15.Text, table2);
                        }
                    }
                    else
                    {
                        if (textBox14.Text == "")
                        {
                            five_boxes_Click(sender, e);
                            AddBv(box32.Text, box23.Text, textBox11.Text, "", textBox14.Text, table2);
                            AddBv(box33.Text, box24.Text, textBox12.Text, box25.Text, textBox15.Text, table2);
                            AddBv(box34.Text, box26.Text, textBox13.Text, box27.Text, textBox16.Text, table2);
                        }
                        else if (textBox14.Text != "")
                        {
                            six_boxes_Click(sender, e);
                            AddBv(box32.Text, box1.Text, textBox11.Text, box2.Text, textBox14.Text, table2);
                            AddBv(box33.Text, box3.Text, textBox12.Text, box4.Text, textBox15.Text, table2);
                            AddBv(box34.Text, box5.Text, textBox13.Text, box6.Text, textBox16.Text, table2);
                        }
                    }
                    // Add a page break to the document
                    //((dynamic)Globals.ThisDocument.Paragraphs).Last.Range.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);
                }
            }
        }

        public Microsoft.Office.Interop.Word.Table AddColumns2()
        {
            object missing = System.Type.Missing;
            Microsoft.Office.Interop.Word.Table tb2 = null;
            try
            {
                // Create a table.
                object miss = System.Type.Missing;
                var paragraph = Globals.ThisDocument.Paragraphs.Add(ref miss);

                tb2 = Globals.ThisDocument.Tables.Add(paragraph.Range, 1, 5, ref missing, ref missing);
                // set the border line style
                tb2.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                tb2.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                tb2.PreferredWidth = 432.18f;
                tb2.Columns[1].PreferredWidth = 62.2f;
                //tbl.Columns[1].Cells.PreferredWidth = 16;
                tb2.Columns[2].PreferredWidth = 35.4f;
                tb2.Columns[3].PreferredWidth = 163.4f;
                tb2.Columns[4].PreferredWidth = 35.4f;
                tb2.Columns[5].PreferredWidth = 128.0f;
                tb2.Rows.SetHeight(12.14f, WdRowHeightRule.wdRowHeightExactly);
                // Insert headings.
                SetHeadings(tb2.Cell(1, 1), "Equivalence class");
                SetHeadings(tb2.Cell(1, 2), "BV#");
                SetHeadings(tb2.Cell(1, 3), "Lower Boundary");
                SetHeadings(tb2.Cell(1, 4), "BV#");
                SetHeadings(tb2.Cell(1, 5), "Upper Boundary");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem creating Products table: " + ex.Message,
                    "Actions Pane", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tb2;
        }

        private void AddBv(string Eq, string BV1, string lb, string BV2, string ub, Microsoft.Office.Interop.Word.Table table2)
        {
            object miss = System.Type.Missing;
            // Add data from data row to the table.
            //Microsoft.Office.Interop.Word.Selection selection = Globals.ThisDocument.Application.Selection;
            Microsoft.Office.Interop.Word.Row newRow2 = table2.Rows.Add(ref miss);
            newRow2.Range.Font.Bold = 0;
            newRow2.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            newRow2.Cells[4].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
            newRow2.Range.Font.Name = "Arial";
            newRow2.Range.Font.Size = 8;
            newRow2.Cells[1].Range.Text = Eq;
            newRow2.Cells[2].Range.Text = BV1;
            newRow2.Cells[3].Range.Text = lb;
            newRow2.Cells[4].Range.Text = BV2;
            newRow2.Cells[5].Range.Text = ub;
        }            
        
        //second table(bv table1 ENDS here)

        //Third table (bv table2) code STARTS here
        private void addBv2_Click(object sender, EventArgs e)
        {
            if (flag2 == 0)
            {
                flag2 = 1;
            }
            else
            {

                var para1 = (dynamic)Globals.ThisDocument.Paragraphs.Add();
                para1.Range.Text = "";
                para1.Range.InsertParagraphAfter();
                para1.Range.Text = "The boundary values for " + textbox1.Text + " are:";
                para1.Range.Font.Name = "Arial";
                para1.Range.Font.Size = 11;
                para1.Range.InsertParagraphAfter();
                Microsoft.Office.Interop.Word.Table tb3 = AddColumns3();

                if (tb3 != null)
                {
                    twelveboxes_Click(sender, e);
                    AddBv2(box32.Text, box11.Text, textBox17.Text, textBox20.Text, box12.Text, textBox23.Text, textBox26.Text, tb3);
                    AddBv2(box33.Text, box13.Text, textBox18.Text, textBox21.Text, box14.Text, textBox24.Text, textBox27.Text, tb3);
                    AddBv2(box34.Text, box15.Text, textBox19.Text, textBox22.Text, box16.Text, textBox25.Text, textBox28.Text, tb3);

                    // Add a page break to the document
                    //((dynamic)Globals.ThisDocument.Paragraphs).Last.Range.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);
                }

            }
        }
        public Microsoft.Office.Interop.Word.Table AddColumns3()
        {
            object missing = System.Type.Missing;
            Microsoft.Office.Interop.Word.Table tb3 = null;
            //Microsoft.Office.Interop.Word.Table tb4 = null;
            //Microsoft.Office.Interop.Word.Table tb5 = null;

            try
            {
                // Create a table.
                object miss = System.Type.Missing;
                var paragraph = Globals.ThisDocument.Paragraphs.Add(ref miss);

                tb3 = Globals.ThisDocument.Tables.Add(paragraph.Range, 1, 5, ref missing, ref missing);
                tb3.PreferredWidth = 432.18f;
                //tb3.Columns[1].PreferredWidth = 62.2f;
                ////tbl.Columns[1].Cells.PreferredWidth = 16;
                //tb3.Columns[2].PreferredWidth = 35.4f;
                //tb3.Columns[3].PreferredWidth = 168.7f;
                //tb3.Columns[4].PreferredWidth = 35.4f;
                //tb3.Columns[5].PreferredWidth = 128.0f;
                //tb3.Columns[6].PreferredWidth = 64.0f;
                //tb3.Columns[7].PreferredWidth = 64.0f;
                tb3.Rows.SetHeight(12.14f, WdRowHeightRule.wdRowHeightExactly);
                //tb3.Rows.Add(ref missing);
                tb3.Cell(2, 3).Split(NumRows: 1, NumColumns: 2);
                tb3.Cell(2, 6).Split(NumRows: 1, NumColumns: 2);
                tb3.Rows.Add(ref missing);
                //tb3.Cell(3, 2).Split(NumRows: 1, NumColumns: 2);
                //tb3.Cell(3, 3).Split(NumRows: 1, NumColumns: 3);
                //tb3.Cell(3, 6).Split(NumRows: 1, NumColumns: 2);
                //tb3.Cell(3, 8).Split(NumRows: 1, NumColumns: 2);

                //==code to merge cells==

                tb3.Rows[1].Cells[3].Merge(tb3.Rows[1].Cells[4]);
                tb3.Rows[1].Cells[5].Merge(tb3.Rows[1].Cells[6]);

                //===End Merge cells===
                //tb3.Columns[1].Cells[1].Merge(tb3.Columns[1].Cells[2]);
                //tb4 = Globals.ThisDocument.Tables.Add(paragraph.Range, 1, 5, ref missing, ref missing);
                //tb5 = Globals.ThisDocument.Tables.Add(paragraph.Range, 1, 7, ref missing, ref missing);
                //tb3.PreferredWidth = 432.18f;
                //tb3.Columns.PreferredWidth = 49.98f;
                //// set the border line style
                tb3.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                tb3.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                //tb3.Rows.SetHeight(12.14f, WdRowHeightRule.wdRowHeightExactly);
                //// Insert headings.
                SetHeadings(tb3.Cell(1, 1), "Equivalence class");
                SetHeadings(tb3.Cell(1, 2), "BV#");
                SetHeadings(tb3.Cell(1, 3), "Lower Boundary");
                SetHeadings(tb3.Cell(1, 4), "BV#");
                SetHeadings(tb3.Cell(1, 5), "Upper Boundary");
                SetHeadings(tb3.Cell(2, 3), textBox2.Text);
                SetHeadings(tb3.Cell(2, 4), textBox3.Text);
                SetHeadings(tb3.Cell(2, 6), textBox2.Text);
                SetHeadings(tb3.Cell(2, 7), textBox3.Text);
                //InsertData(tb3.Cell(3, 2), "Eq");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem creating Products table: " + ex.Message,
                    "Actions Pane", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tb3;

        }
        private void AddBv2(string Eq, string BV1, string lb, string ub, string BV3, string lb1,string ub1, Microsoft.Office.Interop.Word.Table tb3)
        {
            object miss = System.Type.Missing;

            // Add data from data row to the table.
            //Microsoft.Office.Interop.Word.Selection selection = Globals.ThisDocument.Application.Selection;
            Microsoft.Office.Interop.Word.Row newRow3 = tb3.Rows.Add(ref miss);
            newRow3.Range.Font.Bold = 0;
            newRow3.Range.Font.Size = 8;
            newRow3.Range.Font.Name = "Arial";
            newRow3.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            newRow3.Cells[4].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            newRow3.Cells[1].Range.Text = Eq;
            newRow3.Cells[2].Range.Text = BV1;
            newRow3.Cells[3].Range.Text = lb;
            newRow3.Cells[4].Range.Text = ub;
            newRow3.Cells[5].Range.Text = BV3;
            newRow3.Cells[6].Range.Text = lb1;
            newRow3.Cells[7].Range.Text = ub1;
            
        }

        private void whether_checkbox_is_checked_or_not(object sender, EventArgs e)
        {
            /*This function is called by generate table button.
             Here the code is written same as button1 code. This is because if the checkbox1 is checked
             after parsing the condition it gives wrong output. So inorder to cross check it again this code 
             is implemented in this function and this function is cross checked in Generate table function*/
            string a = textbox1.Text;
            textBox5.Text = Convert.ToString(a);
            string b = "Valid";
            textBox8.Text = Convert.ToString(b);
            string d = Convert.ToString(textBox4.Text);
            char[] arr = new char[] { '>', '=' };
            char[] arr1 = new char[] { '<', '=' };

            if (checkBox1.Visible == true) //checkbox is visible means that the textbox is containing a macro
                {
                    if (checkBox1.Checked == true) //if checkbox is checked then the value of macro is zero
                    {
                        for (int i = 0; i < d.Length; i++)
                        {
                            if (d[i] == '<' && d.Length == 1)
                            {
                                textBox5.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Valid");
                                textBox6.Text = textBox2.Text + " is greater than or equal to " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                textBox14.Text = "";
                                count = 1;
                            }

                            if (d.Length == 2 && d[i] == '<' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is less than or equal to " + textBox3.Text;
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                count = 2;
                            }
                        }
                    }
                    else //check box is not checked which implies that the macro value is not zero
                    {
                        for (int i = 0; i < d.Length; i++)
                        {
                            if (d[i] == '<' && d.Length == 1)
                            {
                                textBox5.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Valid");
                                textBox6.Text = textBox2.Text + " is greater than or equal to " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                count = 3;
                            }

                            if (d.Length == 2 && d[i] == '<' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is less than or equal to " + textBox3.Text;
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                count = 4;
                            }
                        }
                    }

                }

                else //two are variables Ex: a<b
                {
                    for (int i = 0; i < d.Length; i++)
                    {
                        if (d[i] == '<' && d.Length == 1)
                        {

                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Valid");
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox8.Text = Convert.ToString("Invalid");
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Invalid");
                            count = 5;
                        }

                        if (d.Length == 2 && d[i] == '<' && d[i + 1] == '=' && textBox8.Text == b)
                        {
                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Valid");
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox8.Text = Convert.ToString("Invalid");
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Invalid");
                            count = 6;
                        }
                    }
                }

                //===========================case for <= ends====================

                //case for >= starts
                if (checkBox1.Visible == true) //checkbox is visible means that the textbox is containing a macro
                {
                    if (checkBox1.Checked == true) //if checkbox is checked then the value of macro is zero
                    {
                        for (int i = 0; i < d.Length; i++)
                        {
                            if (d[i] == '>' && d.Length == 1)
                            {
                                textBox5.Text = textBox2.Text + " is less than or equal to " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Invalid");
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Valid");
                                count = 7;
                            }


                            if (d.Length == 2 && d[i] == '>' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Invalid");
                                textBox6.Text = textBox2.Text + " is greater than or equal to " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Valid");
                                //these are valid only when 'a' is signed int	
                                count = 8;
                            }
                        }
                    }
                    else //Left side is variable and right side is macro Ex: a>MAX
                    {
                        for (int i = 0; i < d.Length; i++)
                        {
                            if (d[i] == '>' && d.Length == 1)
                            {

                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Valid");
                                textBox5.Text = textBox2.Text + " is less than or equal to " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Invalid");
                                count = 9;
                            }

                            if (d.Length == 2 && d[i] == '>' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Invalid");
                                textBox6.Text = textBox2.Text + " is greater than or equal to " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Valid");
                                count = 10;
                            }
                        }
                    }

                }
                else //two are variables Ex: a>b
                {
                    for (int i = 0; i < d.Length; i++)
                    {
                        if (d[i] == '>' && d.Length == 1)
                        {
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Invalid");
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Invalid");
                            count = 11;
                        }

                        if (d.Length == 2 && d[i] == '>' && d[i + 1] == '=' && textBox8.Text == b)
                        {
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Invalid");
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Valid");
                            count = 12;
                        }
                    }
                }
                //==========case for >= ends===================

                //case for != starts
                if (checkBox1.Visible == true) //checkbox is visible means that the textbox is containing a macro
                {
                    if (checkBox1.Checked == true) //if checkbox is checked then the value of macro is zero
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            if (d[i] == '!' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is equal to " + textBox3.Text;
                                textBox8.Text = "Invalid";
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("valid");
                                textBox7.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox10.Text = "Invalid";
                                count = 13;
                            }
                            if (d[i] == '=' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is equal to " + textBox3.Text;
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox7.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                count = 14;
                            }
                        }
                    }

                    else //If macro value is not zero then perform this action
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            if (d[i] == '!' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is equal to " + textBox3.Text;
                                textBox8.Text = "Invalid";
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Valid");
                                textBox7.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox10.Text = Convert.ToString("Valid");
                                count = 15;
                            }

                            if (d[i] == '=' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is equal to " + textBox3.Text;
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                textBox7.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox10.Text = Convert.ToString("Invalid");
                                count = 16;
                            }
                        }
                    }
                }

                else //if both are inputs then perform this ex: a!=b
                {
                    for (int i = 0; i < 1; i++)
                    {
                        if (d[i] == '!' && d[i + 1] == '=' && textBox8.Text == b)
                        {
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Invalid");
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox8.Text = Convert.ToString("Valid");
                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Valid");
                            count = 17;
                        }

                        if (d[i] == '=' && d[i + 1] == '=' && textBox8.Text == b)
                        {
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Valid");
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox8.Text = Convert.ToString("Invalid");
                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Invalid");
                            count = 18;
                        }
                    } //case for != and == ends here
                }
        }
        int count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            textbox1.Enabled = false;
            button2_Click(sender, e);
            if (flag4 == 1) //flag4==1 indicates that the condition is wrong and error message is printed on the screen
            {
                flag4 = 0; 
            }
            else
            {
                Generate_Table.Visible = true;
                comboBox1.Visible = true;
                label27.Visible = false;
                textBox2.Visible = true;
                textBox3.Visible = true;

                string a = textbox1.Text;
                textBox5.Text = Convert.ToString(a);
                string b = "Valid";
                textBox8.Text = Convert.ToString(b);
                string d = Convert.ToString(textBox4.Text);
                char[] arr = new char[] { '>', '=' };
                char[] arr1 = new char[] { '<', '=' };

                //case for <= starts

                if (checkBox1.Visible == true) //checkbox is visible means that the textbox is containing a macro
                {
                    if (checkBox1.Checked == true) //if checkbox is checked then the value of macro is zero
                    {
                        for (int i = 0; i < d.Length; i++)
                        {
                            if (d[i] == '<' && d.Length == 1)
                            {
                                textBox5.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Valid");
                                textBox6.Text = textBox2.Text + " is greater than or equal to " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                textBox14.Text = "";
                                count = 1;
                            }

                            if (d.Length == 2 && d[i] == '<' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is less than or equal to " + textBox3.Text;
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                count = 2;
                            }
                        }
                    }
                    else //check box is not checked which implies that the macro value is not zero
                    {
                        for (int i = 0; i < d.Length; i++)
                        {
                            if (d[i] == '<' && d.Length == 1)
                            {
                                textBox5.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Valid");
                                textBox6.Text = textBox2.Text + " is greater than or equal to " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                count = 3;
                            }

                            if (d.Length == 2 && d[i] == '<' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is less than or equal to " + textBox3.Text;
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                count = 4;
                            }
                        }
                    }

                }

                else //two are variables Ex: a<b
                {
                    for (int i = 0; i < d.Length; i++)
                    {
                        if (d[i] == '<' && d.Length == 1)
                        {

                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Valid");
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox8.Text = Convert.ToString("Invalid");
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Invalid");
                            count = 5;
                        }

                        if (d.Length == 2 && d[i] == '<' && d[i + 1] == '=' && textBox8.Text == b)
                        {
                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Valid");
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox8.Text = Convert.ToString("Invalid");
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Invalid");
                            count = 6;
                        }
                    }
                }

                //===========================case for <= ends====================

                //case for >= starts
                if (checkBox1.Visible == true) //checkbox is visible means that the textbox is containing a macro
                {
                    if (checkBox1.Checked == true) //if checkbox is checked then the value of macro is zero
                    {
                        for (int i = 0; i < d.Length; i++)
                        {
                            if (d[i] == '>' && d.Length == 1)
                            {
                                textBox5.Text = textBox2.Text + " is less than or equal to " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Invalid");
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Valid");
                                count = 7;
                            }


                            if (d.Length == 2 && d[i] == '>' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Invalid");
                                textBox6.Text = textBox2.Text + " is greater than or equal to " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Valid");
                                //these are valid only when 'a' is signed int	
                                count = 8;
                            }
                        }
                    }
                    else //Left side is variable and right side is macro Ex: a>MAX
                    {
                        for (int i = 0; i < d.Length; i++)
                        {
                            if (d[i] == '>' && d.Length == 1)
                            {

                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Valid");
                                textBox5.Text = textBox2.Text + " is less than or equal to " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Invalid");
                                count = 9;
                            }

                            if (d.Length == 2 && d[i] == '>' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox8.Text = Convert.ToString("Invalid");
                                textBox6.Text = textBox2.Text + " is greater than or equal to " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Valid");
                                count = 10;
                            }
                        }
                    }

                }
                else //two are variables Ex: a>b
                {
                    for (int i = 0; i < d.Length; i++)
                    {
                        if (d[i] == '>' && d.Length == 1)
                        {
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Invalid");
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Invalid");
                            count = 11;
                        }

                        if (d.Length == 2 && d[i] == '>' && d[i + 1] == '=' && textBox8.Text == b)
                        {
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Invalid");
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Valid");
                            count = 12;
                        }
                    }
                }
                //==========case for >= ends===================

                //case for != starts
                if (checkBox1.Visible == true) //checkbox is visible means that the textbox is containing a macro
                {
                    if (checkBox1.Checked == true) //if checkbox is checked then the value of macro is zero
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            if (d[i] == '!' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is equal to " + textBox3.Text;
                                textBox8.Text = "Invalid";
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("valid");
                                textBox7.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox10.Text = "Invalid";
                                count = 13;
                            }
                            if (d[i] == '=' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is equal to " + textBox3.Text;
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox7.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                count = 14;
                            }
                        }
                    }

                    else //If macro value is not zero then perform this action
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            if (d[i] == '!' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is equal to " + textBox3.Text;
                                textBox8.Text = "Invalid";
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Valid");
                                textBox7.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox10.Text = Convert.ToString("Valid");
                                count = 15;
                            }

                            if (d[i] == '=' && d[i + 1] == '=' && textBox8.Text == b)
                            {
                                textBox5.Text = textBox2.Text + " is equal to " + textBox3.Text;
                                textBox6.Text = textBox2.Text + " is greater than " + textBox3.Text;
                                textBox9.Text = Convert.ToString("Invalid");
                                textBox7.Text = textBox2.Text + " is less than " + textBox3.Text;
                                textBox10.Text = Convert.ToString("Invalid");
                                count = 16;
                            }
                        }
                    }
                }

                else //if both are inputs then perform this ex: a!=b
                {
                    for (int i = 0; i < 1; i++)
                    {
                        if (d[i] == '!' && d[i + 1] == '=' && textBox8.Text == b)
                        {
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Invalid");
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox8.Text = Convert.ToString("Valid");
                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Valid");
                            count = 17;
                        }

                        if (d[i] == '=' && d[i + 1] == '=' && textBox8.Text == b)
                        {
                            textBox7.Text = textBox2.Text + " is equal to " + textBox3.Text;
                            textBox10.Text = Convert.ToString("Valid");
                            textBox5.Text = textBox2.Text + " is greater than " + textBox3.Text;
                            textBox8.Text = Convert.ToString("Invalid");
                            textBox6.Text = textBox2.Text + " is less than " + textBox3.Text;
                            textBox9.Text = Convert.ToString("Invalid");
                            count = 18;
                        }
                    } //case for != and == ends here
                }
            }
        }
        int flag=1;
        private void Generate_Bv_Click(object sender, EventArgs e)
        {
            string str = Convert.ToString(textBox3.Text);
          
            if (count == 1)
            {
                if (comboBox1.SelectedIndex == 1)
                {
                    textBox11.Text = Convert.ToString("-128");
                    textBox15.Text = Convert.ToString("127");
                    textBox14.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    textBox11.Text = Convert.ToString("-32768");
                    textBox15.Text = Convert.ToString("32767");
                    textBox14.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    textBox11.Text = Convert.ToString("-2147483648");
                    textBox14.Text = Convert.ToString(str + "-1");
                    textBox15.Text = Convert.ToString("2147483647");

                }
                if (comboBox1.SelectedIndex == 4)
                {
                    textBox11.Text = Convert.ToString("-9223372036854775808");
                    textBox15.Text = Convert.ToString("9223372036854775807");
                    textBox14.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 5)
                {
                   MessageBox.Show("Please check the condition", "Error!!");
                   flag = 0;
                }
                if (comboBox1.SelectedIndex == 6)
                {
                    MessageBox.Show("Please check the condition", "Error!!");
                    flag = 0;
                }
                if (comboBox1.SelectedIndex == 7)
                {
                    MessageBox.Show("Please check the condition", "Error!!");
                    flag = 0;
                }
                if (comboBox1.SelectedIndex == 8)
                {
                    MessageBox.Show("Please check the condition", "Error!!");
                    flag = 0;
                }
                textBox12.Text = Convert.ToString(str);

            }
            if (count == 2)
            {
                if (comboBox1.SelectedIndex == 1)
                {
                    textBox11.Text = Convert.ToString("-128");
                    textBox15.Text = Convert.ToString("127");
                    textBox14.Text = Convert.ToString(str);
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    textBox11.Text = Convert.ToString("-32768");
                    textBox15.Text = Convert.ToString("32767");
                    textBox14.Text = Convert.ToString(str);
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    textBox11.Text = Convert.ToString("-2147483648");
                    textBox14.Text = Convert.ToString(str);
                    textBox15.Text = Convert.ToString("2147483647");

                }
                if (comboBox1.SelectedIndex == 4)
                {
                    textBox11.Text = Convert.ToString("-9223372036854775808");
                    textBox15.Text = Convert.ToString("9223372036854775807");
                    textBox14.Text = Convert.ToString(str);
                }
                if (comboBox1.SelectedIndex == 5)
                {
                    MessageBox.Show("Please check the condition", "Error!!");
                    flag = 0;
                }
                if (comboBox1.SelectedIndex == 6)
                {
                    MessageBox.Show("Please check the condition", "Error!!");
                    flag = 0;
                }
                if (comboBox1.SelectedIndex == 7)
                {
                    MessageBox.Show("Please check the condition", "Error!!");
                    flag = 0;
                }
                if (comboBox1.SelectedIndex == 8)
                {
                    MessageBox.Show("Please check the condition", "Error!!");
                    flag = 0;
                }
                textBox12.Text = Convert.ToString(str + "+1");

            }
            if (count == 3)
            {
                if (comboBox1.SelectedIndex == 1)
                    textBox11.Text = Convert.ToString("-128");
                if (comboBox1.SelectedIndex == 2)
                    textBox11.Text = Convert.ToString("-32768");
                if (comboBox1.SelectedIndex == 3)
                    textBox11.Text = Convert.ToString("-2147483648");
                if (comboBox1.SelectedIndex == 4)
                    textBox11.Text = Convert.ToString("-9223372036854775808");
                if (comboBox1.SelectedIndex == 5)
                    textBox11.Text = Convert.ToString("0");
                if (comboBox1.SelectedIndex == 6)
                    textBox11.Text = Convert.ToString("0");
                if (comboBox1.SelectedIndex == 7)
                    textBox11.Text = Convert.ToString("0");
                if (comboBox1.SelectedIndex == 8)
                {
                    textBox11.Text = Convert.ToString("0");
                }
                textBox14.Text = Convert.ToString(str + "-1");
                textBox12.Text = Convert.ToString(str);
                button7_Click(sender, e);               

            }
            if (count == 4)
            {
                textBox13.Visible = false;
                textBox16.Visible = false;
                label10.Visible = false;
                label13.Visible = false;
                if (comboBox1.SelectedIndex == 1)
                    textBox11.Text = Convert.ToString("-128");
                if (comboBox1.SelectedIndex == 2)
                    textBox11.Text = Convert.ToString("-32768");
                if (comboBox1.SelectedIndex == 3)
                    textBox11.Text = Convert.ToString("-2147483648");
                if (comboBox1.SelectedIndex == 4)
                    textBox11.Text = Convert.ToString("-9223372036854775808");
                if (comboBox1.SelectedIndex == 5)
                    textBox11.Text = Convert.ToString("0");
                if (comboBox1.SelectedIndex == 6)
                    textBox11.Text = Convert.ToString("0");
                if (comboBox1.SelectedIndex == 7)
                    textBox11.Text = Convert.ToString("0");
                if (comboBox1.SelectedIndex == 8)
                {
                    textBox11.Text = Convert.ToString("0");
                }
                textBox14.Text = Convert.ToString(str);
                textBox12.Text = Convert.ToString(str + "+1");
                button7_Click(sender, e);

            }
            //a<b
            if (count == 5 || count == 6)
            {                
                button7_Click(sender, e);
                if (comboBox2.Visible = true && comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a valid range for right variable", "Warning!!");
                    flag7 = 1;
                }
            }
            if (count == 7)
            {
                textBox12.Text = Convert.ToString(str + "+1");

                if (comboBox1.SelectedIndex == 1)
                {
                    textBox11.Text = Convert.ToString("-128");
                    textBox15.Text = Convert.ToString("127");
                    textBox14.Text = Convert.ToString(str);
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    textBox11.Text = Convert.ToString("-32768");
                    textBox15.Text = Convert.ToString("32767");
                    textBox14.Text = Convert.ToString(str);
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    textBox11.Text = Convert.ToString("-2147483648");
                    textBox15.Text = Convert.ToString("2147483647");
                    textBox14.Text = Convert.ToString(str);
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    textBox11.Text = Convert.ToString("-9223372036854775808");
                    textBox15.Text = Convert.ToString("9223372036854775807");
                    textBox14.Text = Convert.ToString(str);
                }
                if (comboBox1.SelectedIndex == 5)
                {
                    textBox11.Text = Convert.ToString(str);
                    textBox15.Text = Convert.ToString("255");
                    textBox14.Text = "";
                }
                if (comboBox1.SelectedIndex == 6)
                {
                    textBox11.Text = Convert.ToString(str);
                    textBox15.Text = Convert.ToString("65535");
                    textBox14.Text = "";
                }
                if (comboBox1.SelectedIndex == 7)
                {
                    textBox11.Text = Convert.ToString(str);
                    textBox15.Text = Convert.ToString("4294967295");
                    textBox14.Text = "";
                }
                if (comboBox1.SelectedIndex == 8)
                {
                    textBox11.Text = Convert.ToString(str);
                    textBox15.Text = Convert.ToString("18446744073709551615");
                    textBox14.Text = "";
                }
                //textBox12.Text = Convert.ToString(str);                
               
            }
            if (count == 8)  //a>=MACRO && macro value is zero
            {
                if (comboBox1.SelectedIndex == 1)
                {
                    textBox11.Text = Convert.ToString("-128");
                    textBox15.Text = Convert.ToString("127");
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    textBox11.Text = Convert.ToString("-32768");
                    textBox15.Text = Convert.ToString("32767");
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    textBox11.Text = Convert.ToString("-2147483648");
                    textBox15.Text = Convert.ToString("2147483647");
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    textBox11.Text = Convert.ToString("-9223372036854775808");
                    textBox15.Text = Convert.ToString("9223372036854775807");
                }
                if (comboBox1.SelectedIndex == 5)
                {
                    textBox11.Text = Convert.ToString("0");
                    textBox15.Text = Convert.ToString("255");
                }
                if (comboBox1.SelectedIndex == 6)
                {
                    textBox11.Text = Convert.ToString("0");
                    textBox15.Text = Convert.ToString("65535");
                }
                if (comboBox1.SelectedIndex == 7)
                {
                    textBox11.Text = Convert.ToString("0");
                    textBox15.Text = Convert.ToString("4294967295");
                }
                if (comboBox1.SelectedIndex == 8)
                {
                    textBox11.Text = Convert.ToString("0");
                    textBox15.Text = Convert.ToString("18446744073709551615");
                }
                textBox14.Text = Convert.ToString(str + "-1");
                textBox12.Text = Convert.ToString(str);
                button7_Click(sender, e);              

            }

            if (count == 9)
            {
                if (comboBox1.SelectedIndex == 1)
                    textBox11.Text = Convert.ToString("-128");
                if (comboBox1.SelectedIndex == 2)
                {
                    textBox11.Text = Convert.ToString("-32768");
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    textBox11.Text = Convert.ToString("-2147483648");
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    textBox11.Text = Convert.ToString("-9223372036854775808");
                }
                textBox12.Text = Convert.ToString(str + "+1");
                button7_Click(sender, e);
                if (comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 6 || comboBox1.SelectedIndex == 7 || comboBox1.SelectedIndex == 8)
                {
                    textBox11.Text = Convert.ToString("0");
                }
                textBox14.Text = Convert.ToString(str);                

            }
            if (count == 10)
            {
                if (comboBox1.SelectedIndex == 1)
                    textBox11.Text = Convert.ToString("-128");
                if (comboBox1.SelectedIndex == 2)
                {
                    textBox11.Text = Convert.ToString("-32768");
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    textBox11.Text = Convert.ToString("-2147483648");
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    textBox11.Text = Convert.ToString("-9223372036854775808");
                }
                textBox12.Text = Convert.ToString(str + "+1");
                button7_Click(sender, e);
                if (comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 6 || comboBox1.SelectedIndex == 7 || comboBox1.SelectedIndex == 8)
                {
                    textBox11.Text = Convert.ToString("0");
                }
                textBox12.Text = Convert.ToString(str);
                button7_Click(sender, e);                
                textBox14.Text = Convert.ToString(str + "-1");
                

            }

            if (count == 11 || count == 12)
            {               
                //call function to return a>b and a<b inputs
                button7_Click(sender, e);
                if (comboBox2.Visible = true && comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a valid range for right variable", "Warning!!");
                    flag7 = 1;
                }


            }
            if (count == 13 || count == 14)
            {       
                if (comboBox1.SelectedIndex == 1)
                {
                    textBox13.Text = Convert.ToString("-128");
                    textBox16.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    textBox13.Text = Convert.ToString("-32768");
                    textBox16.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    textBox13.Text = Convert.ToString("-2147483648");
                    textBox16.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    textBox13.Text = Convert.ToString("-9223372036854775808");
                    textBox16.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 6 || comboBox1.SelectedIndex == 7 || comboBox1.SelectedIndex == 8)
                {
                    textBox7.Text = "";
                    textBox10.Text = "";
                    textBox13.Text = "";
                    textBox16.Text = "";
                }
                textBox11.Text = Convert.ToString(str);
                textBox14.Text = "";
                textBox12.Text = Convert.ToString(str + "+1");
                button7_Click(sender, e);
                

            }
            if (count == 15 || count == 16)
            {

                if (comboBox1.SelectedIndex == 4)
                {
                    textBox11.Text = Convert.ToString("-9223372036854775808");
                    textBox15.Text = Convert.ToString("9223372036854775807");
                }

                if (comboBox1.SelectedIndex == 1)
                {
                    textBox13.Text = Convert.ToString("-128");
                    textBox15.Text = Convert.ToString("127");
                    textBox16.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    textBox13.Text = Convert.ToString("-32768");
                    textBox15.Text = Convert.ToString("32767");
                    textBox16.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    textBox13.Text = Convert.ToString("-2147483648");
                    textBox15.Text = Convert.ToString("2147483647");
                    textBox16.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    textBox13.Text = Convert.ToString("-9223372036854775808");
                    textBox15.Text = Convert.ToString("9223372036854775807");
                    textBox16.Text = Convert.ToString(str + "-1");
                }
                if (comboBox1.SelectedIndex == 5)
                {
                    textBox11.Text = Convert.ToString("0");
                    textBox13.Text = Convert.ToString(0);
                    textBox15.Text = Convert.ToString("255");
                }
                if (comboBox1.SelectedIndex == 6)
                {
                    textBox15.Text = Convert.ToString("65535");
                    textBox13.Text = Convert.ToString(0);
                }
                if (comboBox1.SelectedIndex == 7)
                {
                    textBox15.Text = Convert.ToString("4294967295");
                    textBox13.Text = Convert.ToString(0);
                }
                if (comboBox1.SelectedIndex == 8)
                {
                    textBox15.Text = Convert.ToString("18446744073709551615");
                    textBox13.Text = Convert.ToString(0);
                }
                textBox11.Text = Convert.ToString(str);
                textBox14.Text = "";
                textBox12.Text = Convert.ToString(str + "+1");
                button7_Click(sender, e);
                //
                textBox16.Text = Convert.ToString(str + "-1");

            }
            if (count == 17 || count == 18)
            {               
                button7_Click(sender, e);
                if (comboBox2.Visible = true && comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a valid range for right variable", "Warning!!");
                    flag7 = 1;
                }

            }
        }


        int flag4 = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear();
            textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); textBox9.Clear(); textBox10.Clear();
            textBox11.Clear(); textBox12.Clear(); textBox13.Clear(); textBox14.Clear(); textBox15.Clear();
            textBox16.Clear();
            //checkBox1.Checked = false;
            //comboBox2.Visible = true;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            

            string a = textbox1.Text;
            int i, j = 0, c = 0;
            for (i = 0; i < textbox1.Text.Length; i++)
            {
                if ((a[i] == '<' && a[i + 1] == '=') || (a[i] == '>' && a[i + 1] == '=') || (a[i] == '!' && a[i + 1] == '=') || (a[i] == '=' && a[i + 1] == '='))
                {
                    textBox4.Text = Convert.ToString(a[i]) + Convert.ToString(a[i + 1]);
                    c = 1;
                    break;
                }
                if (a[i] == '<' || a[i] == '>')
                {
                    textBox4.Text = Convert.ToString(a[i]);
                    c = 2;
                    break;
                }
                j++;
            }
            if (c == 1)
            {
                string sub1 = a.Substring(0, j);
                string sub2 = a.Substring(j + 2);
                textBox2.Text = Convert.ToString(sub1);
                textBox3.Text = Convert.ToString(sub2);
                for (int k = 0; k < sub2.Length; k++)
                {
                    if (Char.IsUpper(sub2[k])) //if it is a macro then combobox is not visible but checkbox is visible
                    {
                        comboBox2.Visible = false;
                        checkBox1.Visible = true;
                    }
                    else
                    {
                        comboBox2.Visible = true; //if it is not a macro then combobox is visible but checkbox is not visible
                        checkBox1.Visible = false;
                    }
                }
            }

            else if (c == 2)
            {
                string sub1 = a.Substring(0, j);
                string sub2 = a.Substring(j + 1);
                textBox2.Text = Convert.ToString(sub1);
                textBox3.Text = Convert.ToString(sub2);
                for (int k = 0; k < sub2.Length; k++)
                {
                    if (Char.IsUpper(sub2[k])) //if it is a macro then combobox is not visible but checkbox is visible
                    {
                        comboBox2.Visible = false;
                        checkBox1.Visible = true;
                    }
                    else //if it is not a macro then combobox is visible but checkbox is not visible
                    {
                        comboBox2.Visible = true;
                        checkBox1.Visible = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid condition,please enter correct condition", "Error!!");
                flag4 = 1;
                //comboBox2.Visible = false;
                button6_Click(sender, e);
                textbox1.Clear();
                textbox1.Focus();               
            }
        }

        private void button6_Click(object sender, EventArgs e) //Clear button
        {
            textbox1.Enabled = true;
            Generate_Table.Visible = false;
            label27.Visible = false;
            comboBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textbox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear();
            textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); textBox9.Clear(); textBox10.Clear();
            textBox11.Clear(); textBox12.Clear(); textBox13.Clear(); textBox14.Clear(); textBox15.Clear();
            textBox16.Clear(); textBox17.Clear(); textBox18.Clear(); textBox19.Clear();
            textBox20.Clear(); textBox21.Clear(); textBox22.Clear();
            textBox23.Clear(); textBox24.Clear(); textBox25.Clear();
            textBox26.Clear(); textBox27.Clear(); textBox28.Clear();
            checkBox1.Checked = true;
            checkBox1.Checked = false;
            comboBox2.Visible = false;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;                   
            textbox1.Focus();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //These inputs are given when only one side is variable ex a>M , a<M etc
           
            if (comboBox1.SelectedIndex == 1)
                textBox15.Text = Convert.ToString("127");
            if (comboBox1.SelectedIndex == 2)
                textBox15.Text = Convert.ToString("32767");
            if (comboBox1.SelectedIndex == 3)
                textBox15.Text = Convert.ToString("2147483647");
            if (comboBox1.SelectedIndex == 4)
                textBox15.Text = Convert.ToString("9223372036854775807");
            if (comboBox1.SelectedIndex == 5)
                textBox15.Text = Convert.ToString("255");
            if (comboBox1.SelectedIndex == 6)
                textBox15.Text = Convert.ToString("65535");
            if (comboBox1.SelectedIndex == 7)
                textBox15.Text = Convert.ToString("4294967295");
            if (comboBox1.SelectedIndex == 8)
                textBox15.Text = Convert.ToString("18446744073709551615"); //end here

            //These inputs are given when two sides are variables Ex: a>b  or a<b

            if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 1) //Int 8 and Int 8
            {
                textBox17.Text = Convert.ToString("-127");
                textBox18.Text = Convert.ToString("-128");
                textBox19.Text = Convert.ToString("-128");
                textBox20.Text = Convert.ToString("-128");
                textBox21.Text = Convert.ToString("-127");
                textBox22.Text = Convert.ToString("-128");
                textBox23.Text = Convert.ToString("127");
                textBox24.Text = Convert.ToString("126");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("126");
                textBox27.Text = Convert.ToString("127");
                textBox28.Text = Convert.ToString("127");

            }
            if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 2) //Int 8 and Int 16
            {
                textBox17.Text = Convert.ToString("-128");
                textBox18.Text = Convert.ToString("-128");
                textBox19.Text = Convert.ToString("-128");
                textBox20.Text = Convert.ToString("-32768");
                textBox21.Text = Convert.ToString("-127");
                textBox22.Text = Convert.ToString("-128");
                textBox23.Text = Convert.ToString("127");
                textBox24.Text = Convert.ToString("127");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("126");
                textBox27.Text = Convert.ToString("32767");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 3) //Int 8 and Int 32
            {
                textBox17.Text = Convert.ToString("-128");
                textBox18.Text = Convert.ToString("-128");
                textBox19.Text = Convert.ToString("-128");
                textBox20.Text = Convert.ToString("-2147483648");
                textBox21.Text = Convert.ToString("-127");
                textBox22.Text = Convert.ToString("-128");
                textBox23.Text = Convert.ToString("127");
                textBox24.Text = Convert.ToString("127");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("126");
                textBox27.Text = Convert.ToString("2147483647");
                textBox28.Text = Convert.ToString("127");

            }
            if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 4) //Int 8 and Int 64
            {
                textBox17.Text = Convert.ToString("-128");
                textBox18.Text = Convert.ToString("-128");
                textBox19.Text = Convert.ToString("-128");
                textBox20.Text = Convert.ToString("-9223372036854775808");
                textBox21.Text = Convert.ToString("-127");
                textBox22.Text = Convert.ToString("-128");
                textBox23.Text = Convert.ToString("127");
                textBox24.Text = Convert.ToString("127");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("126");
                textBox27.Text = Convert.ToString("9223372036854775807");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 5) //Int 8 and Uint 8
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-128");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("127");
                textBox24.Text = Convert.ToString("127");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("126");
                textBox27.Text = Convert.ToString("255");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 6) //Int 8 and Uint 16
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-128");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("127");
                textBox24.Text = Convert.ToString("127");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("126");
                textBox27.Text = Convert.ToString("65535");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 7) //Int 8 and Uint 32
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-128");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("127");
                textBox24.Text = Convert.ToString("127");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("126");
                textBox27.Text = Convert.ToString("4294967295");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 1 && comboBox2.SelectedIndex == 8)//Int 8 and Uint 64
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-128");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("127");
                textBox24.Text = Convert.ToString("127");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("126");
                textBox27.Text = Convert.ToString("18446744073709551615");
                textBox28.Text = Convert.ToString("127");
            }//end of combo box selected index =1 for left variable



            //start of combobox selected index=2 for left variable i.e a
            if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 1)//Int 16 and Int 8
            {
                textBox17.Text = Convert.ToString("-127");
                textBox18.Text = Convert.ToString("-32768");
                textBox19.Text = Convert.ToString("-128");
                textBox20.Text = Convert.ToString("-128");
                textBox21.Text = Convert.ToString("-128");
                textBox22.Text = Convert.ToString("-128");
                textBox23.Text = Convert.ToString("32767");
                textBox24.Text = Convert.ToString("126");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("127");
                textBox27.Text = Convert.ToString("127");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 2) //Int 16 and Int 16
            {
                textBox17.Text = Convert.ToString("-32767");
                textBox18.Text = Convert.ToString("-32768");
                textBox19.Text = Convert.ToString("-32768");
                textBox20.Text = Convert.ToString("-32768");
                textBox21.Text = Convert.ToString("-32767");
                textBox22.Text = Convert.ToString("-32768");
                textBox23.Text = Convert.ToString("32767");
                textBox24.Text = Convert.ToString("32766");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32766");
                textBox27.Text = Convert.ToString("32767");
                textBox28.Text = Convert.ToString("32767");
            }
            if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 3) //Int 16 and Int 32
            {
                textBox17.Text = Convert.ToString("-32768");
                textBox18.Text = Convert.ToString("-32768");
                textBox19.Text = Convert.ToString("-32768");
                textBox20.Text = Convert.ToString("-2147483648");
                textBox21.Text = Convert.ToString("-32767");
                textBox22.Text = Convert.ToString("-32768");
                textBox23.Text = Convert.ToString("32767");
                textBox24.Text = Convert.ToString("32767");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32766");
                textBox27.Text = Convert.ToString("2147483647");
                textBox28.Text = Convert.ToString("32767");
            }
            if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 4)  //Int 16 and Int 64
            {
                textBox17.Text = Convert.ToString("-32768");
                textBox18.Text = Convert.ToString("-32768");
                textBox19.Text = Convert.ToString("-32768");
                textBox20.Text = Convert.ToString("-9223372036854775808");
                textBox21.Text = Convert.ToString("-32767");
                textBox22.Text = Convert.ToString("-32768");
                textBox23.Text = Convert.ToString("32767");
                textBox24.Text = Convert.ToString("32767");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32766");
                textBox27.Text = Convert.ToString("9223372036854775807");
                textBox28.Text = Convert.ToString("32767");
            }
            if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 5) //Int 16 and Uint 8
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-32768");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("32767");
                textBox24.Text = Convert.ToString("254");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("255");
                textBox27.Text = Convert.ToString("255");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 6) //Int 16 and Uint 16
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-32768");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("32767");
                textBox24.Text = Convert.ToString("32767");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32766");
                textBox27.Text = Convert.ToString("65535");
                textBox28.Text = Convert.ToString("32767");
            }
            if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 7) //Int 16 and Uint 32
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-32768");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("32767");
                textBox24.Text = Convert.ToString("32767");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32766");
                textBox27.Text = Convert.ToString("4294967295");
                textBox28.Text = Convert.ToString("32767");
            }
            if (comboBox1.SelectedIndex == 2 && comboBox2.SelectedIndex == 8) //Int 16 and Uint 64
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-32768");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("32767");
                textBox24.Text = Convert.ToString("32767");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32766");
                textBox27.Text = Convert.ToString("18446744073709551615");
                textBox28.Text = Convert.ToString("32767");
            }//end of combobox selected index=2 for left variable i.e a

            //start of combobox selected index=3 for left variable i.e a
            if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 1) //Int 32 and Int 8
            {
                textBox17.Text = Convert.ToString("-127");
                textBox18.Text = Convert.ToString("-2147483648");
                textBox19.Text = Convert.ToString("-128");
                textBox20.Text = Convert.ToString("-128");
                textBox21.Text = Convert.ToString("-128");
                textBox22.Text = Convert.ToString("-128");
                textBox23.Text = Convert.ToString("2147483647");
                textBox24.Text = Convert.ToString("126");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("127");
                textBox27.Text = Convert.ToString("127");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 2)//Int 32 and Int 16
            {
                textBox17.Text = Convert.ToString("-32767");
                textBox18.Text = Convert.ToString("-2147483648");
                textBox19.Text = Convert.ToString("-32768");
                textBox20.Text = Convert.ToString("-32768");
                textBox21.Text = Convert.ToString("-32768");
                textBox22.Text = Convert.ToString("-32768");
                textBox23.Text = Convert.ToString("2147483647");
                textBox24.Text = Convert.ToString("32766");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32767");
                textBox27.Text = Convert.ToString("32767");
                textBox28.Text = Convert.ToString("32767");
            }
            if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 3)//Int 32 and Int 32
            {
                textBox17.Text = Convert.ToString("-2147483647");
                textBox18.Text = Convert.ToString("-2147483648");
                textBox19.Text = Convert.ToString("-2147483648");
                textBox20.Text = Convert.ToString("-2147483648");
                textBox21.Text = Convert.ToString("-2147483647");
                textBox22.Text = Convert.ToString("-2147483648");
                textBox23.Text = Convert.ToString("2147483647");
                textBox24.Text = Convert.ToString("2147483646");
                textBox25.Text = Convert.ToString("2147483647");
                textBox26.Text = Convert.ToString("2147483646");
                textBox27.Text = Convert.ToString("2147483647");
                textBox28.Text = Convert.ToString("2147483647");
            }
            if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 4) //Int 32 and Int 64
            {
                textBox17.Text = Convert.ToString("-2147483648");
                textBox18.Text = Convert.ToString("-2147483648");
                textBox19.Text = Convert.ToString("-2147483648");
                textBox20.Text = Convert.ToString("-9223372036854775808");
                textBox21.Text = Convert.ToString("-2147483647");
                textBox22.Text = Convert.ToString("-2147483648");
                textBox23.Text = Convert.ToString("2147483647");
                textBox24.Text = Convert.ToString("2147483647");
                textBox25.Text = Convert.ToString("2147483647");
                textBox26.Text = Convert.ToString("2147483646");
                textBox27.Text = Convert.ToString("9223372036854775807");
                textBox28.Text = Convert.ToString("2147483647");
            }
            if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 5) //Int 32 and Uint 8
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-2147483648");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("2147483647");
                textBox24.Text = Convert.ToString("254");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("255");
                textBox27.Text = Convert.ToString("255");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 6)//Int 32 and Uint 16
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-2147483648");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("2147483647");
                textBox24.Text = Convert.ToString("65534");
                textBox25.Text = Convert.ToString("65535");
                textBox26.Text = Convert.ToString("65535");
                textBox27.Text = Convert.ToString("65535");
                textBox28.Text = Convert.ToString("65535");
            }
            if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 7) //Int 32 and Uint 32
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-2147483648");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("2147483647");
                textBox24.Text = Convert.ToString("2147483647");
                textBox25.Text = Convert.ToString("2147483647");
                textBox26.Text = Convert.ToString("2147483646");
                textBox27.Text = Convert.ToString("4294967295");
                textBox28.Text = Convert.ToString("2147483647");
            }
            if (comboBox1.SelectedIndex == 3 && comboBox2.SelectedIndex == 8) //Int 32 and Uint 64
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-2147483648");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("2147483647");
                textBox24.Text = Convert.ToString("2147483647");
                textBox25.Text = Convert.ToString("2147483647");
                textBox26.Text = Convert.ToString("2147483646");
                textBox27.Text = Convert.ToString("18446744073709551615");
                textBox28.Text = Convert.ToString("2147483647");
            }//end of combobox selected index=3 for left variable i.e a



            //start of combobox selected index=4 for left variable i.e a
            if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 1) //Int 64 and int 8
            {
                textBox17.Text = Convert.ToString("-127");
                textBox18.Text = Convert.ToString("-9223372036854775808");
                textBox19.Text = Convert.ToString("-128");
                textBox20.Text = Convert.ToString("-128");
                textBox21.Text = Convert.ToString("-128");
                textBox22.Text = Convert.ToString("-128");
                textBox23.Text = Convert.ToString("9223372036854775807");
                textBox24.Text = Convert.ToString("126");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("127");
                textBox27.Text = Convert.ToString("127");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 2) //Int 64 and Int 16
            {
                textBox17.Text = Convert.ToString("-32767");
                textBox18.Text = Convert.ToString("-9223372036854775808");
                textBox19.Text = Convert.ToString("-32768");
                textBox20.Text = Convert.ToString("-32768");
                textBox21.Text = Convert.ToString("-32768");
                textBox22.Text = Convert.ToString("-32768");
                textBox23.Text = Convert.ToString("9223372036854775807");
                textBox24.Text = Convert.ToString("32766");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32767");
                textBox27.Text = Convert.ToString("32767");
                textBox28.Text = Convert.ToString("32767");
            }
            if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 3) //Int 64 and Int 32
            {
                textBox17.Text = Convert.ToString("-2147483647");
                textBox18.Text = Convert.ToString("-9223372036854775808");
                textBox19.Text = Convert.ToString("-2147483648");
                textBox20.Text = Convert.ToString("-2147483648");
                textBox21.Text = Convert.ToString("-2147483648");
                textBox22.Text = Convert.ToString("-2147483648");
                textBox23.Text = Convert.ToString("9223372036854775807");
                textBox24.Text = Convert.ToString("2147483646");
                textBox25.Text = Convert.ToString("2147483647");
                textBox26.Text = Convert.ToString("2147483647");
                textBox27.Text = Convert.ToString("2147483647");
                textBox28.Text = Convert.ToString("2147483647");
            }
            if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 4) //Int 64 and Int 64
            {
                textBox17.Text = Convert.ToString("-9223372036854775807");
                textBox18.Text = Convert.ToString("-9223372036854775808");
                textBox19.Text = Convert.ToString("-9223372036854775808");
                textBox20.Text = Convert.ToString("-9223372036854775808");
                textBox21.Text = Convert.ToString("-9223372036854775807");
                textBox22.Text = Convert.ToString("-9223372036854775808");
                textBox23.Text = Convert.ToString("9223372036854775807");
                textBox24.Text = Convert.ToString("9223372036854775806");
                textBox25.Text = Convert.ToString("9223372036854775807");
                textBox26.Text = Convert.ToString("9223372036854775806");
                textBox27.Text = Convert.ToString("9223372036854775807");
                textBox28.Text = Convert.ToString("9223372036854775807");
            }
            if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 5)  //Int 64 and Uint 8
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-9223372036854775808");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("9223372036854775807");
                textBox24.Text = Convert.ToString("254");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("255");
                textBox27.Text = Convert.ToString("255");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 6) //Int 64 and Uint 16
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-9223372036854775808");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("9223372036854775807");
                textBox24.Text = Convert.ToString("65534");
                textBox25.Text = Convert.ToString("65535");
                textBox26.Text = Convert.ToString("65535");
                textBox27.Text = Convert.ToString("65535");
                textBox28.Text = Convert.ToString("65535");
            }
            if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 7) //Int 64 and Uint 32
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-9223372036854775808");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("9223372036854775807");
                textBox24.Text = Convert.ToString("4294967294");
                textBox25.Text = Convert.ToString("4294967295");
                textBox26.Text = Convert.ToString("4294967295");
                textBox27.Text = Convert.ToString("4294967295");
                textBox28.Text = Convert.ToString("4294967295");
            }
            if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 8) //Int 64 and Uint 64
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("-9223372036854775807");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("0");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("9223372036854775807");
                textBox24.Text = Convert.ToString("9223372036854775807");
                textBox25.Text = Convert.ToString("9223372036854775807");
                textBox26.Text = Convert.ToString("9223372036854775806");
                textBox27.Text = Convert.ToString("18446744073709551615");
                textBox28.Text = Convert.ToString("9223372036854775807");
            }//end of combobox selected index=4 for left variable i.e a


            //start of combobox selected index=5 for left variable i.e a
            if (comboBox1.SelectedIndex == 5 && comboBox2.SelectedIndex == 1) //Uint 8 and Int 8
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-128");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("255");
                textBox24.Text = Convert.ToString("126");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("127");
                textBox27.Text = Convert.ToString("127");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 5 && comboBox2.SelectedIndex == 2) //Uint 8 and Int 16
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-32768");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("255");
                textBox24.Text = Convert.ToString("255");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("254");
                textBox27.Text = Convert.ToString("32767");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 5 && comboBox2.SelectedIndex == 3)//Uint 8 and Int 32
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-2147483648");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("255");
                textBox24.Text = Convert.ToString("255");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("254");
                textBox27.Text = Convert.ToString("2147483647");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 5 && comboBox2.SelectedIndex == 4)//Uint 8 and Int 64
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-9223372036854775808");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("255");
                textBox24.Text = Convert.ToString("255");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("254");
                textBox27.Text = Convert.ToString("9223372036854775807");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 5 && comboBox2.SelectedIndex == 5) //Uint 8 and Uint 8
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("255");
                textBox24.Text = Convert.ToString("254");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("254");
                textBox27.Text = Convert.ToString("255");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 5 && comboBox2.SelectedIndex == 6)//Uint 8 and Uint 16
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("255");
                textBox24.Text = Convert.ToString("255");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("254");
                textBox27.Text = Convert.ToString("65535");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 5 && comboBox2.SelectedIndex == 7)//Uint 8 and Uint 32
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("255");
                textBox24.Text = Convert.ToString("255");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("254");
                textBox27.Text = Convert.ToString("4294967295");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 5 && comboBox2.SelectedIndex == 8)//Uint 8 and Uint 64
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("255");
                textBox24.Text = Convert.ToString("255");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("254");
                textBox27.Text = Convert.ToString("18446744073709551615");
                textBox28.Text = Convert.ToString("255");
            }//end of combo box selected index =5 for left variable i.e a

            //start of combobox selected index=6 for left variable i.e a
            if (comboBox1.SelectedIndex == 6 && comboBox2.SelectedIndex == 1)//Uint 16 and Int 8
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-128");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("65535");
                textBox24.Text = Convert.ToString("126");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("127");
                textBox27.Text = Convert.ToString("127");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 6 && comboBox2.SelectedIndex == 2)//Uint 16 and Int 16
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-32768");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("65535");
                textBox24.Text = Convert.ToString("32766");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32767");
                textBox27.Text = Convert.ToString("32767");
                textBox28.Text = Convert.ToString("32767");
            }
            if (comboBox1.SelectedIndex == 6 && comboBox2.SelectedIndex == 3)//Uint 16 and Int 32
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-2147483648");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("65535");
                textBox24.Text = Convert.ToString("65535");
                textBox25.Text = Convert.ToString("65535");
                textBox26.Text = Convert.ToString("65534");
                textBox27.Text = Convert.ToString("2147483647");
                textBox28.Text = Convert.ToString("65535");
            }
            if (comboBox1.SelectedIndex == 6 && comboBox2.SelectedIndex == 4)//Uint 16 and Int 64
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-9223372036854775808");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("65535");
                textBox24.Text = Convert.ToString("65535");
                textBox25.Text = Convert.ToString("65535");
                textBox26.Text = Convert.ToString("65534");
                textBox27.Text = Convert.ToString("9223372036854775807");
                textBox28.Text = Convert.ToString("65535");
            }
            if (comboBox1.SelectedIndex == 6 && comboBox2.SelectedIndex == 5)//Uint 16 and Uint 8
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("65535");
                textBox24.Text = Convert.ToString("254");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("255");
                textBox27.Text = Convert.ToString("255");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 6 && comboBox2.SelectedIndex == 6)//Uint 16 and Uint 16
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("65535");
                textBox24.Text = Convert.ToString("65534");
                textBox25.Text = Convert.ToString("65535");
                textBox26.Text = Convert.ToString("65534");
                textBox27.Text = Convert.ToString("65535");
                textBox28.Text = Convert.ToString("65535");
            }
            if (comboBox1.SelectedIndex == 6 && comboBox2.SelectedIndex == 7)//Uint 16 and Uint 32
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("65535");
                textBox24.Text = Convert.ToString("65535");
                textBox25.Text = Convert.ToString("65535");
                textBox26.Text = Convert.ToString("65534");
                textBox27.Text = Convert.ToString("4294967295");
                textBox28.Text = Convert.ToString("65535");
            }
            if (comboBox1.SelectedIndex == 6 && comboBox2.SelectedIndex == 8)//Uint 16 and Uint 64
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("65535");
                textBox24.Text = Convert.ToString("65535");
                textBox25.Text = Convert.ToString("65535");
                textBox26.Text = Convert.ToString("65534");
                textBox27.Text = Convert.ToString("18446744073709551615");
                textBox28.Text = Convert.ToString("65535");
            }//end of combo box selected index =6 for left variable i.e a


            //start of combobox selected index=7 for left variable i.e a
            if (comboBox1.SelectedIndex == 7 && comboBox2.SelectedIndex == 1)//Uint 32 and Int 8
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-128");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("4294967295");
                textBox24.Text = Convert.ToString("126");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("127");
                textBox27.Text = Convert.ToString("127");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 7 && comboBox2.SelectedIndex == 2)//Uint 32 and Int 16
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-32768");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("4294967295");
                textBox24.Text = Convert.ToString("32766");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32767");
                textBox27.Text = Convert.ToString("32767");
                textBox28.Text = Convert.ToString("32767");
            }
            if (comboBox1.SelectedIndex == 7 && comboBox2.SelectedIndex == 3)//Uint 32 and Int 32
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-2147483648");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("4294967295");
                textBox24.Text = Convert.ToString("2147483646");
                textBox25.Text = Convert.ToString("2147483647");
                textBox26.Text = Convert.ToString("2147483647");
                textBox27.Text = Convert.ToString("2147483647");
                textBox28.Text = Convert.ToString("2147483647");
            }
            if (comboBox1.SelectedIndex == 7 && comboBox2.SelectedIndex == 4)//Uint 32 and Int 64
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-9223372036854775808");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("4294967295");
                textBox24.Text = Convert.ToString("4294967295");
                textBox25.Text = Convert.ToString("4294967295");
                textBox26.Text = Convert.ToString("4294967294");
                textBox27.Text = Convert.ToString("9223372036854775807");
                textBox28.Text = Convert.ToString("4294967295");
            }
            if (comboBox1.SelectedIndex == 7 && comboBox2.SelectedIndex == 5)//Uint 32 and Uint 8
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("4294967295");
                textBox24.Text = Convert.ToString("254");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("255");
                textBox27.Text = Convert.ToString("255");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 7 && comboBox2.SelectedIndex == 6)//Uint 32 and Uint 16
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("4294967295");
                textBox24.Text = Convert.ToString("65534");
                textBox25.Text = Convert.ToString("65535");
                textBox26.Text = Convert.ToString("65535");
                textBox27.Text = Convert.ToString("65535");
                textBox28.Text = Convert.ToString("65535");
            }
            if (comboBox1.SelectedIndex == 7 && comboBox2.SelectedIndex == 7)//Uint 32 and Uint 32
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("4294967295");
                textBox24.Text = Convert.ToString("4294967294");
                textBox25.Text = Convert.ToString("4294967295");
                textBox26.Text = Convert.ToString("4294967294");
                textBox27.Text = Convert.ToString("4294967295");
                textBox28.Text = Convert.ToString("4294967295");
            }
            if (comboBox1.SelectedIndex == 7 && comboBox2.SelectedIndex == 8)//Uint 32 and Uint 64
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("4294967295");
                textBox24.Text = Convert.ToString("4294967295");
                textBox25.Text = Convert.ToString("4294967295");
                textBox26.Text = Convert.ToString("4294967294");
                textBox27.Text = Convert.ToString("18446744073709551615");
                textBox28.Text = Convert.ToString("4294967295");
            }//end of combo box selected index =7 for left variable i.e a


            //start of combobox selected index=8 for left variable i.e a
            if (comboBox1.SelectedIndex == 8 && comboBox2.SelectedIndex == 1)//Uint 64 and Int 8
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-128");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("18446744073709551615");
                textBox24.Text = Convert.ToString("126");
                textBox25.Text = Convert.ToString("127");
                textBox26.Text = Convert.ToString("127");
                textBox27.Text = Convert.ToString("127");
                textBox28.Text = Convert.ToString("127");
            }
            if (comboBox1.SelectedIndex == 8 && comboBox2.SelectedIndex == 2)//Uint 64 and Int 16
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-32768");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("18446744073709551615");
                textBox24.Text = Convert.ToString("32766");
                textBox25.Text = Convert.ToString("32767");
                textBox26.Text = Convert.ToString("32767");
                textBox27.Text = Convert.ToString("32767");
                textBox28.Text = Convert.ToString("32767");
            }
            if (comboBox1.SelectedIndex == 8 && comboBox2.SelectedIndex == 3)//Uint 64 and Int 32
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-2147483648");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("18446744073709551615");
                textBox24.Text = Convert.ToString("2147483646");
                textBox25.Text = Convert.ToString("2147483647");
                textBox26.Text = Convert.ToString("2147483647");
                textBox27.Text = Convert.ToString("2147483647");
                textBox28.Text = Convert.ToString("2147483647");
            }
            if (comboBox1.SelectedIndex == 8 && comboBox2.SelectedIndex == 4)//Uint 64 and Int 64
            {
                textBox17.Text = Convert.ToString("0");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("-9223372036854775808");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("18446744073709551615");
                textBox24.Text = Convert.ToString("9223372036854775806");
                textBox25.Text = Convert.ToString("9223372036854775807");
                textBox26.Text = Convert.ToString("9223372036854775807");
                textBox27.Text = Convert.ToString("9223372036854775807");
                textBox28.Text = Convert.ToString("9223372036854775807");
            }
            if (comboBox1.SelectedIndex == 8 && comboBox2.SelectedIndex == 5)//Uint 64 and Uint 8
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("18446744073709551615");
                textBox24.Text = Convert.ToString("254");
                textBox25.Text = Convert.ToString("255");
                textBox26.Text = Convert.ToString("255");
                textBox27.Text = Convert.ToString("255");
                textBox28.Text = Convert.ToString("255");
            }
            if (comboBox1.SelectedIndex == 8 && comboBox2.SelectedIndex == 6)//Uint 64 and Uint 16
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("18446744073709551615");
                textBox24.Text = Convert.ToString("65534");
                textBox25.Text = Convert.ToString("65535");
                textBox26.Text = Convert.ToString("65535");
                textBox27.Text = Convert.ToString("65535");
                textBox28.Text = Convert.ToString("65535");
            }
            if (comboBox1.SelectedIndex == 8 && comboBox2.SelectedIndex == 7)//Uint 64 and Uint 32
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("18446744073709551615");
                textBox24.Text = Convert.ToString("4294967294");
                textBox25.Text = Convert.ToString("4294967295");
                textBox26.Text = Convert.ToString("4294967295");
                textBox27.Text = Convert.ToString("4294967295");
                textBox28.Text = Convert.ToString("4294967295");
            }
            if (comboBox1.SelectedIndex == 8 && comboBox2.SelectedIndex == 8)//Uint 64 and Uint 64
            {
                textBox17.Text = Convert.ToString("1");
                textBox18.Text = Convert.ToString("0");
                textBox19.Text = Convert.ToString("0");
                textBox20.Text = Convert.ToString("0");
                textBox21.Text = Convert.ToString("1");
                textBox22.Text = Convert.ToString("0");
                textBox23.Text = Convert.ToString("18446744073709551615");
                textBox24.Text = Convert.ToString("18446744073709551614");
                textBox25.Text = Convert.ToString("18446744073709551615");
                textBox26.Text = Convert.ToString("18446744073709551614");
                textBox27.Text = Convert.ToString("18446744073709551615");
                textBox28.Text = Convert.ToString("18446744073709551615");
            }//end of combo box selected index =8 for left variable i.e a
            //end of a>b inputs       
        }    

        //this method create eq and bv values in case of a variable( not condition)
        private void variable_button_Click(object sender, EventArgs e)
        {            
            textBox5.Text = textBox29.Text + " is within boundary values";
            textBox8.Text = Convert.ToString("Valid");          
            if (comboBox3.SelectedIndex == 0)
            {
                textBox11.Text = Convert.ToString("-128");
                textBox14.Text = Convert.ToString("127");
            }
            if (comboBox3.SelectedIndex == 1)
            {
                textBox11.Text = Convert.ToString("-32768");
                textBox14.Text = Convert.ToString("32767");
            }
            if (comboBox3.SelectedIndex == 2)
            {
                textBox11.Text = Convert.ToString("–2147483648");
                textBox14.Text = Convert.ToString("2147483647");
            }
            if (comboBox3.SelectedIndex == 3)
            {
                textBox11.Text = Convert.ToString("–9223372036854775808");
                textBox14.Text = Convert.ToString("9223372036854775807");
            }
            if (comboBox3.SelectedIndex == 4)
            {
                textBox11.Text = Convert.ToString("0");
                textBox14.Text = Convert.ToString("255");
            }
            if (comboBox3.SelectedIndex == 5)
            {
                textBox11.Text = Convert.ToString("0");
                textBox14.Text = Convert.ToString("65535");
            }
            if (comboBox3.SelectedIndex == 6)
            {
                textBox11.Text = Convert.ToString("0");
                textBox14.Text = Convert.ToString("4294967295");
            }
            if (comboBox3.SelectedIndex == 7)
            {
                textBox11.Text = Convert.ToString("0");
                textBox14.Text = Convert.ToString("18446744073709551615");
            }

        }
        //This table is used to generate equivalance classes when given is variable
        private void table_variable_Click(object sender, EventArgs e)
        {
            var para1 = (dynamic)Globals.ThisDocument.Paragraphs.Add();
            para1.Range.Text = "";
            para1.Range.InsertParagraphAfter();
            para1.Range.Text = "The equivalence classes for " + textBox29.Text + " are:";
            para1.Range.Font.Name = "Arial";
            para1.Range.Font.Size = 11;
            para1.Range.InsertParagraphAfter();
            Microsoft.Office.Interop.Word.Table table = AddColumns();
            if (table != null)
            {
                one_Eqs_boxes_Click(sender, e);
                AddData(box35.Text, textBox5.Text, textBox8.Text, table);
                ((dynamic)Globals.ThisDocument.Paragraphs).Last.Range.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdLineBreak);

            }
        }
        //This table is used to generate boundary values when given is variable
        private void bv_variable_Click(object sender, EventArgs e)
        {
            var para1 = (dynamic)Globals.ThisDocument.Paragraphs.Add();
            para1.Range.Text = "";
            para1.Range.InsertParagraphAfter();
            para1.Range.Text = "The boundary values for " + textBox29.Text + " are:";
            para1.Range.InsertParagraphAfter();
            Microsoft.Office.Interop.Word.Table table6 = AddColumns2();
            if (table6 != null)
            {
                two_boxes_Click(sender, e);
                
                AddBv(box35.Text, box28.Text, textBox11.Text, box29.Text, textBox14.Text, table6);
            }

        }
        //prints Eq and BV tables
        int flag5 = 0;
        private void bvtable_variable_Click(object sender, EventArgs e)
        {
            if (textBox29.Text == "" || (comboBox3.SelectedIndex == -1))
            {
                MessageBox.Show("Please Enter variable and select valid range", "Warning!!");
                flag5 = 1;
            }
            if (flag5 == 1)
            {
                flag5 = 0;
            }
            else
            {
                variable_button_Click(sender, e);
                table_variable_Click(sender, e);
                bv_variable_Click(sender, e);
            }
            clear_button_for_variable_Click(sender, e);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var para1 = (dynamic)Globals.ThisDocument.Paragraphs.Add();
            para1.Range.Text = "";
            para1.Range.InsertParagraphAfter();
            para1.Range.Text = "The equivalence classes for " + textbox1.Text + " are:";
            para1.Range.Font.Name = "Arial";
            para1.Range.Font.Size = 11;
            para1.Range.InsertParagraphAfter();
            Microsoft.Office.Interop.Word.Table table = AddColumns36();            
        }
        public Microsoft.Office.Interop.Word.Table AddColumns36()
        {
            object missing = System.Type.Missing;
            Microsoft.Office.Interop.Word.Table tbl = null;
            try
            {
                object miss = System.Type.Missing;
                var paragraph = Globals.ThisDocument.Paragraphs.Add(ref miss);
                tbl = Globals.ThisDocument.Tables.Add(paragraph.Range, 1, 3, ref missing, ref missing);
                tbl.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                tbl.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                tbl.PreferredWidth = 432.18f;
                tbl.Columns[1].PreferredWidth = 62.2f;                         
                tbl.Rows.SetHeight(12.14f, WdRowHeightRule.wdRowHeightExactly);

                SetHeadings(tbl.Cell(1, 1), "Equivalence Class");
                SetHeadings(tbl.Cell(1, 2), "Lower Boundary");
                SetHeadings(tbl.Cell(1, 3), "Upper Boundary");                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem creating table: " + ex.Message,
                    "Actions Pane", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tbl;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var para1 = (dynamic)Globals.ThisDocument.Paragraphs.Add();
            para1.Range.Text = "";
            para1.Range.InsertParagraphAfter();           
            Microsoft.Office.Interop.Word.Table table = AddColumns5();
        }
        public Microsoft.Office.Interop.Word.Table AddColumns5()
        {
            object missing = System.Type.Missing;
            Microsoft.Office.Interop.Word.Table tbl = null;
            try
            {
                object miss = System.Type.Missing;
                var paragraph = Globals.ThisDocument.Paragraphs.Add(ref miss);
                tbl = Globals.ThisDocument.Tables.Add(paragraph.Range, 1, 5, ref missing, ref missing);                
                tbl.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                tbl.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                tbl.PreferredWidth = 432.18f;
                tbl.Columns[1].PreferredWidth = 62.2f;                
                tbl.Columns[2].PreferredWidth = 358.34f;
                tbl.Columns[3].PreferredWidth = 49.7f;
                tbl.Columns[4].PreferredWidth = 49.7f;
                tbl.Columns[5].PreferredWidth = 49.7f;
                tbl.Rows.SetHeight(12.14f, WdRowHeightRule.wdRowHeightExactly);         
                
                SetHeadings(tbl.Cell(1, 2), textBox2.Text);
                SetHeadings(tbl.Cell(1, 3), textBox3.Text);
                SetHeadings(tbl.Cell(1, 4), textBox2.Text);
                SetHeadings(tbl.Cell(1, 5), textBox3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem creating table: " + ex.Message,
                    "Actions Pane", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tbl;
        }

        // //===========Incrementing of EQ and BV's STARTS HERE============
        int x = 1,y=1;

        private void three_boxes_Click(object sender, EventArgs e)
        {
            box35.Text = Convert.ToString("BV." + x++);
            box36.Text = Convert.ToString("BV." + x++);
            box37.Text = Convert.ToString("BV." + x++);   
        }
        private void six_boxes_Click(object sender, EventArgs e)
        {
            
            box1.Text = Convert.ToString("BV." + x++);           
            box2.Text = Convert.ToString("BV." + x++);  
            box3.Text = Convert.ToString("BV." + x++);    
            box4.Text = Convert.ToString("BV." + x++);
            box5.Text = Convert.ToString("BV." + x++);  
            box6.Text = Convert.ToString("BV." + x++);         
        }

        private void four_boxes_Click(object sender, EventArgs e)
        {
           
            box7.Text = Convert.ToString("BV." + x++);           
            box8.Text = Convert.ToString("BV." + x++);   
            box9.Text = Convert.ToString("BV." + x++);
            box10.Text = Convert.ToString("BV." + x++);          
        }

        private void twelveboxes_Click(object sender, EventArgs e)
        {
           
            box11.Text = Convert.ToString("BV." + x++);            
            box12.Text = Convert.ToString("BV." + x++);
            box13.Text = Convert.ToString("BV." + x++);
            box14.Text = Convert.ToString("BV." + x++);
            box15.Text = Convert.ToString("BV." + x++);
            box16.Text = Convert.ToString("BV." + x++);
                      
            
        }

        private void five_boxes_Click(object sender, EventArgs e)
        {
           
            box23.Text = Convert.ToString("BV." + x++);           
            box24.Text = Convert.ToString("BV." + x++);
            box25.Text = Convert.ToString("BV." + x++);
            box26.Text = Convert.ToString("BV." + x++);
            box27.Text = Convert.ToString("BV." + x++);

        }
        private void two_boxes_Click(object sender, EventArgs e)
        {
            box28.Text = Convert.ToString("BV." + x++);
            box29.Text = Convert.ToString("BV." + x++);
        }

        //Generate incrementing numbers for EQ's
        private void one_Eqs_boxes_Click(object sender, EventArgs e) //if the table has only one EQ
        {
            box35.Text = Convert.ToString("EQ." + y++);            
        }
        private void two_Eqs_boxes_Click(object sender, EventArgs e)//Two EQ's in table
        {
            box30.Text = Convert.ToString("EQ." + y++);
            box31.Text = Convert.ToString("EQ." + y++);
        }
        private void three_Eqs_boxes_Click(object sender, EventArgs e)//Three EQ"s in table
        {
            box32.Text = Convert.ToString("EQ." + y++);
            box33.Text = Convert.ToString("EQ." + y++);
            box34.Text = Convert.ToString("EQ." + y++);
        }
        //===========Incrementing of EQ and BV's ENDS HERE============


        //+++++++++++Delete the table CODE STARTS HERE+++++++++++++++++++
        private void Delete_table_Click(object sender, EventArgs e)
        {
            if (y == 1)
            {
                MessageBox.Show("There is no table to delete","Warning!!");
            }
            else
            {
                MessageBox.Show("Delete the table manually in the document. Now enter the Eq and BV value to append","Info!!");
                label21.Visible = true; label22.Visible = true; label23.Visible = true;
                textBox31.Clear(); textBox32.Clear();
                textBox31.Visible = true; textBox32.Visible = true;
                ok_button_for_delete.Visible = true;
                cancel_button_for_delete.Visible = true;
            }
        }

        private void ok_button_for_delete_Click(object sender, EventArgs e)
        {
            x = Convert.ToInt32(textBox31.Text);
            y = Convert.ToInt32(textBox32.Text);            
            label21.Visible = false;
            label22.Visible = false; label23.Visible = false;
            textBox31.Visible = false; textBox32.Visible = false;
            ok_button_for_delete.Visible = false;
            cancel_button_for_delete.Visible = false;
            MessageBox.Show("Table Deleted successfully,Now you can continue","Success!!");
        }

        private void cancel_button_for_delete_Click(object sender, EventArgs e)
        {
            label21.Visible = false;
            label22.Visible = false; label23.Visible = false;
            textBox31.Visible = false; textBox32.Visible = false;
            ok_button_for_delete.Visible = false;
            cancel_button_for_delete.Visible = false;
        }

        private void clear_button_for_variable_Click(object sender, EventArgs e)
        {
            textBox29.Clear();
            comboBox3.SelectedIndex = -1;
            textBox29.Focus();
        }

        
        //+++++++++++Delete the table CODE ENDS HERE+++++++++++++++++++
        
     
        

    }
}
   

      
