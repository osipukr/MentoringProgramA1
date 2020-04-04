using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SampleQueries
{
    public static class Program
    {
        private const string TITLE = "HomeWork - Raman Asipuk";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            var harnesses = new List<SampleHarness>();
            var linqHarness = new LinqSamples();

            harnesses.Add(linqHarness);

            Application.EnableVisualStyles();

            using (var form = new SampleForm(TITLE, harnesses))
            {
                form.ShowDialog();
            }
        }
    }
}