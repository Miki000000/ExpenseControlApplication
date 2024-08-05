using ExpenseControlApplication.Data.Entities;

namespace ExpenseControlApplication.Presentation.SpendingPresentation;

public static class SpendingMapper
{
    public static Spending FromCreateToSpending(this SpendingDto spendingDto, string userId)
    {
        return new Spending
        {
            ValueSpended = spendingDto.ValueSpended,
            UserId = userId,
            Description = spendingDto.Description,
            ItemBought = spendingDto.ItemBought
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
            Description = spending.Description,
            ItemBought = spending.ItemBought
        };
    }

    public static GetSpendingDto FromSpendingToGetSpendingDto(this Spending spending, string username)
    {
        return new GetSpendingDto
        {
            Username = username,
            ValueSpended = spending.ValueSpended,
            SpendingDate = spending.SpendingDate,
            Description = spending.Description,
            ItemBought = spending.ItemBought
        };
    }

    public static DeleteSpendingDto FromSpendingToDeleteDto(this Spending spending)
    {
        return new DeleteSpendingDto
        {
            Id = spending.Id,
            ValueSpended = spending.ValueSpended
        };
    }
}