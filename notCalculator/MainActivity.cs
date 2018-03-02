using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace notCalculator
{
    [Activity(Label = "notCalculator", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var inputText = FindViewById<EditText>(Resource.Id.inputText);
            var btnCalculate = FindViewById<Button>(Resource.Id.button);
            var resultText = FindViewById<TextView>(Resource.Id.resultText);
            var additionalInfoText = FindViewById<TextView>(Resource.Id.addInfo);

            btnCalculate.Click += (e, o) =>
            {
                string input = inputText.Text;
                var materialSum = double.Parse(input);

                var resultWithoutFee = GetFee(materialSum);
                var vatTax = resultWithoutFee * 20 / 100;

                resultWithoutFee += vatTax + (decimal)(materialSum * 0.1) / 100;

                var localTaxCalculated = GetLocalTax(materialSum);

                var result = Math.Round((decimal)localTaxCalculated + resultWithoutFee,2);
                resultText.Text = result.ToString();
            };
        }
        public static double GetLocalTax(double materialSum)
        {
            var result = (2.50 * materialSum) / 100;
            return result;
        }

        public static decimal GetFee(double matSum)
        {
            decimal result = 0.0m;

            if (matSum > 100 && matSum <= 1000)
            {
                decimal vpisvane = 10;
                result = 30 + (decimal)(1.5 * (matSum - 100)) / 100.00m;
                decimal vat = (result * 20) / 100;
                result = result + vpisvane + vat;
            }
            else if (matSum > 1000 && matSum <= 10000)
            {
                decimal vpisvane = 10;
                result = 43.50m + (decimal)(1.3 * (matSum - 1000)) / 100.00m;
                decimal vat = (result * 20) / 100;
                result = result + vpisvane + vat;
            }
            else if (matSum > 10000 && matSum <= 50000)
            {
                decimal vpisvane = ((decimal)matSum / 10000) * 10;
                result = 160.50m + (decimal)(0.8 * (matSum - 10000)) / 100.00m;
                decimal vat = (result * 20) / 100;
                result = result + vpisvane + vat;
            }
            else if (matSum > 50000 && matSum <= 100000)
            {
                decimal vpisvane = ((decimal)matSum / 10000) * 10;
                result = 480.50m + (decimal)(0.5 * (matSum - 50000)) / 100.00m;
                decimal vat = (result * 20) / 100;
                result = result + vpisvane + vat;
            }
            else if (matSum > 100000 && matSum <= 500000)
            {
                decimal vpisvane = ((decimal)matSum / 10000) * 10;
                result = (decimal)730.50 + (decimal)(0.2 * (matSum - 100000)) / 100.00m;
                decimal vat = (result * 20) / 100;
                result = result + vpisvane + vat;
            }
            else if (matSum > 500000)
            {
                decimal vpisvane = ((decimal)matSum / 10000) * 10;
                result = 1530.50m + (decimal)(0.1 * (matSum - 500000)) / 100.00m;
                if (result > 6000)
                {
                    result = 6000;
                }
                decimal vat = (result * 20) / 100;
                result = result + vpisvane + vat;
            }
            return result;
        }
    }
}

