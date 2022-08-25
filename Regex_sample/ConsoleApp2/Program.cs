// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

string test = "Png";

Regex regex = new Regex(@"^png$", RegexOptions.IgnoreCase);
if (regex.IsMatch(test))
    Console.WriteLine("1 test is png!");

if (test is "png")
    Console.WriteLine("2 test is png!");