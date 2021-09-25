﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models.ViewModels
{
    public class ExpenseVM
    {
        public Expense Expense { get; set; }
        public IEnumerable<SelectListItem> CategoryDropDown { get; set; }
        public ExpenseVM()
        {

        }

        public ExpenseVM(Expense expense, IEnumerable<SelectListItem> categoryDropDown)
        {
            Expense = expense;
            CategoryDropDown = categoryDropDown;
        }
    }
}
