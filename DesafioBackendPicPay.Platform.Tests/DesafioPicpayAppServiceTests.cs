using DesafioBackendPicPay.Domain.Exceptions;
using DesafioBackendPicPay.Domain.Lojista;
using DesafioBackendPicPay.Domain.User;
using DesafioBackendPicPay.Platform.Application;
using DesafioBackendPicPay.Platform.Application.Lojista.Commands;
using DesafioBackendPicPay.Platform.Application.User.Commands;
using DesafioBackendPicPay.Platform.Infrastructure.Authorization;
using DesafioBackendPicPay.Platform.Infrastructure.Repositories;
using NSubstitute;

namespace DesafioBackendPicPay.Platform.Tests
{
    [TestFixture]
    public class DesafioPicpayAppServiceTests
    {
        private IUnitOfWork unitOfWork = null!;
        private IAuthorizationService authorizationService = null!;
        private DesafioPicpayAppService picpayAppService;


        [SetUp]
        public void SetUp()
        {
            unitOfWork = Substitute.For<IUnitOfWork>();
            authorizationService = Substitute.For<IAuthorizationService>();

            picpayAppService = new DesafioPicpayAppService(unitOfWork, authorizationService);
        }

        [TearDown]
        public void TearDown()
        {
            unitOfWork?.Dispose();

            picpayAppService = null!;
        }

        [Test]
        public async Task AddLojista_Should_Throw_NullException_If_Command_Is_Invalid()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await picpayAppService.Add(null!, CancellationToken.None));

