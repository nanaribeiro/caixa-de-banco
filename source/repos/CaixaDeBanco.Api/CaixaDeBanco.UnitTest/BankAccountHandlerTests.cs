using CaixaDeBanco.Api.Handlers;
using CaixaDeBanco.Database.Data;
using CaixaDeBanco.Database.Models;
using CaixaDeBanco.Models;
using CaixaDeBanco.Requests;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CaixaDeBanco.UnitTest
{
    public class BankAccountHandlerTests
    {
        private readonly Mock<DbSet<BankingAccount>> mockAccount = new();
        private readonly Mock<BancoDbContext> mockContext = new();
        internal void Setup()
        {
            var data = new List<BankingAccount>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Document = "12345678902",
                    Name = "Teste",
                    Balance = 1000M,
                    Status = true
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Document = "12345678909",
                    Name = "Amelia",
                    Balance = 1000M,
                    Status = true
                }
                ,
                new()
                {
                    Id = Guid.NewGuid(),
                    Document = "12345678974",
                    Name = "Teste Josefina",
                    Balance = 1000M,
                    Status = false
                }
            }.AsQueryable();
            mockAccount.As<IQueryable<BankingAccount>>().Setup(m => m.Provider).Returns(data.Provider);
            mockAccount.As<IQueryable<BankingAccount>>().Setup(m => m.Expression).Returns(data.Expression);
            mockAccount.As<IQueryable<BankingAccount>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockAccount.As<IQueryable<BankingAccount>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockAccountHistory = new Mock<DbSet<AccountHistory>>();

            var historyData = new List<AccountHistory>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Document = "12345678902",
                    Action = Enumerator.EAccountAction.Inactivation,
                    CreatedAt = DateTime.Now,
                    Account = data.First()
                }
            }.AsQueryable();

            mockAccountHistory.As<IQueryable<AccountHistory>>().Setup(m => m.Provider).Returns(historyData.Provider);
            mockAccountHistory.As<IQueryable<AccountHistory>>().Setup(m => m.Expression).Returns(historyData.Expression);
            mockAccountHistory.As<IQueryable<AccountHistory>>().Setup(m => m.ElementType).Returns(historyData.ElementType);
            mockAccountHistory.As<IQueryable<AccountHistory>>().Setup(m => m.GetEnumerator()).Returns(() => historyData.GetEnumerator());

            mockContext.Setup(m => m.Account).Returns(mockAccount.Object);
            mockContext.Setup(m => m.AccountHistory).Returns(mockAccountHistory.Object);
        }

        [Fact(DisplayName = "Ao criar uma nova conta com documento ainda não inserido, deve adicionar a conta")]
        [Trait("Camada", "Handler")]
        public async void CreateAccountAsync()
        {
            //Arrange
            Setup();           
            
            var handler = new BankAccountHandler(mockContext.Object);

            var request = new CreateAccountRequest()
            {
                Document = "12345678901",
                Name = "Teste"
            };

            //Act
            var response = await handler.CreateAccountAsync(request);

            //Assert
            Assert.Equal("Conta criada com sucesso!", response.Message);
            Assert.True(response.IsSuccess);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Fact(DisplayName = "Ao criar uma nova conta com documento já inserido, não deve adicionar a conta")]
        [Trait("Camada", "Handler")]
        public async void CreateAccountWithExistingDocumentAsync()
        {
            //Arrange
            Setup();

            var handler = new BankAccountHandler(mockContext.Object);

            var request = new CreateAccountRequest()
            {
                Document = "12345678902",
                Name = "Teste"
            };

            //Act
            var response = await handler.CreateAccountAsync(request);

            //Assert
            Assert.Equal("Já existe uma conta para o documento informado!", response.Message);
            Assert.True(!response.IsSuccess);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Never());
        }

        [Fact(DisplayName = "Ao obter a lista das contas sem passar parametros, deve retornar todas as conta")]
        [Trait("Camada", "Handler")]
        public async void GetAccountsWithoutParameter()
        {
            //Arrange
            Setup();

            var handler = new BankAccountHandler(mockContext.Object);

            var request = new GetAccountRequest();

            //Act
            var response = await handler.GetAccount(request);

            //Assert
            Assert.Equal("Teste", response.Data?[0].Name);
            Assert.True(response.IsSuccess);
            Assert.True(response.TotalCount>1);
        }

        [Fact(DisplayName = "Ao obter a lista das contas com filtro de documento, deve retornar somente uma conta")]
        [Trait("Camada", "Handler")]
        public async void GetAccountsWithDocumentParameter()
        {
            //Arrange
            Setup();

            var handler = new BankAccountHandler(mockContext.Object);

            var request = new GetAccountRequest()
            {
                Document = "12345678902"
            };

            //Act
            var response = await handler.GetAccount(request);

            //Assert
            Assert.Equal("Teste", response.Data?[0].Name);
            Assert.True(response.IsSuccess);
            Assert.True(response.TotalCount == 1);
        }

        [Fact(DisplayName = "Ao obter a lista das contas com filtro de nome, deve retornar somente as " +
                            "contas contendo aquele nome")]
        [Trait("Camada", "Handler")]
        public async void GetAccountsWithNameParameter()
        {
            //Arrange
            Setup();

            var handler = new BankAccountHandler(mockContext.Object);

            var request = new GetAccountRequest()
            {
                Name = "Teste"
            };

            //Act
            var response = await handler.GetAccount(request);

            //Assert
            Assert.True(response.IsSuccess);
            Assert.True(response.TotalCount > 1);
        }

        [Fact(DisplayName = "Ao inativar uma conta ativa, deve mudar o status para 0 e salvar historico")]
        [Trait("Camada", "Handler")]
        public async void DeactivateActiveAccountAsync()
        {
            //Arrange
            Setup();

            var handler = new BankAccountHandler(mockContext.Object);

            var request = new DeactivateAccountRequest()
            {
                Document = "12345678902"
            };

            //Act
            var response = await handler.DeactivateAccountAsync(request);

            //Assert
            Assert.True(response.IsSuccess);
            Assert.True(response.Message == "Conta inativada com sucesso!");
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Exactly(2));
        }

        [Fact(DisplayName = "Ao inativar uma conta inativa, não deve ter sucesso")]
        [Trait("Camada", "Handler")]
        public async void DeactivateInactiveAccountAsync()
        {
            //Arrange
            Setup();

            var handler = new BankAccountHandler(mockContext.Object);

            var request = new DeactivateAccountRequest()
            {
                Document = "12345678974"
            };

            //Act
            var response = await handler.DeactivateAccountAsync(request);

            //Assert
            Assert.True(!response.IsSuccess);
            Assert.True(response.Message == "Conta já está desativada!");
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Never());
        }
    }
}