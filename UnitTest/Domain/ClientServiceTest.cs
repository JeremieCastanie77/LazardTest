using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Services;
using NFluent;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace UnitTest.Domain;

public class ClientServiceTest
{
    private static readonly IList<PriseChargeEmployeurModel> FakePriseChargeEmployeurs =
    [
        new PriseChargeEmployeurModel(1, 1, 7.5m),
        new PriseChargeEmployeurModel(2, 2, 6),
        new PriseChargeEmployeurModel(3, 3, decimal.MaxValue),
        new PriseChargeEmployeurModel(4, 4, 10)
    ];

    private static readonly IList<SupplementModel> FakeSupplements =
    [
        new SupplementModel(1, "Boisson", 1),
        new SupplementModel(2, "Fromage", 1),
        new SupplementModel(3, "Pain", 0.4m),
        new SupplementModel(4, "Petite salade bar", 4),
        new SupplementModel(5, "Grande salade bar", 6),
        new SupplementModel(6, "Portion de fruit", 1),
        new SupplementModel(7, "Entrée supplémentaire", 3),
        new SupplementModel(8, "Plat supplémentaire", 6),
        new SupplementModel(9, "Dessert supplémentaire", 3)
    ];

    private static readonly IList<ProfilModel> FakeProfils =
    [
        new ProfilModel(1, "Interne", true),
        new ProfilModel(2, "Prestataire"),
        new ProfilModel(3, "VIP", true),
        new ProfilModel(4, "Stagiaire"),
        new ProfilModel(5, "Visiteur")
    ];

    [Fact]
    public async Task CrediteCompteAsync_Should_Credite()
    {
        var clientRepo = Substitute.For<IClientRepository>();
        var supplementRepo = Substitute.For<ISupplementRepository>();
        var priseChargeEmployeurRepo = Substitute.For<IPriseChargeEmployeurRepository>();
        var profilRepo = Substitute.For<IProfilRepository>();

        clientRepo.GetClientAsync(Arg.Any<int>()).Returns(new ClientModel(100, "Fake nom 100", "Fake prenom 100", 1, 25.6m));

        var clientService = new ClientService(clientRepo, supplementRepo, priseChargeEmployeurRepo, profilRepo);

        var res = await clientService.CrediteCompteAsync(100, 125);

        await clientRepo.Received().UpdateClientAsync(Arg.Is<ClientModel>(x => x.Compte == 150.6m));
    }

    [Fact]
    public void CrediteCompteAsync_Should_Throw_ClientNotFoundException()
    {
        var clientRepo = Substitute.For<IClientRepository>();
        var supplementRepo = Substitute.For<ISupplementRepository>();
        var priseChargeEmployeurRepo = Substitute.For<IPriseChargeEmployeurRepository>();
        var profilRepo = Substitute.For<IProfilRepository>();

        var clientService = new ClientService(clientRepo, supplementRepo, priseChargeEmployeurRepo, profilRepo);

        Check.ThatCode(async () => await clientService.CrediteCompteAsync(2, 125)).Throws<ClientNotFoundException>();
    }

    [Fact]
    public async Task PayerRepasAsync_Should_DecompterRepas()
    {
        var clientRepo = Substitute.For<IClientRepository>();
        var supplementRepo = Substitute.For<ISupplementRepository>();
        var priseChargeEmployeurRepo = Substitute.For<IPriseChargeEmployeurRepository>();
        var profilRepo = Substitute.For<IProfilRepository>();

        clientRepo.GetClientAsync(Arg.Any<int>()).Returns(new ClientModel(100, "Fake nom 100", "Fake prenom 100", 1, 25.6m));
        supplementRepo.GetSupplementsAsync().Returns(FakeSupplements);
        priseChargeEmployeurRepo.GetPriseChargeEmployeurByProfilAsync(Arg.Any<int>()).Returns(FakePriseChargeEmployeurs[0]);
        profilRepo.GetProfilAsync((Arg.Any<int>())).Returns(FakeProfils[0]);

        var clientService = new ClientService(clientRepo, supplementRepo, priseChargeEmployeurRepo, profilRepo);

        var res = await clientService.PayerRepasAsync(100, [8, 9]);

        await clientRepo.Received().UpdateClientAsync(Arg.Is<ClientModel>(x => x.Compte == 14.1m));
        Check.That(res.Total).IsEqualTo(11.5m);
        Check.That(res.Supplements[0].SupplementLbl).IsEqualTo("Plat supplémentaire");
    }

