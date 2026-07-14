using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Validators
{
    public static class StringValidator
    {
        public static string ValidateEmpty(this string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"El campo{fieldName} no puede estar vacio");
            return value;
        }
        public static string ValidateLength(this string value,string fieldName,int minLength,int maxLength)
        {
            if (value.Length < minLength || value.Length > maxLength)
                throw new ArgumentException($"El campo {fieldName} debe tener entre {minLength} y {maxLength} caracteres.");
            return value;
        }
    }
}
