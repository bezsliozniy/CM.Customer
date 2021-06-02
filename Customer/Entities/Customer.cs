﻿using System;
using System.Collections.Generic;
using StringExtension;
using System.Text.RegularExpressions;
/// <summary>
/// Comment:
/// *r [size] - required field
/// *o [size] - optional field
/// </summary>

namespace CustomerLib
{
	public class Customer : Person
	{
		string _name;
		string _lastName;
		string _phone;
		string _email;
		object _total;

		List<Address> _addresses = new List<Address>();
		List<string> _notes = new List<string>();

        #region Constructors.
        public Customer(string firstName, string lastName, List<Address> addresses, string phone, string email, List<string> notes, object totalPurchasesAmount)
        {
			FirstName = firstName;
			LastName = lastName;
			Addresses = addresses;
			Phone = phone;
			Email = email;
			Notes = notes;
			TotalPurchasesAmount = totalPurchasesAmount;
		}
        public Customer()
        {
			Addresses.Add(new Address());
			Notes.Add("");
		}
		#endregion
        #region Getters and setters.
        public override string FirstName 
		{ 
			get { return _name; }
			set 
			{
				if (value != null && value.Length <= 50 && !value.containsNumbers() && !value.isEmpty())
					_name = value;
				else
					_name = null;
			} 
		}
		public override string LastName 
		{ 
			get { return _lastName; } 
			set 
			{
				if (value != null && value.Length <= 50 && !value.containsNumbers() && !value.isEmpty())
					_lastName = value;
				else
					_lastName = null;
			} 
		}
		public string Phone { 
			get => _phone;
			set 
			{
				if (value.isE164())
					_phone = value;
				else
					_phone = null;
			}
		}
		public string Email {
			get => _email;
			set
			{
				if (value != null)
				{
					string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

					if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
					{
						_email = value;
					}
					else
					{
						_email = null;
					}
				}
				else
					_email = null;
			}
		}
		public object TotalPurchasesAmount {
			get
			{
				return (decimal?)(double?)_total;
			}
			set 
			{
				if (value != null && (decimal?)(double?)value >= 0)
					_total = value;
				else
					_total = null;
			} 
		}
		public List<Address> Addresses 
		{ 
			get => _addresses;
			set {
			_addresses = value; 
			} 
		}
		public List<string> Notes 
		{ 
			get => _notes;
			set 
			{
				_notes = value;
			} 
		} 
        #endregion
        public List<string> CustomerValidator()
		{
			List<string> errors = new List<string>();

			if (LastName is null)
				errors.Add("LastName");
			
			if (Addresses.Count == 0)
				errors.Add("Addresses");
			else 
				for(int i = 0; i < Addresses.Count; i++)
					if (Addresses[i].AddressValidator() != null)
						errors.Add($"Address_{i}");

			if(Notes.Count == 0)
				errors.Add(CustomerErrors.AddressesListIsEmpty.ToString());
			
			return errors;
		}
	}
}