using CaixaDeBanco.Api.Handlers;
using CaixaDeBanco.Database.Data;
using CaixaDeBanco.Database.Models;
using CaixaDeBanco.Models;
using CaixaDeBanco.Requests;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CaixaDeBanco.UnitTest
{
    public class TransactionHandlerTests
    {
        private readonly Mock<BancoDbContext> mockContext = new();
        internal void Setup()
        {
            var mockAccount = new Mock<DbSet<BankingAccount>>();
            var data = new List<BankingAccount>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Document = "12345678902",
                    Name = "Teste",
                    Balance = 1000M,
                    Status = true,
                    AccountNumber = 1
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Document = "12345678909",
                    Name = "Amelia",
                    Balance = 1000M,
                    Status = true,
                    AccountNumber = 2
                }
                ,
                new()
                {
                    Id = Guid.NewGuid(),
                    Document = "12345678974",
                    Name = "Teste Josefina",
                    Balance = 1000M,
                    Status = false,
                    AccountNumber = 3
                }
            }.AsQueryable();
            mockAccount.As<IQueryable<BankingAccount>>().Setup(m => m.Provider).Returns(data.Provider);
            mockAccount.As<IQueryable<BankingAccount>>().Setup(m => m.Expression).Returns(data.Expression);
            mockAccount.As<IQueryable<BankingAccount>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockAccount.As<IQueryable<BankingAccount>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockTransactionHistory = new Mock<DbSet<TransactionHistory>>();

            var historyData = new List<TransactionHistory>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    TransactionType = Enumerator.ETransactionType.Transfer,
                    TransactionValue = 450M,
                    CreatedAt = DateTime.Now,
                    Account = data.First()
                }
            }.AsQueryable();

            mockTransactionHistory.As<IQueryable<TransactionHistory>>().Setup(m => m.Provider).Returns(historyData.Provider);
            mockTransactionHistory.As<IQueryable<TransactionHistory>>().Setup(m => m.Expression).Returns(historyData.Expression);
            mockTransactionHistory.As<IQueryable<TransactionHistory>>().Setup(m => m.ElementType).Returns(historyData.ElementType);
            mockTransactionHistory.As<IQueryable<TransactionHistory>>().Setup(m => m.GetEnumerator()).Returns(() => historyData.GetEnumerator());

            mockContext.Setup(m => m.Account).Returns(mockAccount.Object);
            mockContext.Setup(m => m.TransactionHistory).Returns(mockTransactionHistory.Object);
        }

        [Fact(DisplayName = "Ao fazer transferência entre contas com saldo que cubra a transferência e contas ativas," +
                            "deve-se fazer a transferência com sucesso")]
        [Trait("Camada", "Handler")]
        public async void TransferWithValidValueTransactionAsync()
        {
            //Arrange
            Setup();

            var handler = new TransactionHandler(mockContext.Object);

            var request = new TransferTransactionRequest()
            {
                DestinationAccountNumber = "000001",
                OriginAccountNumber = "000002",
                Value = 450M
            };

            //Act
            var response = await handler.TransferTransactionAsync(request);

            //Assert
            Assert.Equal("Transferência feita com sucesso!", response.Message);
            Assert.True(response.IsSuccess);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Exactly(2));
        }

        [Fact(DisplayName = "Ao fazer transferência entre contas com saldo que não cubra a transferência e contas ativas," +
                           "não deve-se fazer a transferência")]
        [Trait("Camada", "Handler")]
        public async void TransferWithInValidValueTransactionAsync()
        {
            //Arrange
            Setup();

            var handler = new TransactionHandler(mockContext.Object);

            var request = new TransferTransactionRequest()
            {
                DestinationAccountNumber = "000001",
                OriginAccountNumber = "000002",
                Value = 4500M
            };

            //Act
            var response = await handler.TransferTransactionAsync(request);

            //Assert
            Assert.Equal("Saldo insuficiente", response.Message);
            Assert.True(!response.IsSuccess);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Never());
        }

        [Fact(DisplayName = "Ao fazer transferência entre contas inativas, não deve-se fazer a transferência")]
        [Trait("Camada", "Handler")]
        public async void TransferWithInactiveAccountTransactionAsync()
        {
            //Arrange
            Setup();

            var handler = new TransactionHandler(mockContext.Object);

            var request = new TransferTransactionRequest()
            {
                DestinationAccountNumber = "000001",
                OriginAccountNumber = "000003",
                Value = 450M
            };

            //Act
            var response = await handler.TransferTransactionAsync(request);

            //Assert
            Assert.Equal("Conta de origem desativada", response.Message);
            Assert.True(!response.IsSuccess);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Never());
        }

        [Fact(DisplayName = "Ao fazer transferência entre contas inexistentes, não deve-se fazer a transferência")]
        [Trait("Camada", "Handler")]
        public async void TransferWithInexistantAccountTransactionAsync()
        {
            //Arrange
            Setup();

            var handler = new TransactionHandler(mockContext.Object);

            var request = new TransferTransactionRequest()
            {
                DestinationAccountNumber = "000001",
                OriginAccountNumber = "000004",
                Value = 450M
            };

            //Act
            var response = await handler.TransferTransactionAsync(request);

            //Assert
            Assert.Equal("Conta de origem não encontrada", response.Message);
            Assert.True(!response.IsSuccess);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Never());
        }
    }
}
