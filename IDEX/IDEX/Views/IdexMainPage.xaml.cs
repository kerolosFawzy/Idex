using System;
using CustomController;
using IDEX.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX
{
    public partial class IdexMainPage
    {
        public IdexMainPage()
        {
            InitializeComponent();
            
        }
        public IdexMainPage(string navigationParameter) : this()
        {
        }
    }
}