﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.ViewModels
{
    public class ForgotPasswordInputModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}