    [Fact]
    public async Task PayerRepasAsync_Should_DecompterRepasSansSupplement()
    {
        var clientRepo = Substitute.For<IClientRepository>();
        var supplementRepo = Substitute.For<ISupplementRepository>();
        var priseChargeEmployeurRepo = Substitute.For<IPriseChargeEmployeurRepository>();
        var profilRepo = Substitute.For<IProfilRepository>();

        clientRepo.GetClientAsync(Arg.Any<int>()).Returns(new ClientModel(100, "Fake nom 100", "Fake prenom 100", 1, 25.6m));
        supplementRepo.GetSupplementsAsync().Returns(FakeSupplements);
        priseChargeEmployeurRepo.GetPriseChargeEmployeurByProfilAsync(Arg.Any<int>()).Returns(FakePriseChargeEmployeurs[0]);
        profilRepo.GetProfilAsync((Arg.Any<int>())).Returns(FakeProfils[0]);

        var clientService = new ClientService(clientRepo, supplementRepo, priseChargeEmployeurRepo, profilRepo);

        var res = await clientService.PayerRepasAsync(100, []);

        await clientRepo.Received().UpdateClientAsync(Arg.Is<ClientModel>(x => x.Compte == 23.1m));
    }

    [Fact]
    public async Task PayerRepasAsync_Should_DecompterRepasCompteNegatifAuthorise()
    {
        var clientRepo = Substitute.For<IClientRepository>();
        var supplementRepo = Substitute.For<ISupplementRepository>();
        var priseChargeEmployeurRepo = Substitute.For<IPriseChargeEmployeurRepository>();
        var profilRepo = Substitute.For<IProfilRepository>();

        clientRepo.GetClientAsync(Arg.Any<int>()).Returns(new ClientModel(100, "Fake nom 100", "Fake prenom 100", 1, 25.6m));
        supplementRepo.GetSupplementsAsync().Returns(FakeSupplements);
        priseChargeEmployeurRepo.GetPriseChargeEmployeurByProfilAsync(Arg.Any<int>()).Returns(FakePriseChargeEmployeurs[0]);
        profilRepo.GetProfilAsync((Arg.Any<int>())).Returns(FakeProfils[0]);

        var clientService = new ClientService(clientRepo, supplementRepo, priseChargeEmployeurRepo, profilRepo);

        var res = await clientService.PayerRepasAsync(100, [8, 9, 8, 8, 8]);

        await clientRepo.Received().UpdateClientAsync(Arg.Is<ClientModel>(x => x.Compte == -3.9m));
    }

    [Fact]
    public async Task PayerRepasAsync_Should_RepasGratuit()
    {
        var clientRepo = Substitute.For<IClientRepository>();
        var supplementRepo = Substitute.For<ISupplementRepository>();
        var priseChargeEmployeurRepo = Substitute.For<IPriseChargeEmployeurRepository>();
        var profilRepo = Substitute.For<IProfilRepository>();

        clientRepo.GetClientAsync(Arg.Any<int>()).Returns(new ClientModel(100, "Fake nom 100", "Fake prenom 100", 1, 25.6m));
        supplementRepo.GetSupplementsAsync().Returns(FakeSupplements);
        priseChargeEmployeurRepo.GetPriseChargeEmployeurByProfilAsync(Arg.Any<int>()).Returns(FakePriseChargeEmployeurs[2]);
        profilRepo.GetProfilAsync((Arg.Any<int>())).Returns(FakeProfils[0]);

        var clientService = new ClientService(clientRepo, supplementRepo, priseChargeEmployeurRepo, profilRepo);

        var res = await clientService.PayerRepasAsync(100, [8, 9, 8, 8, 8]);

        await clientRepo.Received().UpdateClientAsync(Arg.Is<ClientModel>(x => x.Compte == 25.6m));
    }

