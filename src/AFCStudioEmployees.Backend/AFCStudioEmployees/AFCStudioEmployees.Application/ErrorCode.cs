﻿using System.Text.Json.Serialization;
using AFCStudioEmployees.Application.Converters;

namespace AFCStudioEmployees.Application;

// Result error code for responses
[JsonConverter(typeof(SnakeCaseStringEnumConverter<ErrorCode>))]
public enum ErrorCode
{
    Unknown,
    InvalidModel,
    
    DepartmentAlreadyExists,
    DepartmentNameEmpty,
    DepartmentNotFound,
    
    JobAlreadyExists,
    JobNameEmpty,
    JobSalaryLessThanZero,
    JobNotFound,
    
    EmployeeNotFound,
    
    InvalidPagination,
    PropertyNameNotFound
}