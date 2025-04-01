﻿namespace Assessment.Api.WebApi;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
}

public class ApiResponse
{
    public bool Success { get; set; } = true;
    public string Message { get; set; }
    public object? Data { get; set; }
}