            await unitOfWork.DidNotReceive().CommitAsync();
            await unitOfWork.DidNotReceive().PicpayRepository.Add(Arg.Any<Lojista>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task AddLojista()
        {
            var command = new AddLojistaCommand()
            {
                Cnpj = "XX.XXX.XXX/0001-ZZ",
                Email = "email@gmail.com",
                FirstName = "fistName",
                LastName = "lastName"
            };

            var lojistaId = await picpayAppService.Add(command, CancellationToken.None);

            Assert.That(lojistaId, Is.Not.Empty);

            await unitOfWork.Received(1).CommitAsync();
            await unitOfWork.Received(2).PicpayRepository.Add(Arg.Is<Lojista>(l => l.Id == lojistaId), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task AddUser_Should_Throw_NullException_If_Command_Is_Invalid()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await picpayAppService.AddUser(null!, CancellationToken.None));

            await unitOfWork.DidNotReceive().CommitAsync();
            await unitOfWork.DidNotReceive().PicpayRepository.AddUser(Arg.Any<User>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task AddUser()
        {
            var command = new AddUserCommand()
            {
                Cpf = "123.321.321-10",
                Email = "email@gmail.com",
                FirstName = "fistName",
                LastName = "lastName"
            };

            var userId = await picpayAppService.AddUser(command, CancellationToken.None);

            Assert.That(userId, Is.Not.Empty);

            await unitOfWork.Received(1).CommitAsync();
            await unitOfWork.Received(2).PicpayRepository.AddUser(Arg.Is<User>(u => u.Id == userId), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Transfer_Should_Throw_NullException_If_Command_Is_Invalid()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await picpayAppService.Transfer(null!, CancellationToken.None));

            await unitOfWork.DidNotReceive().CommitAsync();
            await unitOfWork.DidNotReceive().PicpayRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Transfer_Should_Throw_UserNotFoundException_If_GetById_Return_Null()
        {
            var command = new TransferCommand()
            {
                SendById = Guid.NewGuid(),
                ReceivedById = Guid.NewGuid(),
                Value = 50
            };

            Assert.ThrowsAsync<UserNotFoundException>(async () => await picpayAppService.Transfer(command, CancellationToken.None));

            await unitOfWork.DidNotReceive().CommitAsync();
            await unitOfWork.Received(1).PicpayRepository.GetById(Arg.Is<Guid>(u => u.Equals(command.SendById)), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Transfer_Should_Throw_InvalidOperationException_If_GetById_Return_A_Lojista_Entity()
        {
            var command = new TransferCommand()
            {
                SendById = Guid.NewGuid(),
                ReceivedById = Guid.NewGuid(),
                Value = 50
            };

            var lojista = new Lojista()
            {
                Cnpj = "XX.XXX.XXX/0001-ZZ",
                Email = "email@gmail.com",
                FirstName = "fistName",
                LastName = "lastName"
            };

            unitOfWork.PicpayRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(lojista);

            Assert.ThrowsAsync<Domain.Lojista.Exceptions.InvalidOperationException>(async () => await picpayAppService.Transfer(command, CancellationToken.None));

            await unitOfWork.DidNotReceive().CommitAsync();
            await unitOfWork.Received().PicpayRepository.GetById(Arg.Is<Guid>(u => u.Equals(lojista.Id)), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Transfer_Should_Throw_ReceivedUserNotFoundException_If_GetReceivedById_Return_Null()
        {
            var command = new TransferCommand()
            {
                SendById = Guid.NewGuid(),
                ReceivedById = Guid.NewGuid(),
                Value = 50
            };

            var user = new User()
            {
                Cpf = "xxx.xxx.xxx-xx",
                Email = "email@gmail.com",
                FirstName = "fistName",
                LastName = "lastName"
            };

            user.Deposit(300);

            unitOfWork.PicpayRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(user);

            Assert.ThrowsAsync<ReceivedUserNotFoundException>(async () => await picpayAppService.Transfer(command, CancellationToken.None));

            await unitOfWork.DidNotReceive().CommitAsync();
            await unitOfWork.PicpayRepository.Received(1).GetById(Arg.Is<Guid>(u => u.Equals(command.SendById)), Arg.Any<CancellationToken>());
            await unitOfWork.PicpayRepository.Received(1).GetReceivedById(Arg.Is<Guid>(u => u.Equals(command.ReceivedById)), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Transfer_Should_Throw_InsufficientFundsException_If_Send_Entity_Ballance_Is_Less_Than_You_Transfer_Value()
        {
            var user = new User()
            {
                Cpf = "xxx.xxx.xxx-xx",
                Email = "email@gmail.com",
                FirstName = "fistName",
                LastName = "lastName"
            };

            user.Deposit(49);

            var user2 = new User()
            {
                Cpf = "xxx.xxx.xxx-xx",
                Email = "email@gmail.com",
                FirstName = "fistName",
                LastName = "lastName"
            };


            unitOfWork.PicpayRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(user);
            unitOfWork.PicpayRepository.GetReceivedById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(user2);

            var command = new TransferCommand()
            {
                SendById = Guid.NewGuid(),
                ReceivedById = Guid.NewGuid(),
                Value = 50
            };

            Assert.ThrowsAsync<InsufficientFundsException>(async () => await picpayAppService.Transfer(command, CancellationToken.None));

            await unitOfWork.DidNotReceive().CommitAsync();
            await unitOfWork.PicpayRepository.Received(1).GetById(Arg.Is<Guid>(u => u.Equals(command.SendById)), Arg.Any<CancellationToken>());
            await unitOfWork.PicpayRepository.Received(1).GetReceivedById(Arg.Is<Guid>(u => u.Equals(command.ReceivedById)), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Transfer_Should_Throw_UnavelableOperationException_If_IsAuthorized_Method_Returns_False()
        {
            var user = new User()
            {
                Cpf = "xxx.xxx.xxx-xx",
                Email = "email@gmail.com",
                FirstName = "fistName",
                LastName = "lastName"
            };

            user.Deposit(100);

            var user2 = new User()
            {
                Cpf = "xxx.xxx.xxx-xx",
                Email = "email@gmail.com",
                FirstName = "fistName",
                LastName = "lastName"
            };

            var command = new TransferCommand()
            {
                SendById = Guid.NewGuid(),
                ReceivedById = Guid.NewGuid(),
                Value = 50
            };

            unitOfWork.PicpayRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(user);
            unitOfWork.PicpayRepository.GetReceivedById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(user2);

            authorizationService.IsAuthorized().Returns(false);

            Assert.ThrowsAsync<UnavelableOperationException>(async () => await picpayAppService.Transfer(command, CancellationToken.None));

            await unitOfWork.DidNotReceive().CommitAsync();
            await unitOfWork.PicpayRepository.Received(1).GetById(Arg.Is<Guid>(u => u.Equals(command.SendById)), Arg.Any<CancellationToken>());
            await unitOfWork.PicpayRepository.Received(1).GetReceivedById(Arg.Is<Guid>(u => u.Equals(command.ReceivedById)), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Transfer()
        {
            var user = new User()
            {
                Cpf = "xxx.xxx.xxx-xx",
                Email = "email@gmail.com",
                FirstName = "fistName",
                LastName = "lastName"
            };

            user.Deposit(60);

            var user2 = new User()
            {
                Cpf = "xxx.xxx.xxx-xx",
                Email = "email@gmail.com",
                FirstName = "fistName",
                LastName = "lastName"
            };

            var command = new TransferCommand()
            {
                SendById = Guid.NewGuid(),
                ReceivedById = Guid.NewGuid(),
                Value = 50
            };

            unitOfWork.PicpayRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(user);
            unitOfWork.PicpayRepository.GetReceivedById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(user2);
            authorizationService.IsAuthorized().Returns(true);

            await picpayAppService.Transfer(command, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(user.Balance, Is.EqualTo(10));
                Assert.That(user2.Balance, Is.EqualTo(50));
            });

            await unitOfWork.Received(1).CommitAsync();
            await unitOfWork.PicpayRepository.Received(1).GetById(Arg.Is<Guid>(u => u.Equals(command.SendById)), Arg.Any<CancellationToken>());
            await unitOfWork.PicpayRepository.Received(1).GetReceivedById(Arg.Is<Guid>(u => u.Equals(command.ReceivedById)), Arg.Any<CancellationToken>());
        }
    }
}
