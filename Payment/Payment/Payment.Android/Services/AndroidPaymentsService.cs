using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Braintreepayments.Api.Interfaces;
using Payment.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Payment.Droid.Services.AndroidPaymentsService))]
namespace Payment.Droid.Services
{
    public class AndroidPaymentsService
    {

    }
}