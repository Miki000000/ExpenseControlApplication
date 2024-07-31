using ExpenseControlApplication.Data.Entities;

namespace ExpenseControlApplication.Presentation.SpendingPresentation;

public static class SpendingMapper
{
    public static Spending FromCreateToSpending(this SpendingDto spendingDto, string userId)
    {
        return new Spending
        {
            ValueSpended = spendingDto.ValueSpended,
            UserId = userId
        };
    }

    public static CreateSpendingDto FromSpendingToDto(this Spending spending)
    {
        return new CreateSpendingDto
        {
            Id = spending.Id,
            SpendingDate = spending.SpendingDate,
            ValueSpended = spending.ValueSpended,
            UserId = spending.UserId,
        };
    }
}