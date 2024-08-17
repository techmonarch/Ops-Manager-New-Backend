using OpsManagerAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsManagerAPI.Application.Features.Payments.Dtos;
public class PaymentDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType CustomerId { get; set; }
    public DateTime PaymentDate { get; set; }
    public DefaultIdType BillId { get; set; }
    public string? PaymentSource { get; set; }
    public decimal Amount { get; set; }
    public string? AccountType { get; set; }
    public string? CustomerType { get; set; }
}
