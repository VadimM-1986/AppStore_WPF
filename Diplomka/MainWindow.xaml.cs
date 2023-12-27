using AppStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CloudIpspSDK;
using CloudIpspSDK.Checkout;
using System.Collections.Generic;
using System.Data;

namespace AppStore
{

    public partial class MainWindow : Window
    {
        public List<int> IdProd = new List<int>();
        public int summa;
        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadProducts()
        {
            using (AppDBContext context = new AppDBContext())
            {
                var products = context.Products.ToList();
                productListBox.ItemsSource = products;

            }
        }

        private void buttonAddCart_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int tagValue = Convert.ToInt32(button.Tag);

            IdProd.Add(tagValue);

            zagcart.Content = "В корзине есть товары";
 
            int countKolvo = 0;
            foreach (int prod in IdProd)
            {
                countKolvo++;
            }
            kolvoText.Content = $"Количество: {countKolvo} шт.";


            using (AppDBContext context = new AppDBContext())
            {
                var stoimost = context.Products.Where(el => el.Id == tagValue);

                foreach (var prod in stoimost)
                {
                    summa += prod.Price;
                }
            }
            StoimostText.Content = $"Общая стоимость: {summa}$";
        }

        private void buttonCart_Click(object sender, RoutedEventArgs e)
        {
            if (IdProd.Count <= 0)
            {
                MessageBox.Show("Ваша корзина пуста!");
            }
            else
            {
                Config.MerchantId = 1396424;
                Config.SecretKey = "test";

                var req = new CheckoutRequest
                {
                    order_id = Guid.NewGuid().ToString("N"),
                    amount = summa * 100,
                    order_desc = "checkout json demo",
                    currency = "USD"
                };
                var resp = new Url().Post(req);

                if (resp.Error != null)
                {
                    MessageBox.Show("Произошла какая то ошибка!");
                }
                else
                {
                    string url = resp.checkout_url;
                    System.Diagnostics.Process.Start(url);
                }
            }
        }
    }
}



