﻿namespace ShoeStore.dto.User.Request;

public class RegistrationUserRequest
{
    public string? Name { get; set; }
    public string? SecondName { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
}