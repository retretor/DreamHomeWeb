﻿namespace DreamHomeWeb.Models.Domain;

public class Client
{   
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string PrefType { get; set; }
    public float MaxRent { get; set; }
}