    [Fact]
    public void PayerRepasAsync_Should_Throw_ClientDecouvertNonAutoriseException()
    {
        var clientRepo = Substitute.For<IClientRepository>();
        var supplementRepo = Substitute.For<ISupplementRepository>();
        var priseChargeEmployeurRepo = Substitute.For<IPriseChargeEmployeurRepository>();
        var profilRepo = Substitute.For<IProfilRepository>();

        clientRepo.GetClientAsync(Arg.Any<int>()).Returns(new ClientModel(100, "Fake nom 100", "Fake prenom 100", 1, 25.6m));
        supplementRepo.GetSupplementsAsync().Returns(FakeSupplements);
        priseChargeEmployeurRepo.GetPriseChargeEmployeurByProfilAsync(Arg.Any<int>()).Returns(FakePriseChargeEmployeurs[0]);
        profilRepo.GetProfilAsync((Arg.Any<int>())).Returns(FakeProfils[1]);

        var clientService = new ClientService(clientRepo, supplementRepo, priseChargeEmployeurRepo, profilRepo);

        Check.ThatCode(async () => await clientService.PayerRepasAsync(100, [8, 9, 8, 8, 8])).Throws<ClientDecouvertNonAutoriseException>();
    }

    [Fact]
    public void PayerRepasAsync_Should_Throw_ClientNotFoundException()
    {
        var clientRepo = Substitute.For<IClientRepository>();
        var supplementRepo = Substitute.For<ISupplementRepository>();
        var priseChargeEmployeurRepo = Substitute.For<IPriseChargeEmployeurRepository>();
        var profilRepo = Substitute.For<IProfilRepository>();

        var clientService = new ClientService(clientRepo, supplementRepo, priseChargeEmployeurRepo, profilRepo);

        Check.ThatCode(async () => await clientService.PayerRepasAsync(100, [8, 9])).Throws<ClientNotFoundException>();
    }

    [Fact]
    public void PayerRepasAsync_Should_Throw_SupplementNotFoundException()
    {
        var clientRepo = Substitute.For<IClientRepository>();
        var supplementRepo = Substitute.For<ISupplementRepository>();
        var priseChargeEmployeurRepo = Substitute.For<IPriseChargeEmployeurRepository>();
        var profilRepo = Substitute.For<IProfilRepository>();

        clientRepo.GetClientAsync(Arg.Any<int>()).Returns(new ClientModel(100, "Fake nom 100", "Fake prenom 100", 1, 25.6m));
        supplementRepo.GetSupplementsAsync().Returns(FakeSupplements);

        var clientService = new ClientService(clientRepo, supplementRepo, priseChargeEmployeurRepo, profilRepo);

        Check.ThatCode(async () => await clientService.PayerRepasAsync(100, [8, 11])).Throws<SupplementNotFoundException>();
    }

    [Fact]
    public void PayerRepasAsync_Should_Throw_ProfilNotFoundException()
    {
        var clientRepo = Substitute.For<IClientRepository>();
        var supplementRepo = Substitute.For<ISupplementRepository>();
        var priseChargeEmployeurRepo = Substitute.For<IPriseChargeEmployeurRepository>();
        var profilRepo = Substitute.For<IProfilRepository>();

        clientRepo.GetClientAsync(Arg.Any<int>()).Returns(new ClientModel(100, "Fake nom 100", "Fake prenom 100", 1, 25.6m));
        supplementRepo.GetSupplementsAsync().Returns(FakeSupplements);
        priseChargeEmployeurRepo.GetPriseChargeEmployeurByProfilAsync(Arg.Any<int>()).Returns(FakePriseChargeEmployeurs[0]);

        var clientService = new ClientService(clientRepo, supplementRepo, priseChargeEmployeurRepo, profilRepo);

        Check.ThatCode(async () => await clientService.PayerRepasAsync(100, [8, 9, 8, 8, 8])).Throws<ProfilNotFoundException>();
    }
}