using System;
namespace BlockChain
{
	public class Account
	{
		public Account(string address)
		{
			Address = address;
		}

		public string Address { get; set; }
		public string? FullName { get; set; }
		public int Balance { get; set; }
	}
}