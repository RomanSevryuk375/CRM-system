using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Expense;
using FluentAssertions;
using Moq;
using Shared.Enums;

namespace CRMSystem.Business.Tests.UnitTests;

public class ExpenseServiceTest
{
    private readonly Mock<IExpenseRepository> _expenseRepoMock;
    private readonly Mock<IExpenseTypeRepository> _expenseTypeRepoMock;
    private readonly Mock<IPartSetRepository> _partSetRepoMock;
    private readonly Mock<ITaxRepository> _taxRepoMock;
    private readonly Mock<ILogger<ExpenseService>> _loggerMock;
    private readonly ExpenseService _service;

    public ExpenseServiceTest()
    {
        _expenseRepoMock = new Mock<IExpenseRepository>();
        _expenseTypeRepoMock = new Mock<IExpenseTypeRepository>();
        _partSetRepoMock = new Mock<IPartSetRepository>();
        _taxRepoMock = new Mock<ITaxRepository>();
        _loggerMock = new Mock<ILogger<ExpenseService>>();

        _service = new ExpenseService(
            _expenseRepoMock.Object,
            _expenseTypeRepoMock.Object,
            _partSetRepoMock.Object,
            _taxRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateExpense_ShouldThrowNotFoundException_WhenTaxTypeNotExist()
    {
        var expense = ValidObjects.CreateValidExpense(123, null);

        _expenseTypeRepoMock.Setup(x => x.Exists(
                                (int)expense.ExpenseTypeId,
                                It.IsAny<CancellationToken>()))
                                .ReturnsAsync(false);

        var act = () => _service.CreateExpenses(expense, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _expenseRepoMock.Verify(x => x.Create(
                            It.IsAny<Expense>(),
                            CancellationToken.None),
                         Times.Never);
    }

    [Fact]
    public async Task CreateExpense_ShouldThrowNotFoundException_WhenTaxHasValueButNotExist()
    {
        var expense = ValidObjects.CreateValidExpense(123, null);

        _expenseTypeRepoMock.Setup(x => x.Exists(
                                (int)expense.ExpenseTypeId,
                                It.IsAny<CancellationToken>()))
                                .ReturnsAsync(true);

        _taxRepoMock.Setup(x => x.Exists(
                        (int)expense.TaxId!,
                        It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);

        var act = () => _service.CreateExpenses(expense, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _expenseRepoMock.Verify(x => x.Create(
                            It.IsAny<Expense>(),
                            CancellationToken.None),
                         Times.Never);
    }

    [Fact]
    public async Task CreateExpense_WhenTaxTypeExistAndTaxHasValueAndExistAndPartSetIdHasValueAndExist_ShouldReturnId()
    {
        var expenseId = 0;
        var expense = ValidObjects.CreateValidExpense(123, 123);

        _expenseTypeRepoMock.Setup(x => x.Exists(
                                (int)expense.ExpenseTypeId,
                                It.IsAny<CancellationToken>()))
                                .ReturnsAsync(true);

        _taxRepoMock.Setup(x => x.Exists(
                        (int)expense.TaxId!,
                        It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        _partSetRepoMock.Setup(x => x.Exists(
                            expense.PartSetId!.Value,
                            It.IsAny<CancellationToken>()))
                            .ReturnsAsync(true);

        _expenseRepoMock.Setup(x => x.Create(
                            expense,
                            It.IsAny<CancellationToken>()))
                            .ReturnsAsync(expenseId);

        var result = await _service.CreateExpenses(expense, CancellationToken.None);

        result.Should().Be(expenseId);

        _expenseRepoMock.Verify(x => x.Create(
                            It.IsAny<Expense>(),
                            CancellationToken.None),
                         Times.Once);
    }

    [Fact]
    public async Task CreateExpense_ShouldThrowNotFoundException_WhenPartSetIdHasValueButNotExist()
    {
        var expense = ValidObjects.CreateValidExpense(123, 123);

        _expenseTypeRepoMock.Setup(x => x.Exists(
                                (int)expense.ExpenseTypeId,
                                It.IsAny<CancellationToken>()))
                                .ReturnsAsync(true);

        _taxRepoMock.Setup(x => x.Exists(
                        (int)expense.TaxId!,
                        It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        _partSetRepoMock.Setup(x => x.Exists(
                            expense.PartSetId!.Value,
                            It.IsAny<CancellationToken>()))
                            .ReturnsAsync(false);

        var act = () => _service.CreateExpenses(expense, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _expenseRepoMock.Verify(x => x.Create(
                            It.IsAny<Expense>(),
                            CancellationToken.None),
                         Times.Never);
    }

    [Fact] 
    public async Task UpdateExpense_ShouldThrowNotFoundException_WhenExpenseTypeNotExist()
    {
        var expenseId = 123;
        var model = new ExpenseUpdateModel(
            null,
            null,
            ExpenseTypeEnum.OfficeAndSupplies,
            null);

        _expenseTypeRepoMock.Setup(x => x.Exists(
                                (int)model.ExpenseTypeId!,
                                It.IsAny<CancellationToken>()))
                                .ReturnsAsync(false);

        var act = () => _service.UpdateExpense(expenseId, model, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _expenseRepoMock.Verify(x => x.Update(
                            expenseId,
                            model,
                            CancellationToken.None),
                         Times.Never);
    }
}
