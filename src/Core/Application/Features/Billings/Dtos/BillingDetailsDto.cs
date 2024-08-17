namespace OpsManagerAPI.Application.Features.Billings.Dtos;
public class BillingDetailsDto
{
    public DefaultIdType Id { get; set; }
    public DateTime BillDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Consumption { get; set; }
    public decimal Arrears { get; set; }
    public decimal VAT { get; set; }
    public decimal CurrentCharge { get; set; }
    public decimal TotalCharge { get; set; }
    public decimal TotalDue { get; set; }
    public string? AccountType { get; set; }
    public string? CustomerType { get; set; }
}
