﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPortalWebAPIHelper.Extensions
{

    public class SimpleSyntaxHighlightingRTB : RichTextBox
    {
        List<string> keywords = new List<string>();
        public SimpleSyntaxHighlightingRTB()
        {
            keywords.Add("function");
            keywords.Add("var");
            keywords.Add("return");
            keywords.Add("if");
            keywords.Add("else");
        }


        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            HighlightSyntax();
        }
        //to make the js snippets more readable, this function hightlights a specific list of keywords in the code.
        protected override void OnTextChanged(EventArgs e)
        {
            this.ForeColor = Color.Black;
            base.OnTextChanged(e);
            HighlightSyntax();
        }


        private void HighlightSyntax()
        {
            this.SelectAll();
            this.SelectionColor = Color.Black;
            this.DeselectAll();

            foreach (string word in keywords)
            {
                string find = word;

                if (this.Text.Contains(find))
                {

                    var matchString = Regex.Escape(find + " ");
                    foreach (Match match in Regex.Matches(this.Text, matchString))
                    {
                        this.Select(match.Index, find.Length);
                        this.SelectionColor = Color.Blue;
                        this.Select(this.TextLength, 0);
                        // this.SelectionColor = this.ForeColor;
                    };
                }
            }

            int offset = 0;
            foreach (string line in this.Lines)
            {
                if (line.Contains("/*"))
                {
                    int startIndex = line.IndexOf("/*");
                    int endIndex = line.IndexOf("*/")+2; // 2 to account for the * and /
                    int length = endIndex - startIndex;
                    this.Select(startIndex + offset, length);
                    this.SelectionColor = Color.Green;
                    this.Select(this.TextLength, 0);
                }
                offset += line.Length+1; // 1 to account for the new line character

            }




        }
    }
}
