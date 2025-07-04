﻿using System.ComponentModel.DataAnnotations;

namespace ApiDeProdutos.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email é obrigatório")]
        [EmailAddress(ErrorMessage ="Formato de email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
