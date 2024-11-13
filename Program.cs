// See https://aka.ms/new-console-template for more information
using HomeIP;

ReadIP reader = new();
await reader.GetIp();
List<string> server = File.ReadAllLines("Server.txt").ToList();
List<string> emails = File.ReadAllLines("SendList.txt").ToList();
Emailer.Send(reader.IP, server, emails);