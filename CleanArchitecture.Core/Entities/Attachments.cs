using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entities;

public class Attachments
{
    public int AttachmentID { get; set; }
    public string AttachmentURL { get; set;}

    public int? StudentID { get; set; }
    public Student Student { get; set; }
    
    public int? VoucherID { get; set; }
    public Vouchers Voucher { get; set; }
}