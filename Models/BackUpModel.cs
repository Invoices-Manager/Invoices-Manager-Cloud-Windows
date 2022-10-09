﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InvoicesManager.Models
{
    public class BackUpModel
    {
        public int EntityCount { get; set; }
        public DateTime DateOfCreation { get; set; }
        public List<InvoiceBackUpModel> Invoices { get; set; }
    }

    public class InvoiceBackUpModel
    {
        public string Base64 { get; set; }
        public SubInvoiceBackUpModel Invoice { get; set; }
    }

    public class SubInvoiceBackUpModel
    {
        public DateTime ExhibitionDate { get; set; }
        public string Organization { get; set; }
        public string DocumentType { get; set; }
        public string InvoiceNumber { get; set; }
        public string Reference { get; set; }
    }
}